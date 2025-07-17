namespace PLCSimulator
{
    public class PLCSimulator
    {
        #region Singleton

        public static PLCSimulator Instance
        { get { return InstanceHolder.Instance; } }

        private static class InstanceHolder
        {
            public static PLCSimulator Instance = new PLCSimulator();
        }

        private PLCSimulator()
        { }

        #endregion Singleton

        public MacroManager MacroManager;
        public SyncManager SyncManager;

        private MewtocolServer m_panasonicServer;
        private UpperLinkServer m_omronServer;
        public ModbusSimulatorServer ModbusServer;

        public void Start()
        {
            MacroManager = new MacroManager(ProfileRecipe.Instance.ProfileInfo.MacroInfoArray);
            SyncManager = SyncManager.Instance;
            SyncManager.ReConnect();

            Protocol selectedProtocol = ProfileRecipe.Instance.ProfileInfo.Protocol;
            int serverPort = ProfileRecipe.Instance.ProfileInfo.Port;

            switch (selectedProtocol)
            {
                case Protocol.Mewtocol:
                    m_panasonicServer = new MewtocolServer(serverPort);
                    m_panasonicServer.Start();
                    break;

                case Protocol.UpperLink:
                    m_omronServer = new UpperLinkServer(serverPort);
                    m_omronServer.Start();
                    break;

                case Protocol.ModbusTcp:
                case Protocol.ModbusUdp:
                    ModbusServer = new ModbusSimulatorServer(serverPort, selectedProtocol == Protocol.ModbusUdp);
                    break;

                default:
                    break;
            }
        }

        public void Stop()
        {
            if (MacroManager != null)
                for (int i = 0; i < MacroManager.GetAllMacroLength(); i++)
                    MacroManager.StopMacro(i);

            m_panasonicServer?.Stop();
            m_omronServer?.Stop();
        }
    }
}