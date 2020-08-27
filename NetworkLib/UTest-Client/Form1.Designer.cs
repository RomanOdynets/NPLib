namespace UTest_Client
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.connect_btn = new System.Windows.Forms.Button();
            this.connect_lbl = new System.Windows.Forms.Label();
            this.data_tb = new System.Windows.Forms.TextBox();
            this.send_btn = new System.Windows.Forms.Button();
            this.packetType_cb = new System.Windows.Forms.ComboBox();
            this.packetPType_cb = new System.Windows.Forms.ComboBox();
            this.responce_lb = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // connect_btn
            // 
            this.connect_btn.Location = new System.Drawing.Point(493, 43);
            this.connect_btn.Name = "connect_btn";
            this.connect_btn.Size = new System.Drawing.Size(75, 23);
            this.connect_btn.TabIndex = 0;
            this.connect_btn.Text = "Connect";
            this.connect_btn.UseVisualStyleBackColor = true;
            this.connect_btn.Click += new System.EventHandler(this.connect_btn_Click);
            // 
            // connect_lbl
            // 
            this.connect_lbl.AutoSize = true;
            this.connect_lbl.Location = new System.Drawing.Point(493, 25);
            this.connect_lbl.Name = "connect_lbl";
            this.connect_lbl.Size = new System.Drawing.Size(36, 15);
            this.connect_lbl.TabIndex = 1;
            this.connect_lbl.Text = "Close";
            // 
            // data_tb
            // 
            this.data_tb.Location = new System.Drawing.Point(13, 84);
            this.data_tb.Name = "data_tb";
            this.data_tb.Size = new System.Drawing.Size(440, 23);
            this.data_tb.TabIndex = 2;
            // 
            // send_btn
            // 
            this.send_btn.Location = new System.Drawing.Point(460, 84);
            this.send_btn.Name = "send_btn";
            this.send_btn.Size = new System.Drawing.Size(108, 23);
            this.send_btn.TabIndex = 3;
            this.send_btn.Text = "Send";
            this.send_btn.UseVisualStyleBackColor = true;
            this.send_btn.Click += new System.EventHandler(this.send_btn_Click);
            // 
            // packetType_cb
            // 
            this.packetType_cb.FormattingEnabled = true;
            this.packetType_cb.Items.AddRange(new object[] {
            "Message",
            "Session",
            "PublicKey"});
            this.packetType_cb.Location = new System.Drawing.Point(13, 43);
            this.packetType_cb.Name = "packetType_cb";
            this.packetType_cb.Size = new System.Drawing.Size(242, 23);
            this.packetType_cb.TabIndex = 4;
            this.packetType_cb.SelectedIndexChanged += new System.EventHandler(this.packetType_cb_SelectedIndexChanged);
            // 
            // packetPType_cb
            // 
            this.packetPType_cb.FormattingEnabled = true;
            this.packetPType_cb.Location = new System.Drawing.Point(261, 43);
            this.packetPType_cb.Name = "packetPType_cb";
            this.packetPType_cb.Size = new System.Drawing.Size(192, 23);
            this.packetPType_cb.TabIndex = 4;
            // 
            // responce_lb
            // 
            this.responce_lb.FormattingEnabled = true;
            this.responce_lb.ItemHeight = 15;
            this.responce_lb.Location = new System.Drawing.Point(13, 114);
            this.responce_lb.Name = "responce_lb";
            this.responce_lb.Size = new System.Drawing.Size(440, 304);
            this.responce_lb.TabIndex = 5;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(587, 434);
            this.Controls.Add(this.responce_lb);
            this.Controls.Add(this.packetPType_cb);
            this.Controls.Add(this.packetType_cb);
            this.Controls.Add(this.send_btn);
            this.Controls.Add(this.data_tb);
            this.Controls.Add(this.connect_lbl);
            this.Controls.Add(this.connect_btn);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button connect_btn;
        private System.Windows.Forms.Label connect_lbl;
        private System.Windows.Forms.TextBox data_tb;
        private System.Windows.Forms.Button send_btn;
        private System.Windows.Forms.ComboBox packetType_cb;
        private System.Windows.Forms.ComboBox packetPType_cb;
        private System.Windows.Forms.ListBox responce_lb;
    }
}

