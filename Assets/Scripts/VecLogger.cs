using UnityEngine;
using UnityEngine.UI;

public class VecLogger : MonoBehaviour
{
	[SerializeField] private Text text;

	public void Log(string msg)
	{
		text.text = msg;
	}
}
