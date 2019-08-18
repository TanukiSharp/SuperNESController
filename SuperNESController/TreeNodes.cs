using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SuperNESController.Core;

namespace SuperNESController
{
    public class ModuleTreeNode : TreeNode
    {
        public ModuleTreeNode(Module module)
        {
            if (module == null)
                throw new ArgumentNullException(nameof(module));

            if (module.Assembly == null || module.Extensions == null || module.Extensions.Length == 0)
                throw new ArgumentException(nameof(module));

            Text = $"Module: {Path.GetFileName(module.ModuleFilename)}";

            Nodes.AddRange(module.Extensions.Select(x => new ExtensionTreeNode(x)).ToArray());
        }
    }

    public class ExtensionTreeNode : TreeNode
    {
        public ExtensionContainer ExtensionContainer { get; }

        public ExtensionTreeNode(ExtensionContainer extensionContainer)
        {
            if (extensionContainer == null)
                throw new ArgumentNullException(nameof(extensionContainer));

            ExtensionContainer = extensionContainer;

            Text = $"Extension: {extensionContainer.Extension.Name} (version {extensionContainer.Extension.Version})";
        }

        public void UpdateButtonNodes()
        {
            Nodes.Clear();
        }
    }
}
