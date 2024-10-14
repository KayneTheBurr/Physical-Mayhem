using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    
    public InputControls inputController;
    public Rigidbody rb;
    public float spinSpeed, rotateAngle, moveSpeed, verticalSpeed;

    // Start is called before the first frame update
    void Start()
    {
        inputController = new InputControls();
        inputController.Enable();
    }

    private void OnEnable()
    {
        if(inputController == null)
        {
            return;
        }
        inputController.Enable();
    }
    private void OnDisable()
    {
        if (inputController == null)
        {
            return;
        }
        inputController.Disable();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 move = inputController.BasicActions.Move.ReadValue<Vector2>();
        Vector3 moveMode = new Vector3(move.x, 0, move.y);
        transform.Translate(moveMode * Time.fixedDeltaTime * moveSpeed, Space.World);

        //if(Input.GetKey(KeyCode.Q))
        //{
        //    if(this.transform.position.y > 5)
        //    {
                
        //        this.gameObject.transform.Translate(Vector3.down * verticalSpeed * Time.fixedDeltaTime, Space.World);
        //    }
            
        //}
        //if (Input.GetKey(KeyCode.Q))
        //{
        //    if(this.transform.position.y <= 20)
        //    {
        //        this.gameObject.transform.Translate(Vector3.up * verticalSpeed * Time.fixedDeltaTime, Space.World);
        //    }
            
        //}

    }
}
