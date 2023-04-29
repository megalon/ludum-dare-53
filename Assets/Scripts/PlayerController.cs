using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    InputActionAsset actionsAsset;

    private InputAction _moveAction;
    private string _actionMapName = "gameplay";

    private void Awake()
    {
        _moveAction = actionsAsset.FindActionMap(_actionMapName).FindAction("move");
        actionsAsset.FindActionMap(_actionMapName).FindAction("jump").performed += OnJump;
    }

    private void Update()
    {
        Vector2 moveVector = _moveAction.ReadValue<Vector2>();

        Debug.Log(moveVector.ToString());
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        Debug.Log("Jump pressed");
    }

    // We need to enable the action map to read the values
    void OnEnable()
    {
        actionsAsset.FindActionMap(_actionMapName).Enable();
    }

    void OnDisable()
    {
        actionsAsset.FindActionMap(_actionMapName).Disable();
    }
}
