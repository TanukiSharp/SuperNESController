using HIDLibrary.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using SuperNESController.Core;

namespace SuperNESController
{
    public partial class frmMain : Form
    {
        private DeviceInfo[] devices;
        private IMessager messager;

        private ModuleManager moduleManager;
        private string moduleManagerLog;
        private frmModules frmModules;

        public frmMain()
        {
            InitializeComponent();

            mnuClose.Click += mnuClose_Click;
            mnuAbout.Click += mnuAbout_Click;
        }

        protected override void OnLoad(EventArgs e)
        {
 	        base.OnLoad(e);

            string path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            path = Path.Combine(path, "modules");

            messager = new Messager(notifyIcon1);

            if (Directory.Exists(path) == false)
                Directory.CreateDirectory(path);
            else
            {
                moduleManager = new ModuleManager();

                var stringWriter = new StringWriter();

                moduleManager.Initialize(Directory.GetFiles(path, "*.dll", SearchOption.TopDirectoryOnly), stringWriter);
                moduleManagerLog = stringWriter.ToString();

                var extensionsMenu = new ToolStripMenuItem("Extensions");
                extensionsMenu.Click += (ss, ee) =>
                {
                    if (frmModules != null)
                        return;

                    frmModules = new frmModules(moduleManagerLog, moduleManager);
                    frmModules.ShowDialog(this);
                    frmModules = null;

                    moduleManager.SaveConfiguration();
                };

                ctxNotificationMenu.Items.Insert(0, new ToolStripSeparator());
                ctxNotificationMenu.Items.Insert(0, extensionsMenu);
            }

            Visible = false;

            devices = HID.GetDevices(0x0583, 0x2060).ToArray();

            if (devices.Length == 0)
            {
                MessageBox.Show("Impossible to detect the controller device.", "Device Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
                return;
            }

            if (devices.All(d => d.Start()) == false)
            {
                MessageBox.Show("Impossible to start the controller device.", "Device Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
                return;
            }

            RunDevices();
        }

        private void OnButtonTriggered(Buttons button)
        {
            if (moduleManager == null)
                return;

            IEnumerable<IExtension> exts = moduleManager.Modules
                .SelectMany(x => x.Extensions)
                .Where(x => (button & x.ButtonsMask) != 0)
                .Select(x => x.Extension);

            foreach (IExtension ext in exts)
            {
                try
                {
                    ext.Execute(messager);
                }
                catch
                {
                }
            }
        }

        #region Processing code

        private Buttons previousButtons;

        private async void RunDevices()
        {
            var buffer = new byte[devices[0].InputReportLength];

            while (true)
            {
                Buttons buttons = Buttons.None;

                foreach (DeviceInfo device in devices)
                {
                    int len = -1;

                    try
                    {
                        len = await device.Stream.ReadAsync(buffer, 0, buffer.Length);
                    }
                    catch (IOException)
                    {
                        devices = devices.Except(new[] { device }).ToArray();

                        if (devices.Length == 0)
                        {
                            MessageBox.Show("Connection with devices lost, exiting.", "Device Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Application.Exit();
                            return;
                        }
                    }

                    if (len == buffer.Length)
                        SetButtonFlags(buffer, ref buttons);
                }

                if (buttons != Buttons.None)
                    OnButtonChanged(previousButtons, buttons);

                previousButtons = buttons;

                await Task.Delay(10);
            }
        }

        private void OnButtonChanged(Buttons previousButtons, Buttons buttons)
        {
            CheckButtonTriggered(previousButtons, buttons, Buttons.Up);
            CheckButtonTriggered(previousButtons, buttons, Buttons.Down);
            CheckButtonTriggered(previousButtons, buttons, Buttons.Right);
            CheckButtonTriggered(previousButtons, buttons, Buttons.Left);

            CheckButtonTriggered(previousButtons, buttons, Buttons.Select);
            CheckButtonTriggered(previousButtons, buttons, Buttons.Start);

            CheckButtonTriggered(previousButtons, buttons, Buttons.Y);
            CheckButtonTriggered(previousButtons, buttons, Buttons.B);
            CheckButtonTriggered(previousButtons, buttons, Buttons.X);
            CheckButtonTriggered(previousButtons, buttons, Buttons.A);

            CheckButtonTriggered(previousButtons, buttons, Buttons.L);
            CheckButtonTriggered(previousButtons, buttons, Buttons.R);

            CheckButtonTriggered(previousButtons, buttons, Buttons.Turbo);
            CheckButtonTriggered(previousButtons, buttons, Buttons.Clear);
        }

        private void CheckButtonTriggered(Buttons oldButtons, Buttons newButtons, Buttons btn)
        {
            if ((oldButtons & btn) == 0 && (newButtons & btn) != 0)
            {
                if (frmModules != null)
                    frmModules.ProcessButtons(btn);
                else
                    OnButtonTriggered(btn);
            }
        }

        private static void SetButtonFlags(byte[] buffer, ref Buttons buttons)
        {
            if (buffer[1] == 0)
                buttons |= Buttons.Left;
            else if (buffer[1] == 0xFF)
                buttons |= Buttons.Right;

            if (buffer[2] == 0)
                buttons |= Buttons.Up;
            else if (buffer[2] == 0xFF)
                buttons |= Buttons.Down;

            if ((buffer[3] & 1) != 0)
                buttons |= Buttons.A;
            if ((buffer[3] & 2) != 0)
                buttons |= Buttons.B;
            if ((buffer[3] & 4) != 0)
                buttons |= Buttons.X;
            if ((buffer[3] & 8) != 0)
                buttons |= Buttons.Y;
            if ((buffer[3] & 16) != 0)
                buttons |= Buttons.L;
            if ((buffer[3] & 32) != 0)
                buttons |= Buttons.R;
            if ((buffer[3] & 64) != 0)
                buttons |= Buttons.Select;
            if ((buffer[3] & 128) != 0)
                buttons |= Buttons.Start;

            if ((buffer[4] & 16) != 0)
                buttons |= Buttons.Turbo;
            if ((buffer[4] & 32) != 0)
                buttons |= Buttons.Clear;
        }

        #endregion // Processing code

        private void mnuAbout_Click(object sender, EventArgs e)
        {
            var asm = Assembly.GetEntryAssembly();
            var info = FileVersionInfo.GetVersionInfo(asm.Location);

            var sb = new StringBuilder();

            sb.AppendLine(info.ProductName);
            sb.AppendLine($"Version {asm.GetName().Version}");
            sb.AppendLine();
            sb.AppendLine(info.LegalCopyright);

            MessageBox.Show(sb.ToString(), $"About {info.ProductName}", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void mnuClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }

    [Flags]
    public enum Buttons
    {
        None = 0,

        Up = 0x1,
        Down = 0x2,
        Right = 0x4,
        Left = 0x8,

        Select = 0x10,
        Start = 0x20,

        Y = 0x40,
        B = 0x80,
        X = 0x100,
        A = 0x200,

        L = 0x400,
        R = 0x800,

        Turbo = 0x1000,
        Clear = 0x2000,
    }
}
