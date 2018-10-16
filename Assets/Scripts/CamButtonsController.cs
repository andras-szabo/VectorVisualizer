using System.Collections;
using UnityEngine;

public class CamButtonsController : MonoBehaviour
{
	public float lerpDurationSeconds = 2f;
	public Transform cam;
	public FlexibleRotator rotator;

	private Coroutine _runningRoutine;

	public void OnXYTapped()
	{
		RotateTo(new Vector3(0, 0, -1f), Vector3.up);
		rotator.SetRotationAxes(horizontal: Vector3.right, vertical: Vector3.up);
	}

	public void OnZYTapped()
	{
		RotateTo(new Vector3(1f, 0, 0), Vector3.up);
		rotator.SetRotationAxes(horizontal: Vector3.forward, vertical: Vector3.up);
	}

	public void OnZXTapped()
	{
		RotateTo(new Vector3(0, 1f, 0), Vector3.forward);
		rotator.SetRotationAxes(horizontal: Vector3.right, vertical: Vector3.forward);
	}

	private void RotateTo(Vector3 rotationForward, Vector3 targetUpVector)
	{
		if (_runningRoutine != null)
		{
			StopCoroutine(_runningRoutine);
		}

		var current = cam.position;
		var goal = Vector3.Magnitude(cam.position) * rotationForward;

		var axis = Vector3.Cross(current.normalized, goal.normalized);
		var targetAngle = Mathf.Acos(Vector3.Dot(current.normalized, goal.normalized)) * 180.0f / Mathf.PI;

		_runningRoutine = StartCoroutine(RotateRoutine(axis, goal, targetAngle, targetUpVector));
	}

	private IEnumerator RotateRoutine(Vector3 rotationAxis, Vector3 goal, float targetAngle, Vector3 targetUpVector)
	{
		rotator.Lock(true);

		var elapsedSeconds = 0f;
		var startPos = cam.position;
		var up = cam.up;
		while (elapsedSeconds < lerpDurationSeconds)
		{
			var rotation = Quaternion.AngleAxis(targetAngle * elapsedSeconds / lerpDurationSeconds, rotationAxis);
			var rotMatrix = Matrix4x4.Rotate(rotation);
			var currentPosition = rotMatrix.MultiplyPoint(startPos);
			var currentUp = Vector3.Lerp(up, targetUpVector, elapsedSeconds / lerpDurationSeconds);
			cam.SetPositionAndRotation(currentPosition, Quaternion.LookRotation(-currentPosition, currentUp));
			elapsedSeconds += Time.deltaTime;
			yield return null;
		}

		cam.SetPositionAndRotation(goal, Quaternion.LookRotation(-goal, targetUpVector));

		rotator.Lock(false);

		_runningRoutine = null;
	}
}
