using SuperNESController.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StandardMediaPlayer
{
    public abstract class ExtensionBase : IExtension
    {
        private readonly AppCommands command;

        public ExtensionBase(string name, int version, AppCommands command)
        {
            Name = name;
            Version = version;

            this.command = command;
        }

        public string Name { get; }
        public int Version { get; }

        public void Execute(IMessager messager)
        {
            Common.SendAppCommand(command);
        }

        public void Initialize(string moduleFilePath)
        {
        }
    }
}
