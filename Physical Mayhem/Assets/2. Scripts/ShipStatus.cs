using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShipStatus : MonoBehaviour
{
    public int shipHealth = 5;
    public bool vulnerable = true;
    public TextMeshProUGUI healthLabel;
    public GameObject winScreen, loseScreen;
    public BeamCollector beamLogic;

    // Start is called before the first frame update
    void Start()
    {
        beamLogic = FindObjectOfType<BeamCollector>();
    }

    // Update is called once per frame
    void Update()
    {
        healthLabel.text = "Health: " + shipHealth;
        
    }

    public void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.GetComponent<WallConnectionSetter>() && col.gameObject.GetComponent<WallConnectionSetter>().doesDamage == true)
        {
            if (vulnerable)
            {
                shipHealth--;
                if (shipHealth <= 0) ShipDeath();
                vulnerable = false;
                Invoke(nameof(Shielded), 0.5f);
            }

        }
        //else { Debug.Log("Blocked!"); }
    }
    public void Shielded()
    {
        vulnerable = true;
    }
    public void ShipDeath()
    {
        Player thing = FindObjectOfType<Player>();
        thing.enabled = false;
        loseScreen.SetActive(true);
        thing.gameObject.GetComponent<Rigidbody>().isKinematic = false;
    }
    public void WinLevel()
    {
        Player thing = FindObjectOfType<Player>();
        thing.enabled = false;
        winScreen.SetActive(true);
    }
}
