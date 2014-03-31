
// This file has been generated by the GUI designer. Do not modify.

public partial class MainWindow
{
	private global::Gtk.UIManager UIManager;
	private global::Gtk.Action FileAction;
	private global::Gtk.Action OpenAction;
	private global::Gtk.Action FileAction1;
	private global::Gtk.Action openAction;
	private global::Gtk.Action quitAction;
	private global::Gtk.Action openAction1;
	private global::Gtk.Action preferencesAction;
	private global::Gtk.ToggleAction findAction;
	private global::Gtk.Action ToolsAction;
	private global::Gtk.Action preferencesAction1;
	private global::Gtk.VBox MainBox;
	private global::Gtk.Fixed FixedPane;
	private global::Gtk.MenuBar MenuBar;
	private global::Gtk.Toolbar MainToolBar;
	private global::Gtk.HBox FilterPanel;
	private global::Gtk.Label MessageFilterLabel;
	private global::Gtk.Entry MessageFilterCriteria;
	private global::Gtk.Button SearchButton;
	private global::Gtk.Alignment alignment1;
	private global::Gtk.Button FilterClearButton;
	private global::Gtk.VPaned LogPanel;
	private global::Gtk.ScrolledWindow LogLinesWindow;
	private global::Gtk.TreeView MainTable;
	private global::Gtk.ScrolledWindow LogDetailsWindow;
	private global::Gtk.TextView LogDetailsTextView;
	private global::Gtk.Statusbar MainWindowStatusBar;
	private global::Gtk.ProgressBar MainProgressBar;

