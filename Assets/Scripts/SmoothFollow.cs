﻿// SOURCE CODE FROM WARRIOR BUNDLE PACK 3 FREE
// LockY condition changed to follow player vertically
// Rotation follow target added

using UnityEngine;

public class SmoothFollow : MonoBehaviour
{
	#region Consts
	private const float SMOOTH_TIME = 0.3f;
	#endregion
	
	#region Public Properties
	public bool LockX;
	public float offSetZ;
	public bool LockY;
	public float offSetY;
	public bool LockZ;
	public bool useSmoothing;
	public Transform target;
	#endregion
	
	#region Private Properties
	private Transform thisTransform;
	private Vector3 velocity;
	#endregion

	bool hudActive = true;
	
	private void Awake()
	{
		thisTransform = transform;
		velocity = new Vector3(0.5f, 0.5f, 0.5f);
	}

	void Update()
	{

	}

	// ReSharper disable UnusedMember.Local
	private void LateUpdate()
		// ReSharper restore UnusedMember.Local
	{
		var newPos = Vector3.zero;
		
		if (useSmoothing)
		{
			newPos.x = Mathf.SmoothDamp(thisTransform.position.x, target.position.x, ref velocity.x, SMOOTH_TIME);
			newPos.y = Mathf.SmoothDamp(thisTransform.position.y, target.position.y, ref velocity.y, SMOOTH_TIME);
			newPos.z = Mathf.SmoothDamp(thisTransform.position.z, target.position.z + offSetZ, ref velocity.z, SMOOTH_TIME);
		}
		else
		{
			newPos.x = target.position.x;
			newPos.y = target.position.y;
			newPos.z = target.position.z;
		}
		
		#region Locks
		if (LockX)
		{
			newPos.x = thisTransform.position.x;
		}
		
		if (LockY)
		{
			newPos.y = target.position.y + offSetY;
		}
		
		if (LockZ)
		{
			newPos.z = thisTransform.position.z;
		}
		#endregion
		
		transform.position = Vector3.Slerp(transform.position, newPos, Time.time);
		// thisTransform.LookAt(target);
	}
}