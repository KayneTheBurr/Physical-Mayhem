using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeCenter : MonoBehaviour
{
    public Button upgradeButton, massUp, speedUp, BOMB;
    public GameObject upgradePanel;
    public ShipStatus shipInfo;
    public Player shipController;
    public BeamCollector beamLogic;
    public int currency, bombsHave;
    public AudioSource sfxBuy;


    private void OnEnable()
    {
        upgradeButton.onClick.AddListener(() => UpgradeButton());
        massUp.onClick.AddListener(() => IncreaseMass());
        speedUp.onClick.AddListener(() => IncreaseSpeed());
        BOMB.onClick.AddListener(() => MakeBomb());
        
    }
    
    private void Update()
    {
        if(currency < 5)
        {
            massUp.interactable = false;
            speedUp.interactable = false;
            BOMB.interactable = false;
        }
        else if(currency >= 5 && currency < 10)
        {
            massUp.interactable = true;
            speedUp.interactable = true;
            BOMB.interactable = false;
        }
        else if(currency >= 10)
        {
            massUp.interactable = true;
            speedUp.interactable = true;
            BOMB.interactable = true;
        }
    }


    private void Start()
    {
        upgradePanel.SetActive(false);
    }

    private void UpgradeButton()
    {
        upgradePanel.SetActive(true);
    }
    private void IncreaseMass()
    {
        currency -= 5;
        shipInfo.wreckingBallRB.mass += 3;
        sfxBuy.Play();
    }
    void IncreaseSpeed()
    {
        currency -= 5;
        shipController.moveSpeed += 3;
        sfxBuy.Play();
    }
    void MakeBomb()
    {
        currency -= 10;
        bombsHave++;
        sfxBuy.Play();
    }
}
