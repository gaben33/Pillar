using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pillar3D {
	public partial class Transform : Component {
		//updates cached values
		public Action OnTransformChanged;

		private bool dirty;

		public Transform() : base(false) {
			dirty = true;
			Container.OnParentChanged += Reparent;
		}

		public override void OnComponentAdded() {
			Reparent();
		}

		protected void UpdateChildren () {
			
		}

		protected void Reparent() {
			dirty = true;
			Entity currentEntity = Container;
			while (currentEntity.Parent != null) {
				currentEntity = currentEntity.Parent;
				if (currentEntity.Components.Count == 0) continue;
				if (currentEntity.Components[0] is Transform) {
					parent = currentEntity.Components[0] as Transform;
					break;
				}
			}
		}
	}
}
