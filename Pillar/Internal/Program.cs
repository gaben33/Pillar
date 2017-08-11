using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Input;
using Pillar3D.Internal;

namespace Pillar3D {
	//manages iterative update delegates
	public class Rails {
		public static bool GlobalPause = false;
		private static bool exit = false;

		public static Action GlobalUpdate, GlobalPersistantUpdate;

		public Action Initialize, Update, PersistantUpdate;
		public bool Paused = false;
		protected internal static Dictionary<Level, Queue<Entity>> DebasedObjectQueue = new Dictionary<Level, Queue<Entity>>();
		public Rails() {
			Paused = false;
		}

		public static void Exit () {
			exit = true;
		}

		[STAThread]
		static int Main(string[] args) {
			Application app = new Application("New Pillar Game");
			Time time = new Time();
			GlobalPersistantUpdate = time.Frame;
			RoutineRunner runner = new RoutineRunner();
			Level main = new Level("main");
			ThreadManager.AddLevel(main);
			while (!exit) {
				if (!GlobalPause) GlobalUpdate?.Invoke();
				GlobalPersistantUpdate();
				if (ThreadManager.Frame()) Thread.Sleep(1);
			}
			return 0;
		}

		//changes the level in which the entity is housed
		public static void ChangeLevel (Entity entity, Level newLevel) {
			entity.OnLevelChanged();
			entity.OnParentChanged();
			entity.ContainerLevel.Root.Children.Remove(entity);
			DebasedObjectQueue[newLevel].Enqueue(entity);
		}
	}

	public class ThreadManager {
		static List<TrackedThread> threads = new List<TrackedThread>();

		public static void AddLevel (Level level) {
			if (threads.Count == 0 || threads.Count < Environment.ProcessorCount) threads.Add(new TrackedThread(level));
			else {
				TrackedThread lowestThread = threads[0];
				for(int i = 1; i < threads.Count; i++) if (threads[i].Stress < lowestThread.Stress) lowestThread = threads[i];
				lowestThread.AddLevel(level);
			}
			Rails.DebasedObjectQueue.Add(level, new Queue<Entity>());
		}

		public static void AddLevel (Level level, int thread) {
			thread %= Environment.ProcessorCount;
			if (threads.Count < thread) threads.Add(new TrackedThread(level));
			else threads[thread].AddLevel(level);
			Rails.DebasedObjectQueue.Add(level, new Queue<Entity>());
		}

		public static bool GetThreadsIdle () {
			if (threads.Count == 0) return false;
			for (int i = 0; i < threads.Count; i++) if (threads[i].thread.IsAlive) return false;
			return true;
		}

		//returns if there is a need to wait
		public static bool Frame () {
			switch(Settings.VSync) {
				case SyncState.Off:
					for (int i = 0; i < threads.Count; i++) if(!threads[i].thread.IsAlive) threads[i].Frame();
						return false;
				case SyncState.SyncThreads:
					if (GetThreadsIdle()) for (int i = 0; i < threads.Count; i++) threads[i].Frame();
					else return true;
					return false;
				case SyncState.ScreenRefresh:
					bool canRefresh = 1f / Time.DeltaTime < Application.RefreshRate;
					if(canRefresh && GetThreadsIdle()) for (int i = 0; i < threads.Count; i++) threads[i].Frame();
					return canRefresh;
				default:
					for (int i = 0; i < threads.Count; i++) if (!threads[i].thread.IsAlive) threads[i].Frame();
					return false;
			}
		}
	}
}