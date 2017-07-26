using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pillar3D {
	public class Matrix {
		private float[][] values;

		public int Width { get { return values[0].Length; } }
		public int Height { get { return values.Length; } }
		public bool Square {  get { return Width == Height; } }

		#region constructors
		public Matrix(int rows, int columns) {
			values = new float[rows][];
			for (int i = 0; i < rows; i++) values[i] = new float[columns];
		}

		public Matrix(float[][] values) {
			this.values = values;
		}
		#endregion

		#region operators
		public float this[int row, int col] {
			get {
				return values[row][col];
			}
			set {
				values[row][col] = value;
			}
		}

		public float Determinant () {
			if (!Square) throw new ArgumentException($"Determinants only exist for square matrices.  This matrix is {Width} by {Height}");
			if(Height == 2) {
				return (this[0,0] * this[1,1]) - (this[0,1] * this[1,0]);
			}
			SByte sign = 1;
			float sum = 0;
			for(int i = 0; i < Width; i++) {
				sum += sign * this[0, i] * GetExclusionaryMatrix(0,i).Determinant();
				sign *= -1;
			}
			return sum;
		}

		public float Trace () {
			if (!Square) throw new ArgumentException("Trace requires a square matrix!");
			float sum = 0;
			for(int i = 0; i < Height; i++) {
				sum += this[i, i];
			}
			return sum;
		}

		public static Matrix operator *(Matrix lhs, Matrix rhs) {
			int r1 = lhs.Height, r2 = rhs.Height, c1 = lhs.Width, c2 = rhs.Width;//rows and columns of lhs (1) and rhs (2)
			if (c1 != r2) throw new ArgumentException("matrix dimensions are not multipliable");
			Matrix result = new Matrix(r1, c2);
			for(int x = 0; x < result.values.Length; x++) {
				for(int y = 0; y < result.values[0].Length; y++) {
					result[x, y] = GetResult(lhs, rhs, x, y);
				}
			}
			return result;
		}
		//returns the dot product of the rows that corroborate to form (x,y) in the resultant matrix
		private static float GetResult(Matrix lhs, Matrix rhs, int x, int y) {
			if (lhs.Width != rhs.Height) throw new ArgumentException("vector dimensions don't match!");
			return VectorN.Dot(lhs.GetRow(x), rhs.GetColumn(y));
		}
		//returns a new matrix that excludes the given row and column
		public Matrix GetExclusionaryMatrix (int row, int column) {
			Matrix result = new Matrix(Width - 1, Height - 1);
			int r = 0, c = 0;
			for(int x = 0; x < Width; x++) {
				if (x == row) continue;
				r = 0;
				for(int y = 0; y < Height; y++) {
					if (y == column) continue;
					result[c,r] = this[x,y];
					r++;
				}
				c++;
			}
			return result;
		}

		public VectorN GetColumn (int column) {
			if (column > Height) throw new ArgumentException("column doesn't exist");
			VectorN result = new VectorN(Height);
			for(int i = 0; i < Height; i++) {
				result[i] = this[i, column];
			}
			return result;
		}
		public VectorN GetRow(int row) {
			if (row > Width) throw new ArgumentException("row doesn't exist");
			VectorN result = new VectorN(Width);
			for (int i = 0; i < Width; i++) {
				result[i] = this[row, i];
			}
			return result;
		}
		#endregion

		#region functions
		public override string ToString() {
			string s = "";
			for(int i = 0; i < Height; i++) {
				s += "| ";
				for (int j = 0; j < Width; j++) {
					s += $"{this[i,j]} ";
				}
				s += "|\n";
			}
			return s;
		}
		#endregion
	}
}