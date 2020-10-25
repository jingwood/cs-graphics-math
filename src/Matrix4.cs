/*
 * MIT License
 * 
 * Copyright (c) 2015-2020 Jingwood, unvell.com. All right reserved.
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */

using System;
using System.Text;

namespace unvell.GraphicsMath
{
	public class Matrix4
	{

		public float a1 = 1, b1 = 0, c1 = 0, d1 = 0;
		public float a2 = 0, b2 = 1, c2 = 0, d2 = 0;
		public float a3 = 0, b3 = 0, c3 = 1, d3 = 0;
		public float a4 = 0, b4 = 0, c4 = 0, d4 = 1;

		public Matrix4()
		{
		}

		public Matrix4(float a1, float b1, float c1, float d1,
			float a2, float b2, float c2, float d2,
			float a3, float b3, float c3, float d3,
			float a4, float b4, float c4, float d4)
		{
			this.a1 = a1; this.b1 = b1; this.c1 = c1; this.d1 = d1;
			this.a2 = a2; this.b2 = b2; this.c2 = c2; this.d2 = d2;
			this.a3 = a3; this.b3 = b3; this.c3 = c3; this.d3 = d3;
			this.a4 = a4; this.b4 = b4; this.c4 = c4; this.d4 = d4;
		}

		public Matrix4(Matrix4 m)
		{
			CopyFrom(m);
		}

		public Matrix4 LoadIdentity()
		{
			this.a1 = 1; this.b1 = 0; this.c1 = 0; this.d1 = 0;
			this.a2 = 0; this.b2 = 1; this.c2 = 0; this.d2 = 0;
			this.a3 = 0; this.b3 = 0; this.c3 = 1; this.d3 = 0;
			this.a4 = 0; this.b4 = 0; this.c4 = 0; this.d4 = 1;

			return this;
		}

		#region Rotate

		public Matrix4 RotateX(float angle)
		{
			if (angle == 0) return this;

			float d = angle * Const.PI / 180f;

			float sin = (float)Math.Sin(d);
			float cos = (float)Math.Cos(d);

			float m2b2 = cos, m2c2 = -sin;
			float m2b3 = sin, m2c3 = cos;

			float b1 = this.b1 * m2b2 + this.c1 * m2b3;
			float c1 = this.b1 * m2c2 + this.c1 * m2c3;

			float b2 = this.b2 * m2b2 + this.c2 * m2b3;
			float c2 = this.b2 * m2c2 + this.c2 * m2c3;

			float b3 = this.b3 * m2b2 + this.c3 * m2b3;
			float c3 = this.b3 * m2c2 + this.c3 * m2c3;

			float b4 = this.b4 * m2b2 + this.c4 * m2b3;
			float c4 = this.b4 * m2c2 + this.c4 * m2c3;

			this.b1 = b1; this.c1 = c1;
			this.b2 = b2; this.c2 = c2;
			this.b3 = b3; this.c3 = c3;
			this.b4 = b4; this.c4 = c4;

			return this;
		}

		public void RotateY(float angle)
		{
			if (angle == 0) return;

			float d = angle * Const.PI / 180f;

			float sin = (float)Math.Sin(d);
			float cos = (float)Math.Cos(d);

			float m2a1 = cos, m2c1 = sin;
			float m2a3 = -sin, m2c3 = cos;

			float a1 = this.a1 * m2a1 + this.c1 * m2a3;
			float c1 = this.a1 * m2c1 + this.c1 * m2c3;

			float a2 = this.a2 * m2a1 + this.c2 * m2a3;
			float c2 = this.a2 * m2c1 + this.c2 * m2c3;

			float a3 = this.a3 * m2a1 + this.c3 * m2a3;
			float c3 = this.a3 * m2c1 + this.c3 * m2c3;

			float a4 = this.a4 * m2a1 + this.c4 * m2a3;
			float c4 = this.a4 * m2c1 + this.c4 * m2c3;

			this.a1 = a1; this.c1 = c1;
			this.a2 = a2; this.c2 = c2;
			this.a3 = a3; this.c3 = c3;
			this.a4 = a4; this.c4 = c4;
		}

