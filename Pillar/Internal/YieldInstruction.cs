using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pillar3D {
	public abstract class YieldInstruction {
		public abstract Instruction GetInstruction();
	}
	public enum Instruction { wait, resume, exit }

	public class RoutineRunner {
		private static List<IEnumerator<YieldInstruction>> routines = new List<IEnumerator<YieldInstruction>>(50);
		public RoutineRunner() {
			Rails.GlobalPersistantUpdate += Frame;
		}

		~RoutineRunner() {
			Rails.GlobalPersistantUpdate -= Frame;
		}

		//advance one frame
		public void Frame() {
			FrameSubset(routines);
		}

		public static void FrameSubset(List<IEnumerator<YieldInstruction>> subset) {
			for (int i = 0; i < subset.Count; i++) {
				YieldInstruction current = subset[i].Current;
				if (current == null) {
					subset[i].MoveNext();
					continue;
				}
				Instruction inst = current.GetInstruction();
				switch (inst) {
					case (Instruction.exit):
						subset.RemoveAt(i);
						break;
					case (Instruction.resume):
						if (!subset[i].MoveNext()) {
							subset.RemoveAt(i);
						}
						break;
					case (Instruction.wait):
						break;
				}
			}
		}

		public static void RunRoutine(IEnumerator<YieldInstruction> routine) => routines.Add(routine);
		public static void StopRoutine(IEnumerator<YieldInstruction> routine) => routines.Remove(routine);
	}
}
