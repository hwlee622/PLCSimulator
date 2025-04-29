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

    public class MacroContext
    {
        public MacroType MacroType = MacroType.Delay;
        public string Address = string.Empty;
        public ushort Value = 0;
    }

    public class MacroManager
    {
        public List<MacroContext> MacroContextList = ProfileRecipe.Instance.ProfileInfo.MacroContextList;
        public int MacroStep { get; private set; }
        private CancellationTokenSource m_cts = new CancellationTokenSource(0);

        public void RunMacro()
        {
            if (m_cts.IsCancellationRequested)
            {
                m_cts = new CancellationTokenSource();
                MacroStep = 0;
                Thread macroThread = new Thread(() => RunMacro(m_cts.Token));
                macroThread.IsBackground = true;
                macroThread.Start();
            }
        }

        public void StopMacro()
        {
            m_cts.Cancel();
        }

        public bool IsRunMacro()
        {
            return !m_cts.IsCancellationRequested;
        }

        public void RunMacro(CancellationToken token)
        {
            try
            {
                while (!token.IsCancellationRequested)
                {
                    Thread.Sleep(20);

                    if (MacroContextList.Count <= MacroStep)
                        break;

                    var context = MacroContextList[MacroStep];
                    if (RunMacroType(context))
                        MacroStep = (MacroStep + 1) % MacroContextList.Count;
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