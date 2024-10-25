using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombLogic : MonoBehaviour
{
    Rigidbody rb;
    public MeshRenderer myRend;
    public float explosionRadius, explosionStrength, upForce, bombDelay, dropForce;
    public List<Rigidbody> myExplodeList;
    public Material redGlow, redMid, RedDim;

    public void OnTriggerEnter(Collider col)
    {
        if(col.GetComponent<Rigidbody>() && !col.GetComponent<BombLogic>())
        {
            myExplodeList.Add(col.GetComponent<Rigidbody>());
        }
        
    }
    public void OnTriggerExit(Collider col)
    {
        if (col.GetComponent<Rigidbody>() && !col.GetComponent<BombLogic>())
        {
            myExplodeList.Remove(col.GetComponent<Rigidbody>());
        }
    }


    private void OnEnable()
    {
        rb = this.GetComponent<Rigidbody>();
        rb.AddForce(Vector3.up * dropForce, ForceMode.Impulse);
        Invoke(nameof(ColorBrighten1), 1);
        
        
    }

    void Explode()
    {
        foreach(Rigidbody rb in myExplodeList)
        {
            rb.AddExplosionForce(explosionStrength, transform.position, explosionRadius, upForce, ForceMode.Impulse);
            Invoke(nameof(Death), 0.25f);
        }
        
    }
    public void ColorBrighten1()
    {
        
        myRend.material = redMid;
        Invoke(nameof(ColorBrighten2), 1.5f);
    }
    public void ColorBrighten2()
    {
        myRend.material = redGlow;
        
        Invoke(nameof(Explode), bombDelay);
    }
    public void Death()
    {
        Destroy(this.gameObject);
    }
}
