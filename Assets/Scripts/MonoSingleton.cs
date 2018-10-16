using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T: class
{
	public static T Instance { get; private set; }

	protected virtual void Awake()
	{
		if (Instance == null) { Instance = GetComponent<T>(); }
		else { Debug.LogWarning("[MonoSingleton<T>] Already exists."); }
	}

	protected virtual void OnDestroy()
	{
		Instance = null;
	}
}