		public void RotateZ(float angle)
		{
			if (angle == 0) return;

			float d = angle * Const.PI / 180f;

			float sin = (float)Math.Sin(d);
			float cos = (float)Math.Cos(d);

			float m2a1 = cos, m2b1 = sin;
			float m2a2 = -sin, m2b2 = cos;

			float a1 = this.a1 * m2a1 + this.b1 * m2a2;
			float b1 = this.a1 * m2b1 + this.b1 * m2b2;

			float a2 = this.a2 * m2a1 + this.b2 * m2a2;
			float b2 = this.a2 * m2b1 + this.b2 * m2b2;

			float a3 = this.a3 * m2a1 + this.b3 * m2a2;
			float b3 = this.a3 * m2b1 + this.b3 * m2b2;

			float a4 = this.a4 * m2a1 + this.b4 * m2a2;
			float b4 = this.a4 * m2b1 + this.b4 * m2b2;

			this.a1 = a1; this.b1 = b1;
			this.a2 = a2; this.b2 = b2;
			this.a3 = a3; this.b3 = b3;
			this.a4 = a4; this.b4 = b4;
		}

		public void Rotate(Vector3 v, EulerOrder order = EulerOrder.XYZ)
		{
			this.Rotate(v.x, v.y, v.z, order);
		}

		public void Rotate(float x, float y, float z, EulerOrder order = EulerOrder.XYZ)
		{
			double dx = x * Math.PI / 180f;
			double dy = y * Math.PI / 180f;
			double dz = z * Math.PI / 180f;

			float
				sinA = (float)Math.Sin(dx),
				cosA = (float)Math.Cos(dx),
				sinB = (float)Math.Sin(dy),
				cosB = (float)Math.Cos(dy),
				sinC = (float)Math.Sin(dz),
				cosC = (float)Math.Cos(dz);

			float m2a1, m2b1, m2c1,
				m2a2, m2b2, m2c2,
				m2a3, m2b3, m2c3;

			switch (order)
			{
				default:
				case EulerOrder.XYZ:
					m2a1 = cosB * cosC;
					m2b1 = -cosB * sinC;
					m2c1 = sinB;

					m2a2 = cosC * sinA * sinB + cosA * sinC;
					m2b2 = -sinC * sinB * sinA + cosC * cosA;
					m2c2 = -cosB * sinA;

					m2a3 = -cosC * sinB * cosA + sinC * sinA;
					m2b3 = cosC * sinA + sinC * sinB * cosA;
					m2c3 = cosB * cosA;
					break;

				case EulerOrder.ZYX:
					m2a1 = cosC * cosB;
					m2b1 = -sinC * cosA + cosC * sinB * sinA;
					m2c1 = sinC * sinA + cosC * sinB * cosA;

					m2a2 = sinC * cosB;
					m2b2 = cosC * cosA + sinC * sinB * sinA;
					m2c2 = -cosC * sinA + sinC * sinB * cosA;

					m2a3 = -sinB;
					m2b3 = cosB * sinA;
					m2c3 = cosB * cosA;
					break;

				case EulerOrder.XZY:
				case EulerOrder.YXZ:
				case EulerOrder.YZX:
				case EulerOrder.ZXY:
					throw new NotSupportedException("Specified euler order is not supported yet.");
			}

			float a1 = this.a1 * m2a1 + this.b1 * m2a2 + this.c1 * m2a3;
			float b1 = this.a1 * m2b1 + this.b1 * m2b2 + this.c1 * m2b3;
			float c1 = this.a1 * m2c1 + this.b1 * m2c2 + this.c1 * m2c3;

			float a2 = this.a2 * m2a1 + this.b2 * m2a2 + this.c2 * m2a3;
			float b2 = this.a2 * m2b1 + this.b2 * m2b2 + this.c2 * m2b3;
			float c2 = this.a2 * m2c1 + this.b2 * m2c2 + this.c2 * m2c3;

			float a3 = this.a3 * m2a1 + this.b3 * m2a2 + this.c3 * m2a3;
			float b3 = this.a3 * m2b1 + this.b3 * m2b2 + this.c3 * m2b3;
			float c3 = this.a3 * m2c1 + this.b3 * m2c2 + this.c3 * m2c3;

			float a4 = this.a4 * m2a1 + this.b4 * m2a2 + this.c4 * m2a3;
			float b4 = this.a4 * m2b1 + this.b4 * m2b2 + this.c4 * m2b3;
			float c4 = this.a4 * m2c1 + this.b4 * m2c2 + this.c4 * m2c3;

			this.a1 = a1; this.b1 = b1; this.c1 = c1;
			this.a2 = a2; this.b2 = b2; this.c2 = c2;
			this.a3 = a3; this.b3 = b3; this.c3 = c3;
			this.a4 = a4; this.b4 = b4; this.c4 = c4;
		}

