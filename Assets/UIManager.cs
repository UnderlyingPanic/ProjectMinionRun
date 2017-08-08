using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {

    public GameObject canvasObject;
    public ResearchTree[] treeArray;
        
	// Use this for initialization
	void Start () {
        
       
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OpenUI(Lane lane)
    {
        canvasObject.SetActive(true);

        treeArray = FindObjectsOfType<ResearchTree>();

        foreach (ResearchTree tree in treeArray)
        {
            if (tree.lane != lane)
            {
                tree.gameObject.SetActive(false);
            }
        }
    }
    
    public void CloseUI()
    {
        canvasObject.SetActive(false);
    }
}
