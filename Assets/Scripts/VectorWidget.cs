using System;
using UnityEngine;
using UnityEngine.UI;

public class VectorWidget : MonoBehaviour
{
	public enum SelectionState
	{
		None,
		A,
		B
	}

	public event Action<VectorWidget> OnSelectionToggleRequested;

	[SerializeField] private Text label;
	[SerializeField] private Text magnitude;
	[SerializeField] private GameObject selectionMarkerA;
	[SerializeField] private GameObject selectionMarkerB;

	public SelectionState SelectState { get; private set; }
	public VectorWrapper VectorWrapper { get; private set; }

	public void Setup(VectorWrapper vw)
	{
		VectorWrapper = vw;
		var vec = vw.Vec;
		label.text = string.Format("{0}; {1}; {2}", vec.x, vec.y, vec.z);
		magnitude.text = string.Format("|v| = {0}", Vector3.Magnitude(vec));
		SelectState = SelectionState.None;
		UpdateSelectionMarker();
	}

	public void OnSelectButton()
	{
		OnSelectionToggleRequested(this);	
	}

	public void SetSelected(SelectionState state)
	{
		SelectState = state;
		UpdateSelectionMarker();
	}

	private void UpdateSelectionMarker()
	{
		selectionMarkerA.gameObject.SetActive(SelectState == SelectionState.A);
		selectionMarkerB.gameObject.SetActive(SelectState == SelectionState.B);
	}
}
