using System;
using System.Windows.Forms;
using System.IO.Ports;
using System.IO;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using X0xCommon;
using BangOnTekCommon;
using ThreeZeroThreePatternFormats;

namespace K0ntr0l
{
    public partial class Form1 : Form
    {
        private const string K0ntr0lDataFileName = "k0ntr0ldata.xml";
        private const string PatternFileFilter = "ABL files (*.pat)|*.pat|Phoscyon files (*.phptr;*.phptrb)|*.phptr;*.phptrb|Rebirth files (*.rbs)|*.rbs|All Pattern Files|*.pat;*.phptr;*.phptrb;*.rbs";
        private const string EepromDumpFileFilter = "K0ntr0l EEPROM Dump files (*.ked)|*.ked";
        private const string OnlineHelpUrl = "https://kontrol.bangontek.com/documentation.html";
        private const string EepromDumpsFolderName = "EEPROM Dumps";
        private K0ntr0lDataSet.PatternRow _selectedPattern;

        private string DataFileDirectory => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Bang On Tek", "K0ntr0l");
        private string DataFilePath => Path.Combine(DataFileDirectory, K0ntr0lDataFileName);
        
        private readonly DateTime _betaExpiryDate = new DateTime(2019, 1, 1);

        public K0ntr0lDataSet.PatternRow SelectedPattern
        {
            get => _selectedPattern;
            set
            {
                _selectedPattern = value;

                if (_selectedPattern == null)
                {
                    textBoxPatternName.ReadOnly = textBoxPatternDescription.ReadOnly = true;
                    _textBoxPattern.Text = textBoxPatternName.Text = textBoxPatternDescription.Text = string.Empty;

                    _pianoRoll.Sequence = null;
                    if (Properties.Settings.Default.PreviewPatterns)
                    {
                        AuditionPattern(EmptyPattern);
                    }
                }
                else
                {
                    _textBoxPattern.Text = _selectedPattern.ToFreeBeeString();
                    textBoxPatternName.ReadOnly = textBoxPatternDescription.ReadOnly = false;
                    textBoxPatternName.Text = _selectedPattern.Name;
                    textBoxPatternDescription.Text = _selectedPattern.Description;
                    _pianoRoll.Sequence = _selectedPattern.ToPianoRollSequence();
                    if (Properties.Settings.Default.PreviewPatterns)
                    {
                        AuditionPattern(_selectedPattern);
                    }
                }
            }
        }

        public K0ntr0lDataSet.PatternRow EmptyPattern
        {
            get { return Data.AddPatternIfRequired(new X0xPattern() {Name = "Empty"}, 0); }
        }

        private delegate void DisplayTextCallback(string text, params object[] parameters);
        private delegate void SetTempoCallback(ushort tempo);
        private delegate void TreeNodeAfterSelectCallBack(object sender, TreeViewEventArgs arg);
        private delegate void TimerCallBack(object sender, EventArgs args);
        private delegate void ButtonClick(object sender, EventArgs e);
        private delegate void AssignPatternCallBack(K0ntr0lDataSet data);


        private X0xCommunicationHandler _commsHandler;

        private bool IsSettingTempo { get; set; }

        private int SelectedBank { get; set; }
        private int SelectedLocation { get; set; }
        private TreeNode SelectedNode { get; set; }

        private bool HasBetaExpired => DateTime.Now > _betaExpiryDate;

        public K0ntr0lDataSet Data { get; } = new K0ntr0lDataSet();

        public Form1()
        {
            InitializeComponent();

            SelectedBank = SelectedLocation = -1;
        }

        private void Connect()
        {
            _commsHandler?.Dispose();

            DisplayText($"Trying to connect to x0xb0x on port '{comboBoxPorts.Text}'...");

            try
            {
                _commsHandler = new X0xCommunicationHandler(comboBoxPorts.Text, Properties.Settings.Default.CommsBaudRate);
                _commsHandler.TempoChanged += _commsHandler_TempoChanged;
                _commsHandler.SerialPortReadWriteTimeOut = Properties.Settings.Default.SerialPortReadWriteTimeout;
                _commsHandler.WaitForBytesTimeOut = Properties.Settings.Default.WaitForBytesTimeOut;
                _commsHandler.WaitForBytesSleepTime = Properties.Settings.Default.WaitForBytesSleepTime;


                Ping();

                SetTempo(_commsHandler.GetTempo());

                UpdateEepromActionControls(true);
            }
            catch (Exception ex)
            {
                UpdateEepromActionControls(false);
                DisplayError(ex);
            }
        }

