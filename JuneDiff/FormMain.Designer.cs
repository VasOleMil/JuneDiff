
namespace JuneDiff
{
    partial class FormMain
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
            this.bt_Call = new System.Windows.Forms.Button();
            this.bt_Find = new System.Windows.Forms.Button();
            this.tb_Dif = new System.Windows.Forms.TextBox();
            this.tb_Len = new System.Windows.Forms.TextBox();
            this.tb_Cnt = new System.Windows.Forms.TextBox();
            this.bt_Hide = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // bt_Call
            // 
            this.bt_Call.Location = new System.Drawing.Point(12, 12);
            this.bt_Call.Name = "bt_Call";
            this.bt_Call.Size = new System.Drawing.Size(131, 42);
            this.bt_Call.TabIndex = 0;
            this.bt_Call.Text = "Callibrate";
            this.bt_Call.UseVisualStyleBackColor = true;
            this.bt_Call.Click += new System.EventHandler(this.bt_Call_Click);
            // 
            // bt_Find
            // 
            this.bt_Find.Location = new System.Drawing.Point(149, 12);
            this.bt_Find.Name = "bt_Find";
            this.bt_Find.Size = new System.Drawing.Size(131, 42);
            this.bt_Find.TabIndex = 1;
            this.bt_Find.Text = "Find";
            this.bt_Find.UseVisualStyleBackColor = true;
            this.bt_Find.Click += new System.EventHandler(this.bt_Find_Click);
            // 
            // tb_Dif
            // 
            this.tb_Dif.Location = new System.Drawing.Point(584, 12);
            this.tb_Dif.Multiline = true;
            this.tb_Dif.Name = "tb_Dif";
            this.tb_Dif.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tb_Dif.Size = new System.Drawing.Size(119, 41);
            this.tb_Dif.TabIndex = 2;
            // 
            // tb_Len
            // 
            this.tb_Len.Location = new System.Drawing.Point(450, 12);
            this.tb_Len.Name = "tb_Len";
            this.tb_Len.Size = new System.Drawing.Size(128, 20);
            this.tb_Len.TabIndex = 3;
            // 
            // tb_Cnt
            // 
            this.tb_Cnt.Location = new System.Drawing.Point(450, 34);
            this.tb_Cnt.Name = "tb_Cnt";
            this.tb_Cnt.Size = new System.Drawing.Size(128, 20);
            this.tb_Cnt.TabIndex = 4;
            // 
            // bt_Hide
            // 
            this.bt_Hide.Location = new System.Drawing.Point(286, 12);
            this.bt_Hide.Name = "bt_Hide";
            this.bt_Hide.Size = new System.Drawing.Size(131, 42);
            this.bt_Hide.TabIndex = 5;
            this.bt_Hide.Text = "Hide";
            this.bt_Hide.UseVisualStyleBackColor = true;
            this.bt_Hide.Click += new System.EventHandler(this.bt_Hide_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(717, 67);
            this.Controls.Add(this.bt_Hide);
            this.Controls.Add(this.tb_Cnt);
            this.Controls.Add(this.tb_Len);
            this.Controls.Add(this.tb_Dif);
            this.Controls.Add(this.bt_Find);
            this.Controls.Add(this.bt_Call);
            this.DoubleBuffered = true;
            this.HelpButton = true;
            this.Name = "FormMain";
            this.Text = "Find Difference";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bt_Call;
        private System.Windows.Forms.Button bt_Find;
        private System.Windows.Forms.TextBox tb_Dif;
        private System.Windows.Forms.TextBox tb_Len;
        private System.Windows.Forms.TextBox tb_Cnt;
        private System.Windows.Forms.Button bt_Hide;
    }
}

