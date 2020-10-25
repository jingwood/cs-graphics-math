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
	public struct Vector3
	{
		public float x, y, z;

		public Vector3(float v)
			: this(v, v, v)
		{
		}

		public Vector3(float x, float y, float z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}

		public static readonly Vector3 Zero = new Vector3(0, 0, 0);
		public static readonly Vector3 One = new Vector3(1, 1, 1);

		public void Add(float x, float y, float z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}

		public void Set(float x, float y, float z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}

		public static Vector3 operator +(Vector3 v1, Vector3 v2)
		{
			return new Vector3(v1.x + v2.x, v1.y + v2.y, v1.z + v2.z);
		}

		public static Vector3 operator +(Vector3 v1, float v)
		{
			return new Vector3(v1.x + v, v1.y + v, v1.z + v);
		}

		public static Vector3 operator +(float v, Vector3 v1)
		{
			return new Vector3(v1.x + v, v1.y + v, v1.z + v);
		}

		public static Vector3 operator -(Vector3 v1, Vector3 v2)
		{
			return new Vector3(v1.x - v2.x, v1.y - v2.y, v1.z - v2.z);
		}

		public static Vector3 operator -(Vector3 v)
		{
			return new Vector3(-v.x, -v.y, -v.z);
		}

		public static Vector3 operator *(Vector3 v, float d)
		{
			return new Vector3(v.x * d, v.y * d, v.z * d);
		}

		public static Vector3 operator *(float d, Vector3 v)
		{
			return new Vector3(d * v.x, d * v.y, d * v.z);
		}

		public static Vector3 operator *(Vector3 v1, Vector3 v2)
		{
			return Cross(v1, v2);
		}

		public static Vector3 Cross(Vector3 v1, Vector3 v2)
		{
			return new Vector3(v1.y * v2.z - v1.z * v2.y,
				-(v1.x * v2.z - v1.z * v2.x), v1.x * v2.y - v1.y * v2.x);
		}

		public static Vector3 operator *(Vector3 v, Matrix4 m)
		{
			return new Vector3(
				v.x * m.a1 + v.y * m.a2 + v.z * m.a3 + m.a4,
				v.x * m.b1 + v.y * m.b2 + v.z * m.b3 + m.b4,
				v.x * m.c1 + v.y * m.c2 + v.z * m.c3 + m.c4);
		}

		public static Vector3 operator *(Matrix4 m, Vector3 v)
		{
			return new Vector3(
				m.a1 * v.x + m.b1 * v.y + m.c1 * v.z + m.d1,
				m.a2 * v.x + m.b2 * v.y + m.c2 * v.z + m.d2,
				m.a3 * v.x + m.b3 * v.y + m.c3 * v.z + m.d3);
		}

		public static Vector3 operator /(Vector3 v, float d)
		{
			float id = 1.0f / d;
			return new Vector3(v.x * id, v.y * id, v.z * id);
		}

		public static Vector3 operator /(float d, Vector3 v)
		{
			return new Vector3(d / v.x, d / v.y, d / v.z);
		}

		public static Vector3 operator /(Vector3 v1, Vector3 v2)
		{
			return new Vector3(v1.x / v2.x, v1.y / v2.y, v1.z / v2.z);
		}

		public static float Dot(Vector3 v1, Vector3 v2)
		{
			return v1.x * v2.x + v1.y * v2.y + v1.z * v2.z;
		}

		public static float Length(Vector3 v)
		{
			return (float)Math.Sqrt(v.x * v.x + v.y * v.y + v.z * v.z);
		}

		public float Length()
		{
			return (float)Math.Sqrt(this.x * this.x + this.y * this.y + this.z * this.z);
		}

		public static Vector3 Normalize(Vector3 v)
		{
			float ilen = 1.0f / Length(v);
			return new Vector3(v.x * ilen, v.y * ilen, v.z * ilen);
		}

		public Vector3 Normalize()
		{
			float ilen = 1.0f / this.Length();
			return new Vector3(this.x * ilen, this.y * ilen, this.z * ilen);
		}

		public Vector2 XY { get { return new Vector2(this.x, this.y); } }
		public Vector2 XZ { get { return new Vector2(this.x, this.z); } }
		public Vector2 YZ { get { return new Vector2(this.y, this.z); } }

		public static float AngleOf(Vector3 v1, Vector3 v2)
		{
			return (float)Math.Acos(Dot(v1, v2) / Length(v1) / Length(v2));
		}

		internal static float Distance(Vector3 v1, Vector3 v2)
		{
			float dx = v2.x - v1.x;
			float dy = v2.y - v1.y;
			float dz = v2.z - v1.z;
			return (float)Math.Sqrt(dx * dx + dy * dy + dz * dz);
		}

		public Vector3 Ceiling()
		{
			return Vector3.Ceiling(this);
		}

		public static Vector3 Ceiling(Vector3 v)
		{
			return new Vector3((float)Math.Ceiling(v.x), (float)Math.Ceiling(v.y), (float)Math.Ceiling(v.z));
		}

		public static Vector3 Abs(Vector3 v)
		{
			return new Vector3(Math.Abs(v.x), Math.Abs(v.y), Math.Abs(v.z));
		}

		public static Vector3 Average(Vector3[] args)
		{
			if (args.Length < 1) return Vector3.Zero;

			Vector3 a = args[0];

			for (int i = 1; i < args.Length; i++)
			{
				a += args[i];
			}

			return a / args.Length;
		}

		private static readonly Random rand = new Random();

		public static Vector3 Randomly()
		{
			return new Vector3((float)rand.NextDouble(), (float)rand.NextDouble(), (float)rand.NextDouble());
		}

		public static explicit operator Vector2(Vector3 v)
		{
			return new Vector2(v.x, v.y);
		}

		public override int GetHashCode()
		{
			return ((int)(this.x * 512)) ^ ((int)(this.y * 512)) ^ ((int)(this.z * 512));
		}

		public override bool Equals(object obj)
		{
			if (obj == null || !(obj is Vector3)) return false;
			var v2 = (Vector3)obj;
			return this.x == v2.x && this.y == v2.y && this.z == v2.z;
		}

		public static bool operator ==(Vector3 v1, Vector3 v2)
		{
			return v1.Equals(v2);
		}

		public static bool operator !=(Vector3 v1, Vector3 v2)
		{
			return !v1.Equals(v2);
		}

		public override string ToString()
		{
			return string.Format("[{0,5:0.00}, {1,5:#0.00}, {2,5:0.00}]", this.x, this.y, this.z);
		}

		public static readonly Vector3 Up = new Vector3(0, 1, 0);
	}

}
