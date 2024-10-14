using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BeamCollector : MonoBehaviour
{
    public ShipStatus myShip;
    public bool treasureInBeam, gameWin = false;
    public GameObject treasure = null;
    public int collectedTracker = 0, maxTreasures;
    public TextMeshProUGUI scoreLabel, winConditionLabel;

    public void Start()
    {
        myShip = FindObjectOfType<ShipStatus>();
        PreciousThing[] treasureList = FindObjectsOfType<PreciousThing>();
        maxTreasures = treasureList.Length;

    }
    public void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.GetComponent<PreciousThing>())
        {
            treasureInBeam = true;
            treasure = col.gameObject;

        }
        
    }
    public void OnTriggerExit(Collider col)
    {
        if (col.gameObject.GetComponent<PreciousThing>())
        {
            treasureInBeam = false;
            treasure = null;
        }
    }
    private void Update()
    {
        winConditionLabel.text = "Collect " + (maxTreasures - 1) + " to win";
        scoreLabel.text = "Collected: " + collectedTracker;
        if(treasureInBeam)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                treasureInBeam = false;
                if(treasure != null)
                {
                    Destroy(treasure);
                    collectedTracker++;
                    if (collectedTracker >= maxTreasures - 1)
                    {
                        gameWin = true;
                        myShip.WinLevel();
                    }
                }
                
            }
        }
    }

}
