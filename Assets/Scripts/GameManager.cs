using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	private static GameManager instance;
    // Static singleton property
    public static GameManager Instance
    {
        get
        {
            return instance ?? (instance = new GameObject("Singleton").AddComponent<GameManager>());
        }
    }
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        instance = this;
		DontDestroyOnLoad(gameObject);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
