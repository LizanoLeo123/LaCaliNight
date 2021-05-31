using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class MovementTest : MonoBehaviour
{
    public GameObject randomPrefab;

    public ActionBasedController playerControls;
    //InputActionAsset playerControls;
    InputAction movement;
    InputAction activate;

    CharacterController cc;
    public float speed = 5f;
    Vector3 moveVector;

    // Start is called before the first frame update
    void Start()
    {
        //var gameplayActionMap = playerControls.FindActionMap("XRI LeftHand");
        //movement = gameplayActionMap.FindAction("Move");

        //movement.Enable();
        //movement.performed += OnMovementChanged;
        //movement.canceled += OnMovementChanged;

        var gameplayActionMap = playerControls.selectAction.action.actionMap;
        movement = gameplayActionMap.FindAction("Move");

        movement.Enable();
        movement.performed += OnMovementChanged;
        movement.canceled += OnMovementChanged;

        activate = gameplayActionMap.FindAction("Activate");
        //activate.Enable();
        activate.performed += OnActivate;
        //activate.canceled += OnActivate;

        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        cc.Move(transform.TransformDirection (moveVector) * speed * Time.deltaTime);
    }

    private void OnActivate(InputAction.CallbackContext context)
    {
        Instantiate(randomPrefab, transform.position, Quaternion.identity);
    }

    private void OnMovementChanged(InputAction.CallbackContext context)
    {
        Vector2 direction = context.ReadValue<Vector2>();
        moveVector = new Vector3(direction.x, 0, direction.y);
    }
}
