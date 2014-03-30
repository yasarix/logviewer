using System;
using Gtk;
using System.Collections;
using LogViewer;
using Pango;
using System.Text.RegularExpressions;
using System.Threading;

public partial class MainWindow: Gtk.Window
{
	Gtk.ListStore LogStore;
	Gtk.TreeModelFilter LogFilter;
	string log_file_name = null;
	Thread file_load_thread;

	public MainWindow () : base (Gtk.WindowType.Toplevel)
	{
		Build ();
		initUi ();
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}

	protected void initUi()
	{
		Gtk.TreeViewColumn timestamp_column = new Gtk.TreeViewColumn ();
		timestamp_column.Title = "Timestamp";
		Gtk.CellRendererText timestamp_cell = new Gtk.CellRendererText ();
		timestamp_column.PackStart (timestamp_cell, true);

		MainTable.AppendColumn (timestamp_column);

		Gtk.TreeViewColumn process_column = new Gtk.TreeViewColumn ();
		process_column.Title = "Process";
		Gtk.CellRendererText process_cell = new Gtk.CellRendererText ();
		process_column.PackStart (process_cell, true);

		MainTable.AppendColumn (process_column);

		Gtk.TreeViewColumn LevelColumn = new Gtk.TreeViewColumn ();
		LevelColumn.Title = "Level";
		Gtk.CellRendererText level_cell = new Gtk.CellRendererText ();
		LevelColumn.PackStart (level_cell, true);

		MainTable.AppendColumn (LevelColumn);

		Gtk.TreeViewColumn message_column = new Gtk.TreeViewColumn ();
		message_column.Title = "Message";
		Gtk.CellRendererText message_cell = new Gtk.CellRendererText ();
		message_column.PackStart (message_cell, true);

		MainTable.AppendColumn (message_column);

		LogStore = new Gtk.ListStore (typeof (LogLine));

		timestamp_column.SetCellDataFunc (timestamp_cell, new Gtk.TreeCellDataFunc (renderTimestamp));
		process_column.SetCellDataFunc (process_cell, new Gtk.TreeCellDataFunc (renderProcess));
		LevelColumn.SetCellDataFunc (level_cell, new Gtk.TreeCellDataFunc (renderLevel));
		message_column.SetCellDataFunc (message_cell, new Gtk.TreeCellDataFunc (renderMessage));

		LogFilter = new Gtk.TreeModelFilter (LogStore, null);
		LogFilter.VisibleFunc = new Gtk.TreeModelFilterVisibleFunc (filterLogEntries);

		MainTable.Model = LogFilter;

		MainTable.ShowAll ();

		MainTable.ModifyFont(Pango.FontDescription.FromString("Liberation Mono 12"));
		LogDetailsTextView.ModifyFont(Pango.FontDescription.FromString ("Liberation Mono 12"));
	}

	private void renderTimestamp (Gtk.TreeViewColumn column, Gtk.CellRenderer cell, Gtk.TreeModel model, Gtk.TreeIter iter)
	{
		LogLine line = (LogLine) model.GetValue (iter, 0);
		(cell as Gtk.CellRendererText).Background = this.getColorByLevel(line.Level);
		(cell as Gtk.CellRendererText).Text = line.Timestamp;
	}

	private void renderProcess (Gtk.TreeViewColumn column, Gtk.CellRenderer cell, Gtk.TreeModel model, Gtk.TreeIter iter)
	{
		LogLine line = (LogLine) model.GetValue (iter, 0);
		(cell as Gtk.CellRendererText).Background = this.getColorByLevel(line.Level);
		(cell as Gtk.CellRendererText).Text = line.Process;
	}

	private void renderMessage (Gtk.TreeViewColumn column, Gtk.CellRenderer cell, Gtk.TreeModel model, Gtk.TreeIter iter)
	{
		LogLine line = (LogLine) model.GetValue (iter, 0);
		(cell as Gtk.CellRendererText).Background = this.getColorByLevel(line.Level);
		(cell as Gtk.CellRendererText).Text = line.Message;
	}

	private void renderLevel(Gtk.TreeViewColumn column, Gtk.CellRenderer cell, Gtk.TreeModel model, Gtk.TreeIter iter)
	{
		LogLine line = (LogLine) model.GetValue (iter, 0);
		(cell as Gtk.CellRendererText).Background = this.getColorByLevel(line.Level);
		(cell as Gtk.CellRendererText).Text = line.Level;
	}

