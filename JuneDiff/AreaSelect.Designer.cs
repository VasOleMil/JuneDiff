namespace JuneDiff
{
    partial class AreaSelect
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
            this.panelDrag = new System.Windows.Forms.Panel();
            this.bt_Select = new System.Windows.Forms.Button();
            this.panelDrag.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelDrag
            // 
            this.panelDrag.Controls.Add(this.bt_Select);
            this.panelDrag.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this.panelDrag.Location = new System.Drawing.Point(12, 12);
            this.panelDrag.Name = "panelDrag";
            this.panelDrag.Size = new System.Drawing.Size(776, 426);
            this.panelDrag.TabIndex = 0;
            this.panelDrag.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelDrag_MouseDown);
            // 
            // bt_Select
            // 
            this.bt_Select.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.bt_Select.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bt_Select.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bt_Select.Location = new System.Drawing.Point(330, 191);
            this.bt_Select.Name = "bt_Select";
            this.bt_Select.Size = new System.Drawing.Size(140, 40);
            this.bt_Select.TabIndex = 0;
            this.bt_Select.Text = "Continue";
            this.bt_Select.UseVisualStyleBackColor = false;
            this.bt_Select.Click += new System.EventHandler(this.btnCaptureThis_Click);
            // 
            // AreaSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panelDrag);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AreaSelect";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "SelectArea";
            this.TopMost = true;
            this.SizeChanged += new System.EventHandler(this.SelectArea_SizeChanged);
            this.panelDrag.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelDrag;
        private System.Windows.Forms.Button bt_Select;
    }
}