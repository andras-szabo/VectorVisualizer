using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VecUIInventory : MonoBehaviour
{
	public VectorWidget widgetTemplate;
	public Transform scrollRectContents;

	private List<VectorWidget> _inventory = new List<VectorWidget>();

	public VectorWidget SelectedA { get; private set; }
	public VectorWidget SelectedB { get; private set; }

	public void Add(VectorWrapper vw)
	{
		var widget = UnityEngine.Object.Instantiate<VectorWidget>(widgetTemplate);

		widget.Setup(vw);
		widget.gameObject.transform.SetParent(scrollRectContents, false);
		widget.gameObject.transform.localScale = Vector3.one;
		widget.OnSelectionToggleRequested += HandleSelectionToggleRequested;

		_inventory.Add(widget);
	}

	public void TryRemoveSelected()
	{
		if (SelectedA != null)
		{
			SelectedA.SetSelected(VectorWidget.SelectionState.None);
			_inventory.Remove(SelectedA);
			UnityEngine.Object.Destroy(SelectedA.VectorWrapper.gameObject);
			UnityEngine.Object.Destroy(SelectedA.gameObject);
			SelectedA = null;
		}

		if (SelectedB != null)
		{
			SelectedB.SetSelected(VectorWidget.SelectionState.None);
			_inventory.Remove(SelectedB);
			UnityEngine.Object.Destroy(SelectedB.VectorWrapper.gameObject);
			UnityEngine.Object.Destroy(SelectedB.gameObject);
			SelectedB = null;
		}
	}

	private void HandleSelectionToggleRequested(VectorWidget widget)
	{
		if (SelectedA == null && SelectedB == null)
		{
			widget.SetSelected(VectorWidget.SelectionState.A);
			SelectedA = widget;
			return;
		}

		if (SelectedA != null && SelectedB == null)
		{
			if (widget == SelectedA)
			{
				widget.SetSelected(VectorWidget.SelectionState.None);
				SelectedA = null;
				return;
			}

			widget.SetSelected(VectorWidget.SelectionState.B);
			SelectedB = widget;
			return;
		}

		if (SelectedA != null && SelectedB != null)
		{
			if (widget == SelectedA)
			{
				widget.SetSelected(VectorWidget.SelectionState.B);
				SelectedB.SetSelected(VectorWidget.SelectionState.A);
				SelectedA = SelectedB;
				SelectedB = widget;
				return;
			}

			if (widget == SelectedB)
			{
				widget.SetSelected(VectorWidget.SelectionState.None);
				SelectedB = null;
				return;
			}

			SelectedB.SetSelected(VectorWidget.SelectionState.None);
			SelectedB = widget;
			widget.SetSelected(VectorWidget.SelectionState.B);
		}
	}

	public List<VectorWrapper> GetAllSelectedVectors()
	{
		var selected = new List<VectorWrapper>();

		foreach (var widget in _inventory)
		{
			if (widget.SelectState != VectorWidget.SelectionState.None)
			{
				selected.Add(widget.VectorWrapper);
			}
		}

		return selected;
	}
}
