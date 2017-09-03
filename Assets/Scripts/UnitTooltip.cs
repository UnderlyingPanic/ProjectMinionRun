using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitTooltip : MonoBehaviour {

    public GameObject selectedObject;

    public Text unitNameText;
    public Text damageText;
    public Text attackSpeedText;
    public Text lifeStealText;
    public Text hpText;
    public Text armourText;
    public Text shieldText;

    private Transform generalUI;
    private GameManager gameManager;


    // Use this for initialization
    void Start () {
        generalUI = GameObject.Find("General UI").transform;
        gameManager = FindObjectOfType<GameManager>();
	}

    // Update is called once per frame
    void Update() {
        if (selectedObject == null) {
            return;
        }
        if (selectedObject.GetComponent<Creep>())
        {
            Creep creep = selectedObject.GetComponent<Creep>();

            damageText.text = creep.damage.ToString();
            attackSpeedText.text = creep.attackSpeedMod.ToString();
            lifeStealText.text = creep.lifeSteal.ToString();
            armourText.text = creep.armour.ToString();
            shieldText.text = creep.shield.ToString();
            unitNameText.text = creep.type.ToString();

            string currHp = selectedObject.GetComponent<Health>().currHealth.ToString();
            string maxHP = selectedObject.GetComponent<Health>().maxHealth.ToString();

            hpText.text = currHp + "/" + maxHP;
        }
    }
}
