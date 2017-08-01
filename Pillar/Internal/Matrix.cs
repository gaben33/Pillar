using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pillar3D {
	public class Matrix {
		#region variables
		protected float[][] values;

		public int Width { get { return values[0].Length; } }
		public int Height { get { return values.Length; } }
		public bool Square { get { return Width == Height; } }
		#endregion

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

		public float Determinant() {
			if (!Square) throw new ArgumentException($"Determinants only exist for square matrices.  This matrix is {Width} by {Height}");
			if (Height == 2) {
				return (this[0, 0] * this[1, 1]) - (this[0, 1] * this[1, 0]);
			}
			SByte sign = 1;
			float sum = 0;
			for (int i = 0; i < Width; i++) {
				sum += sign * this[0, i] * GetExclusionaryMatrix(0, i).Determinant();
				sign *= -1;
			}
			return sum;
		}

		public float Trace() {
			if (!Square) throw new ArgumentException("Trace requires a square matrix!");
			float sum = 0;
			for (int i = 0; i < Height; i++) {
				sum += this[i, i];
			}
			return sum;
		}

		public Matrix Transpose() {
			int w = Width;
			int h = Height;
			Matrix result = new Matrix(w, h);
			for (int i = 0; i < w; i++) {
				for (int j = 0; j < h; j++) result[i, j] = this[j, i];
			}
			return result;
		}

		public static Matrix operator +(Matrix lhs, Matrix rhs) {
			if (lhs.Width != rhs.Width || lhs.Height != rhs.Height) throw new ArgumentException("Matrix dimensions must match!");
			Matrix result = new Matrix(lhs.Width, lhs.Height);
			for (int i = 0; i < result.Width; i++) {
				for (int j = 0; j < result.Height; j++) result[i, j] = lhs[i, j] + rhs[i, j];
			}
			return result;
		}
		public static Matrix operator -(Matrix lhs, Matrix rhs) {
			if (lhs.Width != rhs.Width || lhs.Height != rhs.Height) throw new ArgumentException("Matrix dimensions must match!");
			Matrix result = new Matrix(lhs.Width, lhs.Height);
			for (int i = 0; i < result.Width; i++) {
				for (int j = 0; j < result.Height; j++) result[i, j] = lhs[i, j] - rhs[i, j];
			}
			return result;
		}
		public static Matrix operator *(Matrix lhs, float scalar) {
			Matrix result = new Matrix(lhs.Width, lhs.Height);
			for (int i = 0; i < result.Width; i++) {
				for (int j = 0; j < result.Height; j++) result[i, j] = lhs[i, j] * scalar;
			}
			return result;
		}
		public static Matrix operator *(float scalar, Matrix rhs) {
			Matrix result = new Matrix(rhs.Width, rhs.Height);
			for (int i = 0; i < result.Width; i++) {
				for (int j = 0; j < result.Height; j++) result[i, j] = rhs[i, j] * scalar;
			}
			return result;
		}
		public static Matrix operator *(Matrix lhs, Matrix rhs) {
			int r1 = lhs.Height, r2 = rhs.Height, c1 = lhs.Width, c2 = rhs.Width;//rows and columns of lhs (1) and rhs (2)
			if (c1 != r2) throw new ArgumentException("matrix dimensions are not multipliable");
			Matrix result = new Matrix(r1, c2);
			for (int x = 0; x < result.values.Length; x++) {
				for (int y = 0; y < result.values[0].Length; y++) {
					result[x, y] = GetResult(lhs, rhs, x, y);
				}
			}
			return result;
		}

		//returns the dot product of the rows that corroborate to form (x,y) in the resultant matrix
		protected static float GetResult(Matrix lhs, Matrix rhs, int x, int y) {
			if (lhs.Width != rhs.Height) throw new ArgumentException("vector dimensions don't match!");
			return VectorN.Dot(lhs.GetRow(x), rhs.GetColumn(y));
		}
		//returns a new matrix that excludes the given row and column
		public Matrix GetExclusionaryMatrix(int row, int column) {
			Matrix result = new Matrix(Width - 1, Height - 1);
			int r = 0, c = 0;
			for (int x = 0; x < Width; x++) {
				if (x == row) continue;
				r = 0;
				for (int y = 0; y < Height; y++) {
					if (y == column) continue;
					result[c, r] = this[x, y];
					r++;
				}
				c++;
			}
			return result;
		}

		public VectorN GetColumn(int column) {
			if (column > Height) throw new ArgumentException("column doesn't exist");
			VectorN result = new VectorN(Height);
			for (int i = 0; i < Height; i++) {
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
		public void SetColumn (int column, float[] values) {
			if (column > Height) throw new ArgumentException("column doesn't exist");
			if (values.Length != this.values.Length) throw new ArgumentException("Input vector has incorrect dimensions");
			for (int i = 0; i < Height; i++) {
				this[i, column] = values[i];
			}
		}
		public void SetRow (int row, float[] values) {
			if (row > Width) throw new ArgumentException("column doesn't exist");
			if (values.Length != this.values[0].Length) throw new ArgumentException("Input vector has incorrect dimensions");
			for (int i = 0; i < Width; i++) {
				this[row, i] = values[i];
			}
		}
		#endregion

		#region functions
		public override string ToString() {
			string s = "";
			for (int i = 0; i < Height; i++) {
				s += "| ";
				for (int j = 0; j < Width; j++) {
					s += $"{this[i, j]} ";
				}
				s += "|\n";
			}
			return s;
		}

		public static Matrix Identity(Matrix A) {
			int size = A.Height;
			Matrix result = new Matrix(A.Height, A.Height);
			for (int i = 0; i < result.Width; i++) {
				for (int j = 0; j < result.Height; j++) result[i, j] = (i == j) ? 1 : 0;
			}
			return result;
		}
		public static Matrix Zero(Matrix A) {
			int size = A.Height;
			Matrix result = new Matrix(A.Height, A.Height);
			for (int i = 0; i < result.Width; i++) {
				for (int j = 0; j < result.Height; j++) result[i, j] = 0;
			}
			return result;
		}
		#endregion
	}

	public class Matrix4x4 : Matrix {
		public Matrix4x4() : base(4, 4) {
			values = new float[][] {
				new float[] {0,0,0,0},
				new float[] {0,0,0,0},
				new float[] {0,0,0,0},
				new float[] {0,0,0,0}
			};
		}

		public Matrix4x4(float[][] initialValues) : base(initialValues) { }

		new public Vector4 GetColumn(int column) {
			if (column > 4) throw new ArgumentException("column doesn't exist");
			Vector4 result = new Vector4();
			for (int i = 0; i < 4; i++) {
				result[i] = this[i, column];
			}
			return result;
		}

		new public Vector4 GetRow(int row) {
			if (row > 4) throw new ArgumentException("row doesn't exist");
			Vector4 result = new Vector4();
			for (int i = 0; i < 4; i++) {
				result[i] = this[row, i];
			}
			return result;
		}

		public void SetColumn(int column, Vector4 values) {
			if (column > 4) throw new ArgumentException("column doesn't exist");
			for (int i = 0; i < 4; i++) {
				this[i, column] = values[i];
			}
		}

		public void SetRow(int row, Vector4 values) {
			if (row > 4) throw new ArgumentException("column doesn't exist");
			for (int i = 0; i < 4; i++) {
				this[row, i] = values[i];
			}
		}

		public static Matrix4x4 TRS(Vector3 p, Vector3 r, Vector3 s) {
			return (Matrix4x4)(Translate(p) * Rotate(r) * Scale(s));
		}

		public static Matrix4x4 Translate(Vector3 p) {
			return new Matrix4x4(new float[][] {
				new float[] { 1, 0, 0, p.x },
				new float[] { 0, 1, 0, p.y },
				new float[] { 0, 0, 1, p.z },
				new float[] { 0, 0, 0, 1 }
			});
		}

		public static Matrix4x4 Scale(Vector3 s) {
			return new Matrix4x4(new float[][] {
				new float[] {s.x,0,0,0},
				new float[] {0,s.y,0,0},
				new float[] {0,0,s.z,0},
				new float[] {0,0,0,1}
			});
		}

		public static Matrix4x4 Rotate(Vector3 r) {
			float radX = r.x * Mathf.Deg2Rad;
			float radY = r.y * Mathf.Deg2Rad;
			float radZ = r.z * Mathf.Deg2Rad;
			float sinX = Mathf.Sin(radX);
			float cosX = Mathf.Cos(radX);
			float sinY = Mathf.Sin(radY);
			float cosY = Mathf.Cos(radY);
			float sinZ = Mathf.Sin(radZ);
			float cosZ = Mathf.Cos(radZ);

			Matrix4x4 matrix = new Matrix4x4();
			matrix.SetColumn(0, new Vector4(cosY * cosZ, cosX * sinZ + sinX * sinY * cosZ, sinX * sinZ - cosX * sinY * cosZ, 0));
			matrix.SetColumn(1, new Vector4(-cosY * sinZ, cosX * cosZ - sinX * sinY * sinZ, sinX * cosZ + cosX * sinY * sinZ, 0));
			matrix.SetColumn(2, new Vector4(sinY, -sinX * cosY,	cosX * cosY, 0));
			matrix.SetColumn(3, new Vector4(0, 0, 0, 1));
			return matrix;
		}
	}
}