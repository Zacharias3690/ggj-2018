﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedComponent : MonoBehaviour, ILockable
{
	#region  ILockable

	public bool Unlock()
	{
		return locked = false;
	}

	public bool IsLocked
	{
		get
		{
			return locked;
		}
	}

	#endregion

	private bool locked = true;

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}
}