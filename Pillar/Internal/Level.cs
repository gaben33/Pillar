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

        public int GetStress () => Rail.Update.GetInvocationList().Length + Rail.PersistantUpdate.GetInvocationList().Length;
	}
}