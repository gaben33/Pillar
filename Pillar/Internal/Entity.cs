using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace Pillar3D {
	public class Entity {
		#region variables
		public List<Component> Components;
		public List<Entity> Children;
		public Entity Parent { get; private set; }
		public bool Active;
		public bool ActiveInTree {
			get {
				if (Parent == null) return Active;
				Entity ce = this;
				while(ce != null) {
					if (!ce.Active) return false;
					ce = ce.Parent;
				}
				return true;
			}
		}
		public string Name;
		public Level ContainerLevel { get; private set; }
		public string Tag {
			get {
				return tag;
			}
			set {
				if (tag != "untagged") if (TaggedObjects.ContainsKey(tag)) TaggedObjects[tag].Remove(this);
				if (value != "untagged") {
					if (!TaggedObjects.ContainsKey(value)) TaggedObjects.Add(value, new List<Entity>() { this });
					else TaggedObjects[value].Add(this);
				}
				tag = value;
			}
		}
		private string tag;

		private static Dictionary<string, List<Entity>> TaggedObjects;
		#endregion

		#region constructors
		public Entity() {
			if (TaggedObjects == null) TaggedObjects = new Dictionary<string, List<Entity>>();
			Components = new List<Component>(1);
			Children = new List<Entity>();
			Parent = Level.MainLevel.Root;
			Active = true;
			Name = "Game Object";
			Tag = "untagged";
			ContainerLevel = Level.CurrentLevel;
		}

		public Entity(string name) : this() {
			Name = name;
		}
		#endregion

		#region instance methods
		public C AddComponent<C>() where C : Component, new() {
			C comp = new C();
			comp.Container = this;
			if (comp is Transform) Components.Insert(0, comp);
			else Components.Add(comp);
			comp.OnComponentAdded();
			return comp;
		}

		/// <summary>
		/// Adds component with default data.  
		/// </summary>
		/// <param name="defaults">XML data with default data for component</param>
		public C AddComponent<C>(XmlReader defaults) where C : Component, new() {
			C comp = new C();
			comp.Container = this;
			comp.OnComponentAdded();
			comp = (C)(new XmlSerializer(typeof(C))).Deserialize(defaults);
			if (comp is Transform) Components.Insert(0, comp);
			else Components.Add(comp);
			return comp;
		}

		public void SetParent(Entity parent) {
			Parent.Children.Remove(this);
			parent.Children.Add(this);
			Parent = parent;
			ContainerLevel = parent.ContainerLevel;
		}

		//public void Simulate () {
		//	if (!active) return;
		//	for (int i = 0; i < Components.Count; i++) Components[i].Update();
		//	for (int i = 0; i < Children.Count; i++) Children[i].Simulate();
		//}

		public Entity Clone() {
			return (Entity)MemberwiseClone();
		}
		#endregion

		#region static methods
		public static Entity FindEntityWithTag (string tag) {
			if (!TaggedObjects.ContainsKey(tag)) return null;
			return TaggedObjects[tag][0];
		}

		public static Entity[] FindEntitiesWithTag (string tag) {
			if(TaggedObjects.ContainsKey(tag)) return TaggedObjects[tag].ToArray();
			return null;
		}
		#endregion
	}
}