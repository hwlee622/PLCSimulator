using System;
using System.Collections.Generic;
using System.Threading;

namespace PLCSimulator
{
    public enum MacroType
    {
        Delay,
        SetValue,
        WaitValue,
        Increase,
        Decrease,
    }

    public class MacroManager
    {
        public MacroManager(MacroInfo[] macroInfoArray)
        {
            MacroInfoArray = macroInfoArray;
            MacroStepArray = new int[MacroInfoArray.Length];
            m_ctsArray = new CancellationTokenSource[MacroInfoArray.Length];
            for (int i = 0; i < MacroInfoArray.Length; i++)
                m_ctsArray[i] = new CancellationTokenSource(0);
        }

        private MacroInfo[] MacroInfoArray;
        private int[] MacroStepArray;
        private CancellationTokenSource[] m_ctsArray;

        public void RunMacro(int index)
        {
            if (index < 0 || index >= MacroInfoArray.Length)
                return;

            if (m_ctsArray[index].IsCancellationRequested)
            {
                m_ctsArray[index] = new CancellationTokenSource();
                MacroStepArray[index] = 0;
                Thread macroThread = new Thread(() => RunMacro(m_ctsArray[index].Token, index));
                macroThread.IsBackground = true;
                macroThread.Start();
            }
        }

        public void StopMacro(int index)
        {
            if (index < 0 || index >= MacroInfoArray.Length)
                return;

            m_ctsArray[index].Cancel();
        }

        public bool IsRunMacro(int index)
        {
            if (index < 0 || index >= MacroInfoArray.Length)
                return false;

            return !m_ctsArray[index].IsCancellationRequested;
        }

        public int GetAllMacroLength()
        {
            return MacroInfoArray.Length;
        }

        public int GetMacroCount(int index)
        {
            if (index < 0 || index >= MacroInfoArray.Length)
                return 0;

            return MacroInfoArray[index].MacroContextList.Count;
        }

        public int GetMacroStep(int index)
        {
            if (index < 0 || index >= MacroInfoArray.Length)
                return 0;

            return MacroStepArray[index];
        }

        public MacroContext GetMacroContext(int index, int step)
        {
            if (index < 0 || index >= MacroInfoArray.Length)
                return null;
            if (step < 0 || step >= MacroInfoArray[index].MacroContextList.Count)
                return null;

            return MacroInfoArray[index].MacroContextList[step];
        }

        public void AddMacroContext(int index)
        {
            if (index < 0 || index >= MacroInfoArray.Length)
                return;

            MacroInfoArray[index].MacroContextList.Add(new MacroContext());
        }

        public void RemoveMacroContext(int index, int removeIndex)
        {
            if (index < 0 || index >= MacroInfoArray.Length)
                return;

            MacroInfoArray[index].MacroContextList.RemoveAt(removeIndex);
        }

        private void RunMacro(CancellationToken token, int index)
        {
            try
            {
                var macroContextList = MacroInfoArray[index].MacroContextList;
                while (!token.IsCancellationRequested)
                {
                    Thread.Sleep(20);
                    int step = MacroStepArray[index];

                    if (macroContextList.Count <= step)
                        break;

                    var context = macroContextList[step];
                    if (RunMacroType(context))
                        MacroStepArray[index] = (step + 1) % macroContextList.Count;
                }
            }
            catch (Exception ex)
            {
                LogWriter.Instance.LogError(ex);
            }
        }

        private bool RunMacroType(MacroContext context)
        {
            switch (context.MacroType)
            {
                case MacroType.Delay:
                    return RunDelay(context);

                case MacroType.SetValue:
                    return RunSetValue(context);

                case MacroType.WaitValue:
                    return RunWaitValue(context);

                case MacroType.Increase:
                    return RunIncrease(context);

                case MacroType.Decrease:
                    return RunDecrease(context);

                default:
                    return false;
            }
        }

        private bool RunDelay(MacroContext context)
        {
            Thread.Sleep(context.Value);
            return true;
        }

        private bool RunSetValue(MacroContext context)
        {
            if (Util.IsDTAddress(context.Address, out int address))
            {
                if (!DataManager.Instance.PlcArea.TryGetValue(DataManager.DataCode, out var dataArea))
                    return false;

                ushort[] value = new ushort[1] { context.Value };
                dataArea.SetData(address, value);
                return true;
            }
            else if (Util.IsContactAddress(context.Address, out string contactCode, out address, out int hex))
            {
                if (!DataManager.Instance.PlcArea.TryGetValue(contactCode, out var contactArea))
                    return false;

                int newValue = context.Value > 0 ? 1 : 0;
                int mask = 1 << hex;
                var singleData = contactArea.GetData(address, 1);
                singleData[0] = newValue > 0 ? (ushort)(singleData[0] | mask) : (ushort)(singleData[0] & ~mask);
                contactArea.SetData(address, singleData);
                return true;
            }
            else
                return false;
        }

        private bool RunWaitValue(MacroContext context)
        {
            if (Util.IsDTAddress(context.Address, out int address))
            {
                if (!DataManager.Instance.PlcArea.TryGetValue(DataManager.DataCode, out var dataArea))
                    return false;

                var data = dataArea.GetData(address, 1)[0];
                if (data == context.Value)
                    return true;
                else
                    return false;
            }
            else if (Util.IsContactAddress(context.Address, out string contactCode, out address, out int hex))
            {
                if (!DataManager.Instance.PlcArea.TryGetValue(contactCode, out var contactArea))
                    return false;

                var dataBlock = contactArea.GetData(address, 1)[0];
                var data = (dataBlock >> hex) & 1;
                if (data == context.Value)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        private bool RunIncrease(MacroContext context)
        {
            if (Util.IsDTAddress(context.Address, out int address))
            {
                if (!DataManager.Instance.PlcArea.TryGetValue(DataManager.DataCode, out var dataArea))
                    return false;

                ushort[] value = dataArea.GetData(address, 1);
                value[0] = (ushort)(value[0] + context.Value);
                dataArea.SetData(address, value);
                return true;
            }
            else if (Util.IsContactAddress(context.Address, out string contactCode, out address, out int hex))
            {
                if (!DataManager.Instance.PlcArea.TryGetValue(contactCode, out var contactArea))
                    return false;

                int mask = 1 << hex;
                var singleData = contactArea.GetData(address, 1);
                int newValue = singleData[0] << hex > 0 ? 1 : 0;
                singleData[0] = newValue > 0 ? (ushort)(singleData[0] | mask) : (ushort)(singleData[0] & ~mask);
                contactArea.SetData(address, singleData);
                return true;
            }
            else
                return false;
        }

        private bool RunDecrease(MacroContext context)
        {
            if (Util.IsDTAddress(context.Address, out int address))
            {
                if (!DataManager.Instance.PlcArea.TryGetValue(DataManager.DataCode, out var dataArea))
                    return false;

                ushort[] value = dataArea.GetData(address, 1);
                value[0] = (ushort)(value[0] - context.Value);
                dataArea.SetData(address, value);
                return true;
            }
            else if (Util.IsContactAddress(context.Address, out string contactCode, out address, out int hex))
            {
                if (!DataManager.Instance.PlcArea.TryGetValue(contactCode, out var contactArea))
                    return false;

                int mask = 1 << hex;
                var singleData = contactArea.GetData(address, 1);
                int newValue = singleData[0] << hex > 0 ? 1 : 0;
                singleData[0] = newValue > 0 ? (ushort)(singleData[0] | mask) : (ushort)(singleData[0] & ~mask);
                contactArea.SetData(address, singleData);
                return true;
            }
            else
                return false;
        }
    }
}