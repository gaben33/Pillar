using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pillar3D {
	public class Component {
		//I'm trusting you with a public set.  please don't abuse it.  
		protected internal Entity Container;
		public static bool AllowMultiple;//can the same component be added multiple times?
		public int ID { get; private set; }
		protected Action Update { get { return Container.ContainerLevel.Rail.Update; } set { Container.ContainerLevel.Rail.Update = value; } }
		protected Action PersistantUpdate { get { return Container.ContainerLevel.Rail.PersistantUpdate; } set { Container.ContainerLevel.Rail.PersistantUpdate = value; } }
		protected bool Paused { get { return Container.ContainerLevel.Rail.Paused; } set { Container.ContainerLevel.Rail.Paused = value; } }
		private static Dictionary<int, Component> componentIDs; 

		public Component(bool allowMultiple) {
			AllowMultiple = allowMultiple;
			if (componentIDs == null) componentIDs = new Dictionary<int, Component>(501);
			for (int r = (new Random()).Next(); componentIDs.ContainsKey(r); r = (new Random()).Next()) ID = r;
			componentIDs.Add(ID, this);
		}

		~Component() {
			componentIDs.Remove(ID);
		}

		public virtual void OnComponentAdded() {

		}

		public virtual void OnComponentRemoved() {
			
		}

		#region Utility Methods
		public static Entity Instantiate(Entity original) => original.Clone();

        public static void Destroy<T>(ref T obj) => Rails.Destroy(ref obj);
		#endregion
	}
}
