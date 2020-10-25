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

using System.Collections.Generic;
using System.Linq;

namespace unvell.GraphicsMath
{
	public struct BoundingBox2D
	{
		public Vector2 min;
		public Vector2 max;

		public BoundingBox2D(float minx, float miny, float maxx, float maxy)
			: this(new Vector2(minx, miny), new Vector2(maxx, maxy))
		{
		}

		public BoundingBox2D(Vector2 min, Vector2 max)
		{
			this.min = min;
			this.max = max;
		}

		public float Width
		{
			get { return max.x - min.x; }
			set { max.x = min.x + value; }
		}

		public float Height
		{
			get { return max.y - min.y; }
			set { max.y = min.y + value; }
		}

		public Vector2 Size
		{
			get
			{
				return max - min;
			}
		}

		public Vector2 Origin
		{
			get
			{
				return this.min + this.Size * 0.5f;
			}
			set
			{
				var halfSize = this.Size * 0.5f;
				this.min = value - halfSize;
				this.max = value + halfSize;
			}
		}

		public BoundingBox2D ExpandTo(Vector2 v)
		{
			if (this.min.x > v.x) this.min.x = v.x;
			if (this.min.y > v.y) this.min.y = v.y;
			if (this.max.x < v.x) this.max.x = v.x;
			if (this.max.y < v.y) this.max.y = v.y;

			return this;
		}

		public BoundingBox2D ExpandTo(BoundingBox2D box)
		{
			this.ExpandTo(box.min);
			this.ExpandTo(box.max);

			return this;
		}

		public BoundingBox2D Inflate(Vector2 v)
		{
			return Inflate(v.x, v.y);
		}

		public BoundingBox2D Inflate(float x, float y)
		{
			Vector2 v = new Vector2(x, y);

			this.min -= (v * 0.5);
			this.max += (v * 0.5);

			return this;
		}

		public bool Intersects(BoundingBox2D b)
		{
			return Intersects(this, b);
		}

		public static bool Intersects(BoundingBox2D a, BoundingBox2D b)
		{
			return Intersects(a.min, a.max, b.min, b.max);
		}

		public static bool Intersects(Vector2 amin, Vector2 amax, Vector2 bmin, Vector2 bmax)
		{
			if (amax.x < bmin.x) return false;
			if (amin.x > bmax.x) return false;
			if (amax.y < bmin.y) return false;
			if (amin.y > bmax.y) return false;

			return true;
		}

		public bool Contains(Vector2 p)
		{
			return (p.x > min.x && p.x < max.x && p.y > min.y && p.y < max.y);
		}

		public static BoundingBox2D FromVectices(Vector2 v1, Vector2 v2)
		{
			var box = new BoundingBox2D();
			box.min = box.max = v1;
			box.ExpandTo(v2);
			return box;
		}

		public static BoundingBox2D FromVectices(IEnumerable<Vector2> vs)
		{
			var box = new BoundingBox2D();
			if (vs.Count() < 1) return box;

			box.min = vs.First();
			box.max = box.min;

			if (vs.Count() < 2) return box;

			foreach (var v in vs.Skip(1))
			{
				box.ExpandTo(v);
			}
			return box;
		}

		public static BoundingBox2D FromTriangle(Vector2 v1, Vector2 v2, Vector2 v3)
		{
			var box = new BoundingBox2D();
			box.min = v1; box.max = v1;

			box.ExpandTo(v2);
			box.ExpandTo(v3);

			return box;
		}

		public static BoundingBox2D operator +(BoundingBox2D box, Vector2 v)
		{
			box.min += v;
			box.max += v;
			return box;
		}

		public static BoundingBox2D operator -(BoundingBox2D box, Vector2 v)
		{
			box.min -= v;
			box.max -= v;
			return box;
		}

		public static BoundingBox2D operator *(BoundingBox2D box, float s)
		{
			box.min *= s;
			box.max *= s;
			return box;
		}
	}

}
