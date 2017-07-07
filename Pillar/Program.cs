using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pillar3D {
	//manages iterative update delegates
	class Rails {
		public static bool Paused = false, Exit = false;
		
		public static Action Update, PersistantUpdate;

		public Rails () {
			Paused = false;
		}

		static int Main (string[] args) {
			Rails MainLoop = new Rails();
			Time time = new Time();
			Input inp = new Input();
			RoutineRunner runner = new RoutineRunner();
			TestClass tc = new TestClass();
			Level main = new Level("main");
			PersistantUpdate = time.Frame;
			PersistantUpdate += runner.Frame;
			Update = () => Console.Write("\rFrame Count: " + Time.FrameCount);
			while (!Exit) {
				if (!Paused) if (Update != null) Update();
				PersistantUpdate();
			}
			return 0;
		}
	}
}
