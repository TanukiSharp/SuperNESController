namespace SuperNESController
{
    partial class frmModules
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmModules));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lblModules = new System.Windows.Forms.Label();
            this.trvModules = new System.Windows.Forms.TreeView();
            this.pnlFlags = new System.Windows.Forms.Panel();
            this.chkA = new SuperNESController.CustomCheckBox();
            this.chkB = new SuperNESController.CustomCheckBox();
            this.chkY = new SuperNESController.CustomCheckBox();
            this.chkX = new SuperNESController.CustomCheckBox();
            this.chkStart = new SuperNESController.CustomCheckBox();
            this.chkSelect = new SuperNESController.CustomCheckBox();
            this.chkDown = new SuperNESController.CustomCheckBox();
            this.chkRight = new SuperNESController.CustomCheckBox();
            this.chkLeft = new SuperNESController.CustomCheckBox();
            this.chkUp = new SuperNESController.CustomCheckBox();
            this.chkR = new SuperNESController.CustomCheckBox();
            this.chkL = new SuperNESController.CustomCheckBox();
            this.lblLog = new System.Windows.Forms.Label();
            this.txtLog = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.pnlFlags.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.lblModules);
            this.splitContainer1.Panel1.Controls.Add(this.trvModules);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.pnlFlags);
            this.splitContainer1.Panel2.Controls.Add(this.lblLog);
            this.splitContainer1.Panel2.Controls.Add(this.txtLog);
            this.splitContainer1.Size = new System.Drawing.Size(971, 635);
            this.splitContainer1.SplitterDistance = 343;
            this.splitContainer1.TabIndex = 0;
            // 
            // lblModules
            // 
            this.lblModules.AutoSize = true;
            this.lblModules.Location = new System.Drawing.Point(12, 9);
            this.lblModules.Name = "lblModules";
            this.lblModules.Size = new System.Drawing.Size(50, 13);
            this.lblModules.TabIndex = 1;
            this.lblModules.Text = "Modules:";
            // 
            // trvModules
            // 
            this.trvModules.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trvModules.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.trvModules.HideSelection = false;
            this.trvModules.Location = new System.Drawing.Point(12, 25);
            this.trvModules.Name = "trvModules";
            this.trvModules.Size = new System.Drawing.Size(328, 598);
            this.trvModules.TabIndex = 0;
            // 
            // pnlFlags
            // 
            this.pnlFlags.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlFlags.BackgroundImage")));
            this.pnlFlags.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pnlFlags.Controls.Add(this.chkA);
            this.pnlFlags.Controls.Add(this.chkB);
            this.pnlFlags.Controls.Add(this.chkY);
            this.pnlFlags.Controls.Add(this.chkX);
            this.pnlFlags.Controls.Add(this.chkStart);
            this.pnlFlags.Controls.Add(this.chkSelect);
            this.pnlFlags.Controls.Add(this.chkDown);
            this.pnlFlags.Controls.Add(this.chkRight);
            this.pnlFlags.Controls.Add(this.chkLeft);
            this.pnlFlags.Controls.Add(this.chkUp);
            this.pnlFlags.Controls.Add(this.chkR);
            this.pnlFlags.Controls.Add(this.chkL);
            this.pnlFlags.Location = new System.Drawing.Point(3, 3);
            this.pnlFlags.Name = "pnlFlags";
            this.pnlFlags.Size = new System.Drawing.Size(504, 222);
            this.pnlFlags.TabIndex = 2;
            // 
            // chkA
            // 
            this.chkA.AutoSize = true;
            this.chkA.BackColor = System.Drawing.Color.Transparent;
            this.chkA.Location = new System.Drawing.Point(434, 105);
            this.chkA.Margin = new System.Windows.Forms.Padding(0);
            this.chkA.Name = "chkA";
            this.chkA.Size = new System.Drawing.Size(15, 14);
            this.chkA.TabIndex = 11;
            this.chkA.UseVisualStyleBackColor = true;
            // 
            // chkB
            // 
            this.chkB.AutoSize = true;
            this.chkB.BackColor = System.Drawing.Color.Transparent;
            this.chkB.Location = new System.Drawing.Point(387, 142);
            this.chkB.Name = "chkB";
            this.chkB.Size = new System.Drawing.Size(15, 14);
            this.chkB.TabIndex = 10;
            this.chkB.UseVisualStyleBackColor = true;
            // 
            // chkY
            // 
            this.chkY.AutoSize = true;
            this.chkY.BackColor = System.Drawing.Color.Transparent;
            this.chkY.Location = new System.Drawing.Point(338, 105);
            this.chkY.Name = "chkY";
            this.chkY.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chkY.Size = new System.Drawing.Size(15, 14);
            this.chkY.TabIndex = 9;
            this.chkY.UseVisualStyleBackColor = true;
            // 
            // chkX
            // 
            this.chkX.AutoSize = true;
            this.chkX.BackColor = System.Drawing.Color.Transparent;
            this.chkX.Location = new System.Drawing.Point(385, 68);
            this.chkX.Name = "chkX";
            this.chkX.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chkX.Size = new System.Drawing.Size(15, 14);
            this.chkX.TabIndex = 8;
            this.chkX.UseVisualStyleBackColor = false;
            // 
            // chkStart
            // 
            this.chkStart.AutoSize = true;
            this.chkStart.BackColor = System.Drawing.Color.Transparent;
            this.chkStart.Location = new System.Drawing.Point(252, 122);
            this.chkStart.Name = "chkStart";
            this.chkStart.Size = new System.Drawing.Size(15, 14);
            this.chkStart.TabIndex = 7;
            this.chkStart.UseVisualStyleBackColor = true;
            // 
            // chkSelect
            // 
            this.chkSelect.AutoSize = true;
            this.chkSelect.BackColor = System.Drawing.Color.Transparent;
            this.chkSelect.Location = new System.Drawing.Point(196, 122);
            this.chkSelect.Name = "chkSelect";
            this.chkSelect.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chkSelect.Size = new System.Drawing.Size(15, 14);
            this.chkSelect.TabIndex = 6;
            this.chkSelect.UseVisualStyleBackColor = true;
            // 
            // chkDown
            // 
            this.chkDown.AutoSize = true;
            this.chkDown.BackColor = System.Drawing.Color.Transparent;
            this.chkDown.Location = new System.Drawing.Point(98, 135);
            this.chkDown.Name = "chkDown";
            this.chkDown.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chkDown.Size = new System.Drawing.Size(15, 14);
            this.chkDown.TabIndex = 5;
            this.chkDown.UseVisualStyleBackColor = true;
            // 
            // chkRight
            // 
            this.chkRight.AutoSize = true;
            this.chkRight.BackColor = System.Drawing.Color.Transparent;
            this.chkRight.Location = new System.Drawing.Point(127, 107);
            this.chkRight.Name = "chkRight";
            this.chkRight.Size = new System.Drawing.Size(15, 14);
            this.chkRight.TabIndex = 4;
            this.chkRight.UseVisualStyleBackColor = true;
            // 
            // chkLeft
            // 
            this.chkLeft.AutoSize = true;
            this.chkLeft.BackColor = System.Drawing.Color.Transparent;
            this.chkLeft.Location = new System.Drawing.Point(69, 107);
            this.chkLeft.Name = "chkLeft";
            this.chkLeft.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chkLeft.Size = new System.Drawing.Size(15, 14);
            this.chkLeft.TabIndex = 3;
            this.chkLeft.UseVisualStyleBackColor = true;
            // 
            // chkUp
            // 
            this.chkUp.AutoSize = true;
            this.chkUp.BackColor = System.Drawing.Color.Transparent;
            this.chkUp.Location = new System.Drawing.Point(98, 78);
            this.chkUp.Name = "chkUp";
            this.chkUp.Size = new System.Drawing.Size(15, 14);
            this.chkUp.TabIndex = 2;
            this.chkUp.UseVisualStyleBackColor = true;
            // 
            // chkR
            // 
            this.chkR.AutoSize = true;
            this.chkR.BackColor = System.Drawing.Color.Transparent;
            this.chkR.Location = new System.Drawing.Point(450, 0);
            this.chkR.Name = "chkR";
            this.chkR.Size = new System.Drawing.Size(34, 17);
            this.chkR.TabIndex = 1;
            this.chkR.Text = "R";
            this.chkR.UseVisualStyleBackColor = true;
            // 
            // chkL
            // 
            this.chkL.AutoSize = true;
            this.chkL.BackColor = System.Drawing.Color.Transparent;
            this.chkL.Location = new System.Drawing.Point(20, 0);
            this.chkL.Name = "chkL";
            this.chkL.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chkL.Size = new System.Drawing.Size(32, 17);
            this.chkL.TabIndex = 0;
            this.chkL.Text = "L";
            this.chkL.UseVisualStyleBackColor = true;
            // 
            // lblLog
            // 
            this.lblLog.AutoSize = true;
            this.lblLog.Location = new System.Drawing.Point(3, 228);
            this.lblLog.Name = "lblLog";
            this.lblLog.Size = new System.Drawing.Size(28, 13);
            this.lblLog.TabIndex = 1;
            this.lblLog.Text = "Log:";
            // 
            // txtLog
            // 
            this.txtLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLog.BackColor = System.Drawing.SystemColors.Window;
            this.txtLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLog.Location = new System.Drawing.Point(3, 244);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtLog.Size = new System.Drawing.Size(609, 379);
            this.txtLog.TabIndex = 0;
            // 
            // frmModules
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(971, 635);
            this.Controls.Add(this.splitContainer1);
            this.DoubleBuffered = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmModules";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Modules and Extensions";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.pnlFlags.ResumeLayout(false);
            this.pnlFlags.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView trvModules;
        private System.Windows.Forms.Label lblLog;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.Label lblModules;
        private System.Windows.Forms.Panel pnlFlags;
        private SuperNESController.CustomCheckBox chkA;
        private SuperNESController.CustomCheckBox chkB;
        private SuperNESController.CustomCheckBox chkY;
        private SuperNESController.CustomCheckBox chkX;
        private SuperNESController.CustomCheckBox chkStart;
        private SuperNESController.CustomCheckBox chkSelect;
        private SuperNESController.CustomCheckBox chkDown;
        private SuperNESController.CustomCheckBox chkRight;
        private SuperNESController.CustomCheckBox chkLeft;
        private SuperNESController.CustomCheckBox chkUp;
        private SuperNESController.CustomCheckBox chkR;
        private SuperNESController.CustomCheckBox chkL;
    }
}