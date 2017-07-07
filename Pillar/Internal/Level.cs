using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace Pillar3D {
	public class Level {
		public Entity Root;
		public string name;

		public static Level MainLevel;
		public Level(string name) {
			if (MainLevel == null) MainLevel = this;
			this.name = name;
			Root = new Entity("Root");
		}

		public Level(XmlReader defaults) {
			if (MainLevel == null) MainLevel = this;
			Level l = (Level)(new XmlSerializer(typeof(Level))).Deserialize(defaults);
			Root = l.Root;
			name = l.name;
		}
	}
}