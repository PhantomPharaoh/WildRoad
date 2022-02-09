using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;


static public class MathUtils
{

	/// <summary>
	/// maps a variable to a new range of values
	/// </summary>
	/// <param name="value"></param>
	/// <param name="old_min"></param>
	/// <param name="old_max"></param>
	/// <param name="new_min"></param>
	/// <param name="new_max"></param>
	/// <returns></returns>
	public static float Map(float value, float old_min, float old_max, float new_min, float new_max, bool noClamp = false)
	{
		if (!noClamp)
			value = Mathf.Clamp(value, old_min, old_max);
		return new_min + (value - old_min) * (new_max - new_min) / (old_max - old_min);
	}

	public static float Lerp(float a, float b, float f)
	{
		return a + f * (b - a);
	}

}