		#endregion // Rotate

		#region Translate & Scale

		internal void Translate(Vector3 value)
		{
			this.Translate(value.x, value.y, value.z);
		}

		public Matrix4 Translate(float x, float y, float z)
		{
			if (x == 0 && y == 0 && z == 0) return this;

			this.d1 += this.a1 * x + this.b1 * y + this.c1 * z;
			this.d2 += this.a2 * x + this.b2 * y + this.c2 * z;
			this.d3 += this.a3 * x + this.b3 * y + this.c3 * z;
			this.d4 += this.a4 * x + this.b4 * y + this.c4 * z;

			return this;
		}

		public static Matrix4 CreateTranslate(float x, float y, float z)
		{
			return new Matrix4().Translate(x, y, z);
		}

		public void Scale(float factor)
		{
			this.Scale(factor, factor, factor);
		}

		public void Scale(Vector3 value)
		{
			this.Scale(value.x, value.y, value.z);
		}

		public void Scale(float x, float y, float z)
		{
			if (x == 1 && y == 1 && z == 1) return;

			this.a1 *= x; this.b1 *= y; this.c1 *= z;
			this.a2 *= x; this.b2 *= y; this.c2 *= z;
			this.a3 *= x; this.b3 *= y; this.c3 *= z;

		}
		#endregion // Translate & Scale

		#region Inverse

		public bool CanInverse
		{
			get
			{
				float
					a = a1, b = b1, c = c1, d = d1,
					e = a2, f = b2, g = c2, h = d2,
					i = a3, j = b3, k = c3, l = d3,
					m = a4, n = b4, o = c4, p = d4;

				float q = f * k * p + j * o * h + n * g * l
					- f * l * o - g * j * p - h * k * n;

				float r = e * k * p + i * o * h + m * g * l
					- e * l * o - g * i * p - h * k * m;

				float s = e * j * p + i * n * h + m * f * l
					- e * l * n - f * i * p - h * j * m;

				float t = e * j * o + i * n * g + m * f * k
					- e * k * n - f * i * o - g * j * m;

				float delta = (a * q - b * r + c * s - d * t);

				return (delta != 0);
			}
		}

