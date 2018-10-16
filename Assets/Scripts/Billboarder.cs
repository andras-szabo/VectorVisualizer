using UnityEngine;

public class Billboarder : MonoBehaviour
{
	public Camera cam;

	private Transform _targetCamTransform;
	private Transform TargetCameraTransform
	{
		get
		{
			return _targetCamTransform ?? (_targetCamTransform = cam.transform);
		}
	}

	private Transform _transform;
	private Transform CachedTransform
	{
		get
		{
			return _transform ?? (_transform = transform);
		}
	}


	private void Update()
	{
		//TODO: only when changed;
		if (cam != null)
		{
			CachedTransform.rotation = TargetCameraTransform.transform.rotation;
		}
	}
}
