using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Pillar3D.Internal {
	public class TrackedThread {
		public int Stress = 0;
		public List<Level> Levels = new List<Level>();
		protected internal Thread thread;
		public TrackedThread(Level level) {
			Levels.Add(level);
			Stress += level.Rail.Update.GetInvocationList().Length + level.Rail.PersistantUpdate.GetInvocationList().Length;
			thread = new Thread(new ThreadStart(level.Rail.Initialize));
			thread.Start();
		}

		public void AddLevel (Level level) {
			Levels.Add(level);
			level.Rail.Initialize?.Invoke();
			Stress += level.Rail.Update.GetInvocationList().Length + level.Rail.PersistantUpdate.GetInvocationList().Length;
		}

		public void Frame () {
			Action del = new Action(Levels[0].Frame);
			for (int i = 1; i < Levels.Count; i++) {
				del += Levels[i].Frame;
				while (Rails.DebasedObjectQueue[Levels[i]].Count > 0) Levels[i].Root.Children.Add(Rails.DebasedObjectQueue[Levels[i]].Dequeue());
			}
			thread = new Thread(new ThreadStart(del));
			thread.Start();
		}

		//pauses the thread to allow Gameobjects in the queue to filter into the level
		public void Repopulate () {
			
		}
	}
}