        private void UpdateEepromActionControls(bool enable)
        {
            buttonDumpEeprom.Enabled = buttonRestoreEeprom.Enabled =
                loadEEPROMBackupToolStripMenuItem.Enabled = saveEEPROMBackupToolStripMenuItem.Enabled = enable;
        }

        private void SelectedPatternChanged(K0ntr0lDataSet.PatternRow patternRow)
        {
            if (Properties.Settings.Default.PreviewPatterns)
                AuditionPattern(patternRow);
        }

        private void AuditionPattern(K0ntr0lDataSet.PatternRow pattern)
        {
            try
            {
                Debug.Assert(_commsHandler != null);

                DisplayText("Sending Pattern '{0}' to Bank 16 Location 8.", pattern.Name);
                _commsHandler.WritePattern(15, 7, new X0xPattern(pattern));
                DisplayText("Pattern '{0}' sent to Bank 16 Location 8.", pattern.Name);
                if (Properties.Settings.Default.AdjustTempo && pattern.Tempo != 0)
                {
                    _commsHandler.SetTempo(pattern.Tempo);
                }
            }
            catch (Exception ex)
            {
                DisplayError(ex);
            }
        }

        private void SetTempo(ushort tempo)
        {
            try
            {
                IsSettingTempo = true;
                numericUpDownX0xTempo.Value = tempo;
            }
            finally
            {
                IsSettingTempo = false;
            }
        }

        private void _commsHandler_TempoChanged(object sender, X0xTempoChangedEventArgs arg)
        {
           if (InvokeRequired)
            {
                try
                {
                    Invoke(new SetTempoCallback(SetTempo), arg.Tempo);
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                    
                }
               
            }
            else
            {
                SetTempo(arg.Tempo);
            }
        }

        private void Ping()
        {
            try
            {
                DisplayText("Pinging x0xb0x...");
                _commsHandler.Ping();
                DisplayText("Ping successful :)");
            }
            catch (Exception ex)
            {
                DisplayError(ex);
            }
        }

        private void DisplayText(string info, params object[] parameters)
        {
            if (InvokeRequired)
            {
                Invoke(new DisplayTextCallback(DisplayText), parameters);
            }
            else
            {
                toolStripStatusLabel.Text = string.Format(info, parameters);
                toolStripStatusLabel.Invalidate();
                listBoxActivityReport.Items.Add(toolStripStatusLabel.Text);
                listBoxActivityReport.Invalidate();
                listBoxActivityReport.SelectedIndex = listBoxActivityReport.Items.Count - 1;
            }
        }

        private void DisplayError(Exception ex)
        {
            if (ex.InnerException != null)
            {
                DisplayError(ex.InnerException);
            }

            DisplayText($"Error - {ex.GetType()} - {ex.Message}");
        }

        private void X0xCommsFeedbackHandler(decimal percentComplete)
        {
            toolStripProgressBar.Value = (int)percentComplete;
        }

