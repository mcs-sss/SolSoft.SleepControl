using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SolSoft.SleepControl.Win32
{
	static partial class NativeMethods
	{
		[DllImport("kernel32.dll", EntryPoint = "SetThreadExecutionState", SetLastError = true)]
		public static extern EXECUTION_STATE SetThreadExecutionState(EXECUTION_STATE esFlags);
	}

	[Flags]
	enum EXECUTION_STATE : uint
	{
		ES_SYSTEM_REQUIRED = 0x00000001,
		ES_DISPLAY_REQUIRED = 0x00000002,
		ES_CONTINUOUS = 0x80000000,
		ES_AWAYMODE_REQUIRED = 0x00000040,
	}
}
