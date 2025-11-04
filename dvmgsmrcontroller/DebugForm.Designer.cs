namespace dvmgsmrcontroller
{
	partial class DebugForm
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
			StopPTT = new Button();
			StartPTT = new Button();
			SuspendLayout();
			// 
			// StopPTT
			// 
			StopPTT.Location = new Point(189, 12);
			StopPTT.Name = "StopPTT";
			StopPTT.Size = new Size(112, 34);
			StopPTT.TabIndex = 0;
			StopPTT.Text = "Stop PTT";
			StopPTT.UseVisualStyleBackColor = true;
			StopPTT.Click += StopPTT_Click;
			// 
			// StartPTT
			// 
			StartPTT.Location = new Point(189, 65);
			StartPTT.Name = "StartPTT";
			StartPTT.Size = new Size(112, 34);
			StartPTT.TabIndex = 1;
			StartPTT.Text = "StartPTT";
			StartPTT.UseVisualStyleBackColor = true;
			StartPTT.Click += StartPTT_Click;
			// 
			// DebugForm
			// 
			AutoScaleDimensions = new SizeF(10F, 25F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(800, 450);
			Controls.Add(StartPTT);
			Controls.Add(StopPTT);
			Name = "DebugForm";
			Text = "DebugForm";
			ResumeLayout(false);
		}

		#endregion

		private Button StopPTT;
		private Button StartPTT;
	}
}