	protected virtual void Build ()
	{
		global::Stetic.Gui.Initialize (this);
		// Widget MainWindow
		this.UIManager = new global::Gtk.UIManager ();
		global::Gtk.ActionGroup w1 = new global::Gtk.ActionGroup ("Default");
		this.FileAction = new global::Gtk.Action ("FileAction", global::Mono.Unix.Catalog.GetString ("File"), null, null);
		this.FileAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("File");
		w1.Add (this.FileAction, null);
		this.OpenAction = new global::Gtk.Action ("OpenAction", global::Mono.Unix.Catalog.GetString ("Open"), null, null);
		this.OpenAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Open");
		w1.Add (this.OpenAction, null);
		this.FileAction1 = new global::Gtk.Action ("FileAction1", global::Mono.Unix.Catalog.GetString ("_File"), null, null);
		this.FileAction1.ShortLabel = global::Mono.Unix.Catalog.GetString ("_File");
		w1.Add (this.FileAction1, null);
		this.openAction = new global::Gtk.Action ("openAction", global::Mono.Unix.Catalog.GetString ("_Open"), null, "gtk-open");
		this.openAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("_Open");
		w1.Add (this.openAction, null);
		this.quitAction = new global::Gtk.Action ("quitAction", global::Mono.Unix.Catalog.GetString ("E_xit"), null, "gtk-quit");
		this.quitAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("E_xit");
		w1.Add (this.quitAction, "<Primary><Mod2>q");
		this.openAction1 = new global::Gtk.Action ("openAction1", null, null, "gtk-open");
		w1.Add (this.openAction1, null);
		this.preferencesAction = new global::Gtk.Action ("preferencesAction", null, null, "gtk-preferences");
		w1.Add (this.preferencesAction, null);
		this.findAction = new global::Gtk.ToggleAction ("findAction", null, null, "gtk-find");
		w1.Add (this.findAction, null);
		this.ToolsAction = new global::Gtk.Action ("ToolsAction", global::Mono.Unix.Catalog.GetString ("Tools"), null, null);
		this.ToolsAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Tools");
		w1.Add (this.ToolsAction, null);
		this.preferencesAction1 = new global::Gtk.Action ("preferencesAction1", global::Mono.Unix.Catalog.GetString ("Preferences"), null, "gtk-preferences");
		this.preferencesAction1.ShortLabel = global::Mono.Unix.Catalog.GetString ("Preferences");
		w1.Add (this.preferencesAction1, null);
		this.UIManager.InsertActionGroup (w1, 0);
		this.AddAccelGroup (this.UIManager.AccelGroup);
		this.Name = "MainWindow";
		this.Title = global::Mono.Unix.Catalog.GetString ("LogViewer");
		this.WindowPosition = ((global::Gtk.WindowPosition)(4));
		this.Modal = true;
		this.AllowShrink = true;
		this.DefaultWidth = 1024;
		this.DefaultHeight = 768;
		// Container child MainWindow.Gtk.Container+ContainerChild
		this.MainBox = new global::Gtk.VBox ();
		this.MainBox.Name = "MainBox";
		this.MainBox.Spacing = 6;
		// Container child MainBox.Gtk.Box+BoxChild
		this.FixedPane = new global::Gtk.Fixed ();
		this.FixedPane.Name = "FixedPane";
		this.FixedPane.HasWindow = false;
		this.MainBox.Add (this.FixedPane);
		global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.MainBox [this.FixedPane]));
		w2.Position = 0;
		w2.Expand = false;
		w2.Fill = false;
		// Container child MainBox.Gtk.Box+BoxChild
		this.UIManager.AddUiFromString ("<ui><menubar name='MenuBar'><menu name='FileAction1' action='FileAction1'><menuitem name='openAction' action='openAction'/><separator/><menuitem name='quitAction' action='quitAction'/></menu><menu name='ToolsAction' action='ToolsAction'><menuitem name='preferencesAction1' action='preferencesAction1'/></menu></menubar></ui>");
		this.MenuBar = ((global::Gtk.MenuBar)(this.UIManager.GetWidget ("/MenuBar")));
		this.MenuBar.Name = "MenuBar";
		this.MainBox.Add (this.MenuBar);
		global::Gtk.Box.BoxChild w3 = ((global::Gtk.Box.BoxChild)(this.MainBox [this.MenuBar]));
		w3.Position = 1;
		w3.Expand = false;
		w3.Fill = false;
		// Container child MainBox.Gtk.Box+BoxChild
		this.UIManager.AddUiFromString ("<ui><toolbar name='MainToolBar'><toolitem name='openAction1' action='openAction1'/><toolitem name='findAction' action='findAction'/><separator/><toolitem name='preferencesAction' action='preferencesAction'/></toolbar></ui>");
		this.MainToolBar = ((global::Gtk.Toolbar)(this.UIManager.GetWidget ("/MainToolBar")));
		this.MainToolBar.Name = "MainToolBar";
		this.MainToolBar.ShowArrow = false;
		this.MainToolBar.ToolbarStyle = ((global::Gtk.ToolbarStyle)(0));
		this.MainBox.Add (this.MainToolBar);
		global::Gtk.Box.BoxChild w4 = ((global::Gtk.Box.BoxChild)(this.MainBox [this.MainToolBar]));
		w4.Position = 2;
		w4.Expand = false;
		w4.Fill = false;
		// Container child MainBox.Gtk.Box+BoxChild
		this.FilterPanel = new global::Gtk.HBox ();
		this.FilterPanel.Name = "FilterPanel";
		this.FilterPanel.Spacing = 6;
		// Container child FilterPanel.Gtk.Box+BoxChild
		this.MessageFilterLabel = new global::Gtk.Label ();
		this.MessageFilterLabel.Name = "MessageFilterLabel";
		this.MessageFilterLabel.Xpad = 5;
		this.MessageFilterLabel.LabelProp = global::Mono.Unix.Catalog.GetString ("Search Message");
		this.FilterPanel.Add (this.MessageFilterLabel);
		global::Gtk.Box.BoxChild w5 = ((global::Gtk.Box.BoxChild)(this.FilterPanel [this.MessageFilterLabel]));
		w5.Position = 0;
		w5.Expand = false;
		w5.Fill = false;
		// Container child FilterPanel.Gtk.Box+BoxChild
		this.MessageFilterCriteria = new global::Gtk.Entry ();
		this.MessageFilterCriteria.CanFocus = true;
		this.MessageFilterCriteria.Name = "MessageFilterCriteria";
		this.MessageFilterCriteria.IsEditable = true;
		this.MessageFilterCriteria.InvisibleChar = '●';
		this.FilterPanel.Add (this.MessageFilterCriteria);
		global::Gtk.Box.BoxChild w6 = ((global::Gtk.Box.BoxChild)(this.FilterPanel [this.MessageFilterCriteria]));
		w6.Position = 1;
		// Container child FilterPanel.Gtk.Box+BoxChild
		this.SearchButton = new global::Gtk.Button ();
		this.SearchButton.CanDefault = true;
		this.SearchButton.CanFocus = true;
		this.SearchButton.Name = "SearchButton";
		this.SearchButton.UseStock = true;
		this.SearchButton.UseUnderline = true;
		this.SearchButton.Label = "gtk-find";
		this.FilterPanel.Add (this.SearchButton);
		global::Gtk.Box.BoxChild w7 = ((global::Gtk.Box.BoxChild)(this.FilterPanel [this.SearchButton]));
		w7.Position = 2;
		w7.Expand = false;
		w7.Fill = false;
		// Container child FilterPanel.Gtk.Box+BoxChild
		this.alignment1 = new global::Gtk.Alignment (0.5F, 0.5F, 1F, 1F);
		this.alignment1.Name = "alignment1";
		this.alignment1.RightPadding = ((uint)(5));
		// Container child alignment1.Gtk.Container+ContainerChild
		this.FilterClearButton = new global::Gtk.Button ();
		this.FilterClearButton.CanFocus = true;
		this.FilterClearButton.Name = "FilterClearButton";
		this.FilterClearButton.UseStock = true;
		this.FilterClearButton.UseUnderline = true;
		this.FilterClearButton.Label = "gtk-clear";
		this.alignment1.Add (this.FilterClearButton);
		this.FilterPanel.Add (this.alignment1);
		global::Gtk.Box.BoxChild w9 = ((global::Gtk.Box.BoxChild)(this.FilterPanel [this.alignment1]));
		w9.Position = 3;
		w9.Expand = false;
		w9.Fill = false;
		this.MainBox.Add (this.FilterPanel);
		global::Gtk.Box.BoxChild w10 = ((global::Gtk.Box.BoxChild)(this.MainBox [this.FilterPanel]));
		w10.Position = 3;
		w10.Expand = false;
		w10.Fill = false;
		// Container child MainBox.Gtk.Box+BoxChild
		this.LogPanel = new global::Gtk.VPaned ();
		this.LogPanel.CanFocus = true;
		this.LogPanel.Name = "LogPanel";
		this.LogPanel.Position = 294;
		// Container child LogPanel.Gtk.Paned+PanedChild
		this.LogLinesWindow = new global::Gtk.ScrolledWindow ();
		this.LogLinesWindow.Name = "LogLinesWindow";
		this.LogLinesWindow.ShadowType = ((global::Gtk.ShadowType)(1));
		this.LogLinesWindow.BorderWidth = ((uint)(5));
		// Container child LogLinesWindow.Gtk.Container+ContainerChild
		this.MainTable = new global::Gtk.TreeView ();
		this.MainTable.HeightRequest = 0;
		this.MainTable.CanFocus = true;
		this.MainTable.Name = "MainTable";
		this.MainTable.EnableSearch = false;
		this.LogLinesWindow.Add (this.MainTable);
		this.LogPanel.Add (this.LogLinesWindow);
		global::Gtk.Paned.PanedChild w12 = ((global::Gtk.Paned.PanedChild)(this.LogPanel [this.LogLinesWindow]));
		w12.Resize = false;
		// Container child LogPanel.Gtk.Paned+PanedChild
		this.LogDetailsWindow = new global::Gtk.ScrolledWindow ();
		this.LogDetailsWindow.Name = "LogDetailsWindow";
		this.LogDetailsWindow.ShadowType = ((global::Gtk.ShadowType)(1));
		// Container child LogDetailsWindow.Gtk.Container+ContainerChild
		this.LogDetailsTextView = new global::Gtk.TextView ();
		this.LogDetailsTextView.HeightRequest = 0;
		this.LogDetailsTextView.CanFocus = true;
		this.LogDetailsTextView.Name = "LogDetailsTextView";
		this.LogDetailsTextView.Editable = false;
		this.LogDetailsTextView.CursorVisible = false;
		this.LogDetailsTextView.WrapMode = ((global::Gtk.WrapMode)(3));
		this.LogDetailsWindow.Add (this.LogDetailsTextView);
		this.LogPanel.Add (this.LogDetailsWindow);
		this.MainBox.Add (this.LogPanel);
		global::Gtk.Box.BoxChild w15 = ((global::Gtk.Box.BoxChild)(this.MainBox [this.LogPanel]));
		w15.Position = 4;
		// Container child MainBox.Gtk.Box+BoxChild
		this.MainWindowStatusBar = new global::Gtk.Statusbar ();
		this.MainWindowStatusBar.Name = "MainWindowStatusBar";
		this.MainWindowStatusBar.Spacing = 6;
		// Container child MainWindowStatusBar.Gtk.Box+BoxChild
		this.MainProgressBar = new global::Gtk.ProgressBar ();
		this.MainProgressBar.Name = "MainProgressBar";
		this.MainWindowStatusBar.Add (this.MainProgressBar);
		global::Gtk.Box.BoxChild w16 = ((global::Gtk.Box.BoxChild)(this.MainWindowStatusBar [this.MainProgressBar]));
		w16.Position = 1;
		this.MainBox.Add (this.MainWindowStatusBar);
		global::Gtk.Box.BoxChild w17 = ((global::Gtk.Box.BoxChild)(this.MainBox [this.MainWindowStatusBar]));
		w17.Position = 5;
		w17.Expand = false;
		w17.Fill = false;
		this.Add (this.MainBox);
		if ((this.Child != null)) {
			this.Child.ShowAll ();
		}
		this.SearchButton.HasDefault = true;
		this.FilterPanel.Hide ();
		this.Show ();
		this.DeleteEvent += new global::Gtk.DeleteEventHandler (this.OnDeleteEvent);
		this.openAction.Activated += new global::System.EventHandler (this.onFileOpen);
		this.quitAction.Activated += new global::System.EventHandler (this.onExit);
		this.openAction1.Activated += new global::System.EventHandler (this.onFileOpen);
		this.preferencesAction.Activated += new global::System.EventHandler (this.OnPreferencesClicked);
		this.findAction.Toggled += new global::System.EventHandler (this.onFilterToggled);
		this.preferencesAction1.Activated += new global::System.EventHandler (this.OnPreferencesClicked);
		this.SearchButton.Clicked += new global::System.EventHandler (this.OnSearchClicked);
		this.FilterClearButton.Clicked += new global::System.EventHandler (this.OnFilterClearClicked);
		this.MainTable.CursorChanged += new global::System.EventHandler (this.onRowSelect);
	}
}
