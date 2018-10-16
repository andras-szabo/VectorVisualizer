using UnityEngine;

public class FlexibleRotator : MonoBehaviour
{
	public Transform cam;

	private Vector3 _axisX = new Vector3(1f, 0f, 0f);
	private Vector3 _axisY = new Vector3(0f, 1f, 0f);

	private float _angleHorizontal;
	private float _angleVertical;

	private bool _isLocked;

	public void SetRotationAxes(Vector3 horizontal, Vector3 vertical)
	{
		_axisX = horizontal;
		_axisY = vertical;
	}

	public void Lock(bool state)
	{
		_isLocked = state;
	}

	private void Start()
	{
		AddListeners();
	}

	private void OnDestroy()
	{
		RemoveListeners();
	}

	private void HandleDrag(Vector2 dragDelta, int touchCount)
	{
		if (_isLocked)
		{
			return;
		}

		if (touchCount == 1)
		{
			_angleHorizontal = (_angleHorizontal + dragDelta.x) / 3.5f;
			_angleVertical = (_angleVertical + dragDelta.y) / 3.5f;

			var rotMatrix = SetupRotMatrix(_angleHorizontal, _angleVertical);
			RotateEverything(rotMatrix);
		}
		else
		{
			var speed = dragDelta.y * 0.1f;
			if (Mathf.Abs(speed) > 0f)
			{
				TryMoveCameraForward(speed);
			}
		}
	}

	private void RotateEverything(Matrix4x4 rotMatrix)
	{
		var newCamPos = rotMatrix.MultiplyPoint(cam.position);
		cam.SetPositionAndRotation(newCamPos, Quaternion.LookRotation(Vector3.zero - cam.position, _axisY));
		_axisX = rotMatrix.MultiplyPoint(_axisX);
		_axisY = rotMatrix.MultiplyPoint(_axisY);
	}

	private Matrix4x4 SetupRotMatrix(float angleHorizontal, float angleVertical)
	{
		var horizontalRotation = Quaternion.AngleAxis(angleHorizontal, _axisY);
		var verticalRotation = Quaternion.AngleAxis(angleVertical, _axisX);

		var fullRotation = horizontalRotation * verticalRotation;
		return Matrix4x4.Rotate(fullRotation);
	}

	private void TryMoveCameraForward(float speedPerFrame)
	{
		cam.Translate(cam.forward * speedPerFrame, Space.World);
		cam.rotation = Quaternion.LookRotation(Vector3.zero - cam.position, _axisY);
	}


	private void AddListeners()
	{
		InputService.Instance.OnDrag += HandleDrag;
	}

	private void RemoveListeners()
	{
		var inputService = InputService.Instance;
		if (inputService != null)
		{
			inputService.OnDrag -= HandleDrag;
		}
	}

}
