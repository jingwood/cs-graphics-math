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
	public class Ray
	{
		public Vector3 origin;
		public Vector3 dir;
		public Vector3 normalizedDir;

		public Ray()
		{
		}

		public Ray(Vector3 origin, Vector3 dir)
		{
			this.origin = origin;
			this.dir = dir;
			this.normalizedDir = Vector3.Normalize(dir);
		}

		public override string ToString()
		{
			return this.origin + " -> " + this.dir;
		}

		public bool IntersectsPlane(Vector4 plane, out float t, out Vector3 hit)
		{
			//float originScale = -Vector4.Dot(plane, new Vector4(this.origin, 1));
			float dirScale = Vector4.Dot(plane, new Vector4(this.dir, 0));

			if (dirScale == 0)
			{
				t = 0;
			}
			else
			{
				t = -Vector4.Dot(plane, new Vector4(this.origin, 1)) / dirScale;
			}

			hit = this.origin + t * this.dir;

			return t > 0;
		}

		public bool IntersectsTriangle(ITriangle3D triangle, float mint = 0, float maxt = 99999999)
		{
			float t;
			Vector3 hit;
			return IntersectsTriangle(triangle, out t, out hit);
		}

		public bool IntersectsTriangle(ITriangle3D triangle, out float t, out Vector3 hit, float mint = 0, float maxt = 99999999)
		{
			Vector3 p = this.origin, pv = this.normalizedDir;
			Vector3 v1 = triangle.V1, v2 = triangle.V2, v3 = triangle.V3;

			var pd = (v2 - v1) * (v3 - v2);

			var len = Vector3.Length(pd);

			var l = (1.0f / len) * new Vector4(pd.x, pd.y, pd.z, Vector3.Dot(-pd, v1));

			t = -Vector4.Dot(l, new Vector4(p, 1)) / Vector4.Dot(l, new Vector4(pv, 0));

			if (t < mint || t > maxt)
			{
				hit = new Vector3();
				return false;
			}

			hit = (p + t * pv);

			Vector3 c;

			c = Vector3.Cross(v2 - v1, hit - v1);
			if (Vector3.Dot(pd, c) < 0) return false;

			c = Vector3.Cross(v3 - v2, hit - v2);
			if (Vector3.Dot(pd, c) < 0) return false;

			c = Vector3.Cross(v1 - v3, hit - v3);
			if (Vector3.Dot(pd, c) < 0) return false;

			return true;
			//return PlaneHitTestForm.PointInTriangle(hit, Vector3.Normalize(pd), v1, v2, v3);
		}

		public bool IntersectsBoundingBox(BoundingBox3D bbox)
		{
			float t;
			return IntersectsBoundingBox(bbox, out t);
		}

		// https://gamedev.stackexchange.com/questions/18436/most-efficient-aabb-vs-ray-collision-algorithms
		public bool IntersectsBoundingBox(BoundingBox3D bbox, out float t)
		{
			// r.dir is unit direction vector of ray
			Vector3 dirfrac = 1.0f / this.dir;

			// lb is the corner of AABB with minimal coordinates - left bottom, rt is maximal corner
			// r.org is origin of ray
			float t1 = (bbox.min.x - this.origin.x) * dirfrac.x;
			float t2 = (bbox.max.x - this.origin.x) * dirfrac.x;
			float t3 = (bbox.min.y - this.origin.y) * dirfrac.y;
			float t4 = (bbox.max.y - this.origin.y) * dirfrac.y;
			float t5 = (bbox.min.z - this.origin.z) * dirfrac.z;
			float t6 = (bbox.max.z - this.origin.z) * dirfrac.z;

			float tmax = Math.Min(Math.Min(Math.Max(t1, t2), Math.Max(t3, t4)), Math.Max(t5, t6));

			// if tmax < 0, ray (line) is intersecting AABB, but whole AABB is behing us
			if (tmax < 0)
			{
				t = tmax;
				return false;
			}

			float tmin = Math.Max(Math.Max(Math.Min(t1, t2), Math.Min(t3, t4)), Math.Min(t5, t6));

			// if tmin > tmax, ray doesn't intersect AABB
			if (tmin > tmax)
			{
				t = tmax;
				return false;
			}

			t = tmin;
			return true;
		}
	}
}
