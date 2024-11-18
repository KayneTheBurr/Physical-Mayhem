using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Cinemachine;


using UnityEngine.Rendering;
using System.Drawing;
using UnityEngine.Rendering.Universal;
using Color = UnityEngine.Color;



public class ShipStatus : MonoBehaviour
{
    public CinemachineVirtualCamera vCam;
    public CinemachineBasicMultiChannelPerlin shakerTech;
    public int shipHealth = 5;
    public int ballMass;
    public bool vulnerable = true;
    public float shakeStrength;
    public TextMeshProUGUI healthLabel;
    public GameObject winScreen, loseScreen;
    public BeamCollector beamLogic;
    public Rigidbody wreckingBallRB;
    public Volume volume, volumeDamaged;
    public Color vinBlack, vinRed;
    public GameObject redFlash;

    public void ShakeCamera(float intensity)
    {
        //volume.GetComponent<Vignette>().color.value = vinRed;
        //CinemachineBasicMultiChannelPerlin cineShaker = vCam.GetComponent<CinemachineBasicMultiChannelPerlin>();

        //cineShaker.m_AmplitudeGain = intensity;
        redFlash.SetActive(true);
        Invoke(nameof(StopShakeCamera), 0.25f);
    }
    public void StopShakeCamera()
    {
        redFlash.SetActive(false);
        //volume.GetComponent<Vignette>().color.value = vinBlack;
        //CinemachineBasicMultiChannelPerlin cineShaker = vCam.GetComponent<CinemachineBasicMultiChannelPerlin>();
        //cineShaker.m_AmplitudeGain  = 0;
    }
    // Start is called before the first frame update
    void Start()
    {
        beamLogic = FindObjectOfType<BeamCollector>();
        wreckingBallRB.mass = ballMass;
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
                switch (col.gameObject.GetComponent<WallConnectionSetter>().myMaterialType)
                {
                    case BlockMaterial.Brick:
                        shipHealth--;
                        break;
                    case BlockMaterial.Stone:
                        shipHealth -= 1;
                        break;
                    case BlockMaterial.Steel:
                        shipHealth -= 2;
                        break;
                    case BlockMaterial.Titanium:
                        shipHealth -= 5;
                        break;
                }
                ShakeCamera(shakeStrength);
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
