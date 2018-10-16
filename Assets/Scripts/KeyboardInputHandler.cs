using System;
using UnityEngine;

public class KeyboardInputHandler : MonoBehaviour, IKeyboardInputHandler
{
	private bool _isRequestingInput;
	private TouchScreenKeyboard _keyboard;
	private string _keyboardInput;

	public event Action<string> OnTextEntered;

	public void StartGettingInput()
	{
		_isRequestingInput = true;
	}

	private void OnGUI()
	{
		if (_isRequestingInput)
		{
#if UNITY_ANDROID && !UNITY_EDITOR
			DoInputGUI();
#elif UNITY_EDITOR
			DoEditorGUI();
#endif
		}
	}

#if UNITY_EDITOR
	private void DoEditorGUI()
	{
		const float BUTTON_WIDTH = 62f;
		_keyboardInput = GUI.TextField(new Rect(0f, 0f, 200f, 30f), _keyboardInput);
		var ok = GUI.Button(new Rect(0f, 30f, BUTTON_WIDTH, 30f), "OK");
		var cancel = GUI.Button(new Rect(BUTTON_WIDTH + 5f, 30f, BUTTON_WIDTH, 30f), "Cancel");

		if (ok && !cancel)
		{
			FinishKeyboardInputWithSuccess(_keyboardInput);
		}

		if (cancel && !ok)
		{
			CancelKeyboardInput();
		}
	}
#endif

	private void FinishKeyboardInputWithSuccess(string input)
	{
		_keyboardInput = input;
		_isRequestingInput = false;
		if (_keyboard != null)
		{
			_keyboard.active = false;
			_keyboard = null;
		}
		OnTextEntered(_keyboardInput);
	}

	private void CancelKeyboardInput()
	{
		_isRequestingInput = false;
		if (_keyboard != null)
		{
			_keyboard.active = false;
			_keyboard = null;
		}
	}

	private void DoInputGUI()
	{
		if (_keyboard != null)
		{
			if (_keyboard.status == TouchScreenKeyboard.Status.Done)
			{
				FinishKeyboardInputWithSuccess(_keyboard.text);
				return;
			}

			if (_keyboard.status == TouchScreenKeyboard.Status.Canceled)
			{
				CancelKeyboardInput();
				return;
			}
		}

		if (_keyboard == null)
		{
			_keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.NamePhonePad, false);
		}

		if (!_keyboard.active)
		{
			_keyboard.active = true;
		}
	}
}

public interface IKeyboardInputHandler
{
	event Action<string> OnTextEntered;
	void StartGettingInput();
}