using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using SolSoft.SleepControl.Properties;

namespace SolSoft.SleepControl
{
	//see: https://msdn.microsoft.com/en-us/magazine/mt620013.aspx
	class Program : IDisposable
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			System.Windows.Forms.Application.EnableVisualStyles();
			System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
			SynchronizationContext.SetSynchronizationContext(new DispatcherSynchronizationContext());

			Program p = new Program(Settings.Default);
			p.Start();
		}


		private readonly App m_app;
		private readonly NotificationIconForm m_notificationIconForm;
		private readonly MainWindow m_mainForm;
		private readonly MainViewModel m_viewModel;

		private Program(ISettings settings)
		{
			m_viewModel = new MainViewModel(settings);

			m_app = new App();
			m_app.ShutdownMode = ShutdownMode.OnExplicitShutdown;
			//m_app.InitializeComponent(); //no resources, etc in the XAML so this doesn't exist

			m_mainForm = new MainWindow();
			m_notificationIconForm = new NotificationIconForm(); //show the notification icon
		}

		public void Start()
		{
			//form is not currently useful
			//m_viewModel.ShowFormRequested += ViewModel_ShowFormRequested; 
			m_viewModel.ExitRequested += viewModel_ExitRequested;
			m_viewModel.Initialize();


			//don't quit on form closed
			//m_mainForm.Closed += (sender, e) =>
			//{
			//	m_viewModel.RequestExit();
			//};

			m_mainForm.DataContext = m_viewModel;
			m_notificationIconForm.SetViewModel(m_viewModel);

			m_app.Run();
		}

		private void ViewModel_ShowFormRequested(object sender, EventArgs e)
		{
			m_mainForm.Show();
		}

		void viewModel_ExitRequested(object sender, EventArgs e)
		{
			m_notificationIconForm.Close(); //clean up the notification icon
			m_app.Shutdown();
		}

		#region IDisposable Support
		private bool disposedValue = false; // To detect redundant calls

		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					m_notificationIconForm.Dispose();
					m_viewModel.Dispose();
				}


				disposedValue = true;
			}
		}

		// This code added to correctly implement the disposable pattern.
		public void Dispose()
		{
			// Do not change this code. Put cleanup code in Dispose(bool disposing) above.
			Dispose(true);
			GC.SuppressFinalize(this);
		}
		#endregion


	}

}
