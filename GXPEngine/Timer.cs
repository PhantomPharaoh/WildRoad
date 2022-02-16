using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

class Timer : GameObject
{
	long startTime = 0;
	public bool isFinished = false;
	public bool finishedThisFrame = false;
	float waitTime;
	public bool going = false;

	public Timer(float waitTime = 1, bool start = true)
	{

		if (start) Start();

		this.waitTime = waitTime * 1000;

	}

	public void Update()
	{
		if (going && Time.now - startTime >= waitTime)
		{
			if (!isFinished)
			{
				isFinished = true;
				finishedThisFrame = true;
			}
			else
			{
				finishedThisFrame = false;
				going = false;
			}
		}
	}

	public void SetWaitTime(float waitTime)
    {
		this.waitTime = waitTime * 1000;
    }

	public void Start()
	{
		going = true;
		isFinished = false;
		finishedThisFrame = false;
		startTime = Time.now;
	}

}
