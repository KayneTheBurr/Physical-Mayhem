using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BlockMaterial { Brick, Stone, Steel, Titanium}
public class WallConnectionSetter : MonoBehaviour
{
    public HingeJoint hingeUp, hingeDown, hingeLeft, hingeRight, hingeForward, hingeBack;
    //public List<HingeJoint> hingeList = new List<HingeJoint>();
    public float rayLength, breakForce, breakTorque;
    public bool doesDamage = true, rubbleAlready = false;
    public BlockMaterial myMaterialType;
    
    void Awake()
    {
        SetHingeConnection(hingeUp, Vector3.up);
        SetHingeConnection(hingeDown, Vector3.down);
        SetHingeConnection(hingeLeft, Vector3.left);
        SetHingeConnection(hingeRight, Vector3.right);
        SetHingeConnection(hingeForward, Vector3.forward);
        SetHingeConnection(hingeBack, Vector3.back);
    }


    void SetHingeConnection(HingeJoint hinge, Vector3 direction)
    {

        // raycast in direction
        if (Physics.Raycast(transform.position, direction, out RaycastHit hitInfo, rayLength))
        {
            // Check if the object hit by the raycast has a Rigidbody and is not the same object
            if (hitInfo.rigidbody != null && hitInfo.rigidbody != GetComponent<Rigidbody>())
            {
                // Set the connected body to the object hit by the ray
                hinge.connectedBody = hitInfo.rigidbody;
                hinge.breakForce = breakForce;
                hinge.breakTorque = breakTorque;

                //Debug.Log($"Connected {hinge.name} to {hitInfo.collider.name} in direction {direction}");
            }
        }


    }
    private void Update()
    {
        if (this.transform.position.y <= -10)
        {
            Destroy(this.gameObject);
        }
        if (hingeBack == null && hingeDown == null && hingeForward == null
            && hingeLeft == null && hingeRight == null && hingeUp == null)
        {
            doesDamage = false;
            if(!rubbleAlready)
            {
                JustRubble();
            }
            
        }
    }
    void JustRubble()
    {
        rubbleAlready = true;
        this.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(1, 1, 1), ForceMode.Impulse);
        //Destroy(this.gameObject);
    }

}
  
