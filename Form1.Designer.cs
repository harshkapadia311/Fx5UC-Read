namespace Fx5UC_Read
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
            components = new System.ComponentModel.Container();
            CtnWithPlc = new GroupBox();
            label2 = new Label();
            label1 = new Label();
            lbIpAddress = new Label();
            btnStatus = new Button();
            btnConnect = new Button();
            txtPortNo = new TextBox();
            txtIpAddress = new TextBox();
            gpDataRegisterD = new GroupBox();
            btnStopDataRdVal = new Button();
            btnStartDataRdVal = new Button();
            lbDataRegisterRdVal = new Label();
            lbDataRegisterAddr = new Label();
            txtDataRegRdVal = new TextBox();
            txtDataRegAddress = new TextBox();
            timer1 = new System.Windows.Forms.Timer(components);
            CtnWithPlc.SuspendLayout();
            gpDataRegisterD.SuspendLayout();
            SuspendLayout();
            // 
            // CtnWithPlc
            // 
            CtnWithPlc.Controls.Add(label2);
            CtnWithPlc.Controls.Add(label1);
            CtnWithPlc.Controls.Add(lbIpAddress);
            CtnWithPlc.Controls.Add(btnStatus);
            CtnWithPlc.Controls.Add(btnConnect);
            CtnWithPlc.Controls.Add(txtPortNo);
            CtnWithPlc.Controls.Add(txtIpAddress);
            CtnWithPlc.Location = new Point(12, 12);
            CtnWithPlc.Name = "CtnWithPlc";
            CtnWithPlc.Size = new Size(382, 105);
            CtnWithPlc.TabIndex = 0;
            CtnWithPlc.TabStop = false;
            CtnWithPlc.Text = "Connection";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(251, 27);
            label2.Name = "label2";
            label2.Size = new Size(47, 25);
            label2.TabIndex = 6;
            label2.Text = "Status";
            label2.UseCompatibleTextRendering = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = SystemColors.Control;
            label1.ForeColor = SystemColors.ControlText;
            label1.Location = new Point(30, 65);
            label1.Name = "label1";
            label1.Size = new Size(59, 20);
            label1.TabIndex = 3;
            label1.Text = "Port No";
            // 
            // lbIpAddress
            // 
            lbIpAddress.AutoSize = true;
            lbIpAddress.BackColor = SystemColors.Control;
            lbIpAddress.ForeColor = SystemColors.ControlText;
            lbIpAddress.Location = new Point(11, 26);
            lbIpAddress.Name = "lbIpAddress";
            lbIpAddress.Size = new Size(77, 25);
            lbIpAddress.TabIndex = 1;
            lbIpAddress.Text = "IP Address";
            lbIpAddress.UseCompatibleTextRendering = true;
            // 
            // btnStatus
            // 
            btnStatus.BackColor = Color.Red;
            btnStatus.Enabled = false;
            btnStatus.ForeColor = Color.Black;
            btnStatus.Location = new Point(304, 23);
            btnStatus.Name = "btnStatus";
            btnStatus.Size = new Size(64, 29);
            btnStatus.TabIndex = 5;
            btnStatus.UseVisualStyleBackColor = false;
            // 
            // btnConnect
            // 
            btnConnect.Location = new Point(246, 60);
            btnConnect.Name = "btnConnect";
            btnConnect.Size = new Size(124, 29);
            btnConnect.TabIndex = 2;
            btnConnect.Text = "Connect";
            btnConnect.UseVisualStyleBackColor = true;
            btnConnect.Click += btnConnect_Click;
            // 
            // txtPortNo
            // 
            txtPortNo.Location = new Point(95, 62);
            txtPortNo.Name = "txtPortNo";
            txtPortNo.Size = new Size(125, 27);
            txtPortNo.TabIndex = 1;
            // 
            // txtIpAddress
            // 
            txtIpAddress.AcceptsTab = true;
            txtIpAddress.Location = new Point(95, 23);
            txtIpAddress.Name = "txtIpAddress";
            txtIpAddress.Size = new Size(125, 27);
            txtIpAddress.TabIndex = 0;
            // 
            // gpDataRegisterD
            // 
            gpDataRegisterD.Controls.Add(btnStopDataRdVal);
            gpDataRegisterD.Controls.Add(btnStartDataRdVal);
            gpDataRegisterD.Controls.Add(lbDataRegisterRdVal);
            gpDataRegisterD.Controls.Add(lbDataRegisterAddr);
            gpDataRegisterD.Controls.Add(txtDataRegRdVal);
            gpDataRegisterD.Controls.Add(txtDataRegAddress);
            gpDataRegisterD.Location = new Point(12, 133);
            gpDataRegisterD.Name = "gpDataRegisterD";
            gpDataRegisterD.Size = new Size(191, 166);
            gpDataRegisterD.TabIndex = 4;
            gpDataRegisterD.TabStop = false;
            gpDataRegisterD.Text = "Data Register (D)";
            // 
            // btnStopDataRdVal
            // 
            btnStopDataRdVal.Location = new Point(98, 91);
            btnStopDataRdVal.Name = "btnStopDataRdVal";
            btnStopDataRdVal.Size = new Size(86, 29);
            btnStopDataRdVal.TabIndex = 19;
            btnStopDataRdVal.Text = "STOP";
            btnStopDataRdVal.UseVisualStyleBackColor = true;
            btnStopDataRdVal.Click += btnStopDataRdVal_Click;
            // 
            // btnStartDataRdVal
            // 
            btnStartDataRdVal.Location = new Point(6, 91);
            btnStartDataRdVal.Name = "btnStartDataRdVal";
            btnStartDataRdVal.Size = new Size(86, 29);
            btnStartDataRdVal.TabIndex = 18;
            btnStartDataRdVal.Text = "START";
            btnStartDataRdVal.UseVisualStyleBackColor = true;
            btnStartDataRdVal.Click += btnStartDataRdVal_Click;
            // 
            // lbDataRegisterRdVal
            // 
            lbDataRegisterRdVal.AutoSize = true;
            lbDataRegisterRdVal.Location = new Point(101, 23);
            lbDataRegisterRdVal.Name = "lbDataRegisterRdVal";
            lbDataRegisterRdVal.Size = new Size(83, 20);
            lbDataRegisterRdVal.TabIndex = 3;
            lbDataRegisterRdVal.Text = "Read Value";
            // 
            // lbDataRegisterAddr
            // 
            lbDataRegisterAddr.AutoSize = true;
            lbDataRegisterAddr.Location = new Point(22, 23);
            lbDataRegisterAddr.Name = "lbDataRegisterAddr";
            lbDataRegisterAddr.Size = new Size(62, 20);
            lbDataRegisterAddr.TabIndex = 2;
            lbDataRegisterAddr.Text = "Address";
            // 
            // txtDataRegRdVal
            // 
            txtDataRegRdVal.Location = new Point(98, 48);
            txtDataRegRdVal.Name = "txtDataRegRdVal";
            txtDataRegRdVal.Size = new Size(86, 27);
            txtDataRegRdVal.TabIndex = 15;
            txtDataRegRdVal.TextAlign = HorizontalAlignment.Center;
            // 
            // txtDataRegAddress
            // 
            txtDataRegAddress.Location = new Point(6, 48);
            txtDataRegAddress.Name = "txtDataRegAddress";
            txtDataRegAddress.Size = new Size(86, 27);
            txtDataRegAddress.TabIndex = 14;
            txtDataRegAddress.TextAlign = HorizontalAlignment.Center;
            // 
            // timer1
            // 
            timer1.Interval = 15;
            timer1.Tick += timer1_Tick;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(808, 472);
            Controls.Add(gpDataRegisterD);
            Controls.Add(CtnWithPlc);
            Name = "Form1";
            Text = "Form1";
            FormClosed += Form1_FormClosed;
            CtnWithPlc.ResumeLayout(false);
            CtnWithPlc.PerformLayout();
            gpDataRegisterD.ResumeLayout(false);
            gpDataRegisterD.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox CtnWithPlc;
        private TextBox txtIpAddress;
        private Label lbIpAddress;
        private Button btnConnect;
        private Label label1;
        private TextBox txtPortNo;
        private Button btnStatus;
        private Label label2;
        private GroupBox gpDataRegisterD;
        private TextBox txtDataRegRdVal;
        private TextBox txtDataRegAddress;
        private Label lbDataRegisterRdVal;
        private Label lbDataRegisterAddr;
        private Button btnStopDataRdVal;
        private Button btnStartDataRdVal;
        internal System.Windows.Forms.Timer timer1;
    }
}
