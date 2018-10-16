using UnityEngine;

public class VectorWrapper : MonoBehaviour
{
	public Transform vecTransform;
	public TextMesh label;
	public Billboarder billboarder;

	public Vector3 Vec { get; private set; }

	public void SetCamera(Camera camera)
	{
		billboarder.cam = camera;
	}

	public void SetLabel(Vector3 position)
	{
		Vec = position;
		label.gameObject.transform.position = position;
		label.text = string.Format("{0}; {1}; {2}", position.x, position.y, position.z);
	}
}
