using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Pillar3D {
	class Time {
		public static float DeltaTime, RealTime, TimeScale, ScaledTime;
		public static long FrameCount;

		private Stopwatch watch;

		public void Frame () {
			DeltaTime = (float) watch.Elapsed.TotalSeconds - RealTime;
			RealTime = (float) watch.Elapsed.TotalSeconds;
			ScaledTime += DeltaTime * TimeScale;
			FrameCount++;
		}

		public Time () {
			RealTime = ScaledTime = FrameCount = 0;
			TimeScale = 1;
			watch = new Stopwatch();
			watch.Start();
		}

		~Time () {
			watch.Stop();
		}
	}
}
