using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SolSoft.SleepControl.Win32
{
	/// <summary>
	/// Encapsulates a pointer to an unmanaged GUID for a power scheme
	/// </summary>
	class PowerPlanHandle : SafeHandle
	{
		// Create a SafeHandle, informing the base class
		// that this SafeHandle instance "owns" the handle,
		// and therefore SafeHandle should call
		// our ReleaseHandle method when the SafeHandle
		// is no longer in use.
		private PowerPlanHandle()
			: base(IntPtr.Zero, true)
		{
		}

		public override bool IsInvalid
		{
			get
			{
				return this.handle == IntPtr.Zero;
			}
		}

		protected override bool ReleaseHandle()
		{
			//A pointer that receives a pointer to a GUID structure. Use the LocalFree function to free this memory.
			//FreeHGlobal exposes the LocalFree function from Kernel32.DLL, which frees all bytes so that you can no longer use the memory pointed to by hglobal.
			Marshal.FreeHGlobal(this.handle);
			return true;
		}
	}
}