	private void onRowSelect(object sender, EventArgs e)
	{
		TreeSelection selection = (sender as TreeView).Selection;
		TreeModel model;
		TreeIter iter;
		LogLine line = new LogLine();

		// THE ITER WILL POINT TO THE SELECTED ROW
		if (selection.GetSelected(out model, out iter)) {
			line = (LogLine) model.GetValue (iter, 0);
		}

		showLogDetails (line);
	}

	public void showLogDetails(LogLine logline)
	{
		LogDetailsTextView.Buffer.Clear ();
		LogDetailsTextView.Buffer.Text = logline.Message;
	}

	protected void onFileOpen (object sender, EventArgs e)
	{
		string file_line = null;
		LogLine log_line = new LogLine ();

		FileChooserDialog chooser = new FileChooserDialog(
			"Select a logfile to view ...",
			this,
			FileChooserAction.Open,
			"Cancel", ResponseType.Cancel,
			"Open", ResponseType.Accept );

		if (chooser.Run () == (int)ResponseType.Accept) {
			try {
				file_load_thread.Abort ();
			} catch {
			}

			// Open the file for reading.
			this.log_file_name = chooser.Filename.ToString ();

			// Set the MainWindow Title to the filename.
			this.Title = "Log Viewer - " + log_file_name;

			chooser.Destroy ();

			MainWindowStatusBar.Push (1, "Loading file contents...");

			file_load_thread = new Thread (() => {
				System.IO.StreamReader file = new System.IO.StreamReader (this.log_file_name);

				int i = 0;
				while ((file_line = file.ReadLine ()) != null) {
					log_line = this.parseLogLine (file_line);

					Gtk.Application.Invoke (delegate {
						Gdk.Threads.Enter ();

						try {
							LogStore.AppendValues (log_line);
						} finally {
							Gdk.Threads.Leave ();
						}
					});

					if (i % 1000 == 0) {
						Gtk.Application.Invoke (delegate {
							Gdk.Threads.Enter ();

							try {
								progressbar1.Pulse ();
							} finally {
								Gdk.Threads.Leave ();
							}
						});
						
					}

					i++;
				}

				Gtk.Application.Invoke (delegate {
					Gdk.Threads.Enter ();
					try {
						MainWindowStatusBar.Push (1, "File loaded.");
						progressbar1.Fraction = 0;
					} finally {
						Gdk.Threads.Leave ();
					}
				});

				file.Close ();
			});

			file_load_thread.Start ();
		} else {
			chooser.Destroy ();
		}
	}

	public LogLine parseLogLine(string str)
	{
		LogLine log_line = new LogLine ();

		Regex regex = new Regex("\\[(.*?)\\] PID-(.*?)\\.(.*?)\\: (.*?)$");
		Match match = regex.Match(str);

		log_line.Timestamp = match.Groups[1].Value;
		log_line.Process = match.Groups[2].Value;
		log_line.Level = match.Groups[3].Value;
		log_line.Message = match.Groups[4].Value;

		return log_line;
	}

	private string getColorByLevel(string level)
	{
		if (level == "INFO") return "light blue";
		else if (level == "DEBUG") return "light yellow";
		else if (level == "NOTICE") return "light green";
		else if (level == "WARNING") return "pink";
		else if (level == "ERROR") return "light red";

		return "white";
	}

	protected void onExit (object sender, EventArgs e)
	{
		Application.Quit ();
	}

	protected void onFilterToggled (object sender, EventArgs e)
	{
		if (FilterPanel.Visible) {
			FilterPanel.Visible = false;
		} else {
			FilterPanel.Visible = true;
			MessageFilterCriteria.GrabFocus ();
		}
	}

	private bool filterLogEntries (Gtk.TreeModel model, Gtk.TreeIter iter)
	{
		LogLine line = (LogLine) model.GetValue (iter, 0);
		//string process = model.GetValue (iter, 0).ToString ();

		if (MessageFilterCriteria.Text == "")
			return true;

		if (line.Message.IndexOf(MessageFilterCriteria.Text, StringComparison.CurrentCultureIgnoreCase) > -1)
			return true;
		else
			return false;
	}

	protected void OnMessageFilterChanged (object sender, EventArgs e)
	{
		LogFilter.Refilter ();
	}
}
