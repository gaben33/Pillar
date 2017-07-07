using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pillar3D {
	public partial class Transform {
		public Vector3 Position, LocalPosition;
		public Quaternion Rotation, LocalRotation;
		public Vector3 Scale, LocalScale;

		public Transform parent;
	}
}
