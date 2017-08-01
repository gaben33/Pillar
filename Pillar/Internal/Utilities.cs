using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pillar3D {
	public static class Utilities {

		/// <summary>
		/// Floats the specified element to the top of the list
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="list"></param>
		/// <param name="index"></param>
		public static void Float<T>(this List<T> list, int index) {
			T elem = list[index];
			list.RemoveAt(index);
			list.Insert(0, elem);
		}
	}

	public class SmartCache<T> {
		private Func<T> Calculator;
		private bool dirty;
		private T val;
		public T Value {
			get {
				if (dirty) {
					dirty = false;
					val = Calculator();
				}
				return val;
			}
			set {
				dirty = false;
				val = value;
			}
		}

		public SmartCache(Func<T> Calculator, T initialValue) {
			dirty = false;
			val = initialValue;
			this.Calculator = Calculator;
		}

		public void Dirty() {
			dirty = true;
		}
	}

	public static class Mathf {
		#region constants
		public static float E { get; } = (float)Math.E;
		public static float Pi { get; } = (float)Math.PI;
		public static float Tau { get; } = 2 * Pi;
		public static float Rad2Deg { get; } = 180f / Pi;
		public static float Deg2Rad { get; } = Pi / 180f;
		public static float Infinity { get; } = float.PositiveInfinity;
		public static float NegativeInfinity { get; } = float.NegativeInfinity;
		public static float Epsilon { get; } = float.Epsilon;
		public static float MinValue { get; } = float.MinValue;
		public static float MaxValue { get; } = float.MaxValue;
		#endregion
		#region functions
		#region trigonometry
		public static float Acos(float d) => (float)Math.Acos(d);
		public static float Asin(float d) => (float)Math.Asin(d);
		public static float Atan(float d) => (float)Math.Atan(d);
		public static float Atan2(float x, float y) => (float)Math.Atan2(x, y);
		public static float Cos(float d) => (float)Math.Cos(d);
		public static float Cosh(float value) => (float)Math.Cosh(value);
		public static float Sin(float a) => (float)Math.Sin(a);
		public static float Sinh(float value) => (float)Math.Sinh(value);
		public static float Tan(float a) => (float)Math.Tan(a);
		public static float Tanh(float value) => (float)Math.Tanh(value);
		#endregion
		#region exponents
		public static float Exp(float d) => (float)Math.Exp(d);
		public static float Log(float d) => (float)Math.Log(d);
		public static float Ln(float d) => Log(d);
		public static float Log10(float d) => (float)Math.Log10(d);
		public static float Pow(float x, float y) => (float)Math.Pow(x, y);
		public static bool IsPowerOfTwo(int n) => (n != 0) && ((n & (n - 1)) == 0);
		public static int NextPowerOfTwo(int val) {
			val--;
			val |= val >> 1;
			val |= val >> 2;
			val |= val >> 4;
			val |= val >> 8;
			val |= val >> 16;
			val++;
			return val;
		}
		#endregion
		#region numerical
		public static bool Approximately(float a, float b) => Abs(a - b) < 5 * Epsilon;
		public static float Abs(float value) => (float)Math.Abs(value);
		public static float Ceiling(float a) => (float)Math.Ceiling(a);
		public static float Floor(float d) => (float)Math.Floor(d);
		public static float IEEERemainder(float x, float y) => (float)Math.IEEERemainder(x, y);
		public static float Round(float a) => (float)Math.Round(a);
		public static float Round(float value, MidpointRounding mode) => (float)Math.Round(value, mode);
		public static int RoundToInt(float value) => (int)(Round(value) + 0.1f);
		public static float Sqrt(float d) => (float)Math.Sqrt(d);
		public static float Truncate(float d) => (float)Math.Truncate(d);
		public static int FloorToInt(float d) => (int)d;
		public static int CeilToInt(float d) => (int)(d + 1);
		public static float LerpUnclamped(float a, float b, float t) => a + t * (b - a);
		public static float Lerp(float a, float b, float t) => Clamp(LerpUnclamped(a, b, t), a, b);
		public static float InverseLerp(float a, float b, float val) => (val - a) / (b - a);
		public static float Clamp(float val, float a, float b) => (val < a) ? a : (val > b) ? b : val;
		public static float Clamp01(float val) => Clamp(val, 0, 1);
		public static float Max(float a, float b) => (a > b) ? a : b;
		public static float Min(float a, float b) => (a < b) ? a : b;
		public static float Repeat(float value, float interval) => interval * ((value / interval) % 1f);
		public static int Sign(float value) => value > 0 ? 1 : -1;
		#endregion
		#endregion
	}
}