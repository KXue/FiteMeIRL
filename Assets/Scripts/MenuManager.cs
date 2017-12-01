using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour {
	private static MenuManager instance;
    // Static singleton property
    public static MenuManager Instance
    {
        get
        {
            return instance ?? (instance = new GameObject("Singleton").AddComponent<MenuManager>());
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
