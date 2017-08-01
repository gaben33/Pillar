using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pillar3D {
	public class Application {
		public static string Version { get; } = $"Version 0.1.5";
		public static string OS { get; } = Environment.OSVersion.Platform.ToString();
		public string Name { get; private set; }
		
		private static Application instance;
		public Application(string name) {
			Name = name;
			instance = this;
		}
	}
}
