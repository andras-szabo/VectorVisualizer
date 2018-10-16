using UnityEngine;

public class VecOrienter : MonoBehaviour
{
	public VectorWrapper vecTemplate;
	public Camera cam;

	public VectorWrapper CreateVector(Vector3 target)
	{
		var vw = UnityEngine.Object.Instantiate<VectorWrapper>(vecTemplate, Vector3.zero, Quaternion.identity);
		DoOrient(vw, target);
		return vw;
	}

	public void DoOrient(VectorWrapper vw, Vector3 target)
	{
		ResetVector(vw);
		ScaleVector(vw, target);
		RotateAndPositionVector(vw, target);
		SetLabel(vw, target);
		SetCamera(vw, cam);
	}

	private void SetCamera(VectorWrapper vw, Camera camera)
	{
		vw.SetCamera(camera);
	}

	private void SetLabel(VectorWrapper vw, Vector3 target)
	{
		vw.SetLabel(target);
	}

	private void ResetVector(VectorWrapper vw)
	{
		vw.vecTransform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
		vw.vecTransform.localScale = Vector3.one;
	}

	private void ScaleVector(VectorWrapper vw, Vector3 target)
	{
		var length = target.magnitude;
		vw.vecTransform.localScale = new Vector3(1f, 1f, length / 2f);
	}

	private void RotateAndPositionVector(VectorWrapper vw, Vector3 target)
	{
		vw.vecTransform.SetPositionAndRotation(target / 2f, Quaternion.LookRotation(target, Vector3.up));
	}
}
