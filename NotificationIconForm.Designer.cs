namespace SolSoft.SleepControl
{
	partial class NotificationIconForm
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NotificationIconForm));
			this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
			this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.systemIsOnBatteryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.enableAwayModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.useAwayModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.blockIdleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.keepDisplayOnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.contextMenuStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// notifyIcon
			// 
			this.notifyIcon.ContextMenuStrip = this.contextMenuStrip;
			this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
			this.notifyIcon.Text = "Sleep Control";
			this.notifyIcon.Visible = true;
			this.notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseDoubleClick);
			// 
			// contextMenuStrip
			// 
			this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.systemIsOnBatteryToolStripMenuItem,
            this.enableAwayModeToolStripMenuItem,
            this.useAwayModeToolStripMenuItem,
            this.toolStripSeparator1,
            this.keepDisplayOnToolStripMenuItem,
            this.blockIdleToolStripMenuItem,
            this.toolStripSeparator2,
            this.exitToolStripMenuItem});
			this.contextMenuStrip.Name = "contextMenuStrip1";
			this.contextMenuStrip.Size = new System.Drawing.Size(239, 170);
			// 
			// systemIsOnBatteryToolStripMenuItem
			// 
			this.systemIsOnBatteryToolStripMenuItem.Enabled = false;
			this.systemIsOnBatteryToolStripMenuItem.Name = "systemIsOnBatteryToolStripMenuItem";
			this.systemIsOnBatteryToolStripMenuItem.Size = new System.Drawing.Size(238, 22);
			this.systemIsOnBatteryToolStripMenuItem.Text = "(System Is On Battery)";
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(235, 6);
			// 
			// enableAwayModeToolStripMenuItem
			// 
			this.enableAwayModeToolStripMenuItem.Name = "enableAwayModeToolStripMenuItem";
			this.enableAwayModeToolStripMenuItem.Size = new System.Drawing.Size(238, 22);
			this.enableAwayModeToolStripMenuItem.Text = "Enable Away &Mode";
			this.enableAwayModeToolStripMenuItem.Click += new System.EventHandler(this.enableAwayModeToolStripMenuItem_Click);
			// 
			// useAwayModeToolStripMenuItem
			// 
			this.useAwayModeToolStripMenuItem.Enabled = false;
			this.useAwayModeToolStripMenuItem.Name = "useAwayModeToolStripMenuItem";
			this.useAwayModeToolStripMenuItem.Size = new System.Drawing.Size(238, 22);
			this.useAwayModeToolStripMenuItem.Text = "Replace Sleep with &Away Mode";
			this.useAwayModeToolStripMenuItem.Click += new System.EventHandler(this.useAwayModeToolStripMenuItem_Click);
			// 
			// blockIdleToolStripMenuItem
			// 
			this.blockIdleToolStripMenuItem.Enabled = false;
			this.blockIdleToolStripMenuItem.Name = "blockIdleToolStripMenuItem";
			this.blockIdleToolStripMenuItem.Size = new System.Drawing.Size(238, 22);
			this.blockIdleToolStripMenuItem.Text = "&Block System Idle";
			this.blockIdleToolStripMenuItem.Click += new System.EventHandler(this.blockIdleToolStripMenuItem_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(235, 6);
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(238, 22);
			this.exitToolStripMenuItem.Text = "&Exit";
			this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
			// 
			// keepDisplayOnToolStripMenuItem
			// 
			this.keepDisplayOnToolStripMenuItem.Enabled = false;
			this.keepDisplayOnToolStripMenuItem.Name = "keepDisplayOnToolStripMenuItem";
			this.keepDisplayOnToolStripMenuItem.Size = new System.Drawing.Size(238, 22);
			this.keepDisplayOnToolStripMenuItem.Text = "Keep &Display On";
			this.keepDisplayOnToolStripMenuItem.Click += new System.EventHandler(this.keepDisplayOnToolStripMenuItem_Click);
			// 
			// NotificationIconForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(284, 261);
			this.Name = "NotificationIconForm";
			this.Text = "NotificationIconForm";
			this.contextMenuStrip.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.NotifyIcon notifyIcon;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem enableAwayModeToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem useAwayModeToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem blockIdleToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem systemIsOnBatteryToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem keepDisplayOnToolStripMenuItem;
	}
}