using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Windows.Input;
using Microsoft.Win32;
using SolSoft.DataBinding;
using SolSoft.SleepControl.Win32;
using SolSoft.Utilities.UI.Wpf.Commands;

namespace SolSoft.SleepControl
{
	public class MainViewModel : IRaisePropertyChanged, INotifyPropertyChanged, IDisposable
	{
		public event EventHandler<EventArgs> ShowFormRequested;
		public event EventHandler<EventArgs> ExitRequested;

		private readonly ISettings m_settings;

		public MainViewModel(ISettings settings)
		{
			if (settings == null)
				throw new ArgumentNullException(nameof(settings));
			this.m_settings = settings;


			IsPluggedInProperty = new NotifyProperty<bool>(this, nameof(IsPluggedIn));


			IsAwayModeEnabledProperty = new NotifyProperty<bool>(this, nameof(IsAwayModeEnabled));
			ToggleAwayModeEnabledCommand = new DelegateCommand(ToggleAwayModeEnabled, CanToggleAwayModeEnabled);


			IsAwayModeSleepOverrideActivatedProperty = new NotifyProperty<bool>(this, nameof(IsAwayModeSleepOverrideActivated), m_settings.IsAwayModeSleepOverrideActivated);
			ToggleAwayModeSleepOverrideCommand = new DelegateCommand(ToggleAwayModeSleepOverride, CanToggleAwayModeSleepOverride);


			EffectiveIsAwayModeSleepOverrideActivatedProperty = this.CreateDerivedNotifyProperty(nameof(EffectiveIsAwayModeSleepOverrideActivated),
				IsAwayModeEnabledProperty, IsAwayModeSleepOverrideActivatedProperty,
				(enabled, active) => enabled && active);


			KeepDisplayOnProperty = new NotifyProperty<bool>(this, nameof(KeepDisplayOn), m_settings.KeepDisplayOn);
			ToggleKeepDisplayOnCommand = new DelegateCommand(ToggleKeepDisplayOn, CanToggleKeepDisplayOn);


			IsIdleOverrideActivatedProperty = new NotifyProperty<bool>(this, nameof(IsIdleOverrideActivated), m_settings.IsIdleOverrideActivated);
			ToggleIdleOverrideCommand = new DelegateCommand(ToggleIdleOverride, CanToggleIdleOverride);


			EffectiveExecutionStateProperty = this.CreateDerivedNotifyProperty(nameof(EffectiveExecutionState),
				EffectiveIsAwayModeSleepOverrideActivatedProperty, KeepDisplayOnProperty, IsIdleOverrideActivatedProperty,
				CalculateEffectiveExecutionState);

		}

		public void RequestShowForm()
		{
			OnShowFormRequested(EventArgs.Empty);
		}
		protected virtual void OnShowFormRequested(EventArgs args)
		{
			ShowFormRequested?.Invoke(this, args);
		}

		public void RequestExit()
		{
			OnExitRequested(EventArgs.Empty);
		}
		protected virtual void OnExitRequested(EventArgs args)
		{
			ExitRequested?.Invoke(this, args);
		}

		public void Initialize()
		{
			SystemEvents.PowerModeChanged += SystemEvents_PowerModeChanged;
			UpdateIsPluggedInProperty();
			UpdateAwayModeEnabledProperty();
			ApplyCurrentThreadExecutionState();
			CommandManager.InvalidateRequerySuggested();
		}

		private void SystemEvents_PowerModeChanged(object sender, PowerModeChangedEventArgs e)
		{
			UpdateIsPluggedInProperty();
		}
		private void UpdateIsPluggedInProperty()
		{
			IsPluggedInProperty.SetValue(SystemInformation.PowerStatus.PowerLineStatus == PowerLineStatus.Online);
			CommandManager.InvalidateRequerySuggested();
		}
		private NotifyProperty<bool> IsPluggedInProperty { get; }
		public bool IsPluggedIn => IsPluggedInProperty.Value;

		#region AwayModeEnabled
		private void UpdateAwayModeEnabledProperty()
		{
			//check the current power configuration
			bool currentAwayModeEnabledSetting;
			using (PowerPlan powerPlan = PowerPlan.GetActivePlan())
			{
				object currentAwayModeEnabledSettingObject = powerPlan.PowerReadACValue(PowerPlan.GUID_SLEEP_SUBGROUP, PowerPlan.AwayModeSetting);
				currentAwayModeEnabledSetting = 1.Equals(currentAwayModeEnabledSettingObject);

				IsAwayModeEnabledProperty.SetValue(currentAwayModeEnabledSetting);
			}
		}
		private NotifyProperty<bool> IsAwayModeEnabledProperty { get; }
		public bool IsAwayModeEnabled => IsAwayModeEnabledProperty.Value;

