using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pillar3D {
	public partial class Transform {
		//boolean dirty 

		public Vector3 Position {
			get {
				return pos.Value;
			}
			set {
				dirty = true;
				pos.Value = value;
			}
		}
		public Vector3 LocalPosition {
			get {
				return localPos.Value;
			}
			set {
				dirty = true;
				localPos.Value = value;
			}
		}
		public Quaternion Rotation {
			get {
				return rot.Value;
			}
			set {
				dirty = true;
				rot.Value = value;
			}
		}
		public Quaternion LocalRotation {
			get {
				return localRot.Value;
			}
			set {
				dirty = true;
				localRot.Value = value;
			}
		}
		public Vector3 Scale {
			get {
				return scale.Value;
			}
			set {
				dirty = true;
				scale.Value = value;
			}
		}
		public Vector3 LocalScale {
			get {
				return localScale.Value;
			}
			set {
				dirty = true;
				localScale.Value = value;
			}
		}

		private SmartCache<Vector3> pos, localPos, scale, localScale;
		private SmartCache<Quaternion> rot, localRot;

		public Transform parent;

		private void RecalculateComponents() {
			
			dirty = false;
		}
	}
}