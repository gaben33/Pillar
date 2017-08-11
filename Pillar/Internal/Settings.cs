using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pillar3D {
	public class Settings {
		public static SyncState VSync = SyncState.SyncThreads;
	}

	public enum SyncState {
		Off, SyncThreads, ScreenRefresh
	}
}
