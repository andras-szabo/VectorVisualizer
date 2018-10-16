using System;
using UnityEngine;

public class InputService : MonoSingleton<InputService>, IInputService
{
	public event Action<Vector2, int> OnDrag;

	public float dragSensitivity = 0.5f;
	public float perFrameDragThreshold = 2f;

	public void ReportEvent(InputEvent evt)
	{
		switch (evt.type)
		{
			case InputEvent.Type.Drag: 
				{
					var touchCount =
#if UNITY_EDITOR
					(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) ?
					2 : 1;
#else
	Input.touchCount;
#endif

					var dx = Mathf.Abs(evt.delta.x) > perFrameDragThreshold ? evt.delta.x : 0f;
					var dy = Mathf.Abs(evt.delta.y) > perFrameDragThreshold ? evt.delta.y : 0f;

					OnDrag(new Vector2(dx * dragSensitivity,
									   dy * dragSensitivity),
									   touchCount);
					break; }
			default:
				break;
		}
	}
}

public struct InputEvent
{
	public enum Type
	{
		None,
		Drag
	}

	public Type type;
	public Vector2 delta;
}

public interface IInputService
{
	event Action<Vector2, int> OnDrag;
	void ReportEvent(InputEvent evt);
}
