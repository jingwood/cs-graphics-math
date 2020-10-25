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
	public struct Vector2
	{
		public float x, y;

		public static readonly Vector2 Zero = new Vector2(0, 0);

		public Vector2(float x, float y)
		{
			this.x = x;
			this.y = y;
		}

		public void Transform(Matrix3 m)
		{
			float x = this.x * m.a1 + this.y * m.a2 + m.a3;
			float y = this.x * m.b1 + this.y * m.b2 + m.b3;
			this.x = x;
			this.y = y;
		}

		public float SingleMagnitude { get => (float)(x + y); }
		public float SqrtMagnitude { get => (float)(x * x + y * y); }
		public float Length { get => (float)Math.Sqrt(x * x + y * y); }

		public static float LengthOf(Vector2 v)
		{
			return v.Length;
		}

		public Vector2 Vector3
		{
			get { return Normalize(this); }
		}

		public Vector2 Normalize()
		{
			float len = Length;
			if (len == 0) return Vector2.Zero;

			this.x /= len;
			this.y /= len;

			return this;
		}

		public Vector2 Transpose()
		{
			var temp = this.x;
			this.x = this.y;
			this.y = temp;
			return this;
		}

		public float Angle { get => (float)Math.Atan2(this.y, this.x); }

		public static float AngleOf(Vector2 v1, Vector2 v2)
		{
			return (float)Math.Acos(Dot(v1, v2) / v1.Length / v2.Length);
		}

		public static Vector2 operator +(Vector2 v1, Vector2 v2)
		{
			return new Vector2(v1.x + v2.x, v1.y + v2.y);
		}

		public static Vector2 operator -(Vector2 v1, Vector2 v2)
		{
			return new Vector2(v1.x - v2.x, v1.y - v2.y);
		}

		public static Vector2 operator *(Vector2 v, double s)
		{
			return new Vector2((float)(v.x * s), (float)(v.y * s));
		}

		public static Vector2 operator *(Vector2 v, float s)
		{
			return new Vector2(v.x * s, v.y * s);
		}

		public static Vector2 operator *(Vector2 v, Vector2 s)
		{
			return new Vector2(v.x * s.x, v.y * s.y);
		}

		public static Vector2 operator *(Vector2 v, Matrix3 m)
		{
			return new Vector2(v.x * m.a1 + v.y * m.a2 + m.a3,
				v.x * m.b1 + v.y * m.b2 + m.b3);
		}

		public static Vector2 operator *(Vector2 v, Matrix4 m)
		{
			return new Vector2(v.x * m.a1 + v.y * m.a2 + m.a4,
				v.x * m.b1 + v.y * m.b2 + m.b4);
		}

		// FIXME: need validation
		public static Vector2 operator *(Matrix3 mat, Vector2 v)
		{
			return new Vector2(v.x * mat.a1 + v.y * mat.b1 + mat.a3,
				v.x * mat.a2 + v.y * mat.b2 + mat.b3);
		}

		public static Vector2 operator /(Vector2 v, float s)
		{
			return new Vector2(v.x / s, v.y / s);
		}

		public static Vector2 operator -(Vector2 v)
		{
			return new Vector2(-v.x, -v.y);
		}

		public static bool operator <(Vector2 v1, Vector2 v2)
		{
			return v1.x < v2.x && v1.y < v2.y;
		}

		public static bool operator >(Vector2 v1, Vector2 v2)
		{
			return v1.x > v2.x && v1.y > v2.y;
		}

		public static bool operator <=(Vector2 v1, Vector2 v2)
		{
			return v1.x <= v2.x && v1.y <= v2.y;
		}

		public static bool operator >=(Vector2 v1, Vector2 v2)
		{
			return v1.x >= v2.x && v1.y >= v2.y;
		}

		public static Vector2 Normalize(Vector2 v)
		{
			float len = v.Length;
			return new Vector2(v.x / len, v.y / len);
		}

		public static float Dot(Vector2 v1, Vector2 v2)
		{
			return v1.x * v2.x + v1.y * v2.y;
		}

		public static Vector2 Abs(Vector2 v)
		{
			return new Vector2(Math.Abs(v.x), Math.Abs(v.y));
		}

		private static readonly Random rand = new Random();

		public static Vector2 Randomly(double max = 1.0)
		{
			return new Vector2((float)(rand.NextDouble() * max), (float)(rand.NextDouble() * max));
		}

		public static bool operator ==(Vector2 v1, Vector2 v2)
		{
			return v1.x == v2.x && v1.y == v2.y;
		}

		public static bool operator !=(Vector2 v1, Vector2 v2)
		{
			return v1.x != v2.x || v1.y != v2.y;
		}

		public override bool Equals(object obj)
		{
			if (!(obj is Vector2)) return false;

			var v2 = (Vector2)obj;
			return x == v2.x && y == v2.y;
		}

		public bool CloseTo(Vector2 v2, float distance = 1)
		{
			return (this - v2).Length <= distance;
		}

		public static bool CloseTo(Vector2 v1, Vector2 v2, float distance = 1)
		{
			return (v1 - v2).Length <= distance;
		}

		public override int GetHashCode()
		{
			return (int)(x * 1024768 + y * 1024);
		}

		public override string ToString()
		{
			return string.Format("[{0,5:0.00}, {1,5:0.00}]", x, y);
		}
	}

}
