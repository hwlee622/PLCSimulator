using System;
using System.Collections.Generic;
using System.Threading;

namespace PLCSimulator
{
    public class DataManager
    {
        #region Singleton

        public static DataManager Instance
        { get { return InstanceHolder.Instance; } }

        private static class InstanceHolder
        {
            public static DataManager Instance = new DataManager();
        }

        private DataManager()
        {
            PlcArea[DataCode] = new Data(MaxDataAreaAddress);
            PlcArea[RAreaCode] = new Data(MaxContactAddress);
            PlcArea[YAreaCode] = new Data(MaxContactAddress);
            PlcArea[XAreaCode] = new Data(MaxContactAddress);
        }

        #endregion Singleton

        public class Data
        {
            public Data(int length)
            {
                m_data = new ushort[length];
            }

            private ushort[] m_data;
            private ReaderWriterLockSlim m_lock = new ReaderWriterLockSlim();

            public ushort[] GetData(int index, int length)
            {
                m_lock.EnterReadLock();
                try
                {
                    length = Math.Min(length, m_data.Length - index);
                    var result = new ushort[length];
                    Array.Copy(m_data, index, result, 0, length);
                    return result;
                }
                finally
                {
                    m_lock.ExitReadLock();
                }
            }

            public void SetData(int index, ushort[] value)
            {
                m_lock.EnterWriteLock();
                try
                {
                    if (value != null)
                    {
                        int length = Math.Min(value.Length, m_data.Length - index);
                        Array.Copy(value, 0, m_data, index, length);
                    }
                }
                finally
                {
                    m_lock.ExitWriteLock();
                }
            }
        }

        public const int MaxDataAreaAddress = 50000;
        public const int MaxContactAddress = 1000;

        public const string DataCode = "Data";
        public const string RAreaCode = "R";
        public const string YAreaCode = "Y";
        public const string XAreaCode = "X";

        public Dictionary<string, Data> PlcArea = new Dictionary<string, Data>();
    }
}