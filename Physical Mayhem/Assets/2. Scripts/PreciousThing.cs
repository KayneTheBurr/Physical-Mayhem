using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreciousThing : MonoBehaviour
{
    public BeamCollector beamLogic;
    
    void Start()
    {
        beamLogic = FindObjectOfType<BeamCollector>();
    }

    
    void Update()
    {
        //destroy the treasure if it falls off the map 
        if (this.transform.position.y <= -10)
        {
            Destroy(this.gameObject);
        }
            
    }
}
