using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pillar3D {
	public partial class Transform {
		//boolean dirty 
		
		public Vector3 Position, LocalPosition;
		public Quaternion Rotation, LocalRotation;
		public Vector3 Scale, LocalScale;

		public Transform parent;

		private void RecalculateComponents () {
			dirty = false;
		}
	}
}