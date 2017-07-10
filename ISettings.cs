using System.Collections.Generic;

namespace SolSoft.SleepControl
{
	public interface ISettings
	{
		bool IsAwayModeSleepOverrideActivated { get; set; }
		bool IsIdleOverrideActivated { get; set; }
		bool KeepDisplayOn { get; set; }

		void Save();
	}
}