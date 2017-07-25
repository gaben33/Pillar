﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Pillar3D {
	public class Input {
		#region variables
		public Key CurrentKey { get; private set;}
		public Key[] PressedKeys { get; private set; }
		public bool AnyKey { get { return PressedKeys.Any(); } }
		private static Dictionary<string, InputAxis> Axes;
		private static Dictionary<string, InputVector2D> VectorInputs2D;
		private static Dictionary<string, InputVector3D> VectorInputs3D;
		private static Input instance;
		private static readonly byte[] DistinctVirtualKeys = Enumerable
			.Range(0, 256)
			.Select(KeyInterop.KeyFromVirtualKey)
			.Where(item => item != Key.None)
			.Distinct()
			.Select(item => (byte)KeyInterop.VirtualKeyFromKey(item))
			.ToArray();
		#endregion

		#region constructors
		public Input() {
			instance = this;
			Rails.PersistantUpdate += Poll;
			Axes = new Dictionary<string, InputAxis>();
			VectorInputs2D = new Dictionary<string, InputVector2D>();
			VectorInputs3D = new Dictionary<string, InputVector3D>();
		}
		~Input() {
			Rails.PersistantUpdate -= Poll;
		}
		#endregion

		#region internal
		public class InputAxis {
			private Key Negative, Positive;
			public InputAxis(Key NegativeKey, Key PositiveKey) {
				Negative = NegativeKey;
				Positive = PositiveKey;
			}

			public float Evaluate() {
				return (Input.GetKey(Positive) ? 1f : 0f) + (Input.GetKey(Negative) ? -1f : 0);
			}
		}
		public class InputVector2D {
			public InputAxis X, Y;
			public InputVector2D(InputAxis x, InputAxis y) {
				X = x;
				Y = y;
			}
			public Vector2 Evaluate() {
				return new Vector2(X.Evaluate(), Y.Evaluate());
			}
		}
		public class InputVector3D : InputVector2D {
			public InputAxis Z;
			public InputVector3D (InputAxis x, InputAxis y, InputAxis z) : base(x, y) {
				Z = z;
			}
			new public Vector3 Evaluate() {
				return new Vector3(X.Evaluate(), Y.Evaluate(), Z.Evaluate());
			}
		}
		public static void Poll () {
			instance.PressedKeys = CurrentKeysDown().ToArray();
		}
		private static IEnumerable<Key> CurrentKeysDown () {
			var keyboardState = new byte[256];
			GetKeyboardState(keyboardState);

			var downKeys = new List<Key>();
			for (var index = 0; index < DistinctVirtualKeys.Length; index++) {
				var virtualKey = DistinctVirtualKeys[index];
				if ((keyboardState[virtualKey] & 0x80) != 0) {
					downKeys.Add(KeyInterop.KeyFromVirtualKey(virtualKey));
				}
			}

			return downKeys;
		}
		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool GetKeyboardState(byte[] lpKeyState);
		#endregion

		#region external
		public static KeyStates GetKeyState(Key key) {
			return Keyboard.GetKeyStates(key);
		}
		public static bool GetKey(Key key) {
			return Keyboard.IsKeyDown(key);
		}
		public static bool GetKeyDown(Key key) {
			return Keyboard.IsKeyDown(key) && Keyboard.IsKeyToggled(key);
		}
		public static bool GetKeyUp(Key key) {
			return Keyboard.IsKeyUp(key) && Keyboard.IsKeyToggled(key);
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