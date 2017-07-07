using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pillar3D.Coroutines {
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

	public class Frame : YieldInstruction {
		public override Instruction GetInstruction() {
			if (!framed) {
				framed = true;
				return Instruction.wait;
			} else return Instruction.resume;
		}

		private bool framed;
		public Frame() {
			framed = false;
		}
	}


}