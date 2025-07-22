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
			comboBox1 = new ComboBox();
			comboBox2 = new ComboBox();
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
			SuspendLayout();
			// 
			// connectbutton
			// 
			connectbutton.Location = new Point(12, 54);
			connectbutton.Name = "connectbutton";
			connectbutton.Size = new Size(121, 23);
			connectbutton.TabIndex = 0;
			connectbutton.Text = "Connect Serial";
			connectbutton.UseVisualStyleBackColor = true;
			connectbutton.Click += connectbutton_Click;
			// 
			// serialportlistbox
			// 
			serialportlistbox.FormattingEnabled = true;
			serialportlistbox.Location = new Point(12, 12);
			serialportlistbox.Name = "serialportlistbox";
			serialportlistbox.Size = new Size(121, 23);
			serialportlistbox.TabIndex = 1;
			// 
			// comboBox1
			// 
			comboBox1.FormattingEnabled = true;
			comboBox1.Location = new Point(275, 12);
			comboBox1.Name = "comboBox1";
			comboBox1.Size = new Size(121, 23);
			comboBox1.TabIndex = 2;
			// 
			// comboBox2
			// 
			comboBox2.FormattingEnabled = true;
			comboBox2.Location = new Point(275, 55);
			comboBox2.Name = "comboBox2";
			comboBox2.Size = new Size(121, 23);
			comboBox2.TabIndex = 3;
			// 
			// txaudioLabel
			// 
			txaudioLabel.AutoSize = true;
			txaudioLabel.Location = new Point(208, 15);
			txaudioLabel.Name = "txaudioLabel";
			txaudioLabel.Size = new Size(50, 15);
			txaudioLabel.TabIndex = 4;
			txaudioLabel.Text = "TxAudio";
			// 
			// rxaudiolabel
			// 
			rxaudiolabel.AutoSize = true;
			rxaudiolabel.Location = new Point(206, 58);
			rxaudiolabel.Name = "rxaudiolabel";
			rxaudiolabel.Size = new Size(52, 15);
			rxaudiolabel.TabIndex = 5;
			rxaudiolabel.Text = "RxAudio";
			// 
			// daemonportlabel
			// 
			daemonportlabel.AutoSize = true;
			daemonportlabel.Location = new Point(32, 170);
			daemonportlabel.Name = "daemonportlabel";
			daemonportlabel.Size = new Size(77, 15);
			daemonportlabel.TabIndex = 6;
			daemonportlabel.Text = "Daemon Port";
			// 
			// daemonaddrlabel
			// 
			daemonaddrlabel.AutoSize = true;
			daemonaddrlabel.Location = new Point(23, 126);
			daemonaddrlabel.Name = "daemonaddrlabel";
			daemonaddrlabel.Size = new Size(97, 15);
			daemonaddrlabel.TabIndex = 7;
			daemonaddrlabel.Text = "Daemon Address";
			// 
			// daemonaddrTXT
			// 
			daemonaddrTXT.Location = new Point(12, 144);
			daemonaddrTXT.Name = "daemonaddrTXT";
			daemonaddrTXT.Size = new Size(121, 23);
			daemonaddrTXT.TabIndex = 8;
			// 
			// daemonptTXT
			// 
			daemonptTXT.Location = new Point(12, 188);
			daemonptTXT.Name = "daemonptTXT";
			daemonptTXT.Size = new Size(121, 23);
			daemonptTXT.TabIndex = 9;
			// 
			// daemonconnectbut
			// 
			daemonconnectbut.Location = new Point(12, 217);
			daemonconnectbut.Name = "daemonconnectbut";
			daemonconnectbut.Size = new Size(121, 23);
			daemonconnectbut.TabIndex = 10;
			daemonconnectbut.Text = "Daemon Connect";
			daemonconnectbut.UseVisualStyleBackColor = true;
			daemonconnectbut.Click += daemonconnectbut_Click;
			// 
			// saveBUT
			// 
			saveBUT.Location = new Point(275, 217);
			saveBUT.Name = "saveBUT";
			saveBUT.Size = new Size(121, 23);
			saveBUT.TabIndex = 11;
			saveBUT.Text = "Save";
			saveBUT.UseVisualStyleBackColor = true;
			saveBUT.Click += saveBUT_Click;
			// 
			// countryTXT
			// 
			countryTXT.Location = new Point(275, 102);
			countryTXT.Name = "countryTXT";
			countryTXT.Size = new Size(121, 23);
			countryTXT.TabIndex = 12;
			// 
			// ctryLAB
			// 
			ctryLAB.AutoSize = true;
			ctryLAB.Location = new Point(208, 105);
			ctryLAB.Name = "ctryLAB";
			ctryLAB.Size = new Size(50, 15);
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
			autostartLEB.Location = new Point(275, 199);
			autostartLEB.Name = "autostartLEB";
			autostartLEB.Size = new Size(73, 15);
			autostartLEB.TabIndex = 14;
			autostartLEB.Text = "AutoStart In:";
			// 
			// asCountLAB
			// 
			asCountLAB.AutoSize = true;
			asCountLAB.Location = new Point(352, 198);
			asCountLAB.Name = "asCountLAB";
			asCountLAB.Size = new Size(19, 15);
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
			ASstopButt.Location = new Point(377, 195);
			ASstopButt.Name = "ASstopButt";
			ASstopButt.Size = new Size(39, 21);
			ASstopButt.TabIndex = 16;
			ASstopButt.Text = "Stop";
			ASstopButt.UseVisualStyleBackColor = true;
			ASstopButt.Click += ASstopButt_Click;
			// 
			// ridinboundBOX
			// 
			ridinboundBOX.Location = new Point(619, 12);
			ridinboundBOX.Name = "ridinboundBOX";
			ridinboundBOX.Size = new Size(100, 23);
			ridinboundBOX.TabIndex = 17;
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new Point(514, 15);
			label1.Name = "label1";
			label1.Size = new Size(99, 15);
			label1.TabIndex = 21;
			label1.Text = "Inbound Radio ID";
			// 
			// MainForm
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			BackgroundImage = Properties.Resources._48450756326_5aab7b8186_b;
			BackgroundImageLayout = ImageLayout.Center;
			ClientSize = new Size(800, 450);
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
			Controls.Add(comboBox2);
			Controls.Add(comboBox1);
			Controls.Add(serialportlistbox);
			Controls.Add(connectbutton);
			Name = "MainForm";
			Text = "MainForm";
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Button connectbutton;
		private ComboBox serialportlistbox;
		private ComboBox comboBox1;
		private ComboBox comboBox2;
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
	}
}