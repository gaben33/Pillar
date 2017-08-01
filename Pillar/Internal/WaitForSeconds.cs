using System;
using System.Timers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Pillar3D.Coroutines {
	public class WaitForSecondsAccurate : YieldInstruction {
		public override Instruction GetInstruction() {
			bool resume = timer.Elapsed.TotalSeconds > time;
			if(resume) {
				timer.Stop();
				return Instruction.resume;
			}
			return Instruction.wait;
		}

		private Stopwatch timer;
		private float time;
		public WaitForSecondsAccurate(float t) {
			timer = new Stopwatch();
			timer.Start();
			time = t;
		}
	}
	public class WaitForSecondsUnscaled : YieldInstruction {
		public override Instruction GetInstruction() {
			return ((Time.RealTime > WaitTime + StartTime) ? Instruction.resume : Instruction.wait);
		}

		private float StartTime, WaitTime;
		public WaitForSecondsUnscaled(float seconds) {
			StartTime = Time.RealTime;
			WaitTime = seconds;
		}
	}

	public class WaitForSeconds : YieldInstruction {
		public override Instruction GetInstruction() {
			return ((Time.ScaledTime > WaitTime + StartTime) ? Instruction.resume : Instruction.wait);
		}

		private float StartTime, WaitTime;
		public WaitForSeconds(float seconds) {
			StartTime = Time.ScaledTime;
			WaitTime = seconds;
		}
	}

	public class WaitUntil : YieldInstruction {
		public override Instruction GetInstruction() {
			return (fn() ? Instruction.resume : Instruction.wait);
		}

		private Func<bool> fn;
		public WaitUntil(Func<bool> condition) {
			fn = condition;
		}
	}

	public class WaitWhile : YieldInstruction {
		public override Instruction GetInstruction() {
			return (fn() ? Instruction.wait : Instruction.resume);
		}

		private Func<bool> fn;
		public WaitWhile(Func<bool> condition) {
			fn = condition;
		}
	}

	public class WaitForSecondsInterruptable : YieldInstruction {
		public override Instruction GetInstruction() {
			return (Time.RealTime > EndTime || Predicate()) ? Instruction.resume : Instruction.wait;
		}

		private float EndTime;
		private Func<bool> Predicate;
		public WaitForSecondsInterruptable(float time, Func<bool> predicate) {
			EndTime = Time.RealTime + time;
			Predicate = predicate;
		}
	}

	public class WaitForSecondsFunctional : YieldInstruction {

		private float EndTime, StartTime, Duration;
		private Action<float> Actor;
		public WaitForSecondsFunctional(float time, Action<float> actor) {
			StartTime = Time.RealTime;
			EndTime = StartTime + time;
			Duration = time;
			Actor = actor;
		}

		public override Instruction GetInstruction() {
			Actor((Time.RealTime - StartTime) / Duration);
			return (Time.RealTime > EndTime) ? Instruction.resume : Instruction.wait;
		}
	}
}