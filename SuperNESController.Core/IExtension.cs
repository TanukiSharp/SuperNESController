using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperNESController.Core
{
    public interface IExtension
    {
        string Name { get; }
        int Version { get; }
        void Initialize(string moduleFilePath);
        void Execute(IMessager messager);
    }
}
