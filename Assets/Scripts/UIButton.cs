using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButton : MonoBehaviour {

    public Lane lane;
    private UIManager uiManager;

	// Use this for initialization
	void Start () {
        uiManager = GameObject.FindObjectOfType<UIManager>();

        
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.Rotate(Vector3.forward, 2f);
	}

    private void OnMouseDown()
    {
        uiManager.OpenUI(lane);
    }
}
