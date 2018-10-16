using System.Collections.Generic;
using UnityEngine;

public class ControlPanel : MonoBehaviour
{
	public KeyboardInputHandler inputHandler;
	public VecOrienter vecSpawner;
	public VecUIInventory inventory;
	public VecLogger vecLogger;

	private void Awake()
	{
		AddListeners();
	}

	private void OnDestroy()
	{
		RemoveListeners();
	}

	public void OnAddNewVectorTapped()
	{
		inputHandler.StartGettingInput();
	}

	public void OnRemoveSelectedVectorsTapped()
	{
		inventory.TryRemoveSelected();
	}

	public void OnDotProductTapped()
	{
		TryCalculateDotProduct(inventory.GetAllSelectedVectors());
	}

	public void OnCrossProductTapped()
	{
		Vector3 crossProduct;
		if (TryCalculateCrossProduct(inventory.GetAllSelectedVectors(), out crossProduct))
		{
			AddNewVector(crossProduct);
		}
	}

	private bool TryCalculateCrossProduct(List<VectorWrapper> selectedVectors, out Vector3 crossProduct)
	{
		crossProduct = Vector3.zero;

		var success = false;
		var resultString = "[CrossProduct] Couldn't calculate";

		if (inventory.SelectedA != null && inventory.SelectedB != null)
		{
			var a = inventory.SelectedA.VectorWrapper.Vec;
			var b = inventory.SelectedB.VectorWrapper.Vec;

			crossProduct = Vector3.Cross(a, b);

			if (crossProduct != Vector3.zero)
			{
				resultString = string.Format("({0}) x ({1}) = ({2})", a, b, crossProduct);
				success = true;
			}
			else
			{
				resultString = "[CrossProduct] Would be nullvector.";
				success = false;
			}
		}

		vecLogger.Log(resultString);
		return success;
	}

	private bool TryCalculateDotProduct(List<VectorWrapper> selectedVectors)
	{
		var success = false;
		var resultString = "[DotProduct] Couldn't calculate";

		if (selectedVectors.Count == 2)
		{
			var a = selectedVectors[0].Vec;
			var b = selectedVectors[1].Vec;

			resultString = string.Format("({0}) o ({1}) = {2}", a, b, Vector3.Dot(a, b));
			success = true;
		}

		vecLogger.Log(resultString);
		return success;
	}

	public void HandleInputEntered(string input)
	{
		Vector3 vector;
		if (VectorParser.TryParse(input, out vector))
		{
			AddNewVector(vector);
		}
		else
		{
			Debug.Log("Couldn't parse input: " + input);
		}
	}

	private void AddNewVector(Vector3 vector)
	{
		var newVecWrapper = vecSpawner.CreateVector(vector);
		inventory.Add(newVecWrapper);
	}

	private void AddListeners()
	{
		inputHandler.OnTextEntered += HandleInputEntered;
	}

	private void RemoveListeners()
	{
		if (inputHandler != null)
		{
			inputHandler.OnTextEntered -= HandleInputEntered;
		}
	}
}
