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
    public float spinSpeed, forceSpeed, moveSpeed, verticalSpeed;

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
        
        if(Input.GetKeyDown(KeyCode.LeftShift) && upgradeLogic.bombsHave > 0)
        {
            Debug.Log("make bomb");
            Instantiate(bombPrefab, new Vector3(transform.position.x, transform.position.y+3, transform.position.z), Quaternion.identity);
            upgradeLogic.bombsHave--;
        }

    }
}