		public Matrix4 Inverse()
		{
			float
				a = a1, b = b1, c = c1, d = d1,
				e = a2, f = b2, g = c2, h = d2,
				i = a3, j = b3, k = c3, l = d3,
				m = a4, n = b4, o = c4, p = d4;

			float q = f * k * p + j * o * h + n * g * l
				- f * l * o - g * j * p - h * k * n;

			float r = e * k * p + i * o * h + m * g * l
				- e * l * o - g * i * p - h * k * m;

			float s = e * j * p + i * n * h + m * f * l
				- e * l * n - f * i * p - h * j * m;

			float t = e * j * o + i * n * g + m * f * k
				- e * k * n - f * i * o - g * j * m;

			float delta = (a * q - b * r + c * s - d * t);

			if (delta == 0) return this;

			float detM = 1 / delta;

			// adj
			float m2a1 = q, m2b1 = r, m2c1 = s, m2d1 = t;
			float m2a2 = b * k * p + j * o * d + n * c * l - b * l * o - c * j * p - d * k * n;
			float m2b2 = a * k * p + i * o * d + m * c * l - a * l * o - c * i * p - d * k * m;
			float m2c2 = a * j * p + i * n * d + m * b * l - a * l * n - b * i * p - d * j * m;
			float m2d2 = a * j * o + i * n * c + m * b * k - a * k * n - b * i * o - c * j * m;
			float m2a3 = b * g * p + f * o * d + n * c * h - b * h * o - c * f * p - d * g * n;
			float m2b3 = a * g * p + e * o * d + m * c * h - a * h * o - c * e * p - d * g * m;
			float m2c3 = a * f * p + e * n * d + m * b * h - a * h * n - b * e * p - d * f * m;
			float m2d3 = a * f * o + e * n * c + m * b * g - a * g * n - b * e * o - c * f * m;
			float m2a4 = b * g * l + f * k * d + j * c * h - b * h * k - c * f * l - d * g * j;
			float m2b4 = a * g * l + e * k * d + i * c * h - a * h * k - c * e * l - d * g * i;
			float m2c4 = a * f * l + e * j * d + i * b * h - a * h * j - b * e * l - d * f * i;
			float m2d4 = a * f * k + e * j * c + i * b * g - a * g * j - b * e * k - c * f * i;

			m2b1 = -m2b1; m2d1 = -m2d1;
			m2a2 = -m2a2; m2c2 = -m2c2;
			m2b3 = -m2b3; m2d3 = -m2d3;
			m2a4 = -m2a4; m2c4 = -m2c4;

			// transpose
			float m3a1 = m2a1, m3b1 = m2a2, m3c1 = m2a3, m3d1 = m2a4;
			float m3a2 = m2b1, m3b2 = m2b2, m3c2 = m2b3, m3d2 = m2b4;
			float m3a3 = m2c1, m3b3 = m2c2, m3c3 = m2c3, m3d3 = m2c4;
			float m3a4 = m2d1, m3b4 = m2d2, m3c4 = m2d3, m3d4 = m2d4;

			this.a1 = m3a1 * detM; this.b1 = m3b1 * detM; this.c1 = m3c1 * detM; this.d1 = m3d1 * detM;
			this.a2 = m3a2 * detM; this.b2 = m3b2 * detM; this.c2 = m3c2 * detM; this.d2 = m3d2 * detM;
			this.a3 = m3a3 * detM; this.b3 = m3b3 * detM; this.c3 = m3c3 * detM; this.d3 = m3d3 * detM;
			this.a4 = m3a4 * detM; this.b4 = m3b4 * detM; this.c4 = m3c4 * detM; this.d4 = m3d4 * detM;

			return this;
		}
		#endregion // Inverse

		#region Transpose
		public Matrix4 Transpose()
		{
			float a2 = this.b1;
			float a3 = this.c1;
			float a4 = this.d1;

			float b1 = this.a2;
			float b3 = this.c2;
			float b4 = this.d2;

			float c1 = this.a3;
			float c2 = this.b3;
			float c4 = this.d3;

			float d1 = this.a4;
			float d2 = this.b4;
			float d3 = this.c4;

			this.b1 = b1; this.c1 = c1; this.d1 = d1;
			this.a2 = a2; this.c2 = c2; this.d2 = d2;
			this.a3 = a3; this.b3 = b3; this.d3 = d3;
			this.a4 = a4; this.b4 = b4; this.c4 = c4;

			return this;
		}
		#endregion // Transpose

