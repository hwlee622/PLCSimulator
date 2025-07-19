using System;
using System.Windows.Forms;

namespace PLCSimulator
{
    public partial class UserControl_Macro : UserControl
    {
        private MacroManager m_macroManager;
        private int m_selectedIndex = 0;

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

        private void comboBox_Macro_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ComboBox cb = sender as ComboBox;
                if (cb == null)
                    return;

                m_selectedIndex = cb.SelectedIndex;
                dataGridView_Macro.RowCount = m_macroManager.GetMacroCount(m_selectedIndex);
                dataGridView_Macro.Invalidate();
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
                bool isRunning = m_macroManager.IsRunMacro(m_selectedIndex);
                if (isRunning)
                {
                    button_RunMacro.Text = "Stop";
                    dataGridView_Macro.ReadOnly = true;

                    int step = m_macroManager.GetMacroStep(m_selectedIndex);
                    var context = m_macroManager.GetMacroContext(m_selectedIndex, step);
                    if (m_macroManager.GetMacroCount(m_selectedIndex) <= step || context == null)
                        return;

                    label_Step.Text = $"{context.MacroType} : {context.Address} To {context.Value}";

                    if (step < dataGridView_Macro.Rows.Count)
                        dataGridView_Macro.CurrentCell = dataGridView_Macro.Rows[step].Cells[2];
                }
                else
                {
                    button_RunMacro.Text = "Start";
                    dataGridView_Macro.ReadOnly = false;

                    label_Step.Text = "Step";
                }
            }
            catch
            {
            }
        }

        private void dataGridView_Macro_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
        {
            try
            {
                var context = m_macroManager.GetMacroContext(m_selectedIndex, e.RowIndex);
                if (e.RowIndex < 0 || e.RowIndex >= m_macroManager.GetMacroCount(m_selectedIndex) || context == null)
                    return;

                if (e.ColumnIndex == 0)
                    e.Value = context.MacroType;
                else if (e.ColumnIndex == 1)
                    e.Value = context.Address;
                else if (e.ColumnIndex == 2)
                    e.Value = context.Value;
            }
            catch
            {
            }
        }

        private void dataGridView_Macro_CellValuePushed(object sender, DataGridViewCellValueEventArgs e)
        {
            try
            {
                var context = m_macroManager.GetMacroContext(m_selectedIndex, e.RowIndex);
                if (e.RowIndex < 0 || e.RowIndex >= m_macroManager.GetMacroCount(m_selectedIndex) || context == null)
                    return;

                if (e.ColumnIndex == 0)
                {
                    if (context.MacroType != (MacroType)e.Value)
                    {
                        context.MacroType = (MacroType)e.Value;
                        context.Address = string.Empty;
                        context.Value = "0";
                    }
                }
                else if (e.ColumnIndex == 1)
                {
                    if (context.MacroType == MacroType.Delay)
                    {
                        context.Address = string.Empty;
                    }
                    else
                    {
                        string sAddress = e.Value?.ToString().ToUpper();
                        if (Util.ValidWordAddress(sAddress, out string code, out int index))
                        {
                            sAddress = DataManager.Instance.WordDataDict[code].GetAddress(index);
                            context.Address = $"{code}{sAddress}";
                            context.Value = "0";
                        }
                        else if (Util.ValidBitAddress(sAddress, out code, out index))
                        {
                            sAddress = DataManager.Instance.BitDataDict[code].GetAddress(index);
                            context.Address = $"{code}{sAddress}";
                            context.Value = "0";
                        }
                    }
                }
                else if (e.ColumnIndex == 2)
                {
                    string sAddress = context.Address;
                    if (context.MacroType == MacroType.Delay)
                    {
                        int.TryParse(e.Value?.ToString(), out var value);
                        context.Value = value.ToString();
                    }
                    else if (Util.ValidWordAddress(sAddress, out _, out _))
                    {
                        short.TryParse(e.Value?.ToString(), out var value);
                        context.Value = value.ToString();
                    }
                    else if (Util.ValidBitAddress(sAddress, out _, out _))
                    {
                        int.TryParse(e.Value?.ToString(), out var value);
                        value = value > 0 ? 1 : 0;
                        context.Value = value.ToString();
                    }
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
                if (!m_macroManager.IsRunMacro(m_selectedIndex))
                    m_macroManager.RunMacro(m_selectedIndex);
                else
                    m_macroManager.StopMacro(m_selectedIndex);
            }
            catch
            {
            }
        }

        private void button_AddMacro_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_macroManager.IsRunMacro(m_selectedIndex))
                    return;

                int index = 0;
                if (dataGridView_Macro.SelectedCells.Count > 0)
                    index = dataGridView_Macro.SelectedCells[0].RowIndex;

                m_macroManager.AddMacroContext(m_selectedIndex, index);

                dataGridView_Macro.RowCount = m_macroManager.GetMacroCount(m_selectedIndex);
                dataGridView_Macro.Invalidate();
            }
            catch
            {
            }
        }

        private void button_RemoveMacro_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_macroManager.IsRunMacro(m_selectedIndex))
                    return;
                if (dataGridView_Macro.SelectedCells.Count == 0)
                    return;

                int removeIndex = dataGridView_Macro.SelectedCells[0].RowIndex;
                m_macroManager.RemoveMacroContext(m_selectedIndex, removeIndex);

                dataGridView_Macro.RowCount = m_macroManager.GetMacroCount(m_selectedIndex);
                dataGridView_Macro.Invalidate();
            }
            catch
            {
            }
        }
    }
}