using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pillar3D {
	public partial class Transform : Component {

		public Action OnTransformChanged;

		public Transform() : base(false) {

		}

		public override void OnComponentAdded() {
			Reparent();
		}

		protected void UpdateChildren () {
			
		}

		protected void Reparent() {
			parent.OnTransformChanged -= OnTransformChanged;
			Entity currentEntity = Container;
			while (currentEntity.Parent != null) {
				currentEntity = currentEntity.Parent;
				if (currentEntity.Components.Count == 0) continue;
				if (currentEntity.Components[0] is Transform) {
					parent = currentEntity.Components[0] as Transform;
					parent.OnTransformChanged += OnTransformChanged;
					break;
				}
			}
		}
	}
}
