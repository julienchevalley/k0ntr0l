using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using ThreeZeroThreePatternFormats;
using System.Diagnostics;

namespace K0ntr0l
{
    public partial class FormPatternLibrary : Form
    {
        public delegate void SelectedPatternChangedCallback(K0ntr0lDataSet.PatternRow patternRow);

        private readonly SelectedPatternChangedCallback _selectedPatternChangedCallback;
        private readonly K0ntr0lDataSet.PatternRow _patternToSelect;

        public K0ntr0lDataSet.PatternRow[] SelectedRows
        {
            get
            {
                // sort the rows
                List<DataGridViewRow> rows =
                    (from DataGridViewRow row in dataGridView1.SelectedRows
                        where !row.IsNewRow
                        orderby row.Index
                        select row).ToList();

                var selectedRows = new K0ntr0lDataSet.PatternRow[rows.Count];

                for (int i = 0; i < rows.Count; i++)
                {
                    selectedRows[i] = ((DataRowView) rows[i].DataBoundItem).Row as K0ntr0lDataSet.PatternRow;
                }

                return selectedRows;
            }
        }

        public FormPatternLibrary(K0ntr0lDataSet data, SelectedPatternChangedCallback selectedPatternChangedCallback,
            K0ntr0lDataSet.PatternRow selectedPattern = null)
        {
            InitializeComponent();
            _data = data;
            _patternBindingSource.DataSource = _data;
            Debug.Assert(dataGridView1 != null, nameof(dataGridView1) + " != null");
            // ReSharper disable once PossibleNullReferenceException
            dataGridView1.Columns[_data.Pattern.X0xPatternBytesColumn.ColumnName].Visible = false;
            _selectedPatternChangedCallback = selectedPatternChangedCallback;
            _patternToSelect = selectedPattern;
        }

        private void FormPatternLibrary_Load(object sender, EventArgs e)
        {
            Size = Properties.Settings.Default.PatternLibraryWindowSize;
            WindowState = Properties.Settings.Default.PatternLibraryWindowState;
            nameDataGridViewTextBoxColumn.Width = Properties.Settings.Default.PatternLinbraryColumnNameWidth;
            descriptionDataGridViewTextBoxColumn.Width =
                Properties.Settings.Default.PatternLinbraryColumnDescriptionWidth;
            lengthDataGridViewTextBoxColumn.Width = Properties.Settings.Default.PatternLinbraryColumnLengthWidth;
            tempoDataGridViewTextBoxColumn.Width = Properties.Settings.Default.PatternLinbraryColumnTempoWidth;
            sourceDataGridViewTextBoxColumn.Width = Properties.Settings.Default.PatternLinbraryColumnSourceWidth;

            if (_patternToSelect != null)
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    var dataRowView = row.DataBoundItem as DataRowView;
                    Debug.Assert(dataRowView != null);
                    var patternRow = dataRowView.Row as K0ntr0lDataSet.PatternRow;
                    Debug.Assert(patternRow != null);
                    row.Selected = patternRow.PatternId == _patternToSelect.PatternId;
                }
            }

            dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.SelectedRows[0].Index;
            labelNumberOfPatterns.Text = dataGridView1.SelectedRows.Count + "/"+ dataGridView1.Rows.Count.ToString();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var selectedRows = SelectedRows;
            if (selectedRows.Length == 1)
            {
                using (var pianoRoll = new FormPianoRoll(selectedRows[0].Name, selectedRows[0].ToPianoRollSequence()))
                {
                    pianoRoll.ShowDialog(this);
                }
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (SelectedRows.Length > 0)
            {
                _selectedPatternChangedCallback(SelectedRows.Last());
            }
            else
            {
                var emptyPattern = _data.AddPatternIfRequired(new X0xPattern() {Name = "Empty"}, 0);
                emptyPattern.Source = "Bang On Tek Empty Pattern";
                emptyPattern.Description = "Do Not Delete";
                emptyPattern.AcceptChanges();
                _selectedPatternChangedCallback(emptyPattern);
            }

            labelNumberOfPatterns.Text = $"{SelectedRows.Length}/{dataGridView1.RowCount}";
        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show(e.Exception.Message);
        }

        private void FormPatternLibrary_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.PatternLibraryWindowSize = Size;
            Properties.Settings.Default.PatternLibraryWindowState = WindowState;

            Properties.Settings.Default.PatternLinbraryColumnNameWidth = nameDataGridViewTextBoxColumn.Width;
            Properties.Settings.Default.PatternLinbraryColumnDescriptionWidth = descriptionDataGridViewTextBoxColumn.Width;
            Properties.Settings.Default.PatternLinbraryColumnLengthWidth = lengthDataGridViewTextBoxColumn.Width;
            Properties.Settings.Default.PatternLinbraryColumnTempoWidth = tempoDataGridViewTextBoxColumn.Width;
            Properties.Settings.Default.PatternLinbraryColumnSourceWidth = sourceDataGridViewTextBoxColumn.Width;
        }
    }
}
