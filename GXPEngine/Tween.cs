using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

class Tween : GameObject
{

	public enum Property { x, y, rotation, scale, alpha };
	public enum Curves { Linear, EaseIn, EaseInBack, EaseOut, EaseOutBack, EaseInOut, EaseInOutBack, SinDamp, SinBounce }

	Func<float, float> linear = x => x;
	Func<float, float> easeIn = x => (float)Math.Pow(x, 3);
	Func<float, float> easeInBack = x => 3 * (float)Math.Pow(x, 3) - 2 * (float)Math.Pow(x, 2);
	Func<float, float> easeOut = x => (float)Math.Pow(x, 3) - 3 * (float)Math.Pow(x, 2) + 3 * x;
	Func<float, float> easeOutBack = x => -1.5f * (float)Math.Pow(x, 3) + (float)Math.Pow(x, 2) + 1.5f * x;
	Func<float, float> easeInOut = x => -2 * (float)Math.Pow(x, 3) + 3 * (float)Math.Pow(x, 2);
	Func<float, float> easeInOutBack = x => -4 * (float)Math.Pow(x, 3) + 6 * (float)Math.Pow(x, 2) - x;
	Func<float, float> sinDamp = x => (float)Math.Sin(Math.PI * 4 * x) * (1 - x);
	//custom
	Func<float, float> sinBounce = x => (float)Math.Sin(x * Math.PI);

	private float targetValue;
	private float initialValue;
	private Property propertyToTween;
	private float time;
	private Curves curve;
	
	private float creationTime;
	Func<float, float> currentCurve;

	public Tween(Property propertyToTween, float initialValue, float targetValue, float time, Curves curve)
	{
		this.targetValue = targetValue;
		this.initialValue = initialValue;
		this.time = time * 1000;
		this.curve = curve;
		this.propertyToTween = propertyToTween;

		creationTime = Time.now;

		switch (curve)
		{
			case Curves.Linear:
				currentCurve = linear;
				break;
			case Curves.EaseIn:
				currentCurve = easeIn;
				break;
			case Curves.EaseInBack:
				currentCurve = easeInBack;
				break;
			case Curves.EaseOut:
				currentCurve = easeOut;
				break;
			case Curves.EaseOutBack:
				currentCurve = easeOutBack;
				break;
			case Curves.EaseInOut:
				currentCurve = easeInOut;
				break;
			case Curves.EaseInOutBack:
				currentCurve = easeInOutBack;
				break;
			case Curves.SinDamp:
				currentCurve = sinDamp;
				break;
			case Curves.SinBounce:
				currentCurve = sinBounce;
				break;
		}

	}

	public void Update()
	{

		if (Time.now - creationTime > time)
		{
			switch (propertyToTween)
			{
				case Property.x:
					parent.x = targetValue;
					break;
				case Property.y:
					parent.y = targetValue;
					break;
				case Property.rotation:
					parent.rotation = targetValue;
					break;
				case Property.scale:
					parent.scaleX = targetValue;
					parent.scaleY = targetValue;
					break;
				case Property.alpha:
					(parent as Sprite).alpha = targetValue;
					break;
			}
			LateDestroy();
		}
		else
		{
			float timeDif = MathUtils.Map(Time.now - creationTime, 0, time, 0, 1);
			switch (propertyToTween)
			{
				case Property.x:
					parent.x = MathUtils.Map(currentCurve(timeDif), 0, 1, initialValue, targetValue, true);
					break;
				case Property.y:
					parent.y = MathUtils.Map(currentCurve(timeDif), 0, 1, initialValue, targetValue, true);
					break;
				case Property.rotation:
					parent.rotation = MathUtils.Map(currentCurve(timeDif), 0, 1, initialValue, targetValue, true);
					break;
				case Property.scale:
					parent.scaleX = MathUtils.Map(currentCurve(timeDif), 0, 1, initialValue, targetValue, true);
					parent.scaleY = parent.scaleX;
					break;
				case Property.alpha:
					(parent as Sprite).alpha = MathUtils.Map(currentCurve(timeDif), 0, 1, initialValue, targetValue, true);
					break;
			}
		}
	}
}


