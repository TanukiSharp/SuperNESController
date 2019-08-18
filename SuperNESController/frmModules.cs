using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuperNESController
{
    public partial class frmModules : Form
    {
        private readonly IDictionary<Buttons, CheckBox> checkBoxes;

        public frmModules(string log, ModuleManager moduleManager)
        {
            InitializeComponent();

            checkBoxes = pnlFlags.Controls
                .OfType<CheckBox>()
                .Select(PrepareCheckBox)
                .ToDictionary(x => (Buttons)x.Tag, y => y);

            trvModules.AfterSelect += trvModules_AfterSelect;

            txtLog.Text = log;

            if (moduleManager != null)
            {
                foreach (Module module in moduleManager.Modules)
                {
                    if (module.Assembly != null && module.Extensions != null && module.Extensions.Length > 0)
                        trvModules.Nodes.Add(new ModuleTreeNode(module));
                }
            }

            trvModules.ExpandAll();
        }

        private CheckBox PrepareCheckBox(CheckBox chk)
        {
            if (chk.Name.StartsWith("chk"))
            {
                if (Enum.TryParse(chk.Name.Substring(3), false, out Buttons result))
                {
                    chk.Tag = result;
                    chk.CheckedChanged += chk_CheckedChanged;
                }
            }

            return chk;
        }

        private bool isResetting;

        private void chk_CheckedChanged(object sender, EventArgs e)
        {
            if (isResetting)
                return;

            var ext = trvModules.SelectedNode as ExtensionTreeNode;
            if (ext == null)
                return;

            Buttons[] flags = pnlFlags.Controls
                .OfType<CheckBox>()
                .Where(x => x.Checked)
                .Select(x => (Buttons)x.Tag)
                .ToArray();

            ext.ExtensionContainer.ButtonsMask = Utility.MergeButtonFlags(flags);
        }

        private void trvModules_AfterSelect(object sender, TreeViewEventArgs e)
        {
            isResetting = true;

            try
            {
                foreach (CheckBox chk in pnlFlags.Controls.OfType<CheckBox>())
                    chk.Checked = false;
            }
            finally
            {
                isResetting = false;
            }

            var ext = e.Node as ExtensionTreeNode;
            if (ext == null)
            {
                if (pnlFlags.Enabled)
                    pnlFlags.Enabled = false;

                return;
            }

            if (pnlFlags.Enabled == false)
                pnlFlags.Enabled = true;

            Buttons[] masks = Utility.SplitButtonFlags(ext.ExtensionContainer.ButtonsMask);

            foreach (Buttons m in masks)
                checkBoxes[m].Checked = true;
        }

        public void ProcessButtons(Buttons buttons)
        {
            foreach (CheckBox chk in Utility.SplitButtonFlags(buttons).Select(x => checkBoxes[x]))
                chk.Checked = !chk.Checked;
        }
    }
}
