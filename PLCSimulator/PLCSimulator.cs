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

        private MewtocolServer m_panasonicServer;
        private UpperLinkServer m_omronServer;

        public void Start()
        {
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
                default:
                    break;
            }
        }

        public void Stop()
        {
            m_panasonicServer?.Stop();
            m_omronServer?.Stop();
        }
    }
}