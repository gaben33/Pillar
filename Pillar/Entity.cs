using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace Pillar3D {
	public class Entity {
		public List<Component> Components;
		public List<Entity> Children;
		public Entity Parent { get; private set; }
		public bool active;
		public string Name;

		public Entity () {
			Components = new List<Component>(1);
			Children = new List<Entity>();
			Parent = Level.MainLevel.Root;
			active = true;
			Name = "Game Object";
		}

		public Entity (string name) : this() {
			Name = name;
		}

		public C AddComponent<C> () where C : Component, new() {
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
		public C AddComponent<C> (XmlReader defaults) where C : Component, new() {
			C comp = new C();
			comp.Container = this;
			comp.OnComponentAdded();
			comp = (C) (new XmlSerializer(typeof(C))).Deserialize(defaults);
			if (comp is Transform) Components.Insert(0, comp);
			else Components.Add(comp);
			return comp;
		}

		public void SetParent (Entity parent) {
			Parent.Children.Remove(this);
			parent.Children.Add(this);
			Parent = parent;
		}

		//public void Simulate () {
		//	if (!active) return;
		//	for (int i = 0; i < Components.Count; i++) Components[i].Update();
		//	for (int i = 0; i < Children.Count; i++) Children[i].Simulate();
		//}

		public Entity Clone () {
			return (Entity) MemberwiseClone();
		}
	}
}
