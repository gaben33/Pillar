using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Pillar3D {
	//manages iterative update delegates
	public class Rails {
		public static bool Paused = false;
		private static bool exit = false;

		public static Action Update, PersistantUpdate;

		private static Dictionary<int, Action> TimedRails;

		public Rails() {
			Paused = false;
			TimedRails = new Dictionary<int, Action>(7);
		}

		public static void Exit () {
			exit = true;
		}

		[STAThread]
		static int Main(string[] args) {
			Application app = new Application("New Pillar Game");
			Rails MainLoop = new Rails();
			Time time = new Time();
			PersistantUpdate = time.Frame;
			Input inp = new Input();
			RoutineRunner runner = new RoutineRunner();
			TestClass tc = new TestClass();
			Level main = new Level("main");
			Matrix m = new Matrix(new float[][] {
				new float[] { 1, 2, 3, 4 },
				new float[] { 5, 6, 7, 8 },
				new float[] { 9, 10, 11, 12 }
			});
			VectorN v = new VectorN(1, 2, 3, 4);
			//Update = () => Console.Write($"\r{(int)(1f / Time.SmoothDeltaTime)} FPS    ");
			//Update = () => Console.Write($"\r{v.ToString()}");
			while (!exit) {
				if (!Paused) Update?.Invoke();
				PersistantUpdate();
			}
			return 0;
		}
	}
}
