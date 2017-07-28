using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pillar3D;
using Pillar3D.Coroutines;
using System.IO;

class TestClass {
	public TestClass() {
		RoutineRunner.RunRoutine(ExampleRoutine());
	}

	public IEnumerator<YieldInstruction> ExampleRoutine() {
		float startTime = Time.RealTime;
		Console.WriteLine($"\nstarting, time is {Time.RealTime}");
		//start
		Rails.Update += PrintKeyState;
		yield return new WaitForSecondsFunctional(10f, PrintWaitProgress);
		//yield return new WaitUntil(() => Input.GetKey(System.Windows.Input.Key.Escape));
		float elapsed = Time.RealTime - startTime;
		Console.WriteLine($"\rYou waited for {elapsed} seconds");
		Rails.Update -= PrintKeyState;
		/*
		float runtime = 10f;
		yield return new WaitForSeconds(runtime);
		//end
		Console.WriteLine($"\rending, time is {Time.RealTime}");
		float elapsedTime = Time.RealTime - startTime;
		Console.WriteLine($"elapsed time: {elapsedTime}");
		float error = 100f * (elapsedTime - runtime) / runtime;
		Console.WriteLine($"Error: {error}%");
		Console.WriteLine($"Average Frame Rate: {(int)(Time.FrameCount / Time.RealTime)} FPS");
		*/
		Rails.Paused = true;
	}

	private void PrintWaitProgress (float t) {
		Console.WriteLine($"\r{t}");
	}

	private void PrintKeyState () {
		string s = "";
		if (Input.GetKeyDown(System.Windows.Input.Key.Enter)) s = $"Enter pressed down at {(int)Time.RealTime}s";
		else if (Input.GetKeyUp(System.Windows.Input.Key.Enter)) s = $"Enter released at {(int)Time.RealTime}s";
		if (s != "") Console.WriteLine($"\n{s}");
	}
}