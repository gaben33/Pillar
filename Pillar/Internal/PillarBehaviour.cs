using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pillar3D {
	public abstract class PillarBehaviour : Component {
		#region internal
		public bool Enabled;
		public PillarBehaviour() : base(true) {
			base.Update += RunUpdate;
			Container.OnLevelChanged += Rebase;
			Start();
		}

		public abstract void Start();
		private void RunUpdate() {
			if (Enabled) {
				if (attachedCoroutines.Count > 0) {
					FrameCoroutines();
				}
				Update();
			}
		}

		new public abstract void Update();

		//for when entity is moved between levels
		private void Rebase() => base.Update -= RunUpdate;
		#endregion
		#region coroutine support
		private List<IEnumerator<YieldInstruction>> attachedCoroutines = new List<IEnumerator<YieldInstruction>>();
		protected void StartCoroutine(IEnumerator<YieldInstruction> routine) => attachedCoroutines.Add(routine);

		protected void StopCoroutine(IEnumerator<YieldInstruction> routine) => attachedCoroutines.Remove(routine);

		protected void StopAllCoroutines() => attachedCoroutines.Clear();

		private void FrameCoroutines() => RoutineRunner.FrameSubset(attachedCoroutines);
		#endregion
		#region invocation
		protected void Invoke(float startTime, Action callback) => Timers.AddTimer(startTime, callback);

		#endregion
		#region misc utils
		protected void Print(object o) => Console.WriteLine(o);
		protected void Print() => Console.WriteLine();
		#endregion
	}
}
