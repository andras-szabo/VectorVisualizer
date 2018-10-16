using UnityEngine;
using UnityEngine.EventSystems;

public class InputHandler : MonoBehaviour, IDragHandler
{
	private InputService _inputService;

	private void Start()
	{
		_inputService = InputService.Instance;	
	}

	public void OnDrag(PointerEventData eventData)
	{
		if (eventData.dragging)
		{
			_inputService.ReportEvent(new InputEvent
			{
				type = InputEvent.Type.Drag,
				delta = eventData.delta
			});
		}
	}
}
