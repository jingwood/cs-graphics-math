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

namespace unvell.GraphicsMath
{
	public struct BoundingBox3D
	{
		public Vector3 min, max;

		public BoundingBox3D(Vector3 min, Vector3 max)
		{
			this.min = min;
			this.max = max;
		}

		public static readonly BoundingBox3D Zero = new BoundingBox3D(Vector3.Zero, Vector3.Zero);

		public static BoundingBox3D FromOrigin(Vector3 origin, Vector3 size)
		{
			return new BoundingBox3D(origin - size / 2.0f, origin + size / 2.0f);
		}

		public static BoundingBox3D FromVertices(Vector3[] vs)
		{
			var bbox = new BoundingBox3D(vs[0], vs[0]);
			for (int i = 1; i < vs.Length; i++)
			{
				bbox.ExpandTo(vs[i]);
			}
			return bbox;
		}

		public Vector3 Size
		{
			get { return max - min; }
		}

		public Vector3 Origin
		{
			get { return min + (max - min) / 2.0f; }
		}

		public void ExpandTo(params Vector3[] vs)
		{
			foreach (var v in vs)
			{
				if (min.x > v.x) min.x = v.x;
				if (min.y > v.y) min.y = v.y;
				if (min.z > v.z) min.z = v.z;

				if (max.x < v.x) max.x = v.x;
				if (max.y < v.y) max.y = v.y;
				if (max.z < v.z) max.z = v.z;
			}
		}

		public void ExpandTo(BoundingBox3D box)
		{
			this.ExpandTo(box.min, box.max);
		}

		public bool Contains(Vector3 v)
		{
			return v.x >= min.x && v.x <= max.x
				&& v.y >= min.y && v.y <= max.y
				&& v.z >= min.z && v.z <= max.z;
		}

		public bool Contains(BoundingBox3D bbox)
		{
			return this.Contains(bbox.min) && this.Contains(bbox.max);
		}

		public bool Intersects(Ray ray)
		{
			return ray.IntersectsBoundingBox(this);
		}

		public bool Intersects(Ray ray, out float t)
		{
			return ray.IntersectsBoundingBox(this, out t);
		}

		public override string ToString()
		{
			return string.Format("{0} - {1}", this.min, this.max);
		}
	}
}
