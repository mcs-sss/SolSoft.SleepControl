using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SolSoft.SleepControl
{
	public partial class NotificationIconForm : Form
	{
		public NotificationIconForm()
		{
			InitializeComponent();
			toolStripSeparator1.Enabled = false;
			toolStripSeparator2.Enabled = false;
		}

		private MainViewModel m_viewModel;
		public void SetViewModel(MainViewModel viewModel)
		{
			if (m_viewModel != null)
			{
				m_viewModel.PropertyChanged -= viewModel_PropertyChanged;
				m_viewModel.ToggleAwayModeEnabledCommand.CanExecuteChanged -= ToggleAwayModeEnabled_CanExecuteChanged;
				m_viewModel.ToggleAwayModeSleepOverrideCommand.CanExecuteChanged -= ToggleAwayModeSleepOverride_CanExecuteChanged;
				m_viewModel.ToggleIdleOverrideCommand.CanExecuteChanged -= ToggleIdleOverride_CanExecuteChanged;
			}

			m_viewModel = viewModel;
			if (viewModel != null)
			{
				m_viewModel.PropertyChanged += viewModel_PropertyChanged;
				m_viewModel.ToggleAwayModeEnabledCommand.CanExecuteChanged += ToggleAwayModeEnabled_CanExecuteChanged;
				m_viewModel.ToggleAwayModeSleepOverrideCommand.CanExecuteChanged += ToggleAwayModeSleepOverride_CanExecuteChanged;
				m_viewModel.ToggleIdleOverrideCommand.CanExecuteChanged += ToggleIdleOverride_CanExecuteChanged;

				//update all UI states
				SyncUIWithModel(null);
			}

			contextMenuStrip.Enabled = (m_viewModel != null);
		}

		private void ToggleIdleOverride_CanExecuteChanged(object sender, EventArgs e)
		{
			SyncUIWithModel(nameof(m_viewModel.ToggleIdleOverrideCommand));
		}

		private void ToggleAwayModeSleepOverride_CanExecuteChanged(object sender, EventArgs e)
		{
			SyncUIWithModel(nameof(m_viewModel.ToggleAwayModeSleepOverrideCommand));
		}

		private void ToggleAwayModeEnabled_CanExecuteChanged(object sender, EventArgs e)
		{
			SyncUIWithModel(nameof(m_viewModel.ToggleAwayModeEnabledCommand));
		}

		private void viewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			SyncUIWithModel(e.PropertyName);
		}

		//tool strip stuff can't be data-bound, so we effect it ourselves here
		private void SyncUIWithModel(string changedPropertyNameOrNullForEverything)
		{
			if (IsChanged(changedPropertyNameOrNullForEverything, nameof(MainViewModel.IsPluggedIn)))
			{
				systemIsOnBatteryToolStripMenuItem.Visible = !m_viewModel.IsPluggedIn;
				toolStripSeparator1.Visible = !m_viewModel.IsPluggedIn;
			}

			if (IsChanged(changedPropertyNameOrNullForEverything, nameof(MainViewModel.IsAwayModeEnabled)))
				enableAwayModeToolStripMenuItem.Checked = m_viewModel.IsAwayModeEnabled;
			if (IsChanged(changedPropertyNameOrNullForEverything, nameof(m_viewModel.ToggleAwayModeEnabledCommand)))
				enableAwayModeToolStripMenuItem.Enabled = m_viewModel.ToggleAwayModeEnabledCommand.CanExecute(null);

			if (IsChanged(changedPropertyNameOrNullForEverything, nameof(MainViewModel.IsAwayModeSleepOverrideActivated)))
				useAwayModeToolStripMenuItem.Checked = m_viewModel.IsAwayModeSleepOverrideActivated;
			if (IsChanged(changedPropertyNameOrNullForEverything, nameof(m_viewModel.ToggleAwayModeSleepOverrideCommand)))
				useAwayModeToolStripMenuItem.Enabled = m_viewModel.ToggleAwayModeSleepOverrideCommand.CanExecute(null);

			if (IsChanged(changedPropertyNameOrNullForEverything, nameof(MainViewModel.KeepDisplayOn)))
				keepDisplayOnToolStripMenuItem.Checked = m_viewModel.KeepDisplayOn;
			if (IsChanged(changedPropertyNameOrNullForEverything, nameof(m_viewModel.ToggleKeepDisplayOnCommand)))
				keepDisplayOnToolStripMenuItem.Enabled = m_viewModel.ToggleKeepDisplayOnCommand.CanExecute(null);

			if (IsChanged(changedPropertyNameOrNullForEverything, nameof(MainViewModel.IsIdleOverrideActivated)))
				blockIdleToolStripMenuItem.Checked = m_viewModel.IsIdleOverrideActivated;
			if (IsChanged(changedPropertyNameOrNullForEverything, nameof(m_viewModel.ToggleIdleOverrideCommand)))
				blockIdleToolStripMenuItem.Enabled = m_viewModel.ToggleIdleOverrideCommand.CanExecute(null);
		}
		private static bool IsChanged(string changedPropertyName, string toCompare)
		{
			return String.IsNullOrEmpty(changedPropertyName) 
				|| String.Equals(changedPropertyName, toCompare, StringComparison.Ordinal);
		}


		private void keepDisplayOnToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (m_viewModel != null)
				m_viewModel.ToggleKeepDisplayOnCommand.Execute(null);
		}

		private void enableAwayModeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (m_viewModel != null)
				m_viewModel.ToggleAwayModeEnabledCommand.Execute(null);
		}

		private void useAwayModeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (m_viewModel != null)
				m_viewModel.ToggleAwayModeSleepOverrideCommand.Execute(null);
		}

		private void blockIdleToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (m_viewModel != null)
				m_viewModel.ToggleIdleOverrideCommand.Execute(null);
		}

		private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			m_viewModel?.RequestShowForm();
		}

		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			m_viewModel?.RequestExit();
		}

		
	}
}
