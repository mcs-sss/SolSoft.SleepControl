using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using SolSoft.SleepControl.Win32;

namespace SolSoft.SleepControl
{
	/// <summary>
	/// Encapsulates a power plan
	/// </summary>
	public class PowerPlan : IDisposable
	{
		public static Guid AwayModeSetting { get; } = Guid.Parse("25dfa149-5dd1-4736-b5ab-e8a37b5b8187");
		public static Guid GUID_SLEEP_SUBGROUP { get; }= Guid.Parse("238C9FA8-0AAD-41ED-83F4-97BE242C8F20");

		public static PowerPlan GetActivePlan()
		{
			PowerPlanHandle currentPlanHandle;
			HandlePossibleErrorResult(NativeMethods.PowerGetActiveScheme(IntPtr.Zero, out currentPlanHandle));

			return new PowerPlan(currentPlanHandle);
		}

		private PowerPlanHandle m_planHandle;

		PowerPlan(PowerPlanHandle currentPlanHandle)
		{
			if (currentPlanHandle == null)
				throw new ArgumentNullException(nameof(currentPlanHandle));
			this.m_planHandle = currentPlanHandle;
		}

		
		public object PowerReadACValue(Guid subGroupOfPowerSettingsGuid, Guid powerSettingGuid)
		{
			RegistryTypes type;
			uint bufferSize = 0;

			//get necessary buffer size by passing in "null" pointer to buffer
			HandlePossibleErrorResult(NativeMethods.PowerReadACValue(IntPtr.Zero, this.m_planHandle, ref subGroupOfPowerSettingsGuid, ref powerSettingGuid, out type, IntPtr.Zero, ref bufferSize));

			//pass in and get the buffer populated
			byte[] buffer = new byte[bufferSize];
			GCHandle bufferHandle = default(GCHandle);
			try
			{
				//get pointer to the buffer
				bufferHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
				IntPtr bufferPointer = bufferHandle.AddrOfPinnedObject();
				HandlePossibleErrorResult(NativeMethods.PowerReadACValue(IntPtr.Zero, this.m_planHandle, ref subGroupOfPowerSettingsGuid, ref powerSettingGuid, out type, bufferPointer, ref bufferSize));
			}
			finally
			{
				if (bufferHandle.IsAllocated)
					bufferHandle.Free();
			}


			switch (type)
			{
				case RegistryTypes.REG_DWORD:
					return BitConverter.ToInt32(buffer, 0);

				case RegistryTypes.REG_BINARY:
					return buffer;

				case RegistryTypes.REG_QWORD:
					return BitConverter.ToInt64(buffer, 0);

				default:
					throw new NotSupportedException();
			}
		}

		public void PowerWriteACValueIndex(Guid subGroupOfPowerSettingsGuid, Guid powerSettingGuid, uint value)
		{
			HandlePossibleErrorResult(NativeMethods.PowerWriteACValueIndex(IntPtr.Zero, this.m_planHandle, ref subGroupOfPowerSettingsGuid, ref powerSettingGuid, value));
		}

		public void ApplyCurrentValuesToSystem()
		{
			HandlePossibleErrorResult(NativeMethods.PowerSetActiveScheme(IntPtr.Zero, this.m_planHandle));
		}

		private static void HandlePossibleErrorResult(uint result)
		{
			if (result != 0)
				throw new Win32Exception((int)result);
		}

		#region IDisposable Support
		private bool disposedValue = false; // To detect redundant calls

		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					m_planHandle.Dispose();
				}

				m_planHandle = null;
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
