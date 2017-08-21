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

			while (!exit) {
				Input.Poll();
				if (!GlobalPause) GlobalUpdate?.Invoke();
				GlobalPersistantUpdate();
				if (ThreadManager.Frame()) Thread.Sleep(1);

				Console.Write($"\r{Time.SampleCount}: {1f/Time.SmoothDeltaTime}       ");
				if (Input.GetKeyDown(Key.Up)) Time.SampleCount += 100;
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

        public static void Destroy<T>(ref T obj) {
            obj = default(T);
        }
	}

	public class ThreadManager {
		static List<TrackedThread> threads = new List<TrackedThread>();
        private static Queue<Level> queuedLevels = new Queue<Level>();

        public static void AddLevel (Level level) {
            queuedLevels.Enqueue(level);
            Rails.DebasedObjectQueue.Add(level, new Queue<Entity>());
        }

        public static void RemoveLevel (Level level) {
            if (!Rails.DebasedObjectQueue.ContainsKey(level)) throw new ArgumentException("Level has not been added to the thread manager.");
            Rails.DebasedObjectQueue.Remove(level);
            for(int i = 0; i < threads.Count; i++) {
                if(threads[i].Levels.Contains(level)) {
                    threads[i].Levels.Remove(level);
                    return;
                }
            }
        }

        //places all levels in the queue into the thread pool
        private static void Flush () {
            while (queuedLevels.Count > 0) PlaceIndividual(queuedLevels.Dequeue());
        }

        private static void FlushThread (TrackedThread thread) {
            for(int i = 0; i < thread.Levels.Count; i++) {
                queuedLevels.Enqueue(thread.Levels[i]);
            }
            thread.Levels = new List<Level>();
        }

        private static void PlaceIndividual (Level level) {
            if (threads.Count == 0 || threads.Count < Environment.ProcessorCount) threads.Add(new TrackedThread(level));
            else {
                TrackedThread lowestThread = threads[GetLowestStressThread()];
                lowestThread.Stress += level.GetStress();
                lowestThread.AddLevel(level);
            }
        }

        private static void ResetThreadStress () {
            for (int i = 0; i < threads.Count; i++) threads[i].Stress = 0;
        }

        private static int GetLowestStressThread () {
            int lt = 0;
            for (int i = 1; i < threads.Count; i++) if (threads[i].Stress < threads[lt].Stress) lt = i;
            return lt;
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
                    for (int i = 0; i < threads.Count; i++) if (!threads[i].thread.IsAlive) FlushThread(threads[i]);
                    Flush();
					return false;
				case SyncState.SyncThreads:
                    if (GetThreadsIdle()) {
                        for (int i = 0; i < threads.Count; i++) FlushThread(threads[i]);
                        Flush();
                    } else {
                        return true;
                    }
                    Flush();
                    return false;
				case SyncState.ScreenRefresh:
					bool canRefresh = 1f / Time.DeltaTime < Application.RefreshRate;
                    if (canRefresh && GetThreadsIdle()) {
                        for (int i = 0; i < threads.Count; i++) FlushThread(threads[i]);
                        Flush();
                    }
					return canRefresh;
				default:
					for (int i = 0; i < threads.Count; i++) if (!threads[i].thread.IsAlive) FlushThread(threads[i]);
                    Flush();
					return false;
			}
		}
	}
}