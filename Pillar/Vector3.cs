using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pillar3D {
	public class Vector3 : Vector2 {
		public float z;

		#region constructors
		public Vector3 () : base() {
			z = 0;
		}
		public Vector3 (float x, float y, float z) : base(x, y) {
			this.z = z;
		}
		public Vector3 (Vector3 original) : base(original) {
			z = original.z;
		}
		#endregion

		#region math functions
		new public float Magnitude () {
			return (float) ( Math.Sqrt(( x * x ) + ( y * y ) + (z * z)) );
		}

		new public float SqrMagnitude () {
			return ( x * x ) + ( y * y ) + (z * z);
		}

		public static float Dot (Vector3 lhs, Vector3 rhs) {
			return ( lhs.x * rhs.x ) + ( lhs.y * rhs.y ) + (lhs.z * rhs.z);
		}

		new public Vector3 Normalize () {
			float mag = Magnitude();
			x /= mag;
			y /= mag;
			z /= mag;
			return this;
		}

		new public Vector3 Normalized () {
			return new Vector3(this) / Magnitude();
		}

		//returns the angle in radians
		public static float Angle (Vector3 lhs, Vector3 rhs) {
			return (float) Math.Acos(Vector3.Dot(lhs.Normalized(), rhs.Normalized()));
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
		public static Vector3 operator * (Vector3 lhs, float rhs) {
			return new Vector3(lhs.x * rhs, lhs.y * rhs, lhs.z * rhs);
		}
		public static Vector3 operator * (float lhs, Vector3 rhs) {
			return rhs * lhs;
		}

		public static Vector3 operator / (Vector3 lhs, float rhs) {
			return new Vector3(lhs.x / rhs, lhs.y / rhs, lhs.z / rhs);
		}

		public static Vector3 operator + (Vector3 lhs, Vector3 rhs) {
			return new Vector3(lhs.x + rhs.x, lhs.y + rhs.y, lhs.z + rhs.z);
		}

		public static Vector3 operator - (Vector3 lhs, Vector3 rhs) {
			return new Vector3(lhs.x - rhs.x, lhs.y - rhs.y, lhs.z - rhs.z);
		}

		public static Vector3 operator * (Quaternion q, Vector3 v) {
			Vector3 u = new Vector3(q.x, q.y, q.z);
			float s = q.w;
			return ( 2f * Dot(u, v) * u ) + ((s*s - Dot(u, u)) * v) + (2f * s * Cross(u, v));
		}

		public static Vector3 operator * (Vector3 lhs, Vector3 rhs) {
			return new Vector3(lhs.x * rhs.x, lhs.y * rhs.y, lhs.z * rhs.z);
		}
		public static Vector3 operator / (Vector3 lhs, Vector3 rhs) {
			return new Vector3(lhs.x / rhs.x, lhs.y / rhs.y, lhs.z / rhs.z);
		}
		#endregion
	}

	public class Vector2 {
		public float x, y;

		#region constructors
		public Vector2 () {
			x = y = 0;
		}
		public Vector2 (float x, float y) {
			this.x = x;
			this.y = y;
		}
		public Vector2 (Vector2 original) {
			x = original.x;
			y = original.y;
		}
		#endregion

		#region math functions
		public float Magnitude () {
			return (float) ( Math.Sqrt(( x * x ) + ( y * y )) );
		}

		public float SqrMagnitude () {
			return ( x * x ) + ( y * y );
		}

		public static float Dot (Vector2 lhs, Vector2 rhs) {
			return (lhs.x * rhs.x) + (lhs.y * rhs.y);
		}

		public Vector2 Normalize () {
			float mag = Magnitude();
			x /= mag;
			y /= mag;
			return this;
		}

		public Vector2 Normalized () {
			return new Vector2(this) / Magnitude();
		}

		public static Vector2 Lerp (Vector2 a, Vector2 b, float t) {
			return new Vector3() {
				x = Utilities.Lerp(a.x, b.x, t),
				y = Utilities.Lerp(a.y, b.y, t)
			};
		}
		#endregion

		#region operators
		public static Vector2 operator * (Vector2 lhs, float rhs) {
			return new Vector2(lhs.x * rhs, lhs.y * rhs);
		}

		public static Vector2 operator / (Vector2 lhs, float rhs) {
			return new Vector2(lhs.x / rhs, lhs.y / rhs);
		}

		public static Vector2 operator + (Vector2 lhs, Vector2 rhs) {
			return new Vector2(lhs.x + rhs.x, lhs.y + rhs.y);
		}

		public static Vector2 operator - (Vector2 lhs, Vector2 rhs) {
			return new Vector2(lhs.x - rhs.x, lhs.y - rhs.y);
		}

		public static Vector2 operator * (Vector2 lhs, Vector2 rhs) {
			return new Vector2(lhs.x * rhs.x, lhs.y * rhs.y);
		}
		public static Vector2 operator / (Vector2 lhs, Vector2 rhs) {
			return new Vector2(lhs.x / rhs.x, lhs.y / rhs.y);
		}
		#endregion
	}

	public class Quaternion {
		public float x, y, z, w;
		public Quaternion () {
			x = y = z = w = 0;
		}
		public Quaternion (float x, float y, float z, float w) {
			this.x = x;
			this.y = y;
			this.z = z;
			this.w = w;
		}

		/// <summary>
		/// Angles is xyz on a unit sphere
		/// </summary>
		/// <param name="Angles"></param>
		public Quaternion (Vector3 Angles) {
			float t0 = (float)Math.Cos(Angles.x * 0.5f);
			float t1 = (float) Math.Sin(Angles.x * 0.5f);
			float t2 = (float) Math.Cos(Angles.y * 0.5f);
			float t3 = (float) Math.Sin(Angles.y * 0.5f);
			float t4 = (float) Math.Cos(Angles.z * 0.5f);
			float t5 = (float) Math.Sin(Angles.z * 0.5f);

			w = t0 * t2 * t4 + t1 * t3 * t5;
			x = t0 * t3 * t4 - t1 * t2 * t5;
			y = t0 * t2 * t5 + t1 * t3 * t4;
			z = t1 * t2 * t4 - t0 * t3 * t5;
		}

		/// <summary>
		/// Converts to xyz on a unit sphere
		/// </summary>
		/// <returns></returns>
		public Vector3 ToAngles () {
			Vector3 angles = new Vector3();

			float ySqr = y * y;

			float t0 = 2f * (w * x + y * z);
			float t1 = 1f - 2f * (x * x + ySqr);
			angles.x = (float) Math.Atan2(t0, t1);

			float t2 = 2f * (w * y - z * x);
			t2 = t2 > 1 ? 1 : t2;
			t2 = t2 < -1 ? -1 : t2;
			angles.y = (float) Math.Asin(t2);

			float t3 = 2f * (w * z + x * y);
			float t4 = 1 - 2f * (ySqr + z * z);
			angles.z = (float) Math.Atan2(t3, t4);

			return angles;
		}

		public Quaternion LookRotation (Vector3 Rotation, Vector3 Axis) {
			throw new NotImplementedException();
		}

		public static Quaternion operator * (Quaternion a, Quaternion b) {
			return new Quaternion() {
				x = a.w * b.w - a.x * b.x - a.y * b.y - a.z * b.z,
				y = a.w * b.x + a.x * b.w + a.y * b.z - a.z * b.y,
				z = a.w * b.y - a.x * b.z + a.y * b.w + a.z * b.x,
				w = a.w * b.z + a.x * b.y - a.y * b.x + a.z * b.w
			};
		}
	}

	public class Quaternion2D {
		public float x, y;
		public Quaternion2D() {
			x = y = 0;
		}
		//takes Angles.x + i*Angles.y
		public Quaternion2D(Vector2 Angles) {

		}
	}
}