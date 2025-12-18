using System;
using System.Windows.Forms;

namespace PLCSimulator
{
    public partial class UserControl_Macro : UserControl
    {
        private MacroManager m_macroManager;
        private Macro m_selectedMacro;

        public UserControl_Macro(MacroManager macroManager)
        {
            InitializeComponent();

            m_macroManager = macroManager;
            comboBox_Macro.Items.Clear();
            for (int i = 0; i < m_macroManager.GetAllMacroLength(); i++)
                comboBox_Macro.Items.Add($"Macro{i}");

            dataGridView_Macro.Columns[0].ValueType = typeof(MacroType);
            (dataGridView_Macro.Columns[0] as DataGridViewComboBoxColumn).DataSource = Enum.GetValues(typeof(MacroType));
        }

        private void UserControl_Macro_Load(object sender, EventArgs e)
        {
            try
            {
                comboBox_Macro.SelectedIndex = 0;
            }
            catch
            {
            }
        }

        private void InvalidateGridView()
        {
            dataGridView_Macro.RowCount = m_selectedMacro.GetMacroContextList().Count;
            dataGridView_Macro.Invalidate();
        }

        private void comboBox_Macro_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ComboBox cb = sender as ComboBox;
                if (cb == null)
                    return;

                m_selectedMacro = m_macroManager.GetMacro(cb.SelectedIndex);
                InvalidateGridView();
            }
            catch
            {
            }
        }

        private void UserControl_Macro_VisibleChanged(object sender, EventArgs e)
        {
            try
            {
                timer_gui_update.Enabled = Visible;
            }
            catch
            {
            }
        }

        private void timer_gui_update_Tick(object sender, EventArgs e)
        {
            try
            {
                var isRunning = m_selectedMacro.IsRun();
                var contextList = m_selectedMacro.GetMacroContextList();
                var context = contextList[m_selectedMacro.MacroStep];

                button_RunMacro.Text = isRunning ? "Stop" : "Start";
                dataGridView_Macro.ReadOnly = isRunning;
                label_Step.Text = isRunning ? $"{context.MacroType} : {context.Address} To {context.Value}" : "Step";

                if (isRunning && m_selectedMacro.MacroStep < dataGridView_Macro.Rows.Count)
                    dataGridView_Macro.CurrentCell = dataGridView_Macro.Rows[m_selectedMacro.MacroStep].Cells[2];
            }
            catch
            {
            }
        }

        private void dataGridView_Macro_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
        {
            try
            {
                var contextList = m_selectedMacro.GetMacroContextList();
                var rowIndex = e.RowIndex;
                var columnIndex = e.ColumnIndex;
                if (contextList == null || contextList.Count <= rowIndex || rowIndex < 0)
                    return;

                if (columnIndex == 0)
                    e.Value = contextList[rowIndex].MacroType;
                else if (columnIndex == 1)
                    e.Value = contextList[rowIndex].Address;
                else if (columnIndex == 2)
                    e.Value = contextList[rowIndex].Value;
            }
            catch
            {
            }
        }

        private void dataGridView_Macro_CellValuePushed(object sender, DataGridViewCellValueEventArgs e)
        {
            try
            {
                var contextList = m_selectedMacro.GetMacroContextList();
                var rowIndex = e.RowIndex;
                var columnIndex = e.ColumnIndex;
                if (contextList == null || contextList.Count <= rowIndex || rowIndex < 0)
                    return;

                if (e.ColumnIndex == 0)
                {
                    // Change Macro Type, reset Address and Value
                    if (contextList[rowIndex].MacroType != (MacroType)e.Value)
                    {
                        contextList[rowIndex].MacroType = (MacroType)e.Value;
                        contextList[rowIndex].Address = string.Empty;
                        contextList[rowIndex].Value = "0";
                    }
                }
                else if (e.ColumnIndex == 1)
                {
                    if (contextList[rowIndex].MacroType == MacroType.Delay)
                    {
                        // For Delay, clear Address
                        contextList[rowIndex].Address = string.Empty;
                    }
                    else
                    {
                        // Validate and set Address
                        var sAddress = e.Value?.ToString().ToUpper();
                        if (Util.ValidWordAddress(sAddress, out var code, out var index))
                        {
                            sAddress = DataManager.Instance.WordDataDict[code].GetAddress(index);
                            contextList[rowIndex].Address = $"{code}{sAddress}";
                            contextList[rowIndex].Value = "0";
                        }
                        else if (Util.ValidBitAddress(sAddress, out code, out index))
                        {
                            sAddress = DataManager.Instance.BitDataDict[code].GetAddress(index);
                            contextList[rowIndex].Address = $"{code}{sAddress}";
                            contextList[rowIndex].Value = "0";
                        }
                    }
                }
                else if (e.ColumnIndex == 2)
                {
                    var sAddress = contextList[rowIndex].Address;
                    var sValue = "0";
                    if (contextList[rowIndex].MacroType == MacroType.Delay)
                    {
                        int.TryParse(e.Value?.ToString(), out var value);
                        sValue = value.ToString();
                    }
                    else if (contextList[rowIndex].MacroType == MacroType.CopyValue)
                    {
                        if (Util.ValidWordAddress(sAddress, out _, out _) && Util.ValidWordAddress(e.Value?.ToString(), out var code, out var index))
                            sValue = $"{code}{DataManager.Instance.WordDataDict[code].GetAddress(index)}";
                        else if (Util.ValidBitAddress(sAddress, out _, out _) && Util.ValidBitAddress(e.Value?.ToString(), out code, out index))
                            sValue = $"{code}{DataManager.Instance.BitDataDict[code].GetAddress(index)}";
                    }
                    else if (Util.ValidWordAddress(sAddress, out _, out _))
                    {
                        short.TryParse(e.Value?.ToString(), out var value);
                        sValue = value.ToString();
                    }
                    else if (Util.ValidBitAddress(sAddress, out _, out _))
                    {
                        int.TryParse(e.Value?.ToString(), out var value);
                        value = value > 0 ? 1 : 0;
                        sValue = value.ToString();
                    }
                    contextList[rowIndex].Value = sValue;
                }

                dataGridView_Macro.InvalidateRow(e.RowIndex);
            }
            catch
            {
            }
        }

        private void button_RunMacro_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_selectedMacro.IsRun() == true)
                    m_selectedMacro.Stop();
                else
                    m_selectedMacro.Run();
            }
            catch
            {
            }
        }

        private void button_AddMacro_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_selectedMacro.IsRun() == true)
                    return;

                var index = 0;
                if (dataGridView_Macro.SelectedCells.Count > 0)
                    index = dataGridView_Macro.SelectedCells[0].RowIndex;

                m_selectedMacro.AddMacroContext(index);

                InvalidateGridView();
            }
            catch
            {
            }
        }

        private void button_RemoveMacro_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_selectedMacro.IsRun() == true)
                    return;
                if (dataGridView_Macro.SelectedCells.Count == 0)
                    return;

                int index = dataGridView_Macro.SelectedCells[0].RowIndex;
                m_selectedMacro.RemoveMacroContext(index);

                InvalidateGridView();
            }
            catch
            {
            }
        }
    }
}