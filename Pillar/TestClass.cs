using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pillar3D.Coroutines;
using System.IO;

namespace Pillar3D {
	class TestClass {
		public TestClass () {
			RoutineRunner.RunRoutine(ExampleRoutine());
		}

		public IEnumerator<YieldInstruction> ExampleRoutine () {
			float startTime = Time.RealTime;
			Console.WriteLine("\nstarting, time is " + Time.RealTime);
			//start
			float runtime = 10f;
			yield return new WaitForSeconds(runtime);
			//end
			Console.WriteLine("\rending, time is " + Time.RealTime);
			float elapsedTime = Time.RealTime - startTime;
			Console.WriteLine("elapsed time: " + elapsedTime);
			float error = 100f * ( elapsedTime - runtime ) / runtime;
			Console.WriteLine(String.Format("Error: {0}%", error));
			Console.WriteLine("Average Frame Rate: " + (int)((float)Time.FrameCount / Time.RealTime));
			Rails.Paused = true;
		}
	}
}