		#region Operator
		public static Matrix4 operator *(Matrix4 m1, Matrix4 m2)
		{
			return new Matrix4(
				m1.a1 * m2.a1 + m1.b1 * m2.a2 + m1.c1 * m2.a3 + m1.d1 * m2.a4,
				m1.a1 * m2.b1 + m1.b1 * m2.b2 + m1.c1 * m2.b3 + m1.d1 * m2.b4,
				m1.a1 * m2.c1 + m1.b1 * m2.c2 + m1.c1 * m2.c3 + m1.d1 * m2.c4,
				m1.a1 * m2.d1 + m1.b1 * m2.d2 + m1.c1 * m2.d3 + m1.d1 * m2.d4,

				m1.a2 * m2.a1 + m1.b2 * m2.a2 + m1.c2 * m2.a3 + m1.d2 * m2.a4,
				m1.a2 * m2.b1 + m1.b2 * m2.b2 + m1.c2 * m2.b3 + m1.d2 * m2.b4,
				m1.a2 * m2.c1 + m1.b2 * m2.c2 + m1.c2 * m2.c3 + m1.d2 * m2.c4,
				m1.a2 * m2.d1 + m1.b2 * m2.d2 + m1.c2 * m2.d3 + m1.d2 * m2.d4,

				m1.a3 * m2.a1 + m1.b3 * m2.a2 + m1.c3 * m2.a3 + m1.d3 * m2.a4,
				m1.a3 * m2.b1 + m1.b3 * m2.b2 + m1.c3 * m2.b3 + m1.d3 * m2.b4,
				m1.a3 * m2.c1 + m1.b3 * m2.c2 + m1.c3 * m2.c3 + m1.d3 * m2.c4,
				m1.a3 * m2.d1 + m1.b3 * m2.d2 + m1.c3 * m2.d3 + m1.d3 * m2.d4,

				m1.a4 * m2.a1 + m1.b4 * m2.a2 + m1.c4 * m2.a3 + m1.d4 * m2.a4,
				m1.a4 * m2.b1 + m1.b4 * m2.b2 + m1.c4 * m2.b3 + m1.d4 * m2.b4,
				m1.a4 * m2.c1 + m1.b4 * m2.c2 + m1.c4 * m2.c3 + m1.d4 * m2.c4,
				m1.a4 * m2.d1 + m1.b4 * m2.d2 + m1.c4 * m2.d3 + m1.d4 * m2.d4);
		}

		public static Matrix4 operator *(Matrix4 m1, float s)
		{
			return new Matrix4(
				m1.a1 * s, m1.b1 * s, m1.c1 * s, m1.d1 * s,
				m1.a1 * s, m1.b1 * s, m1.c1 * s, m1.d1 * s,
				m1.a1 * s, m1.b1 * s, m1.c1 * s, m1.d1 * s,
				m1.a1 * s, m1.b1 * s, m1.c1 * s, m1.d1 * s);
		}

		public static bool operator ==(Matrix4 m1, Matrix4 m2)
		{
			if (object.ReferenceEquals(m1, null))
			{
				return object.ReferenceEquals(m2, null);
			}

			return m1.Equals(m2);
		}

		public static bool operator !=(Matrix4 m1, Matrix4 m2)
		{
			if (object.ReferenceEquals(m1, null))
			{
				return !object.ReferenceEquals(m2, null);
			}

			return !m1.Equals(m2);
		}

		#endregion // Operator

		#region Utility
		public Matrix4 CopyFrom(Matrix4 m)
		{
			this.a1 = m.a1; this.b1 = m.b1; this.c1 = m.c1; this.d1 = m.d1;
			this.a2 = m.a2; this.b2 = m.b2; this.c2 = m.c2; this.d2 = m.d2;
			this.a3 = m.a3; this.b3 = m.b3; this.c3 = m.c3; this.d3 = m.d3;
			this.a4 = m.a4; this.b4 = m.b4; this.c4 = m.c4; this.d4 = m.d4;

			return this;
		}

		public override int GetHashCode()
		{
			return (int)Math.Round((this.a1 + this.b1 + this.c1 + this.d1)
				* (this.a2 + this.b2 + this.c2 + this.d2)
				* (this.a3 + this.b3 + this.c3 + this.d3)
				* (this.a4 + this.b4 + this.c4 + this.d4));
		}

		public override bool Equals(object obj)
		{
			if (obj == null || !(obj is Matrix4)) return false;

			Matrix4 m2 = (Matrix4)obj;

			return this.a1 == m2.a1 && this.b1 == m2.b1 && this.c1 == m2.c1 && this.d1 == m2.d1
				&& this.a2 == m2.a2 && this.b2 == m2.b2 && this.c2 == m2.c2 && this.d2 == m2.d2
				&& this.a3 == m2.a3 && this.b3 == m2.b3 && this.c3 == m2.c3 && this.d3 == m2.d3
				&& this.a4 == m2.a4 && this.b4 == m2.b4 && this.c4 == m2.c4 && this.d4 == m2.d4;
		}

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();

