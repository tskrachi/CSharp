using System;

using System.Runtime.InteropServices;

namespace ClientInfo
{
    public static class NativeMethods
    {
        public enum WTS_INFO_CLASS
        {
            WTSInitialProgram,
            WTSApplicationName,
            WTSWorkingDirectory,
            WTSOEMId,
            WTSSessionId,
            WTSUserName,
            WTSWinStationName,
            WTSDomainName,
            WTSConnectState,
            WTSClientBuildNumber,
            WTSClientName,
            WTSClientDirectory,
            WTSClientProductId,
            WTSClientHardwareId,
            WTSClientAddress,
            WTSClientDisplay,
            WTSClientProtocolType,
            WTSIdleTime,
            WTSLogonTime,
            WTSIncomingBytes,
            WTSOutgoingBytes,
            WTSIncomingFrames,
            WTSOutgoingFrames,
            WTSClientInfo,
            WTSSessionInfo,
            WTSSessionInfoEx,
            WTSConfigInfo,
            WTSValidationInfo,
            WTSSessionAddressV4,
            WTSIsRemoteSession
        };
        public const int CLIENTNAME_LENGTH = 1;
        [StructLayout(LayoutKind.Sequential)]
        public struct WTSCLIENT
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 21)]    // CLIENTNAME_LENGTH + 1
            public byte[] ClientName;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 18)]    // DOMAIN_LENGTH + 1
            public byte[] Domain;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 21)]    // USERNAME_LENGTH + 1
            public byte[] UserName;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 261)]    // MAX_PATH + 1
            public byte[] WorkDirectory;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 261)]    // MAX_PATH + 1
            public byte[] InitialProgram;
            public byte EncryptionLevel;       // security level of encryption pd
            public uint ClientAddressFamily;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 31)]    // CLIENTADDRESS_LENGTH + 1
            public ushort[] ClientAddress;
            public ushort HRes;
            public ushort VRes;
            public ushort ColorDepth;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 261)]    // MAX_PATH + 1
            public byte[] ClientDirectory;
            public uint ClientBuildNumber;
            public uint ClientHardwareId;    // client software serial number
            public ushort ClientProductId;     // client software product id
            public ushort OutBufCountHost;     // number of outbufs on host
            public ushort OutBufCountClient;   // number of outbufs on client
            public ushort OutBufLength;        // length of outbufs in bytes
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 261)]    // MAX_PATH + 1
            public byte[] DeviceId;
        }
        [DllImport("Wtsapi32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool WTSQuerySessionInformation(
            IntPtr hServer,                 // [in]
            uint SessionId,                 // [in]
            WTS_INFO_CLASS WTSInfoClass,    // [in]
            //[MarshalAs(UnmanagedType.LPStr)] out string ppBuffer,
            out IntPtr ppBuffer,                // [out]
            out uint pBytesReturned
            );
        [DllImport("Wtsapi32.dll")]
        public static extern void WTSFreeMemory(
            IntPtr pMemory
            );
    }
    class Program
    {


        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            IntPtr ppBuffer;
            uint dwSize;

            bool ret = NativeMethods.WTSQuerySessionInformation(
                IntPtr.Zero,
                uint.MaxValue,
                NativeMethods.WTS_INFO_CLASS.WTSDomainName,
                out ppBuffer,
                out dwSize
                );
            if (ret)
            {
                if (dwSize > 0)
                {
                    string domainname = Marshal.PtrToStringAnsi(ppBuffer);
                    Console.WriteLine(domainname);
                }
                NativeMethods.WTSFreeMemory(ppBuffer);
            }

            ret = NativeMethods.WTSQuerySessionInformation(
                IntPtr.Zero,
                uint.MaxValue,
                NativeMethods.WTS_INFO_CLASS.WTSClientInfo,
                out ppBuffer,
                out dwSize
                );
            if (ret)
            {
                if (dwSize > 0)
                {
                    NativeMethods.WTSCLIENT cInfo = (NativeMethods.WTSCLIENT)Marshal.PtrToStructure(ppBuffer, typeof(NativeMethods.WTSCLIENT));
                    Console.WriteLine(cInfo.ClientAddress);
                }
                NativeMethods.WTSFreeMemory(ppBuffer);
            }

        }
    }
}
