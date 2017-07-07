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

		public Component (bool allowMultiple) {
		}

		public virtual void OnComponentAdded () {

		}

		#region Utility Methods
		public static Entity Instantiate (Entity original) {
			return original.Clone();
		}

		
		#endregion
	}
}
