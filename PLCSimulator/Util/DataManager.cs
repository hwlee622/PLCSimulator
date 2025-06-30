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
        { }

        #endregion Singleton

        #region BitData

        /// <summary>
        /// 1bit 데이터를 관리하는 클래스
        /// </summary>
        public class BitData
        {
            public BitData(int length, bool hexAddress = false)
            {
                _data = new bool[length];
                _hexAddress = hexAddress;
            }

            private bool[] _data;
            private bool _hexAddress;
            private ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();

            public int DataLength { get { return _data.Length; } }

            public bool ValidateAddress(string text, out int index)
            {
                index = -1;
                if (_hexAddress)
                {
                    if (int.TryParse(text.Substring(0, text.Length - 1), out int address) && TryParseHexToInt(text.Substring(text.Length - 1, 1), out int hex))
                    {
                        index = address * 16 + hex;
                        return true;
                    }
                    return false;
                }
                else
                {
                    return int.TryParse(text, out index);
                }
            }

            public string GetAddress(int index)
            {
                if (_hexAddress)
                {
                    return $"{index / 16:D3}{index % 16:X}";
                }
                else
                    return $"{index:D5}";
            }

            public bool[] GetData(int index, int length)
            {
                _lock.EnterReadLock();
                try
                {
                    var data = new bool[length];
                    if (index >= _data.Length)
                        return data;

                    length = Math.Min(length, _data.Length - index);
                    Array.Copy(_data, index, data, 0, length);
                    return data;
                }
                finally
                {
                    _lock.ExitReadLock();
                }
            }

            public void SetData(int index, bool[] data)
            {
                _lock.EnterWriteLock();
                try
                {
                    if (data == null || index >= _data.Length)
                        return;

                    int length = Math.Min(data.Length, _data.Length - index);
                    Array.Copy(data, 0, _data, index, length);
                }
                finally
                {
                    _lock.ExitWriteLock();
                }
            }

            public void ClearData()
            {
                _lock.EnterWriteLock();
                try
                {
                    _data = new bool[_data.Length];
                }
                finally
                {
                    _lock.ExitWriteLock();
                }
            }
        }

        #endregion BitData

        #region WordData

        /// <summary>
        /// 2byte 데이터를 관리하는 클래스
        /// </summary>
        public class WordData
        {
            public WordData(int length)
            {
                _data = new ushort[length];
            }

            private ushort[] _data;
            private ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();

            public int DataLength { get { return _data.Length; } }

            public bool ValidateAddress(string text, out int index)
            {
                return int.TryParse(text, out index);
            }

            public string GetAddress(int index)
            {
                return index.ToString();
            }

            public ushort[] GetData(int index, int length)
            {
                _lock.EnterReadLock();
                try
                {
                    var data = new ushort[length];
                    if (index >= _data.Length)
                        return data;

                    length = Math.Min(length, _data.Length - index);
                    Array.Copy(_data, index, data, 0, length);
                    return data;
                }
                finally
                {
                    _lock.ExitReadLock();
                }
            }

            public void SetData(int index, ushort[] data)
            {
                _lock.EnterWriteLock();
                try
                {
                    if (data == null || index >= _data.Length)
                        return;

                    int length = Math.Min(data.Length, _data.Length - index);
                    Array.Copy(data, 0, _data, index, length);
                }
                finally
                {
                    _lock.ExitWriteLock();
                }
            }

            public void ClearData()
            {
                _lock.EnterWriteLock();
                try
                {
                    _data = new ushort[_data.Length];
                }
                finally
                {
                    _lock.ExitWriteLock();
                }
            }
        }

        #endregion WordData

        private static bool TryParseHexToInt(string hex, out int value)
        {
            value = 0;
            try
            {
                value = Convert.ToInt16(hex, 16);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public Dictionary<string, BitData> BitDataDict { get; private set; } = new Dictionary<string, BitData>();

        public Dictionary<string, WordData> WordDataDict { get; private set; } = new Dictionary<string, WordData>();
    }
}