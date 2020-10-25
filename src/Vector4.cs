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
	public struct Vector4
	{
		public float x, y, z, w;

		public Vector4(float v)
			: this(v, v, v, 1)
		{
		}

		public Vector4(float x, float y, float z)
			: this(x, y, z, 1)
		{
		}

		public Vector4(Vector3 v)
			: this(v, 1)
		{
		}

		public Vector4(Vector3 v, float w)
			: this(v.x, v.y, v.z, w)
		{
		}

		public Vector4(Vector4 v)
			: this(v.x, v.y, v.z, v.w)
		{
		}

		public Vector4(float x, float y, float z, float w)
		{
			this.x = x; this.y = y; this.z = z; this.w = w;
		}

		public Vector3 XYZ { get { return new Vector3(this.x, this.y, this.z); } }

		public Vector2 XY { get { return new Vector2(this.x, this.y); } }

		public Vector4 Normalize()
		{
			return Vector4.Normalize(this);
		}

		public static Vector4 operator +(Vector4 v1, Vector4 v2)
		{
			return new Vector4(v1.x + v2.x, v1.y + v2.y, v1.z + v2.z, v1.w + v2.w);
		}

		public static Vector4 operator +(Vector4 v1, Vector3 n)
		{
			return new Vector4(v1.x + n.x, v1.y + n.y, v1.z + n.z);
		}

		public static Vector4 operator +(Vector4 v1, float p)
		{
			return new Vector4(v1.x + p, v1.y + p, v1.z + p, v1.w + p);
		}

		public static Vector4 operator -(Vector4 v)
		{
			return new Vector4(-v.x, -v.y, -v.z, -v.w);
		}

		public static Vector4 operator -(Vector4 v1, Vector4 v2)
		{
			return new Vector4(v1.x - v2.x, v1.y - v2.y, v1.z - v2.z, v1.w - v2.w);
		}

		public static Vector4 operator *(Vector4 v1, Vector4 v2)
		{
			return new Vector4(v1.y * v2.z - v1.z * v2.y,
				-(v1.x * v2.z - v1.z * v2.x), v1.x * v2.y - v1.y * v2.x);
		}

		public static Vector4 operator *(Vector4 v, Matrix4 m)
		{
			return new Vector4(
						v.x * m.a1 + v.y * m.a2 + v.z * m.a3 + v.w * m.a4,
						v.x * m.b1 + v.y * m.b2 + v.z * m.b3 + v.w * m.b4,
						v.x * m.c1 + v.y * m.c2 + v.z * m.c3 + v.w * m.c4,
						v.x * m.d1 + v.y * m.d2 + v.z * m.d3 + v.w * m.d4);
		}

		public static Vector4 operator *(Matrix4 m, Vector4 v)
		{
			return new Vector4(
						v.x * m.a1 + v.y * m.b1 + v.z * m.c1 + v.w * m.d1,
						v.x * m.a2 + v.y * m.b2 + v.z * m.c2 + v.w * m.d2,
						v.x * m.a3 + v.y * m.b3 + v.z * m.c3 + v.w * m.d3,
						v.x * m.a4 + v.y * m.b4 + v.z * m.c4 + v.w * m.d4);
		}

		public static Vector4 operator *(Vector4 v, float s)
		{
			return new Vector4(v.x * s, v.y * s, v.z * s, v.w * s);
		}

		public static Vector4 operator *(float s, Vector4 v)
		{
			return new Vector4(v.x * s, v.y * s, v.z * s, v.w * s);
		}

		public static Vector4 operator /(Vector4 v, float s)
		{
			return new Vector4(v.x / s, v.y / s, v.z / s, v.w / s);
		}

		public static Vector4 operator /(Vector4 v1, Vector4 v2)
		{
			return new Vector4(v1.x / v2.x, v1.y / v2.y, v1.z / v2.z, v1.w / v2.w);
		}

		public static float Dot(Vector4 v1, Vector4 v2)
		{
			return v1.x * v2.x + v1.y * v2.y + v1.z * v2.z + v1.w * v2.w;
		}

		public float Length()
		{
			return Length(this);
		}

		public static float Length(Vector4 v)
		{
			return (float)Math.Sqrt(v.x * v.x + v.y * v.y + v.z * v.z + v.w * v.w);
		}

		public static Vector4 Normalize(Vector4 v)
		{
			float len = Length(v);
			if (len == 0) return v;

			len = 1.0f / len;
			return new Vector4(v.x * len, v.y * len, v.z * len, v.w * len);
		}

		public static float Distance(Vector4 v1, Vector4 v2)
		{
			return (v2 - v1).Length();
		}

		public static float AngleOf(Vector4 v1, Vector4 v2)
		{
			return (float)Math.Acos(Dot(v1, v2) / Length(v1) / Length(v2));
		}

		public static explicit operator Vector3(Vector4 v)
		{
			return new Vector3(v.x, v.y, v.z);
		}

		#region Utility

		public override int GetHashCode()
		{
			return (int)(this.x) ^ (int)(this.y) ^ (int)(this.z) ^ (int)(this.w);
		}

		public static bool Similar(Vector4 v1, Vector4 v2, float epsilon = Const.EPSILON)
		{
			return Math.Abs((v1.x - v2.x)) <= epsilon
				&& Math.Abs((v1.y - v2.y)) <= epsilon
				&& Math.Abs((v1.z - v2.z)) <= epsilon
				&& Math.Abs((v1.w - v2.w)) <= epsilon;
		}

		public static bool operator ==(Vector4 v1, Vector4 v2)
		{
			return v1.x == v2.x && v1.y == v2.y && v1.z == v2.z && v1.w == v2.w;
		}

		public static bool operator !=(Vector4 v1, Vector4 v2)
		{
			return v1.x != v2.x || v1.y != v2.y || v1.z != v2.z || v1.w != v2.w;
		}

		public override bool Equals(object obj)
		{
			return obj is Vector4 v && this == v;
		}

		public override string ToString()
		{
			return string.Format("[{0,5:0.00}, {1,5:0.00}, {2,5:0.00}, {3,5:0.00}]", this.x, this.y, this.z, this.w);
		}

		#endregion // Utility
	}

}
