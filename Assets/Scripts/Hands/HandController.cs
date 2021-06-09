using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class HandController : MonoBehaviour
{
    ActionBasedController controller;
    InputAction closeHand;

    public Hand hand;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<ActionBasedController>();

        var actionMap = controller.selectAction.action.actionMap;
        closeHand = actionMap.FindAction("Select");

        closeHand.Enable();
        closeHand.performed += CloseHand;
        closeHand.canceled += OpenHand;

    }

    // Update is called once per frame
    void CloseHand(InputAction.CallbackContext context)
    {
        hand.SetClosed(true);
    }

    void OpenHand(InputAction.CallbackContext context)
    {
        hand.SetClosed(false);
    }
}
