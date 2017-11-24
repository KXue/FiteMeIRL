using UnityEngine;

public class Billboard : MonoBehaviour {
	/// <summary>
	/// Update is called every frame, if the MonoBehaviour is enabled.
	/// </summary>
	void Update()
	{
		transform.LookAt(Camera.main.transform);
	}
}
