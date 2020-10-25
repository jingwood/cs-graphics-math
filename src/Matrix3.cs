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

namespace unvell.GraphicsMath
{
	public class Matrix3
	{
		public float a1, b1, c1;
		public float a2, b2, c2;
		public float a3, b3, c3;

		public static readonly Matrix3 Identify = new Matrix3()
		{
			a1 = 1,
			b1 = 0,
			c1 = 0,
			a2 = 0,
			b2 = 1,
			c2 = 0,
			a3 = 0,
			b3 = 0,
			c3 = 1,
		};

		public Matrix3()
		{
		}

		public Matrix3(float a1, float b1, float c1,
			float a2, float b2, float c2,
			float a3, float b3, float c3)
		{
			this.a1 = a1; this.b1 = b1; this.c1 = c1;
			this.a2 = a2; this.b2 = b2; this.c2 = c2;
			this.a3 = a3; this.b3 = b3; this.c3 = c3;
		}

		public Matrix3(Matrix3 mat2)
		{
			this.CopyFrom(mat2);
		}

		public Matrix3 LoadIdentity()
		{
			a1 = 1; b1 = 0; c1 = 0;
			a2 = 0; b2 = 1; c2 = 0;
			a3 = 0; b3 = 0; c3 = 1;

			return this;
		}

		public Matrix3 CopyFrom(Matrix3 m2)
		{
			this.a1 = m2.a1; this.b1 = m2.b1; this.c1 = m2.c1;
			this.a2 = m2.a2; this.b2 = m2.b2; this.c2 = m2.c2;
			this.a3 = m2.a3; this.b3 = m2.b3; this.c3 = m2.c3;

			return this;
		}

		public Matrix3 Clone()
		{
			return new Matrix3(this);
		}

		public Matrix3 Rotate(float angle)
		{
			float radians = (float)(angle / 180f * Math.PI);
			float sin = (float)Math.Sin(radians);
			float cos = (float)Math.Cos(radians);

			float m2a1 = cos, m2b1 = sin;
			float m2a2 = -sin, m2b2 = cos;

			// post
			float a1 = this.a1 * m2a1 + this.a2 * m2b1;
			float b1 = this.b1 * m2a1 + this.b2 * m2b1;

			float a2 = this.a1 * m2a2 + this.a2 * m2b2;
			float b2 = this.b1 * m2a2 + this.b2 * m2b2;

			this.a1 = a1; this.b1 = b1;
			this.a2 = a2; this.b2 = b2;

			return this;
		}

		public Matrix3 Scale(float x, float y)
		{
			this.a1 *= x; this.b1 *= x;
			this.a2 *= y; this.b2 *= y;

			return this;
		}

		public Matrix3 Translate(Vector2 v)
		{
			return Translate(v.x, v.y);
		}

		public Matrix3 Translate(float x, float y)
		{
			this.a3 += this.a1 * x + this.a2 * y;
			this.b3 += this.b1 * x + this.b2 * y;

			return this;
		}

		public static Matrix3 operator *(Matrix3 m1, Matrix3 m2)
		{
			return new Matrix3(
				m1.a1 * m2.a1 + m1.b1 * m2.a2 + m1.c1 * m2.a3,
				m1.a1 * m2.b1 + m1.b1 * m2.b2 + m1.c1 * m2.b3,
				m1.a1 * m2.c1 + m1.b1 * m2.c2 + m1.c1 * m2.c3,

				m1.a2 * m2.a1 + m1.b2 * m2.a2 + m1.c2 * m2.a3,
				m1.a2 * m2.b1 + m1.b2 * m2.b2 + m1.c2 * m2.b3,
				m1.a2 * m2.c1 + m1.b2 * m2.c2 + m1.c2 * m2.c3,

				m1.a3 * m2.a1 + m1.b3 * m2.a2 + m1.c3 * m2.a3,
				m1.a3 * m2.b1 + m1.b3 * m2.b2 + m1.c3 * m2.b3,
				m1.a3 * m2.c1 + m1.b3 * m2.c2 + m1.c3 * m2.c3);
		}

		public static Matrix3 CreateRotation(float angle)
		{
			return new Matrix3().Rotate(angle);
		}

		public static Matrix3 CreateTranslation(float x, float y)
		{
			return new Matrix3().Translate(x, y);
		}

		public static Matrix3 CreateScale(float x, float y)
		{
			return new Matrix3().Scale(x, y);
		}

		public void Inverse()
		{
			float a = this.a1, b = this.b1, c = this.c1,
				d = this.a2, e = this.b2, f = this.c2,
				g = this.a3, h = this.b3, i = this.c3;

			float det = a * e * i - a * f * h + b * f * g - b * d * i + c * d * h - c * e * g;
			if (det == 0) return;

			det = 1.0f / det;

			float m2a = det * e * i - f * h, m2b = det * c * h - b * i, m2c = det * b * f - c * e,
				m2d = det * f * g - d * i, m2e = det * a * i - c * g, m2f = det * c * d - a * f,
				m2g = det * d * h - e * g, m2h = det * b * g - a * h, m2i = det * a * e - b * d;

			this.a1 = m2a; this.b1 = m2b; this.c1 = m2c;
			this.a2 = m2d; this.b2 = m2e; this.c2 = m2f;
			this.a3 = m2g; this.b3 = m2h; this.c3 = m2i;
		}

		public float[] ToArray()
		{
			var arr = new float[9];

			arr[0] = a1; arr[1] = b1; arr[2] = c1;
			arr[3] = a2; arr[4] = b2; arr[5] = c2;
			arr[6] = a3; arr[7] = b3; arr[8] = c3;

			return arr;
		}
	}
}
