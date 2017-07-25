using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Pillar3D {
	//manages iterative update delegates
	public class Rails {
		public static bool Paused = false, Exit = false;

		public static Action Update, PersistantUpdate;

		private static Dictionary<int, Action> TimedRails;

		public Rails() {
			Paused = false;
			TimedRails = new Dictionary<int, Action>(7);
		}

		[STAThread]
		static int Main(string[] args) {
			Rails MainLoop = new Rails();
			Time time = new Time();
			PersistantUpdate = time.Frame;
			Input inp = new Input();
			RoutineRunner runner = new RoutineRunner();
			TestClass tc = new TestClass();
			Level main = new Level("main");
			Input.CreateInputVector2D("Direction", new Input.InputVector2D(new Input.InputAxis(Key.Left, Key.Right), new Input.InputAxis(Key.Down, Key.Up)));
			Update = () => Console.Write("\r" + new Vector3(Input.GetInputVector2D("Direction")) + $", {1f / Time.SmoothDeltaTime}");
			while (!Exit) {
				if (!Paused) Update?.Invoke();
				PersistantUpdate();
			}
			return 0;
		}
	}
}
