using System;

namespace GXPEngine.Core
{
	public struct Vector2
	{

		public float x;
		public float y;

		public Vector2(float x, float y)
		{
			this.x = x;
			this.y = y;
		}

		override public string ToString()
		{
			return "[Vector2 " + x + ", " + y + "]";
		}

		//my additions
		public float Length()
		{
			return (float)Math.Sqrt(x * x + y * y);
		}

		public Vector2 Normalized()
		{
			if (this.Length() == 0) return Vector2.ZERO;

			float length = Length();
			return new Vector2(x / length, y / length);

		}
		public Vector2 Rotated(float angle, bool asDegrees = false)
		{
			if (asDegrees) angle = angle * (float)Math.PI / 180f;

			float ca = (float)Math.Cos(angle);
			float sa = (float)Math.Sin(angle);

			return new Vector2(ca * this.x - sa * this.y, sa * this.x + ca * this.y);
		}

		public Vector2 Reflected(Vector2 wallNormal)
		{
			return this - 2 * wallNormal * (wallNormal.Dot(this));
		}

		public float DistanceTo(Vector2 other)
		{
			return (float)Math.Sqrt(Math.Pow(other.x - x, 2) + Math.Pow(other.y - y, 2));
		}

		public void CapLength(float maxLength)
		{
			if (this.Length() > maxLength)
			{
				Vector2 caped = this.Normalized() * maxLength;
				x = caped.x;
				y = caped.y;
			}
		}

		public float Dot(Vector2 other)
		{
			return this.x * other.x + this.y * other.y;
		}

		public float AngleTo(Vector2 other, bool asDegrees = false)
		{
			float radian = (float)Math.Atan2(other.y, other.x) - (float)Math.Atan2(this.y, this.x);
			if (!asDegrees) return radian;
			float degrees = radian * 180f / (float)Math.PI;
			return degrees;
		}

		//operator overloads
		public static Vector2 operator *(Vector2 a, float b)
		{
			return new Vector2(a.x * b, a.y * b);
		}
		public static Vector2 operator *(float a, Vector2 b)
		{
			return new Vector2(b.x * a, b.y * a);
		}
		public static Vector2 operator *(Vector2 a, Vector2 b)
		{
			return new Vector2(a.x * b.x, a.y * b.y);
		}
		public static Vector2 operator /(Vector2 a, float b)
		{
			return new Vector2(a.x / b, a.y / b);
		}
		public static Vector2 operator /(Vector2 a, Vector2 b)
		{
			return new Vector2(a.x / b.x, a.y / b.y);
		}
		public static Vector2 operator +(Vector2 a, Vector2 b)
		{
			return new Vector2(a.x + b.x, a.y + b.y);
		}
		public static Vector2 operator -(Vector2 a, Vector2 b)
		{
			return new Vector2(a.x - b.x, a.y - b.y);
		}
		public static Vector2 operator -(Vector2 a)
		{
			return new Vector2(a.x * -1, a.y * -1);
		}
		public static bool operator ==(Vector2 a, Vector2 b)
		{
			return a.x == b.x && a.y == b.y;
		}
		public static bool operator !=(Vector2 a, Vector2 b)
		{
			return a.x != b.x || a.y != b.y;
		}

		//static vectors
		public static Vector2 ZERO = new Vector2(0, 0);
		public static Vector2 UP = new Vector2(0, 1);
		public static Vector2 DOWN = new Vector2(0, -1);
		public static Vector2 RIGHT = new Vector2(1, 0);
		public static Vector2 LEFT = new Vector2(-1, 0);



	}
}

