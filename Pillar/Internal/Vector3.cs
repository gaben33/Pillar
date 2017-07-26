using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Pillar3D {
	public class Vector3 : Vector2 {
		public float z;

		#region constructors
		public Vector3() : base() {
			z = 0;
		}
		public Vector3(float x, float y, float z) : base(x, y) {
			this.z = z;
		}
		public Vector3(Vector3 original) : base(original) {
			z = original.z;
		}
		public Vector3(Vector2 original) : base(original) {
			z = 0;
		}
		#endregion

		#region math functions
		new public float Magnitude() {
			return (float)(Math.Sqrt((x * x) + (y * y) + (z * z)));
		}

		new public float SqrMagnitude() {
			return (x * x) + (y * y) + (z * z);
		}

		public static float Dot(Vector3 lhs, Vector3 rhs) {
			return (lhs.x * rhs.x) + (lhs.y * rhs.y) + (lhs.z * rhs.z);
		}

		new public Vector3 Normalize() {
			float mag = Magnitude();
			x /= mag;
			y /= mag;
			z /= mag;
			return this;
		}

		new public Vector3 Normalized() {
			return new Vector3(this) / Magnitude();
		}

		//returns the angle in radians
		public static float Angle(Vector3 lhs, Vector3 rhs) {
			return (float)Math.Acos(Dot(lhs.Normalized(), rhs.Normalized()));
		}

		public static Vector3 Cross(Vector3 lhs, Vector3 rhs) {
			return new Vector3() {
				x = (lhs.y * rhs.z) - (lhs.z * rhs.y),
				y = (lhs.x * rhs.z) - (lhs.z * rhs.x),
				z = (lhs.x * rhs.y) - (lhs.y * rhs.x)
			};
		}

		public static Vector3 Lerp(Vector3 a, Vector3 b, float t) {
			return new Vector3() {
				x = Utilities.Lerp(a.x, b.x, t),
				y = Utilities.Lerp(a.y, b.y, t),
				z = Utilities.Lerp(a.z, b.z, t)
			};
		}
		#endregion

		#region operators
		public static Vector3 operator *(Vector3 lhs, float rhs) {
			return new Vector3(lhs.x * rhs, lhs.y * rhs, lhs.z * rhs);
		}
		public static Vector3 operator *(float lhs, Vector3 rhs) {
			return rhs * lhs;
		}
		public static Vector3 operator /(Vector3 lhs, float rhs) {
			return new Vector3(lhs.x / rhs, lhs.y / rhs, lhs.z / rhs);
		}
		public static Vector3 operator +(Vector3 lhs, Vector3 rhs) {
			return new Vector3(lhs.x + rhs.x, lhs.y + rhs.y, lhs.z + rhs.z);
		}
		public static Vector3 operator -(Vector3 lhs, Vector3 rhs) {
			return new Vector3(lhs.x - rhs.x, lhs.y - rhs.y, lhs.z - rhs.z);
		}
		public static Vector3 operator *(Quaternion q, Vector3 v) {
			Vector3 u = new Vector3(q.x, q.y, q.z);
			float s = q.w;
			return (2f * Dot(u, v) * u) + ((s * s - Dot(u, u)) * v) + (2f * s * Cross(u, v));
		}
		public static Vector3 operator *(Vector3 lhs, Vector3 rhs) {
			return new Vector3(lhs.x * rhs.x, lhs.y * rhs.y, lhs.z * rhs.z);
		}
		public static Vector3 operator /(Vector3 lhs, Vector3 rhs) {
			return new Vector3(lhs.x / rhs.x, lhs.y / rhs.y, lhs.z / rhs.z);
		}

		new public float this[int i] {
			get { return (new float[] { x, y, z })[i]; }
			set {
				switch (i) {
					case (0):
						x = value;
						break;
					case (1):
						y = value;
						break;
					default:
						z = value;
						break;
				}
			}
		}
		#endregion

		#region misc functions
		public override string ToString() {
			return $"<{x}, {y}, {z}>";
		}
		#endregion

		#region defaults
		public static Vector3 Forward { get { return new Vector3(0, 0, 1); } }
		public static Vector3 Up { get { return new Vector3(0, 1, 0); } }
		public static Vector3 Right { get { return new Vector3(1, 0, 0); } }
		public static Vector3 Back { get { return new Vector3(0, 0, -1); } }
		public static Vector3 Down { get { return new Vector3(0, -1, 0); } }
		public static Vector3 Left { get { return new Vector3(-1, 0, 0); } }
		public static Vector3 One { get { return new Vector3(1, 1, 1); } }
		public static Vector3 Zero { get { return new Vector3(0, 0, 0); } }
		#endregion
	}

	public class Vector2 {
		public float x, y;

		#region constructors
		public Vector2() {
			x = y = 0;
		}
		public Vector2(float x, float y) {
			this.x = x;
			this.y = y;
		}
		public Vector2(Vector2 original) {
			x = original.x;
			y = original.y;
		}
		#endregion

		#region math functions
		public float Magnitude() {
			return (float)(Math.Sqrt((x * x) + (y * y)));
		}

		public float SqrMagnitude() {
			return (x * x) + (y * y);
		}

		public static float Dot(Vector2 lhs, Vector2 rhs) {
			return (lhs.x * rhs.x) + (lhs.y * rhs.y);
		}

		public Vector2 Normalize() {
			float mag = Magnitude();
			x /= mag;
			y /= mag;
			return this;
		}

		public Vector2 Normalized() {
			return new Vector2(this) / Magnitude();
		}

		public static Vector2 Lerp(Vector2 a, Vector2 b, float t) {
			return new Vector3() {
				x = Utilities.Lerp(a.x, b.x, t),
				y = Utilities.Lerp(a.y, b.y, t)
			};
		}
		#endregion

		#region operators
		public static Vector2 operator *(Vector2 lhs, float rhs) {
			return new Vector2(lhs.x * rhs, lhs.y * rhs);
		}
		public static Vector2 operator /(Vector2 lhs, float rhs) {
			return new Vector2(lhs.x / rhs, lhs.y / rhs);
		}
		public static Vector2 operator +(Vector2 lhs, Vector2 rhs) {
			return new Vector2(lhs.x + rhs.x, lhs.y + rhs.y);
		}
		public static Vector2 operator -(Vector2 lhs, Vector2 rhs) {
			return new Vector2(lhs.x - rhs.x, lhs.y - rhs.y);
		}
		public static Vector2 operator *(Vector2 lhs, Vector2 rhs) {
			return new Vector2(lhs.x * rhs.x, lhs.y * rhs.y);
		}
		public static Vector2 operator /(Vector2 lhs, Vector2 rhs) {
			return new Vector2(lhs.x / rhs.x, lhs.y / rhs.y);
		}

		public float this[int i] {
			get { return i == 0 ? x : y; }
			set { if (i == 0) x = value; else y = value; }
		}
		#endregion

		#region misc functions
		public override string ToString() {
			return $"<{x}, {y}>";
		}
		#endregion

		#region defaults
		public static Vector2 Up { get { return new Vector2(0, 1); } }
		public static Vector2 Right { get { return new Vector2(1, 0); } }
		public static Vector2 Down { get { return new Vector2(0, -1); } }
		public static Vector2 Left { get { return new Vector2(-1, 0); } }
		public static Vector2 Zero { get { return new Vector2(0, 0); } }
		public static Vector2 One { get { return new Vector2(1, 1); } }
		#endregion
	}

	public class VectorN {
		#region variables
		private float[] values;
		public int Dimension { get { return values.Length; } }
		#endregion

		#region constructors
		public VectorN(int dimension) {
			values = new float[dimension];
		}
		#endregion

		#region math operators
		public float this[int i] {
			get { return values[i]; }
			set { values[i] = value; }
		}

		public static float Dot(VectorN lhs, VectorN rhs) {
			if (lhs.values.Length != rhs.values.Length) throw new ArgumentException();
			float sum = 0;
			for(int i = 0; i < lhs.values.Length; i++) {
				sum += lhs[i] * rhs[i];
			}
			return sum;
		}
		#endregion

		#region functions
		public override string ToString() {
			string s = "<";
			for(int i = 0; i < Dimension; i++) {
				s += this[i] + (i == Dimension - 1 ? "" : " ");
			}
			s += ">";
			return s;
		}
		#endregion
	}
}