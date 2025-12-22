namespace PLCSimulator
{
    public class AppInstance
    {
        #region Singleton

        public static AppInstance Instance
        { get { return InstanceHolder.Instance; } }

        private static class InstanceHolder
        {
            public static AppInstance Instance = new AppInstance();
        }

        private AppInstance()
        { }

        #endregion Singleton

        public MacroManager MacroManager;
        public SyncManager SyncManager;

        private MewtocolServer m_panasonicServer;
        private UpperLinkServer m_omronServer;
        public ModbusSimulatorServer ModbusServer;

        public void Start()
        {
            var maxBitData = ProfileRecipe.Instance.ProfileInfo.MaxBitData;
            var maxWordData = ProfileRecipe.Instance.ProfileInfo.MaxWordData;
            MacroManager = new MacroManager(DataManager.Instance, ProfileRecipe.Instance.ProfileInfo.MacroInfoArray);
            SyncManager = SyncManager.Instance;

            Protocol selectedProtocol = ProfileRecipe.Instance.ProfileInfo.Protocol;
            int serverPort = ProfileRecipe.Instance.ProfileInfo.Port;
            string serverPortName = ProfileRecipe.Instance.ProfileInfo.PortName;

            switch (selectedProtocol)
            {
                case Protocol.MewtocolUdp:
                    m_panasonicServer = new MewtocolServer(serverPort, maxBitData, maxWordData);
                    m_panasonicServer.Start();
                    break;

                case Protocol.MewtocolSerial:
                    m_panasonicServer = new MewtocolServer(serverPortName, maxBitData, maxWordData);
                    m_panasonicServer.Start();
                    break;

                case Protocol.UpperLinkUdp:
                    m_omronServer = new UpperLinkServer(serverPort, maxBitData, maxWordData);
                    m_omronServer.Start();
                    break;

                case Protocol.ModbusTcp:
                case Protocol.ModbusUdp:
                    ModbusServer = new ModbusSimulatorServer(serverPort, selectedProtocol == Protocol.ModbusUdp, maxBitData, maxWordData);
                    break;

                default:
                    break;
            }
        }

        public void Stop()
        {
            if (MacroManager != null)
                for (int i = 0; i < MacroManager.GetAllMacroLength(); i++)
                    MacroManager.GetMacro(i).Stop();

            m_panasonicServer?.Stop();
            m_omronServer?.Stop();
        }
    }
}