			sb.AppendLine(string.Format("[{0,5:0.00}, {1,5:0.00}, {2,5:0.00}, {3,5:0.00}", a1, b1, c1, d1));
			sb.AppendLine(string.Format(" {0,5:0.00}, {1,5:0.00}, {2,5:0.00}, {3,5:0.00}", a2, b2, c2, d2));
			sb.AppendLine(string.Format(" {0,5:0.00}, {1,5:0.00}, {2,5:0.00}, {3,5:0.00}", a3, b3, c3, d3));
			sb.AppendLine(string.Format(" {0,5:0.00}, {1,5:0.00}, {2,5:0.00}, {3,5:0.00}]", a4, b4, c4, d4));

			return sb.ToString();
		}

		#endregion // Utility

		#region Projection

		public Matrix4 Frustum(float left, float right, float top, float bottom, float near, float far)
		{
			float x = right - left, y = top - bottom, z = far - near;

			this.a1 = near * 2 / x; this.b1 = 0; this.c1 = 0; this.d1 = 0;
			this.a2 = 0; this.b2 = near * 2 / y; this.c2 = 0; this.d2 = 0;
			this.a3 = (left + right) / x; this.b3 = (top + bottom) / y; this.c3 = -(far + near) / z; this.d3 = -(far * near * 2) / z;
			this.a4 = 0; this.b4 = 0; this.c4 = -1; this.d4 = 0;

			return this;
		}

		public Matrix4 Perspective(float angle, float widthRate, float near, float far)
		{
			float topRate = (float)(near * Math.Tan(angle * Math.PI / 360.0f));
			widthRate = topRate * widthRate;
			this.Frustum(-widthRate, widthRate, -topRate, topRate, near, far);

			return this;
		}

		public Matrix4 Ortho(float left, float right, float bottom, float top, float near, float far)
		{
			float x = right - left, y = top - bottom, z = far - near;

			this.a1 = 2 / x; this.b1 = 0; this.c1 = 0; this.d1 = 0;
			this.a2 = 0; this.b2 = 2 / y; this.c2 = 0; this.d2 = 0;
			this.a3 = 0; this.b3 = 0; this.c3 = -2 / z; this.d3 = 0;

			this.a4 = -(left + right) / x;
			this.b4 = -(top + bottom) / y;
			this.c4 = -(far + near) / z;
			this.d4 = 1;

			return this;
		}
		#endregion // Projection

		public Matrix4 LookAt(Vector3 from, Vector3 to, Vector3 up)
		{
			Vector3 zaxis = (from - to).Normalize();                  // forward
			Vector3 xaxis = Vector3.Cross(up, zaxis).Normalize();     // right
			Vector3 yaxis = Vector3.Cross(zaxis, xaxis);              // up

			a1 = xaxis.x; b1 = yaxis.x; c1 = zaxis.x; d1 = 0;
			a2 = xaxis.y; b2 = yaxis.y; c2 = zaxis.y; d2 = 0;
			a3 = xaxis.z; b3 = yaxis.z; c3 = zaxis.z; d3 = 0;

			// this.a4 = -xaxis.dot(eye); this.b4 = -yaxis.dot(eye); this.c4 = -zaxis.dot(eye); this.d4 = 1;
			a4 = 0; b4 = 0; c4 = 0; d4 = 1;

			return this;
		}

		public float[] ToArray()
		{
			var arr = new float[16];

			arr[0] = a1; arr[1] = b1; arr[2] = c1; arr[3] = d1;
			arr[4] = a2; arr[5] = b2; arr[6] = c2; arr[7] = d2;
			arr[8] = a3; arr[9] = b3; arr[10] = c3; arr[11] = d3;
			arr[12] = a4; arr[13] = b4; arr[14] = c4; arr[15] = d4;

			return arr;
		}
	}

}
