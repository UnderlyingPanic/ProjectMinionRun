using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour {

    public Text title;
    public Text description;
    private Vector2 ttPos;
    public GameObject canvas;
    public Vector2 offset;

    // Use this for initialization
    void Start () {
                
    }
	
	// Update is called once per frame
	void Update () {
        ttPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y) + offset;

        transform.position = ttPos;
        
    }

    public void InstantiateTooltip()
    {
        print("Mouse Over");
        GameObject newToolTip = Instantiate(this.gameObject, GameObject.Find("Research Trees UI").transform);
    }

    public void DestroyTooltips ()
    {
        foreach (Tooltip tt in GameObject.FindObjectsOfType<Tooltip>())
        {
            Destroy(tt.gameObject);
        }
    }

    public void ChangeTooltipTitle(string name)
    {
        this.title.text = name;
    }

    public void ChangeTooltipDescription(string desc)
    {
        this.description.text = desc;
    }
    
    public void ClearTooltip()
    {
        this.title.text = null;
        this.description.text = null;
    }
}
