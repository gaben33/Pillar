using System;


namespace Pillar3D {
	public class Quaternion {
		#region variables, constructors
		public float x, y, z, w;
		public Quaternion() {
			x = y = z = w = 0;
		}
		public Quaternion(float x, float y, float z, float w) {
			this.x = x;
			this.y = y;
			this.z = z;
			this.w = w;
		}
		#endregion

		#region vector-quaternion conversions
		/// <summary>
		/// Angles is xyz on a unit sphere
		/// </summary>
		/// <param name="Angles"></param>
		public Quaternion(Vector3 Angles) {
			float t0 = (float)Math.Cos(Angles.x * 0.5f);
			float t1 = (float)Math.Sin(Angles.x * 0.5f);
			float t2 = (float)Math.Cos(Angles.y * 0.5f);
			float t3 = (float)Math.Sin(Angles.y * 0.5f);
			float t4 = (float)Math.Cos(Angles.z * 0.5f);
			float t5 = (float)Math.Sin(Angles.z * 0.5f);

			w = t0 * t2 * t4 + t1 * t3 * t5;
			x = t0 * t3 * t4 - t1 * t2 * t5;
			y = t0 * t2 * t5 + t1 * t3 * t4;
			z = t1 * t2 * t4 - t0 * t3 * t5;
		}

		/// <summary>
		/// Converts to xyz on a unit sphere
		/// </summary>
		/// <returns></returns>
		public Vector3 ToAngles() {
			Vector3 angles = new Vector3();

			float ySqr = y * y;

			float t0 = 2f * (w * x + y * z);
			float t1 = 1f - 2f * (x * x + ySqr);
			angles.x = (float)Math.Atan2(t0, t1);

			float t2 = 2f * (w * y - z * x);
			t2 = t2 > 1 ? 1 : t2;
			t2 = t2 < -1 ? -1 : t2;
			angles.y = (float)Math.Asin(t2);

			float t3 = 2f * (w * z + x * y);
			float t4 = 1 - 2f * (ySqr + z * z);
			angles.z = (float)Math.Atan2(t3, t4);

			return angles;
		}

		public Quaternion LookRotation (Vector3 forward) {
			return LookRotation(forward, Vector3.Up);
		}

		public Quaternion LookRotation(Vector3 forward, Vector3 up) {
			forward = forward.Normalize();

			Vector3 vector = forward.Normalize();
			Vector3 vector2 = Vector3.Cross(up, vector).Normalize();
			Vector3 vector3 = Vector3.Cross(vector, vector2);
			var m00 = vector2.x;
			var m01 = vector2.y;
			var m02 = vector2.z;
			var m10 = vector3.x;
			var m11 = vector3.y;
			var m12 = vector3.z;
			var m20 = vector.x;
			var m21 = vector.y;
			var m22 = vector.z;

			float num8 = (m00 + m11) + m22;
			var quaternion = new Quaternion();
			if (num8 > 0f) {
				var num = (float)Math.Sqrt(num8 + 1.0);
				quaternion.w = num * 0.5f;
				num = 0.5f / num;
				quaternion.x = (m12 - m21) * num;
				quaternion.y = (m20 - m02) * num;
				quaternion.z = (m01 - m10) * num;
				return quaternion;
			}
			if ((m00 >= m11) && (m00 >= m22)) {
				var num7 = (float)Math.Sqrt(((1.0 + m00) - m11) - m22);
				var num4 = 0.5f / num7;
				quaternion.x = 0.5f * num7;
				quaternion.y = (m01 + m10) * num4;
				quaternion.z = (m02 + m20) * num4;
				quaternion.w = (m12 - m21) * num4;
				return quaternion;
			}
			if (m11 > m22) {
				var num6 = (float)Math.Sqrt(((1.0 + m11) - m00) - m22);
				var num3 = 0.5f / num6;
				quaternion.x = (m10 + m01) * num3;
				quaternion.y = 0.5f * num6;
				quaternion.z = (m21 + m12) * num3;
				quaternion.w = (m20 - m02) * num3;
				return quaternion;
			}
			var num5 = (float)Math.Sqrt(((1.0 + m22) - m00) - m11);
			var num2 = 0.5f / num5;
			quaternion.x = (m20 + m02) * num2;
			quaternion.y = (m21 + m12) * num2;
			quaternion.z = 0.5f * num5;
			quaternion.w = (m01 - m10) * num2;
			return quaternion;
		}
		#endregion

		#region operators
		public static Quaternion operator *(Quaternion a, Quaternion b) {
			return new Quaternion() {
				x = a.w * b.w - a.x * b.x - a.y * b.y - a.z * b.z,
				y = a.w * b.x + a.x * b.w + a.y * b.z - a.z * b.y,
				z = a.w * b.y - a.x * b.z + a.y * b.w + a.z * b.x,
				w = a.w * b.z + a.x * b.y - a.y * b.x + a.z * b.w
			};
		}
		public static Vector3 operator *(Quaternion lhs, Vector3 rhs) {
			Vector3 u = new Vector3(lhs.x, lhs.y, lhs.z);
			return (2 * Vector3.Dot(u, rhs) * u)
			+ ((lhs.w * lhs.w - Vector3.Dot(u, u)) * rhs)
			+ (2 * lhs.w * Vector3.Cross(u, rhs));
		}
		public static Vector3 operator *(Vector3 lhs, Quaternion rhs) {
			Vector3 u = new Vector3(rhs.x, rhs.y, rhs.z);
			return (2 * Vector3.Dot(u, lhs) * u)
			+ ((rhs.w * rhs.w - Vector3.Dot(u, u)) * lhs)
			+ (2 * rhs.w * Vector3.Cross(u, lhs));
		}
		#endregion

		#region functions
		public static explicit operator Matrix (Quaternion q) {
			return new Matrix(new float[][] { 
				new float[] { q.x, -q.y, -q.z, -q.w },
				new float[] { q.y, q.x, -q.w, q.z },
				new float[] { q.z, q.w, q.x, -q.y },
				new float[] { q.w, -q.z, q.y, q.x }
			});

		}
		#endregion
	}
}