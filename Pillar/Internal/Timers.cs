using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pillar3D.Coroutines;

namespace Pillar3D {
	public class Timers {
		public static void AddTimer(float duration, Action callback) => RoutineRunner.RunRoutine(RunTimer(duration, callback));

		public static void AddTimer(int frames, Action callback) => RoutineRunner.RunRoutine(RunTimer(frames, callback));

		private static IEnumerator<YieldInstruction> RunTimer(float duration, Action callback) {
			yield return new WaitForSeconds(duration);
			callback();
		}

		private static IEnumerator<YieldInstruction> RunTimer(int frames, Action callback) {
			for (int i = 0; i < frames; i++) yield return null;
			callback();
		}
	}
}