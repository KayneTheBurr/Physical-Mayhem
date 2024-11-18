using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public UpgradeCenter upgradeLogic;
    public GameObject bombPrefab;
    public InputControls inputController;
    public Rigidbody rb;
    public float spinSpeed, forceSpeed, moveSpeed, verticalSpeed, shipRadius;
    public LayerMask wallLayer;

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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.Joystick1Button2) && upgradeLogic.bombsHave > 0)
        {
            Debug.Log("make bomb");
            Instantiate(bombPrefab, new Vector3(transform.position.x, transform.position.y - 3, transform.position.z), Quaternion.identity);
            upgradeLogic.bombsHave--;
        }
    }
    void FixedUpdate()
    {
        Vector2 move = inputController.BasicActions.Move.ReadValue<Vector2>();
        Vector3 moveMode = new Vector3(move.x, 0, move.y);
        if(CanMove(moveMode))
        {
            rb.velocity = moveMode * Time.fixedDeltaTime * moveSpeed * 100;
        }
        
        
    }
    bool CanMove(Vector3 direction)
    {
        RaycastHit hit;
        float checkDistance = Time.deltaTime * moveSpeed * 100;
        Debug.DrawRay(transform.position, direction, Color.red, 1f); 

        if (Physics.SphereCast(transform.position, shipRadius, direction, out hit, checkDistance, wallLayer))
        {
            
            return false;
        }
            
        return true;
    }
}
