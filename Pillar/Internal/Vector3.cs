using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Pillar3D {
	public class Vector4 : Vector3 {
		public float w;

		#region constructors
		public Vector4() : base() {
			w = 0;
		}
		public Vector4(float x, float y, float z, float w) : base(x,y,z) {
			this.w = w;
		}
		public Vector4(Vector4 original) : base(original) {
			w = original.w;
		}
		public Vector4(Vector3 original) : base(original) {
			w = 0;
		}
		public Vector4(Vector2 original) : base(original) {
			w = 0;
		}
		#endregion

		#region math functions
		new public float Magnitude () => (float) (Math.Sqrt((x * x) + (y * y) + (z * z) + (w * w)));

		new public float SqrMagnitude () => (x * x) + (y * y) + (z * z) + (w * w);

		public static float Dot (Vector4 lhs, Vector4 rhs) => (lhs.x * rhs.x) + (lhs.y * rhs.y) + (lhs.z * rhs.z) + (lhs.w * rhs.w);

		new public Vector4 Normalize () {
			float mag = Magnitude();
			x /= mag;
			y /= mag;
			z /= mag;
			w /= mag;
			return this;
		}

		new public Vector4 Normalized () => new Vector4(this) / Magnitude();

		public static float Angle (Vector4 lhs, Vector4 rhs) => (float) Math.Acos(Dot(lhs.Normalized(), rhs.Normalized()));

		public static Vector4 Lerp (Vector4 a, Vector4 b, float t) {
			return new Vector4() {
				x = Mathf.Lerp(a.x, b.x, t),
				y = Mathf.Lerp(a.y, b.y, t),
				z = Mathf.Lerp(a.z, b.z, t),
				w = Mathf.Lerp(a.w, b.w, t)
			};
		}
		#endregion

		#region operators
		public static Vector4 operator * (Vector4 lhs, float rhs) => new Vector4(lhs.x * rhs, lhs.y * rhs, lhs.z * rhs, lhs.w * rhs);
		public static Vector4 operator * (float lhs, Vector4 rhs) => rhs * lhs;
		public static Vector4 operator / (Vector4 lhs, float rhs) => new Vector4(lhs.x / rhs, lhs.y / rhs, lhs.z / rhs, lhs.w / rhs);
		public static Vector4 operator + (Vector4 lhs, Vector4 rhs) => new Vector4(lhs.x + rhs.x, lhs.y + rhs.y, lhs.z + rhs.z, lhs.w + rhs.w);
		public static Vector4 operator - (Vector4 lhs, Vector4 rhs) => new Vector4(lhs.x - rhs.x, lhs.y - rhs.y, lhs.z - rhs.z, lhs.w - rhs.w);
		public static Vector4 operator * (Vector4 lhs, Vector4 rhs) => new Vector4(lhs.x * rhs.x, lhs.y * rhs.y, lhs.z * rhs.z, lhs.w * rhs.w);
		public static Vector4 operator / (Vector4 lhs, Vector4 rhs) => new Vector4(lhs.x / rhs.x, lhs.y / rhs.y, lhs.z / rhs.z, lhs.w / rhs.w);

		new public float this[int i] {
			get { return (new float[] { x, y, z, w })[i]; }
			set {
				switch (i) {
					case (0):
						x = value;
						break;
					case (1):
						y = value;
						break;
					case (2):
						z = value;
						break;
					default:
						w = value;
						break;
				}
			}
		}
		#endregion

		#region misc functions
		public override string ToString () => $"<{x}, {y}, {z}, {w}>";
		#endregion

		#region defaults
		new public static Vector4 One { get; } = new Vector4(1, 1, 1, 1);
		new public static Vector4 Zero { get; } = new Vector4(0, 0, 0, 0);
		#endregion
	}

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
		new public float Magnitude() => (float)(Math.Sqrt((x * x) + (y * y) + (z * z)));

		new public float SqrMagnitude() => (x * x) + (y * y) + (z * z);

		public static float Dot(Vector3 lhs, Vector3 rhs) => (lhs.x * rhs.x) + (lhs.y * rhs.y) + (lhs.z * rhs.z);

		new public Vector3 Normalize() {
			float mag = Magnitude();
			x /= mag;
			y /= mag;
			z /= mag;
			return this;
		}

		new public Vector3 Normalized() => new Vector3(this) / Magnitude();

		//returns the angle in radians
		public static float Angle(Vector3 lhs, Vector3 rhs) => (float)Math.Acos(Dot(lhs.Normalized(), rhs.Normalized()));

		public static Vector3 Cross(Vector3 lhs, Vector3 rhs) {
			return new Vector3() {
				x = (lhs.y * rhs.z) - (lhs.z * rhs.y),
				y = (lhs.x * rhs.z) - (lhs.z * rhs.x),
				z = (lhs.x * rhs.y) - (lhs.y * rhs.x)
			};
		}

		public static Vector3 Lerp(Vector3 a, Vector3 b, float t) {
			return new Vector3() {
				x = Mathf.Lerp(a.x, b.x, t),
				y = Mathf.Lerp(a.y, b.y, t),
				z = Mathf.Lerp(a.z, b.z, t)
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
		public override string ToString() => $"<{x}, {y}, {z}>";
		#endregion

		#region defaults
		public static Vector3 Forward { get; } = new Vector3(0, 0, 1);
		new public static Vector3 Up { get; } = new Vector3(0, 1, 0);
		new public static Vector3 Right { get; } = new Vector3(1, 0, 0);
		public static Vector3 Back { get; } = new Vector3(0, 0, -1);
		new public static Vector3 Down { get; } = new Vector3(0, -1, 0);
		new public static Vector3 Left { get; } = new Vector3(-1, 0, 0);
		new public static Vector3 One { get; } = new Vector3(1, 1, 1);
		new public static Vector3 Zero { get; } = new Vector3(0, 0, 0);
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
				x = Mathf.Lerp(a.x, b.x, t),
				y = Mathf.Lerp(a.y, b.y, t)
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

		public static explicit operator VectorN (Vector2 vec) {
			return new VectorN(vec.x, vec.y);
		}
		#endregion

		#region misc functions
		public override string ToString() => $"<{x}, {y}>";
		public override bool Equals(object other) => ((VectorN)this).Equals(other as VectorN);
		public override int GetHashCode() => base.GetHashCode();
		#endregion

		#region defaults
		public static Vector2 Up { get; } = new Vector2(0, 1);
		public static Vector2 Right { get; } = new Vector2(1, 0);
		public static Vector2 Down { get; } = new Vector2(0, -1);
		public static Vector2 Left { get; } = new Vector2(-1, 0);
		public static Vector2 Zero { get; } = new Vector2(0, 0);
		public static Vector2 One { get; } = new Vector2(1, 1);
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
		public VectorN(params float[] initialValues) {
			values = initialValues;
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
		public static VectorN operator *(VectorN lhs, VectorN rhs) {
			if (lhs.Dimension != rhs.Dimension) throw new ArgumentException("Vectors must be the same length");
			VectorN result = new VectorN(lhs.Dimension);
			for (int i = 0; i < lhs.Dimension; i++) result[i] = lhs[i] * rhs[i];
			return result;
		}
		public static VectorN operator /(VectorN lhs, VectorN rhs) {
			if (lhs.Dimension != rhs.Dimension) throw new ArgumentException("Vectors must be the same length");
			VectorN result = new VectorN(lhs.Dimension);
			for (int i = 0; i < lhs.Dimension; i++) result[i] = lhs[i] / rhs[i];
			return result;
		}
		public static VectorN operator *(VectorN lhs, float rhs) {
			VectorN result = new VectorN(lhs.Dimension);
			for (int i = 0; i < lhs.Dimension; i++) result[i] = lhs[i] * rhs;
			return result;
		}
		public static VectorN operator *(float lhs, VectorN rhs) {
			VectorN result = new VectorN(rhs.Dimension);
			for (int i = 0; i < rhs.Dimension; i++) result[i] = lhs * rhs[i];
			return result;
		}
		public static VectorN operator /(VectorN lhs, float rhs) {
			VectorN result = new VectorN(lhs.Dimension);
			for (int i = 0; i < lhs.Dimension; i++) result[i] = lhs[i] / rhs;
			return result;
		}
		public static VectorN operator /(float lhs, VectorN rhs) {
			VectorN result = new VectorN(rhs.Dimension);
			for (int i = 0; i < rhs.Dimension; i++) result[i] = lhs / rhs[i];
			return result;
		}
		public static VectorN operator +(VectorN lhs, VectorN rhs) {
			if (lhs.Dimension != rhs.Dimension) throw new ArgumentException("Vectors must be the same length");
			VectorN result = new VectorN(lhs.Dimension);
			for (int i = 0; i < lhs.Dimension; i++) result[i] = lhs[i] + rhs[i];
			return result;
		}
		public static VectorN operator -(VectorN lhs, VectorN rhs) {
			if (lhs.Dimension != rhs.Dimension) throw new ArgumentException("Vectors must be the same length");
			VectorN result = new VectorN(lhs.Dimension);
			for (int i = 0; i < lhs.Dimension; i++) result[i] = lhs[i] - rhs[i];
			return result;
		}
		public static bool operator ==(VectorN lhs, VectorN rhs) => lhs.Equals(rhs);
		public static bool operator !=(VectorN lhs, VectorN rhs) => !lhs.Equals(rhs);
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

		public Matrix ToMatrix() {
			Matrix result = new Matrix(Dimension, 1);
			result.SetColumn(0, values);
			return result;
		}

		public override bool Equals(object other) {
			if (!(other is VectorN)) return false;
			for (int i = 0; i < Dimension; i++) if (!Mathf.Approximately(this[i], ((VectorN)other)[i])) return false;
			return true;
		}

		public override int GetHashCode() => base.GetHashCode();
		#endregion
	}
}