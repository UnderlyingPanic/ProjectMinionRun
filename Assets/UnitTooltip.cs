using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitTooltip : MonoBehaviour {

    public GameObject selectedObject;

    public Text damageText;
    public Text attackSpeedText;
    public Text lifeStealText;
    public Text hpText;
    public Text armourText;
    public Text shieldText;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
        if (selectedObject.GetComponent<Creep>())
        {
            Creep creep = selectedObject.GetComponent<Creep>();

            damageText.text = creep.damage.ToString();
            attackSpeedText.text = creep.attackSpeedMod.ToString();
            lifeStealText.text = creep.lifeSteal.ToString();
            armourText.text = creep.armour.ToString();
            shieldText.text = creep.shield.ToString();

            string currHp = selectedObject.GetComponent<Health>().currHealth.ToString();
            string maxHP = selectedObject.GetComponent<Health>().maxHealth.ToString();

            hpText.text = currHp + "/" + maxHP;
        }
            
           
    }

    public void SetText ()
    {
       
    }
}
