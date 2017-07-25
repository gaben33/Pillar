using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Pillar3D {
	class Time {
		public static float DeltaTime { get; private set; }
		public static float RealTime { get; private set; }
		public static float ScaledTime { get; private set; }
		public static float TimeScale;
		public static int SampleCount;
		public static long FrameCount { get; private set; }
		public static float SmoothDeltaTime { get; private set; }

		private Stopwatch watch;
		private Queue<float> DTSamples;

		public void Frame() {
			DeltaTime = (float)watch.Elapsed.TotalSeconds - RealTime;
			RealTime = (float)watch.Elapsed.TotalSeconds;
			ScaledTime += DeltaTime * TimeScale;
			FrameCount++;
			while (DTSamples.Count >= SampleCount) DTSamples.Dequeue();
			DTSamples.Enqueue(DeltaTime);
			SmoothDeltaTime = DTSamples.Average();
		}

		public Time() {
			RealTime = ScaledTime = FrameCount = 0;
			TimeScale = 1;
			SampleCount = 500;
			watch = new Stopwatch();
			DTSamples = new Queue<float>();
			watch.Start();
		}

		~Time() {
			watch.Stop();
		}
	}
}
