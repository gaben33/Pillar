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
			//NOTE: FUNC NEEDS TO BE REPLACED WITH ACTUAL LINEAR ALGEBRA
			pos = new SmartCache<Vector3>(() => Vector3.Zero, Vector3.Zero);
			localPos = new SmartCache<Vector3>(() => Vector3.Zero, Vector3.Zero);
			scale = new SmartCache<Vector3>(() => Vector3.Zero, Vector3.Zero);
			localScale = new SmartCache<Vector3>(() => Vector3.Zero, Vector3.Zero);
			rot = new SmartCache<Quaternion>(() => Quaternion.Identity, Quaternion.Identity);
			localRot = new SmartCache<Quaternion>(() => Quaternion.Identity, Quaternion.Identity);
		}

		public override void OnComponentAdded() {
			Reparent();
		}

		protected void UpdateChildren () {
			
		}

		protected void Reparent() {
			if(parent != null) parent.OnTransformChanged -= Dirty;
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
			if(parent != null) parent.OnTransformChanged += Dirty;
		}

		private void Dirty () {
			dirty = true;
		}
	}
}
