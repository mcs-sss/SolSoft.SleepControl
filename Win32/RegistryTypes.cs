using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolSoft.SleepControl.Win32
{
	enum RegistryTypes : int
	{
		/// REG_SZ -> ( 1 )
		REG_SZ = 1,

		/// REG_QWORD_LITTLE_ENDIAN -> ( 11 )
		REG_QWORD_LITTLE_ENDIAN = 11,

		//A 64-bit number.
		/// REG_QWORD -> ( 11 )
		REG_QWORD = 11,

		/// REG_NONE -> ( 0 )
		REG_NONE = 0,

		/// REG_DWORD_BIG_ENDIAN -> ( 5 )
		REG_DWORD_BIG_ENDIAN = 5,

		/// REG_MULTI_SZ -> ( 7 )
		REG_MULTI_SZ = 7,

		/// REG_LINK -> ( 6 )
		REG_LINK = 6,

		/// REG_EXPAND_SZ -> ( 2 )
		REG_EXPAND_SZ = 2,

		//A 32-bit number in little-endian format.
		//Windows is designed to run on little-endian computer architectures.Therefore, this value is defined as REG_DWORD in the Windows header files.
		/// REG_DWORD_LITTLE_ENDIAN -> ( 4 )
		REG_DWORD_LITTLE_ENDIAN = 4,

		//A 32-bit number.
		/// REG_DWORD -> ( 4 )
		REG_DWORD = 4,

		//Binary data in any form.
		/// REG_BINARY -> ( 3 )
		REG_BINARY = 3,
	}

}
