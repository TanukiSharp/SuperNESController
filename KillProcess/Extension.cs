using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using SuperNESController.Core;

namespace KillProcess
{
    public class Extension : IExtension
    {
        public string Name { get; } = "Kill Process";
        public int Version { get; } = 1;

        private string[] processes;

        public void Initialize(string moduleFilePath)
        {
            string confFile = Path.ChangeExtension(moduleFilePath, ".txt");

            processes = File.ReadAllLines(confFile)
                .Where(x => string.IsNullOrWhiteSpace(x) == false)
                .ToArray();
        }

        public void Execute(IMessager messager)
        {
            IEnumerable<Process> procs = processes.SelectMany(x => Process.GetProcessesByName(x));

            var names = new HashSet<string>();

            foreach (Process proc in procs)
            {
                names.Add(proc.ProcessName);
                proc.Kill();
            }

            if (names.Count > 0)
                messager.ShowMessage(3000, "Process killed", string.Join(Environment.NewLine, names), MessageIcon.Info);
        }
    }
}
