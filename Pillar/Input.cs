using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pillar3D {
	class Input {
		private Dictionary<Keys, KeyState> States;

		private static Input instance;
		public Input () {
			States = new Dictionary<Keys, KeyState>(100);
			Rails.PersistantUpdate += Poll;
			instance = this;
		}

		~Input () {
			Rails.PersistantUpdate -= Poll;
		}

		//polls to update key dictionary
		public static void Poll () {

		}

		public static KeyState GetKey (Keys key) {
			return instance.States[key];
		}

		public enum KeyState { Up, Down, Pressed, Released }
		public enum Keys { }
	}
}
