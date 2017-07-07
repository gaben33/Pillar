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
		
		public static float Lerp (float a, float b, float t) {
			return a + t * (b - a);
		}
	}
}
