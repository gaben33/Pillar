using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace Pillar3D {
	public class Level {
		public Entity Root;
		public string Name;
		public Rails Rail;

		public static Level MainLevel, CurrentLevel;
		public Level(string name) {
			if (MainLevel == null) CurrentLevel = MainLevel = this;
			Name = name;
			Root = new Entity("Root");
			Rail = new Rails();
			ThreadManager.AddLevel(this);
		}

		public Level(string name, int thread) {
			if (MainLevel == null) CurrentLevel = MainLevel = this;
			Name = name;
			Rail = new Rails();
			Root = new Entity("Root");
			ThreadManager.AddLevel(this, thread);
		}

		public Level(XmlReader defaults) {
			if (MainLevel == null) MainLevel = this;
			Level l = (Level)(new XmlSerializer(typeof(Level))).Deserialize(defaults);
			Root = l.Root;
			Name = l.Name;
			Rail = l.Rail;
			ThreadManager.AddLevel(this);
		}

		public void Frame () {
			Rail.PersistantUpdate?.Invoke();
			if (!Rail.Paused) Rail.Update?.Invoke();
		}
	}
}