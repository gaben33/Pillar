using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pillar3D {
	public class Transform : Component {
		public Vector3 Position, LocalPosition;
		public Quaternion Rotation, LocalRotation;
		public Vector3 Scale, LocalScale;

		public Transform parent;

		public Action OnTransformChanged;

		public Transform () : base(false) {
			
		}

		public override void OnComponentAdded () {
			Reparent();
		}

		public void Reparent () {
			parent.OnTransformChanged -= OnTransformChanged;
			Entity currentEntity = Container;
			while(currentEntity.Parent != null) {
				currentEntity = currentEntity.Parent;
				if(currentEntity.Components[0] is Transform) {
					parent = currentEntity.Components[0] as Transform;
					parent.OnTransformChanged += OnTransformChanged;
					break;
				}
			}
		}
	}
}