        private void buttonDumpEeprom_Click(object sender, EventArgs e)
        {
            try
            {
                using (new WaitCursor())
                {
                    Debug.Assert(_commsHandler != null);

                    DisplayText("Reading EEPROM contents...");
                    toolStripProgressBar.Maximum = 100;
                    toolStripProgressBar.Minimum = 0;

                    var eepromContents = _commsHandler.ReadEepromContents(X0xCommsFeedbackHandler);

                    DisplayText("Done.");

                    // TODO: Implement eeprom dump file reader/writer class.
                    var saveFileDialog = new SaveFileDialog
                    {
                        AddExtension = true,
                        DefaultExt = ".ked",
                        Filter = EepromDumpFileFilter,
                        OverwritePrompt = true,
                        InitialDirectory = string.IsNullOrEmpty(Properties.Settings.Default.LastDumpBrowsePath) ?
                            Path.Combine(DataFileDirectory, EepromDumpsFolderName) : Properties.Settings.Default.LastDumpBrowsePath
                    };

                    if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
                    {
                        Properties.Settings.Default.LastDumpBrowsePath = Path.GetDirectoryName(saveFileDialog.FileName);
                        using (BinaryWriter writer = new BinaryWriter(File.OpenWrite(saveFileDialog.FileName)))
                        {
                            writer.Write(eepromContents);

                            writer.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DisplayError(ex);
            }
            finally
            {
                ResetToolStripProgressBar();
            }
        }

        private void ResetToolStripProgressBar()
        {
            toolStripProgressBar.Value = 0;
        }

        private void RetrieveAllX0xPatterns()
        {
            try
            {
                using (new WaitCursor())
                {
                    toolStripProgressBar.Minimum = 0;
                    toolStripProgressBar.Maximum = X0xCommunicationHandler.X0XBankCount * X0xCommunicationHandler.X0XLocationsPerBank;
                    toolStripProgressBar.Step = 1;

                    //var patterns = new List<X0xPattern>();
                    for (ushort bank = 0; bank < X0xCommunicationHandler.X0XBankCount; ++bank)
                    {
                        for (ushort location = 0; location < X0xCommunicationHandler.X0XLocationsPerBank; ++location)
                        {
                            DisplayText($"Requesting pattern in bank {bank + 1} location {location + 1}...");
                            var pattern = _commsHandler.RetrievePattern(bank, location);
                            Data.AddPatternIfRequired(pattern, (ushort)numericUpDownX0xTempo.Value);
                            //patterns.Add(pattern);
                            DisplayText("Received Pattern");
                            toolStripProgressBar.PerformStep();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DisplayError(ex);
            }
            finally
            {
                ResetToolStripProgressBar();
            }
        }

        private void buttonRestoreEeprom_Click(object sender, EventArgs e)
        {
            try
            {
                // TODO: Implement eeprom dump file reader/writer class.
                var openFileDialog = new OpenFileDialog
                {
                    AddExtension = true,
                    DefaultExt = ".ked",
                    Filter = EepromDumpFileFilter,
                    InitialDirectory = string.IsNullOrEmpty(Properties.Settings.Default.LastDumpBrowsePath) ?
                        Path.Combine(DataFileDirectory, EepromDumpsFolderName) : Properties.Settings.Default.LastDumpBrowsePath
                };

                if (openFileDialog.ShowDialog(this) == DialogResult.OK)
                {
                    Properties.Settings.Default.LastDumpBrowsePath = Path.GetDirectoryName(Path.GetFullPath(openFileDialog.FileName));
                    using (var reader = new BinaryReader(File.OpenRead(openFileDialog.FileName)))
                    {
                        using (new WaitCursor())
                        {
                            var bytes = new byte[X0xCommunicationHandler.EepromSize];
                            reader.Read(bytes, 0, X0xCommunicationHandler.EepromSize);
                            reader.Close();

                            Debug.Assert(_commsHandler != null);

                            DisplayText("Restoring EEPROM contents...");
                            toolStripProgressBar.Maximum = 100;
                            toolStripProgressBar.Minimum = 0;
                            _commsHandler.WriteEepromContents(bytes, X0xCommsFeedbackHandler);

                            DisplayText("Done.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DisplayError(ex);
            }
            finally
            {
                ResetToolStripProgressBar();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.ShowToolTips)
                InitialiseToolTips();

            // Initialise the ports combo box
            comboBoxPorts.Items.AddRange(SerialPort.GetPortNames());

            Icon = Properties.Resources.Kontrol;

            try
            {
                Data.ReadXml(DataFilePath);               
            }
            catch (Exception ex)
            {
                DisplayError(ex);
            }
            finally
            {
                Data.AcceptChanges();
                Data.RebuildX0xPatternIndex();
                var emptyPattern = EmptyPattern;
                timerStartUpInitialisation.Enabled = true;
            }
        }

         private void comboBoxPorts_SelectedIndexChanged(object sender, EventArgs e)
        {
            Connect();
            InitialiseX0xTreeView();

            RetrieveAllX0xPatterns();

            treeViewX0x.ExpandAll();
            treeViewX0x.Nodes[0].EnsureVisible();
        }

        private void numericUpDownTempo_ValueChanged(object sender, EventArgs e)
        {
            if (IsSettingTempo)
                return;

            try
            {
                if (_commsHandler != null)
                {
                    DisplayText("Setting tempo...");
                    _commsHandler.SetTempo((ushort)numericUpDownX0xTempo.Value);
                    DisplayText("Tempo successfully set.");
                }
            }
            catch (Exception ex)
            {
                DisplayError(ex);
            }
        }

        /// <summary>
        /// Initialises the tree view. At this stage only the nodes for the box, banks and locations are created.
        /// They are empty placeholders that will get populated dynamically as required when the user expands them
        /// to speed things up.
        /// </summary>
        private void InitialiseX0xTreeView()
        {
            treeViewX0x.Nodes.Clear();

            if (_commsHandler != null && _commsHandler.IsConnected)
            {
                var rootNode = treeViewX0x.Nodes.Add($"x0x b0x attached to {comboBoxPorts.Text}");

                for (byte bank = 1; bank <= X0xCommunicationHandler.X0XBankCount; bank++)
                {
                    var bankNode = rootNode.Nodes.Add($"Bank {bank:00}");

                    for (byte location = 1; location <= X0xCommunicationHandler.X0XLocationsPerBank; ++location)
                    {
                        bankNode.Nodes.Add($"Location {location:00}");
                    }
                }
            }
        }

        /// <summary>
        /// Handles the selection of a tree element. The children are created dynamically depending on type
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeViewX0x_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (InvokeRequired)
            {
                Debug.WriteLine("treeViewX0x_AfterSelect ASYNCHRONOUS");
                Invoke(new TreeNodeAfterSelectCallBack(treeViewX0x_AfterSelect), this, e);
                return;
            }

            SelectedNode = e.Node;

            if (e.Node.Text.StartsWith("Bank"))
            {
                SelectedBank = int.Parse(e.Node.Text.Split(' ')[1]);
                SelectedLocation = -1;
            }
            else if (e.Node.Text.StartsWith("Location"))
            {
                SelectedBank = int.Parse(e.Node.Parent.Text.Split(' ')[1]);
                SelectedLocation = int.Parse(e.Node.Text.Split(' ')[1]);
            }
            else
            {
                SelectedBank = SelectedLocation = -1;
            }

            // retrieve the x0xb0x pattern at that location.
            RetrievePatternForSelectedLocation();

            UpdateBrowseControls();
        }

        private void RetrievePatternForSelectedLocation()
        {
            if (SelectedLocation == -1 || SelectedBank == -1)
            {
                SelectedPattern = null;
            }
            else if (_commsHandler != null)
            {
                try
                {
                    DisplayText($"Retrieving Pattern from Bank {SelectedBank} Location {SelectedLocation}...");
                    var pattern = _commsHandler.RetrievePattern((ushort)(SelectedBank - 1), (ushort)(SelectedLocation - 1));
                    DisplayText("Pattern successfully retrieved.");

                    SelectedPattern = Data.AddPatternIfRequired(pattern, (ushort)numericUpDownX0xTempo.Value);
                }
                catch (Exception ex)
                {
                    DisplayError(ex);
                }
            }
        }

        /// <summary>
        /// Handles mouse click on a tree node by updating the Browse buttons
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeViewX0x_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            //e.Node.Text 
            UpdateBrowseControls();
        }

        /// <summary>
        /// Enables or disables the Browse buttons based on the tree control's item selection status
        /// </summary>
        private void UpdateBrowseControls()
        {
            buttonBrowseFiles.Enabled = buttonBrowseLibrary.Enabled = buttonBrowseFolder.Enabled =
                loadPatternsToolStripMenuItem.Enabled = importPatternsDirecoryToolStripMenuItem.Enabled =
                treeViewX0x.SelectedNode != null;
        }
        
        /// <summary>
        /// Assigns a bunch of patterns starting at the selected bank/location and proceeding serialy.
        /// WARNING : if the number of patterns exceeds the available memory space we stop.
        /// </summary>
        /// <param name="patterns"></param>
        private void AssignPatternsToSelectedLocation(K0ntr0lDataSet patterns)
        {
            DisplayStopPlaybackWarningIfRequired();
            ushort bank = (ushort)(SelectedBank == -1 ? 0 : SelectedBank - 1);
            ushort location = (ushort)(SelectedLocation == -1 ? 0 : SelectedLocation - 1);

            if (_commsHandler != null)
            {
                using (new WaitCursor())
                {
                    try
                    {
                       toolStripProgressBar.Minimum = 0;
                        toolStripProgressBar.Maximum = patterns.Pattern.Count;
                        toolStripProgressBar.Step = 1;
                                               
                        foreach (var pattern in patterns.Pattern)
                        {
                            if (location >= X0xCommunicationHandler.X0XLocationsPerBank)
                            {
                                ++bank;
                                location = 0;
                            }

                            if (bank >= X0xCommunicationHandler.X0XBankCount)
                            {
                                DisplayText("Reached the last pattern location, stopping.");
                                break;
                            }

                            toolStripProgressBar.PerformStep();
                            DisplayText("Sending pattern '{0}' to bank {1} location {2}.", pattern.Name, bank + 1, location + 1);
                            var x0xPattern = new X0xPattern(pattern);
                           Debug.Assert(x0xPattern.ToByteArray().ToByteString() == pattern.X0xPatternBytes.ToByteString());

                            _commsHandler.WritePattern(bank, location, x0xPattern);
                            
                            DisplayText("Pattern successfully sent.");
                            //SelectedPattern = pattern;

                            ++location;
                        }

                        /* if (patterns.Pattern.Count > 0)
                         {
                             SelectedPattern = null;
                             SelectedPattern = _data.Pattern.FindByPatternId(patterns.Pattern[0].PatternId);
                         }*/

                    }
                    catch (Exception ex)
                    {
                        DisplayError(ex);
                    }
                    finally
                    {
                       Invoke(new TreeNodeAfterSelectCallBack(treeViewX0x_AfterSelect), this, new TreeViewEventArgs(SelectedNode));
                    
                        ResetToolStripProgressBar();
                    }
                }
            }
        }

        private void DisplayStopPlaybackWarningIfRequired()
        {
           if (Properties.Settings.Default.PreviewPatterns
                && Properties.Settings.Default.FormPreviewCommsWarning)
            {
                using (var form = new FormPreviewCommsWarning())
                {
                    form.ShowDialog(this);
                }
            }
        }

        /*private void OnMenuItemClick(object src, EventArgs args)
        {
            Debug.WriteLine("Click");
            var menuItem = src as MenuItem;

            // Load patterns from file, and assign them to selected bank/location.
            // Multiple patterns are assigned to next available locations.
            var patterns = new FreeBeeFileReader(menuItem.Tag as string).Data;
            _data.AddPatternsIfRequired(patterns);

            AssignPatternsToSelectedLocation(patterns);
        }*/

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Persist all data

            Properties.Settings.Default.Save();
            
            try
            {
                if (Properties.Settings.Default.PreviewPatterns)
                {
                    AuditionPattern(EmptyPattern);
                }

                Data.AcceptChanges();
                Data.WriteXml(DataFilePath);
            }
            catch(Exception ex)
            {
                DisplayError(ex);
            }
        }

        private void comboBoxPorts_DropDown(object sender, EventArgs e)
        {
            // Initialise the ports combo box
            comboBoxPorts.Items.Clear();
            comboBoxPorts.Items.AddRange(SerialPort.GetPortNames());
        }

        private void buttonBrowseFiles_Click(object sender, EventArgs e)
        {
            using (new WaitCursor())
            {
                var fileDialog = new OpenFileDialog();
                /*fileDialog.CheckFileExists =*/
                fileDialog.CheckPathExists = true;
                fileDialog.Multiselect = true;
                fileDialog.Title = "Select Pattern Files...";
                fileDialog.Filter = PatternFileFilter;
                fileDialog.FilterIndex = Properties.Settings.Default.FileFilterIndex;
                fileDialog.InitialDirectory = GetBrowsePath();

                if (fileDialog.ShowDialog(this) == DialogResult.OK)
                {
                    Properties.Settings.Default.FileFilterIndex = fileDialog.FilterIndex;
                    Properties.Settings.Default.LastPatternBrowsePath = Path.GetDirectoryName(Path.GetFullPath(fileDialog.FileName));

                    var fileNames = fileDialog.FileNames;

                    LoadFiles(fileNames);
                }
            }
        }

        private void LoadFiles(string[] fileNames)
        {
            var allPatterns = new K0ntr0lDataSet();

            toolStripProgressBar.Minimum = 0;
            toolStripProgressBar.Maximum = fileNames.Length;
            toolStripProgressBar.Step = 1;
            toolStripProgressBar.Value = 0;

            using (new WaitCursor())
            {
                foreach (var fileName in fileNames)
                {
                    K0ntr0lDataSet patterns = null;

                    switch (Path.GetExtension(fileName)?.ToLower())
                    {
                        case ".rbs":
                            DisplayText("Importing data from file '{0}'...", Path.GetFileName(fileName));
                            patterns = new RebirthFileReader(fileName).Data;
                            break;
                        case ".pat":
                            DisplayText("Importing data from file '{0}'...", Path.GetFileName(fileName));
                            patterns = new FreeBeeFileReader(fileName).Data;
                            break;
                        case ".phptr":
                        case ".phptrb":
                            DisplayText("Importing data from file '{0}'...", Path.GetFileName(fileName));
                            patterns = new PhoscyonFileReader(fileName).Data;
                            break;
                        default:
                            // Ignore
                            DisplayText("Ignoring file with unknown extension: '" + fileName + "'.");
                            break;
                    }

                    if (patterns != null)
                    {
                        allPatterns.Merge(patterns, true);
                    }

                    toolStripProgressBar.PerformStep();
                    toolStripProgressBar.Invalidate();
                    //Application.DoEvents();
                }

                allPatterns.AcceptChanges();
                Data.AddPatternsIfRequired(allPatterns);
            }

            AssignPatternsToSelectedLocation(allPatterns);
        }

        private K0ntr0lDataSet LoadPatterns(string[] filesPath)
        {
            // TODO...
            return new K0ntr0lDataSet();
        }

        private void buttonBrowseLibrary_Click(object sender, EventArgs e)
        {
            try
            {
                var currentPattern = SelectedPattern;
                
                var form = new FormPatternLibrary(Data, SelectedPatternChanged, currentPattern);

                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    var data = new K0ntr0lDataSet();
                    var selectedRows = form.SelectedRows;
                    data.Merge(selectedRows);
                    foreach (var row in selectedRows)
                    {
                        data.Merge(row.GetStepRows());
                    }

                    data.AcceptChanges();

                    BeginInvoke(new AssignPatternCallBack(AssignPatternsToSelectedLocation), data);
                   
                }
            }
            catch(Exception ex)
            {
                DisplayError(ex);
            }
        }

        private void treeViewX0x_MouseClick(object sender, MouseEventArgs e)
        {
            UpdateBrowseControls();
        }

        private void textBoxPatternName_TextChanged(object sender, EventArgs e)
        {
            if (SelectedPattern != null)
            {
                SelectedPattern.BeginEdit();
                SelectedPattern.Name = textBoxPatternName.Text;
                SelectedPattern.EndEdit();
                SelectedPattern.AcceptChanges();
            }
        }


        private void textBoxPatternDescription_TextChanged(object sender, EventArgs e)
        {
            if (SelectedPattern != null)
            {
                SelectedPattern.BeginEdit();
                SelectedPattern.Description = textBoxPatternDescription.Text;
                SelectedPattern.EndEdit();
                SelectedPattern.AcceptChanges();
            }
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            
        }

        private void _pianoRoll_Click(object sender, EventArgs e)
        {
           
        }

        private void _pianoRoll_DoubleClick(object sender, EventArgs e)
        {
            using (var form = new FormPianoRollConfiguration())
            {
                form.ShowDialog(this);

                RefreshPianoRoll();
            }
        }

        private void RefreshPianoRoll()
        {
            PianoRoll.NoteBrush = new SolidBrush(Properties.Settings.Default.NoteColor);
            PianoRoll.AccentedNoteBrush = new SolidBrush(Properties.Settings.Default.AccentedNoteColor);
            PianoRoll.RestBrush = new SolidBrush(Properties.Settings.Default.RestColor);
            PianoRoll.AccentedRestBrush = new SolidBrush(Properties.Settings.Default.AccentedRestColor);
            PianoRoll.SlidePen = new Pen(Properties.Settings.Default.SlideColor, PianoRoll.SlidePenThickness);
            _pianoRoll.Refresh();
        }
        
        private void treeViewX0x_DoubleClick(object sender, EventArgs e)
        {
            if (treeViewX0x.SelectedNode.Text.StartsWith("x0x b0x"))
            {
                treeViewX0x.ExpandAll();
                treeViewX0x.Nodes[0].EnsureVisible();
            }
            else
            {
                if (!treeViewX0x.SelectedNode.Text.StartsWith("Location")) return;
                BeginInvoke(
                    Properties.Settings.Default.SelectedPatternDoubleClickAction == 0
                        ? buttonBrowseFiles_Click
                        : new ButtonClick(buttonBrowseLibrary_Click), sender, e);
            }
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var form = new FormAbout())
            {
                form.ShowDialog(this);
            }
        }

        private void fileToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            loadPatternsToolStripMenuItem.Enabled = treeViewX0x.SelectedNode != null;
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var form = new FormSettings())
            {
                form.ShowDialog(this);
                RefreshPianoRoll();
                Connect();
            }
        }

        private void timerStartUpInitialisation_Tick(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new TimerCallBack(timerStartUpInitialisation_Tick), sender, e);
            }
            else
            {
                timerStartUpInitialisation.Enabled = false;
                // If only }one port, we attempt to connect to it by default
                if (comboBoxPorts.Items.Count == 1)
                {
                    comboBoxPorts.SelectedIndex = 0;
                }

                UpdateBrowseControls();

                RefreshPianoRoll();
            }
        }

        /// <summary>
        /// Defines the tool tips to be displayed for the various form controls
        /// </summary>
        private void InitialiseToolTips()
        {
            // Create the ToolTip and associate with the Form container.
            ToolTip toolTip1 = new ToolTip
            {
                AutoPopDelay = 5000, InitialDelay = 100, ReshowDelay = 100, ShowAlways = true
            };

            // Set up the delays for the ToolTip.
            // Force the ToolTip text to be displayed whether or not the form is active.

            // Set up the ToolTip text for the form's controls
            toolTip1.SetToolTip(textBoxPatternDescription, "Pattern Description");
            toolTip1.SetToolTip(textBoxPatternName, "Pattern Name");
            toolTip1.SetToolTip(numericUpDownX0xTempo, "x0xb0x Tempo");
            toolTip1.SetToolTip(_textBoxPattern, "Pattern Data in ABL Format");
            toolTip1.SetToolTip(treeViewX0x, "x0xb0x Pattern Banks");
            toolTip1.SetToolTip(listBoxActivityReport, "Activity Report Window");
            toolTip1.SetToolTip(buttonBrowseFiles, "Load Patterns From File(s), starting at the currently selected location");
            toolTip1.SetToolTip(buttonBrowseFolder, "Load Patterns From Folder(s) recursively, starting at the currently selected location");
            toolTip1.SetToolTip(buttonBrowseLibrary, "Load Patterns From Library, starting at the currently selected location");
            toolTip1.SetToolTip(buttonDumpEeprom, "Save x0xb0x EEPROM contents to file");
            toolTip1.SetToolTip(buttonRestoreEeprom, "Restore x0xb0x EEPROM contents from file");
            toolTip1.SetToolTip(comboBoxPorts, "Selects the port on which the x0xb0x is connected");
            toolTip1.SetToolTip(_pianoRoll, "Displays the pattern data in Piano Roll format");
        }

        private void buttonBrowseFolder_Click(object sender, EventArgs e)
        {
            using (var folderBrowser = new FolderBrowserDialog())
            {
                folderBrowser.SelectedPath = GetBrowsePath();
                if (folderBrowser.ShowDialog(this) == DialogResult.OK)
                {
                    Properties.Settings.Default.LastPatternBrowsePath = folderBrowser.SelectedPath;

                    var files = Directory.EnumerateFiles(folderBrowser.SelectedPath, "*.*",
                        SearchOption.AllDirectories);
                    LoadFiles(files.ToArray());
                }
            }
        }

        private string GetBrowsePath()
        {
            if (string.IsNullOrEmpty(Properties.Settings.Default.LastPatternBrowsePath))
                return Path.Combine(DataFileDirectory, "Sample Patterns");

            return Properties.Settings.Default.LastPatternBrowsePath;
        }
    }
}