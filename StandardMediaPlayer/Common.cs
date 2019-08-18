using SuperNESController.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StandardMediaPlayer
{
    public enum AppCommands
    {
        BrowserBack = 1,
        BrowserForward = 2,
        BrowserRefresh = 3,
        BrowserStop = 4,
        BrowserSearch = 5,
        BrowserFavorite = 6,
        BrowserHome = 7,
        VolumeMute = 8,
        VolumeDown = 9,
        VolumeUp = 10,
        MediaNext = 11,
        MediaPrevious = 12,
        MediaStop = 13,
        MediaPlayPause = 14,
        LaunchMail = 15,
        LaunchMediaSelect = 16,
        LaunchApp1 = 17,
        LaunchApp2 = 18,
        BassDown = 19,
        BassBoost = 20,
        BassUp = 21,
        TrebleUp = 22,
        TrebleDown = 23,
        MicrophoneMute = 24,
        MicrophoneVolumeUp = 25,
        MicrophoneVolumeDown = 26,
        Help = 27,
        Find = 28,
        New = 29,
        Open = 30,
        Close = 31,
        Save = 32,
        Print = 33,
        Undo = 34,
        Redo = 35,
        Copy = 36,
        Cut = 37,
        Paste = 38,
        ReplyToMail = 39,
        ForwardMail = 40,
        SendMail = 41,
        SpellCheck = 42,
        Dictate = 43,
        MicrophoneOnOff = 44,
        CorrectionList = 45,
        MediaPlay = 46,
        MediaPause = 47,
        MediaRecord = 48,
        MediaFastForward = 49,
        MediaRewind = 50,
        MediaChannelUp = 51,
        MediaChannelDown = 52,
        Delete = 53,
        Flip3D = 54,
    }

    public static class Common
    {
        private static Form form;
        private static readonly ManualResetEventSlim mre = new ManualResetEventSlim();
        private static bool isInitialized;

        private static void Initialize()
        {
            var thread = new Thread(() =>
            {
                form = new Form();
                IntPtr handle = form.Handle; // force handle creation
                mre.Set();
                Application.Run();
            });

            thread.SetApartmentState(ApartmentState.STA);
            thread.IsBackground = true;
            thread.Start();

            if (mre.Wait(3000) == false)
                return;

            isInitialized = true;
        }

        public static void SendAppCommand(AppCommands command)
        {
            if (isInitialized == false)
            {
                Initialize();

                if (isInitialized == false)
                    return;
            }

            form.BeginInvoke((Action)delegate
            {
                IntPtr handle = form.Handle;
                SendMessage(handle, WM_APPCOMMAN, handle, (int)command << 16);
            });
        }

        private const int WM_APPCOMMAN = 793;

        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, int lp);
    }
}