		public ICommand ToggleAwayModeEnabledCommand
		{
			get;
		}
		private void ToggleAwayModeEnabled(object _)
		{
			bool currentAwayModeEnabledSetting = IsAwayModeEnabled;

			bool desiredAwayModeEnabledSetting = !currentAwayModeEnabledSetting;

			//set value in current plan
			using (PowerPlan powerPlan = PowerPlan.GetActivePlan())
			{
				uint newSettingUint;
				if (desiredAwayModeEnabledSetting)
					newSettingUint = 1;
				else
					newSettingUint = 0;

				powerPlan.PowerWriteACValueIndex(PowerPlan.GUID_SLEEP_SUBGROUP, PowerPlan.AwayModeSetting, newSettingUint);
				powerPlan.ApplyCurrentValuesToSystem();
			}

			//read again from system
			UpdateAwayModeEnabledProperty();
			ApplyCurrentThreadExecutionState();
			CommandManager.InvalidateRequerySuggested();
		}
		private bool CanToggleAwayModeEnabled(object _) => IsPluggedIn;
		#endregion


		#region AwayModeSleepOverrideActivated
		private NotifyProperty<bool> IsAwayModeSleepOverrideActivatedProperty { get; }
		public bool IsAwayModeSleepOverrideActivated => IsAwayModeSleepOverrideActivatedProperty.Value;

		public ICommand ToggleAwayModeSleepOverrideCommand
		{
			get;
		}
		private void ToggleAwayModeSleepOverride(object _)
		{
			IsAwayModeSleepOverrideActivatedProperty.SetValue(!IsAwayModeSleepOverrideActivated);
			m_settings.IsAwayModeSleepOverrideActivated = IsAwayModeSleepOverrideActivated;
			m_settings.Save();
			ApplyCurrentThreadExecutionState();
			CommandManager.InvalidateRequerySuggested();
		}
		private bool CanToggleAwayModeSleepOverride(object _) => IsPluggedIn && IsAwayModeEnabled;
		#endregion


		private DerivedNotifyProperty<bool> EffectiveIsAwayModeSleepOverrideActivatedProperty { get; }
		public bool EffectiveIsAwayModeSleepOverrideActivated => EffectiveIsAwayModeSleepOverrideActivatedProperty.Value;


		#region IdleOverride
		private NotifyProperty<bool> IsIdleOverrideActivatedProperty { get; }
		public bool IsIdleOverrideActivated => IsIdleOverrideActivatedProperty.Value;

		public ICommand ToggleIdleOverrideCommand
		{
			get;
		}
		private void ToggleIdleOverride(object _)
		{
			IsIdleOverrideActivatedProperty.SetValue(!IsIdleOverrideActivated);
			m_settings.IsIdleOverrideActivated = IsIdleOverrideActivated;
			m_settings.Save();
			ApplyCurrentThreadExecutionState();
			CommandManager.InvalidateRequerySuggested();
		}
		private bool CanToggleIdleOverride(object _) => true;
		#endregion


		#region DisplayIdleOverride
		private NotifyProperty<bool> KeepDisplayOnProperty { get; }
		public bool KeepDisplayOn => KeepDisplayOnProperty.Value;

		public ICommand ToggleKeepDisplayOnCommand
		{
			get;
		}
		private void ToggleKeepDisplayOn(object _)
		{
			KeepDisplayOnProperty.SetValue(!KeepDisplayOn);
			m_settings.KeepDisplayOn = KeepDisplayOn;
			m_settings.Save();
			ApplyCurrentThreadExecutionState();
		}
		private bool CanToggleKeepDisplayOn(object _) => true;
		#endregion


		#region CurrentExecutionState
		private DerivedNotifyProperty<EXECUTION_STATE> EffectiveExecutionStateProperty { get; }
		internal EXECUTION_STATE EffectiveExecutionState => EffectiveExecutionStateProperty.Value;
		private EXECUTION_STATE CalculateEffectiveExecutionState(bool effectiveIsAwayModeActivated, bool keepDisplayOn, bool isIdleOverrideActivated)
		{
			EXECUTION_STATE toReturn = EXECUTION_STATE.ES_CONTINUOUS;
			if (effectiveIsAwayModeActivated)
				toReturn |= EXECUTION_STATE.ES_AWAYMODE_REQUIRED;
			if (isIdleOverrideActivated)
				toReturn |= EXECUTION_STATE.ES_SYSTEM_REQUIRED;
			if (keepDisplayOn)
				toReturn |= EXECUTION_STATE.ES_DISPLAY_REQUIRED;
			return toReturn;
		}
		private void ApplyCurrentThreadExecutionState()
		{
			NativeMethods.SetThreadExecutionState(EffectiveExecutionState);
		}
		#endregion

		// PART 1: required for any class that implements INotifyPropertyChanged
		public event PropertyChangedEventHandler PropertyChanged;
		protected virtual void OnPropertyChanged(PropertyChangedEventArgs args)
		{
			// In C# 6, you can use PropertyChanged?.Invoke.
			// Otherwise I'd suggest an extension method.
			PropertyChanged?.Invoke(this, args);
		}
		// PART 2: IRaisePropertyChanged-specific
		protected virtual void RaisePropertyChanged(string propertyName)
		{
			OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
		}
		// This method is only really for the sake of the interface,
		// not for general usage, so I implement it explicitly.
		void IRaisePropertyChanged.RaisePropertyChanged(string propertyName)
		{
			this.RaisePropertyChanged(propertyName);
		}

		#region IDisposable Support
		private bool disposedValue = false; // To detect redundant calls

		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					SystemEvents.PowerModeChanged -= SystemEvents_PowerModeChanged;
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