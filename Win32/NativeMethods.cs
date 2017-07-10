using System;
using System.Runtime.InteropServices;

namespace SolSoft.SleepControl.Win32
{
	internal partial class NativeMethods
	{
		private const string POWRPROF = "powrprof.dll";

		/// Return Type: DWORD->unsigned int
		///RootPowerKey: HKEY->HKEY__*
		///SchemeGuid: GUID*
		///SubGroupOfPowerSettingsGuid: GUID*
		///PowerSettingGuid: GUID*
		///Type: PULONG->ULONG*
		///Buffer: LPBYTE->BYTE*
		///BufferSize: LPDWORD->DWORD*
		[DllImport(POWRPROF, EntryPoint = "PowerReadACValue", SetLastError = true)]
		public static extern uint PowerReadACValue(
			//This parameter is reserved for future use and must be set to NULL.
			[In][Optional]IntPtr RootPowerKey,
			//The identifier of the power scheme
			[In][Optional]PowerPlanHandle SchemeGuid,
			//The subgroup of power settings.This parameter can be one of the following values defined in WinNT.h.Use NO_SUBGROUP_GUID to retrieve the setting for the default power scheme.
			[In][Optional]ref Guid SubGroupOfPowerSettingsGuid,
			//The identifier of the power setting.
			[In][Optional]ref Guid PowerSettingGuid,
			//A pointer to a variable that receives the type of data for the value. The possible values are listed in Registry Value Types.This parameter can be NULL and the type of data is not returned.
			[Out][Optional]out RegistryTypes Type,
			//A pointer to a buffer that receives the data value. If this parameter is NULL, the BufferSize parameter receives the required buffer size.
			[Out][Optional]IntPtr Buffer,
			//A pointer to a variable that contains the size of the buffer pointed to by the Buffer parameter. 
			//If the Buffer parameter is NULL, the function returns ERROR_SUCCESS and the variable receives the required buffer size. 
			//If the specified buffer size is not large enough to hold the requested data, the function returns ERROR_MORE_DATA and the variable receives the required buffer size.
			[In][Out][Optional]ref uint BufferSize);

		/// Return Type: DWORD->unsigned int
		///RootPowerKey: HKEY->HKEY__*
		///SchemeGuid: GUID*
		///SubGroupOfPowerSettingsGuid: GUID*
		///PowerSettingGuid: GUID*
		///AcValueIndex: DWORD->unsigned int
		[DllImport(POWRPROF, EntryPoint = "PowerWriteACValueIndex", SetLastError = true)]
		public static extern uint PowerWriteACValueIndex(
			//This parameter is reserved for future use and must be set to NULL.
			[In][Optional]IntPtr RootPowerKey,
			//The identifier of the power scheme
			[In]PowerPlanHandle SchemeGuid,
			//The subgroup of power settings.This parameter can be one of the following values defined in WinNT.h.Use NO_SUBGROUP_GUID to retrieve the setting for the default power scheme.
			[In][Optional]ref Guid SubGroupOfPowerSettingsGuid,
			//The identifier of the power setting.
			[In][Optional]ref Guid PowerSettingGuid,
			//The AC value index.
			[In]uint AcValueIndex);


		/// Return Type: DWORD->unsigned int
		///UserRootPowerKey: HKEY->HKEY__*
		///ActivePolicyGuid: GUID**
		[DllImport(POWRPROF, EntryPoint = "PowerGetActiveScheme", SetLastError = true)]
		public static extern uint PowerGetActiveScheme(
			//This parameter is reserved for future use and must be set to NULL.
			[In][Optional]IntPtr UserRootPowerKey,
			//A pointer that receives a pointer to a GUID structure. Use the LocalFree function to free this memory.
			[Out]out PowerPlanHandle ActivePolicyGuid);

		/// Return Type: DWORD->unsigned int
		///UserRootPowerKey: HKEY->HKEY__*
		///SchemeGuid: GUID*
		[DllImport(POWRPROF, EntryPoint = "PowerSetActiveScheme", SetLastError = true)]
		public static extern uint PowerSetActiveScheme(
			//This parameter is reserved for future use and must be set to NULL.
			[In][Optional]IntPtr UserRootPowerKey,
			//The identifier of the power scheme.
			[In]PowerPlanHandle SchemeGuid);

	}

}

