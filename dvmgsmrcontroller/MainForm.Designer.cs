namespace dvmgsmrcontroller
{
	partial class MainForm
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
			components = new System.ComponentModel.Container();
			connectbutton = new Button();
			serialportlistbox = new ComboBox();
			txaudioCMBO = new ComboBox();
			rxaudioCMBO = new ComboBox();
			txaudioLabel = new Label();
			rxaudiolabel = new Label();
			daemonportlabel = new Label();
			daemonaddrlabel = new Label();
			daemonaddrTXT = new TextBox();
			daemonptTXT = new TextBox();
			daemonconnectbut = new Button();
			saveBUT = new Button();
			countryTXT = new TextBox();
			ctryLAB = new Label();
			autoconnectTMR = new System.Windows.Forms.Timer(components);
			autostartLEB = new Label();
			asCountLAB = new Label();
			headtimeoutTMR = new System.Windows.Forms.Timer(components);
			ASstopButt = new Button();
			ridinboundBOX = new TextBox();
			label1 = new Label();
			label2 = new Label();
			discoallBUT = new Button();
			SuspendLayout();
			// 
			// connectbutton
			// 
			connectbutton.Location = new Point(17, 90);
			connectbutton.Margin = new Padding(4, 5, 4, 5);
			connectbutton.Name = "connectbutton";
			connectbutton.Size = new Size(173, 38);
			connectbutton.TabIndex = 0;
			connectbutton.Text = "Connect Serial";
			connectbutton.UseVisualStyleBackColor = true;
			connectbutton.Click += connectbutton_Click;
			// 
			// serialportlistbox
			// 
			serialportlistbox.FormattingEnabled = true;
			serialportlistbox.Location = new Point(17, 20);
			serialportlistbox.Margin = new Padding(4, 5, 4, 5);
			serialportlistbox.Name = "serialportlistbox";
			serialportlistbox.Size = new Size(171, 33);
			serialportlistbox.TabIndex = 1;
			// 
			// txaudioCMBO
			// 
			txaudioCMBO.FormattingEnabled = true;
			txaudioCMBO.Location = new Point(393, 20);
			txaudioCMBO.Margin = new Padding(4, 5, 4, 5);
			txaudioCMBO.Name = "txaudioCMBO";
			txaudioCMBO.Size = new Size(171, 33);
			txaudioCMBO.TabIndex = 2;
			// 
			// rxaudioCMBO
			// 
			rxaudioCMBO.FormattingEnabled = true;
			rxaudioCMBO.Location = new Point(393, 92);
			rxaudioCMBO.Margin = new Padding(4, 5, 4, 5);
			rxaudioCMBO.Name = "rxaudioCMBO";
			rxaudioCMBO.Size = new Size(171, 33);
			rxaudioCMBO.TabIndex = 3;
			// 
			// txaudioLabel
			// 
			txaudioLabel.AutoSize = true;
			txaudioLabel.Location = new Point(297, 25);
			txaudioLabel.Margin = new Padding(4, 0, 4, 0);
			txaudioLabel.Name = "txaudioLabel";
			txaudioLabel.Size = new Size(75, 25);
			txaudioLabel.TabIndex = 4;
			txaudioLabel.Text = "TxAudio";
			// 
			// rxaudiolabel
			// 
			rxaudiolabel.AutoSize = true;
			rxaudiolabel.Location = new Point(294, 97);
			rxaudiolabel.Margin = new Padding(4, 0, 4, 0);
			rxaudiolabel.Name = "rxaudiolabel";
			rxaudiolabel.Size = new Size(79, 25);
			rxaudiolabel.TabIndex = 5;
			rxaudiolabel.Text = "RxAudio";
			// 
			// daemonportlabel
			// 
			daemonportlabel.AutoSize = true;
			daemonportlabel.Location = new Point(46, 283);
			daemonportlabel.Margin = new Padding(4, 0, 4, 0);
			daemonportlabel.Name = "daemonportlabel";
			daemonportlabel.Size = new Size(117, 25);
			daemonportlabel.TabIndex = 6;
			daemonportlabel.Text = "Daemon Port";
			// 
			// daemonaddrlabel
			// 
			daemonaddrlabel.AutoSize = true;
			daemonaddrlabel.Location = new Point(33, 210);
			daemonaddrlabel.Margin = new Padding(4, 0, 4, 0);
			daemonaddrlabel.Name = "daemonaddrlabel";
			daemonaddrlabel.Size = new Size(150, 25);
			daemonaddrlabel.TabIndex = 7;
			daemonaddrlabel.Text = "Daemon Address";
			// 
			// daemonaddrTXT
			// 
			daemonaddrTXT.Location = new Point(17, 240);
			daemonaddrTXT.Margin = new Padding(4, 5, 4, 5);
			daemonaddrTXT.Name = "daemonaddrTXT";
			daemonaddrTXT.Size = new Size(171, 31);
			daemonaddrTXT.TabIndex = 8;
			// 
			// daemonptTXT
			// 
			daemonptTXT.Location = new Point(17, 313);
			daemonptTXT.Margin = new Padding(4, 5, 4, 5);
			daemonptTXT.Name = "daemonptTXT";
			daemonptTXT.Size = new Size(171, 31);
			daemonptTXT.TabIndex = 9;
			// 
			// daemonconnectbut
			// 
			daemonconnectbut.Location = new Point(17, 362);
			daemonconnectbut.Margin = new Padding(4, 5, 4, 5);
			daemonconnectbut.Name = "daemonconnectbut";
			daemonconnectbut.Size = new Size(173, 38);
			daemonconnectbut.TabIndex = 10;
			daemonconnectbut.Text = "Daemon Connect";
			daemonconnectbut.UseVisualStyleBackColor = true;
			daemonconnectbut.Click += daemonconnectbut_Click;
			// 
			// saveBUT
			// 
			saveBUT.Location = new Point(393, 362);
			saveBUT.Margin = new Padding(4, 5, 4, 5);
			saveBUT.Name = "saveBUT";
			saveBUT.Size = new Size(173, 38);
			saveBUT.TabIndex = 11;
			saveBUT.Text = "Save";
			saveBUT.UseVisualStyleBackColor = true;
			saveBUT.Click += saveBUT_Click;
			// 
			// countryTXT
			// 
			countryTXT.Location = new Point(393, 170);
			countryTXT.Margin = new Padding(4, 5, 4, 5);
			countryTXT.Name = "countryTXT";
			countryTXT.Size = new Size(171, 31);
			countryTXT.TabIndex = 12;
			// 
			// ctryLAB
			// 
			ctryLAB.AutoSize = true;
			ctryLAB.Location = new Point(297, 175);
			ctryLAB.Margin = new Padding(4, 0, 4, 0);
			ctryLAB.Name = "ctryLAB";
			ctryLAB.Size = new Size(75, 25);
			ctryLAB.TabIndex = 13;
			ctryLAB.Text = "Country";
			// 
			// autoconnectTMR
			// 
			autoconnectTMR.Enabled = true;
			autoconnectTMR.Interval = 1000;
			autoconnectTMR.Tick += autoconnectTMR_Tick;
			// 
			// autostartLEB
			// 
			autostartLEB.AutoSize = true;
			autostartLEB.Location = new Point(393, 332);
			autostartLEB.Margin = new Padding(4, 0, 4, 0);
			autostartLEB.Name = "autostartLEB";
			autostartLEB.Size = new Size(111, 25);
			autostartLEB.TabIndex = 14;
			autostartLEB.Text = "AutoStart In:";
			// 
			// asCountLAB
			// 
			asCountLAB.AutoSize = true;
			asCountLAB.Location = new Point(503, 330);
			asCountLAB.Margin = new Padding(4, 0, 4, 0);
			asCountLAB.Name = "asCountLAB";
			asCountLAB.Size = new Size(32, 25);
			asCountLAB.TabIndex = 15;
			asCountLAB.Text = "10";
			// 
			// headtimeoutTMR
			// 
			headtimeoutTMR.Enabled = true;
			headtimeoutTMR.Interval = 2000;
			headtimeoutTMR.Tick += headtimeoutTMR_Tick;
			// 
			// ASstopButt
			// 
			ASstopButt.Location = new Point(539, 325);
			ASstopButt.Margin = new Padding(4, 5, 4, 5);
			ASstopButt.Name = "ASstopButt";
			ASstopButt.Size = new Size(56, 35);
			ASstopButt.TabIndex = 16;
			ASstopButt.Text = "Stop";
			ASstopButt.UseVisualStyleBackColor = true;
			ASstopButt.Click += ASstopButt_Click;
			// 
			// ridinboundBOX
			// 
			ridinboundBOX.Location = new Point(884, 20);
			ridinboundBOX.Margin = new Padding(4, 5, 4, 5);
			ridinboundBOX.Name = "ridinboundBOX";
			ridinboundBOX.Size = new Size(141, 31);
			ridinboundBOX.TabIndex = 17;
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new Point(734, 25);
			label1.Margin = new Padding(4, 0, 4, 0);
			label1.Name = "label1";
			label1.Size = new Size(154, 25);
			label1.TabIndex = 21;
			label1.Text = "Inbound Radio ID";
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Location = new Point(743, 23);
			label2.Name = "label2";
			label2.Size = new Size(282, 25);
			label2.TabIndex = 22;
			label2.Text = "THIS DOESNT WORK DUE TO RC2";
			// 
			// discoallBUT
			// 
			discoallBUT.Location = new Point(393, 417);
			discoallBUT.Name = "discoallBUT";
			discoallBUT.Size = new Size(173, 34);
			discoallBUT.TabIndex = 23;
			discoallBUT.Text = "Exit";
			discoallBUT.UseVisualStyleBackColor = true;
			discoallBUT.Click += discoallBUT_Click;
			// 
			// MainForm
			// 
			AutoScaleDimensions = new SizeF(10F, 25F);
			AutoScaleMode = AutoScaleMode.Font;
			BackgroundImage = Properties.Resources._48450756326_5aab7b8186_b;
			BackgroundImageLayout = ImageLayout.Center;
			ClientSize = new Size(1143, 750);
			Controls.Add(discoallBUT);
			Controls.Add(label2);
			Controls.Add(label1);
			Controls.Add(ridinboundBOX);
			Controls.Add(ASstopButt);
			Controls.Add(asCountLAB);
			Controls.Add(autostartLEB);
			Controls.Add(ctryLAB);
			Controls.Add(countryTXT);
			Controls.Add(saveBUT);
			Controls.Add(daemonconnectbut);
			Controls.Add(daemonptTXT);
			Controls.Add(daemonaddrTXT);
			Controls.Add(daemonaddrlabel);
			Controls.Add(daemonportlabel);
			Controls.Add(rxaudiolabel);
			Controls.Add(txaudioLabel);
			Controls.Add(rxaudioCMBO);
			Controls.Add(txaudioCMBO);
			Controls.Add(serialportlistbox);
			Controls.Add(connectbutton);
			Margin = new Padding(4, 5, 4, 5);
			Name = "MainForm";
			Text = "MainForm";
			FormClosing += MainForm_FormClosing;
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Button connectbutton;
		private ComboBox serialportlistbox;
		private ComboBox txaudioCMBO;
		private ComboBox rxaudioCMBO;
		private Label txaudioLabel;
		private Label rxaudiolabel;
		private Label daemonportlabel;
		private Label daemonaddrlabel;
		private Button daemonconnectbut;
		private Button saveBUT;
		private TextBox countryTXT;
		private Label ctryLAB;
		private System.Windows.Forms.Timer autoconnectTMR;
		internal TextBox daemonaddrTXT;
		internal TextBox daemonptTXT;
		private Label autostartLEB;
		private Label asCountLAB;
		private System.Windows.Forms.Timer headtimeoutTMR;
		private Button ASstopButt;
		private TextBox ridinboundBOX;
		private Label label1;
		private Label label2;
		private Button discoallBUT;
	}
}