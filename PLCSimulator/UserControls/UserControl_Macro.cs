using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace PLCSimulator
{
    public partial class UserControl_Macro : UserControl
    {
        private List<MacroContext> m_macroContextList;

        public UserControl_Macro()
        {
            InitializeComponent();
        }

        private void UserControl_Macro_Load(object sender, EventArgs e)
        {
            try
            {
                m_macroContextList = PLCSimulator.Instance.MacroManager.MacroContextList;
                dataGridView_Macro.Columns[0].ValueType = typeof(MacroType);
                (dataGridView_Macro.Columns[0] as DataGridViewComboBoxColumn).DataSource = Enum.GetValues(typeof(MacroType));

                dataGridView_Macro.RowCount = m_macroContextList.Count;
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

        private void dataGridView_Macro_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0 || e.RowIndex >= m_macroContextList.Count)
                    return;

                if (e.ColumnIndex == 0)
                    e.Value = m_macroContextList[e.RowIndex].MacroType;
                else if (e.ColumnIndex == 1)
                    e.Value = m_macroContextList[e.RowIndex].Address;
                else if (e.ColumnIndex == 2)
                    e.Value = m_macroContextList[e.RowIndex].Value;
            }
            catch
            {
            }
        }

        private void dataGridView_Macro_CellValuePushed(object sender, DataGridViewCellValueEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0 || e.RowIndex >= m_macroContextList.Count)
                    return;

                var context = m_macroContextList[e.RowIndex];
                if (e.ColumnIndex == 0)
                {
                    if (context.MacroType != (MacroType)e.Value)
                    {
                        context.MacroType = (MacroType)e.Value;
                        context.Address = string.Empty;
                        context.Value = 0;
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
                        if (Util.IsDTAddress(sAddress, out int address))
                            context.Address = $"DT{address:D5}";
                        else if (Util.IsContactAddress(sAddress, out string contactCode, out address, out int hex))
                            context.Address = $"{contactCode}{address:D3}{hex:X}";
                    }
                }
                else if (e.ColumnIndex == 2)
                {
                    string sAddress = context.Address;
                    if (context.MacroType == MacroType.Delay)
                    {
                        short.TryParse(e.Value?.ToString(), out short value);
                        context.Value = (ushort)value;
                    }
                    else if (Util.IsDTAddress(sAddress, out _))
                    {
                        short.TryParse(e.Value?.ToString(), out short value);
                        context.Value = (ushort)value;
                    }
                    else if (Util.IsContactAddress(sAddress, out _, out _, out _))
                    {
                        int.TryParse(e.Value?.ToString(), out int value);
                        value = value > 0 ? 1 : 0;
                        context.Value = (ushort)value;
                    }
                }

                dataGridView_Macro.InvalidateRow(e.RowIndex);
            }
            catch
            {
            }
        }

        private void timer_gui_update_Tick(object sender, EventArgs e)
        {
            try
            {
                if (PLCSimulator.Instance.MacroManager.IsRunMacro())
                {
                    int step = PLCSimulator.Instance.MacroManager.MacroStep;
                    if (m_macroContextList.Count <= step)
                        return;
                    var context = m_macroContextList[step];
                    label_Step.Text = $"{context.MacroType} : {context.Address} To {context.Value}";

                    dataGridView_Macro.CurrentCell = dataGridView_Macro.Rows[step].Cells[2];
                }
                else
                {
                    label_Step.Text = "Step";
                }
            }
            catch
            {
            }
        }

        private void button_RunMacro_Click(object sender, EventArgs e)
        {
            if (!PLCSimulator.Instance.MacroManager.IsRunMacro())
            {
                PLCSimulator.Instance.MacroManager.RunMacro();
                button_RunMacro.Text = "Stop";
                dataGridView_Macro.ReadOnly = true;
            }
            else
            {
                PLCSimulator.Instance.MacroManager.StopMacro();
                button_RunMacro.Text = "Start";
                dataGridView_Macro.ReadOnly = false;
            }
        }

        private void button_AddMacro_Click(object sender, EventArgs e)
        {
            if (PLCSimulator.Instance.MacroManager.IsRunMacro())
                return;

            m_macroContextList.Add(new MacroContext());

            dataGridView_Macro.RowCount = m_macroContextList.Count;
            dataGridView_Macro.Invalidate();
        }

        private void button_RemoveMacro_Click(object sender, EventArgs e)
        {
            if (PLCSimulator.Instance.MacroManager.IsRunMacro())
                return;
            if (dataGridView_Macro.SelectedCells.Count == 0)
                return;

            int index = dataGridView_Macro.SelectedCells[0].RowIndex;
            m_macroContextList.RemoveAt(index);

            dataGridView_Macro.RowCount = m_macroContextList.Count;
            dataGridView_Macro.Invalidate();
        }
    }
}