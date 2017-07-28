using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pillar3D {
	public class Component {
		//I'm trusting you with a public set.  please don't abuse it.  
		public Entity Container;
		public static bool AllowMultiple;//can the same component be added multiple times?
		public int ID { get; private set; }
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
		public static Entity Instantiate(Entity original) {
			return original.Clone();
		}


		#endregion
	}
}
