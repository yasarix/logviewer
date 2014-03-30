using System;
using Gtk;
using System.Collections;
using LogViewer;
using Pango;
using System.Text.RegularExpressions;
using System.Threading;

public partial class MainWindow: Gtk.Window
{
	string WindowTitle = "LogViewer";

	Gtk.ListStore LogStore;
	Gtk.ListStore EmptyLogStore;
	string[] FileContent = new string[]{ };
	Thread FileLoadThread;

	public MainWindow () : base (Gtk.WindowType.Toplevel)
	{
		Build ();
		initUi ();
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		onExit (sender, a);
	}

	protected void resetUi()
	{
		MainTable.Model = EmptyLogStore;
		LogDetailsTextView.Buffer.Text = "";
		MessageFilterCriteria.Text = "";
		LogStore = new Gtk.ListStore (typeof(LogLine));
		MainProgressBar.Fraction = 0;
	}

	protected void initUi()
	{
		// This is needed when model of MainTable changed
		MainTable.RedrawOnAllocate = true;

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
		EmptyLogStore = new Gtk.ListStore (typeof (LogLine));

		MainTable.Model = EmptyLogStore;

		timestamp_column.SetCellDataFunc (timestamp_cell, new Gtk.TreeCellDataFunc (renderTimestamp));
		process_column.SetCellDataFunc (process_cell, new Gtk.TreeCellDataFunc (renderProcess));
		LevelColumn.SetCellDataFunc (level_cell, new Gtk.TreeCellDataFunc (renderLevel));
		message_column.SetCellDataFunc (message_cell, new Gtk.TreeCellDataFunc (renderMessage));

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
		FileChooserDialog chooser = new FileChooserDialog(
			"Select a logfile to view ...",
			this,
			FileChooserAction.Open,
			"Cancel", ResponseType.Cancel,
			"Open", ResponseType.Accept );

		if (chooser.Run () == (int)ResponseType.Accept) {
			try {
				FileLoadThread.Abort ();
			}
			catch {
				// I see the problem, and I plan to do nothing about it
			}

			// Open the file for reading.
			string file_name = chooser.Filename.ToString ();

			// Set the MainWindow Title to the filename.
			addFilenameToTitle (file_name);

			chooser.Destroy ();

			// Reset the interface to load new file
			resetUi ();

			MainWindowStatusBar.Push (1, "Loading file contents...");

			// Load file in a thread
			FileLoadThread = new Thread (() => {loadFile (file_name);});
			FileLoadThread.Start ();

		} else {
			chooser.Destroy ();
		}
	}

	public void addFilenameToTitle(string file_name) {
		this.Title = this.WindowTitle + " - " + file_name;
	}

	public void loadFile(string file_name)
	{
		LogLine log_line = new LogLine ();

		FileContent = System.IO.File.ReadAllLines (file_name);

		double progress_step = FileContent.Length / 100;
		int i = 0;
		double current_progress = 0;
		foreach (string file_line in FileContent) {
			log_line = this.parseLogLine (file_line);
			LogStore.AppendValues (log_line);

			if (i % progress_step == 0) {
				Gtk.Application.Invoke (delegate {
					Gdk.Threads.Enter ();

					try {
						current_progress = MainProgressBar.Fraction;
						current_progress += 0.01;
						if (current_progress > 1.0) {
							current_progress = 1.0;
						}

						MainProgressBar.Fraction = current_progress;
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
				MainTable.Model = LogStore;
				MainWindowStatusBar.Push (1, "File loaded.");
			} finally {
				Gdk.Threads.Leave ();
			}
		});
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
		try {
			FileLoadThread.Abort();
		}
		catch {
		}

		Application.Quit ();
	}

	protected void onFilterToggled (object sender, EventArgs e)
	{
		if (FilterPanel.Visible) {
			MessageFilterCriteria.Text = "";
			FilterPanel.Visible = false;
		} else {
			FilterPanel.Visible = true;
			MessageFilterCriteria.GrabFocus ();
		}
	}

	private bool filterLogEntries (Gtk.TreeModel model, Gtk.TreeIter iter)
	{
		LogLine line = (LogLine) model.GetValue (iter, 0);

		if (MessageFilterCriteria.Text == "")
			return true;

		if (line.Message.IndexOf(MessageFilterCriteria.Text, StringComparison.CurrentCultureIgnoreCase) > -1)
			return true;
		else
			return false;
	}

	protected void OnMessageFilterEntered (object sender, EventArgs e)
	{
	}

	protected void OnPreferencesClicked(object sender, EventArgs e)
	{}
}
