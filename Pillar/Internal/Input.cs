using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;


namespace Pillar3D {
	public static class Input {
		#region variables
		public static Key CurrentKey { get; private set;}
		//public static Key[] PressedKeys { get; private set; }
		//public bool AnyKey { get { return PressedKeys.Any(); } }
		private static Dictionary<string, InputAxis> Axes = new Dictionary<string, InputAxis>();
		private static Dictionary<string, InputVector2D> VectorInputs2D = new Dictionary<string, InputVector2D>();
		private static Dictionary<string, InputVector3D> VectorInputs3D = new Dictionary<string, InputVector3D>();
		private static Dictionary<Key, bool> CachedKeyValues = new Dictionary<Key, bool>(123);
		#endregion

		#region internal
		public class InputAxis {
			private Key Negative, Positive;
			public InputAxis(Key NegativeKey, Key PositiveKey) {
				Negative = NegativeKey;
				Positive = PositiveKey;
			}

			public float Evaluate() {
				return (GetKey(Positive) ? 1f : 0f) + (GetKey(Negative) ? -1f : 0);
			}

			public bool Dead () {
				return Mathf.Approximately(Evaluate(), 0f);
			}
		}
		public class InputVector2D {
			public InputAxis X, Y;
			public InputVector2D(InputAxis x, InputAxis y) {
				X = x;
				Y = y;
			}
			public Vector2 Evaluate() => new Vector2(X.Evaluate(), Y.Evaluate());
			public bool Dead() => Evaluate() == Vector2.Zero;
		}
		public class InputVector3D : InputVector2D {
			public InputAxis Z;
			public InputVector3D (InputAxis x, InputAxis y, InputAxis z) : base(x, y) {
				Z = z;
			}
			new public Vector3 Evaluate() => new Vector3(X.Evaluate(), Y.Evaluate(), Z.Evaluate());
		}
		//private static IEnumerable<Key> CurrentKeysDown () {
		//	var keys = new byte[256];
		//	GetKeyboardState(keys);
		//	var downKeys = new List<Key>();
		//	for (var index = 0; index < 256; index++) {
		//		byte key = keys[index];
		//		if ((key & 0x80) != 0) {
		//			downKeys.Add((Key)key);
		//		}
		//	}
		//	return downKeys;
		//}
		//private static byte GetVirtualKeyCode(Key key) {
		//	int value = (int)key;
		//	return (byte)(value & 0xFF);
		//}
		//private static string ToArrayString (byte[] array) {
		//	string s = array[0].ToString();
		//	for (int i = 1; i < array.Length; i++) s += $"{array[i].ToString()}, ";
		//	return s;
		//}
		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool GetKeyboardState(byte[] lpKeyState);
		#endregion

		#region external
		public static KeyStates GetKeyState(Key key) {
			if (CachedKeyValues.ContainsKey(key)) CachedKeyValues[key] = Keyboard.IsKeyDown(key);
			else CachedKeyValues.Add(key, Keyboard.IsKeyDown(key));
			return Keyboard.GetKeyStates(key);
		}
		public static bool GetKey(Key key) {
			GetKeyState(key);
			return Keyboard.IsKeyDown(key);
		}
		public static bool GetKeyDown(Key key) {
			if (!CachedKeyValues.ContainsKey(key)) CachedKeyValues.Add(key, false);
			bool IsDown = Keyboard.IsKeyDown(key) && CachedKeyValues[key] == false;
			if (IsDown) CachedKeyValues[key] = true;
			return IsDown;
		}
		public static bool GetKeyUp(Key key) {
			if (!CachedKeyValues.ContainsKey(key)) CachedKeyValues.Add(key, false);
			bool IsUp = !Keyboard.IsKeyDown(key) && CachedKeyValues[key] == true;
			if (IsUp) CachedKeyValues[key] = false;
			return IsUp;
		}

		public static void CreateAxis(string name, InputAxis axis) {
			if (Axes.ContainsKey(name)) Axes[name] = axis;
			else Axes.Add(name, axis);
		}
		public static float GetAxis(string name) {
			return Axes[name].Evaluate();
		}

		public static void CreateInputVector2D(string name, InputVector2D axis) {
			if (VectorInputs2D.ContainsKey(name)) VectorInputs2D[name] = axis;
			else VectorInputs2D.Add(name, axis);
		}
		public static Vector2 GetInputVector2D(string name) {
			return VectorInputs2D[name].Evaluate();
		}

		public static void CreateInputVector3D(string name, InputVector3D axis) {
			if (VectorInputs3D.ContainsKey(name)) VectorInputs3D[name] = axis;
			else VectorInputs3D.Add(name, axis);
		}
		public static Vector3 GetInputVector3D(string name) {
			return VectorInputs3D[name].Evaluate();
		}
		#endregion
	}
}