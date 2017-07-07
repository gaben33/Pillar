using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pillar3D {
	public abstract class YieldInstruction {
		public abstract Instruction GetInstruction ();
	}
	public enum Instruction { wait, resume, exit }

	public class RoutineRunner {
		private static List<IEnumerator<YieldInstruction>> routines;
		public RoutineRunner () {
			routines = new List<IEnumerator<YieldInstruction>>(50);
		}

		//advance one frame
		public void Frame () {
			for (int i = 0; i < routines.Count; i++) {
				YieldInstruction current = routines[i].Current;
				if (current == null) {
					routines[i].MoveNext();
					continue;
				}
				Instruction inst = current.GetInstruction();
				switch (inst) {
					case ( Instruction.exit ):
						routines.RemoveAt(i);
						break;
					case ( Instruction.resume ):
						if(!routines[i].MoveNext()) {
							routines.RemoveAt(i);
						}
						break;
					case ( Instruction.wait ):
						break;
				}
			}
		}

		public static void RunRoutine (IEnumerator<YieldInstruction> routine) {
			routines.Add(routine);
		}
	}
}
