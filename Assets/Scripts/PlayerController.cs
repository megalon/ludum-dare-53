using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float _moveSpeed = 4f;

    [SerializeField]
    private BoxCollider _movementBounds;
    [SerializeField]
    private float _jumpForce;

    private Rigidbody _rb;
    private InputAction _moveAction;
    private string _actionMapName = "gameplay";
    private Vector2 _moveVector;
    private bool _isXOutOfBounds;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();

        _moveAction = InputManager.Instance.ActionsAsset.FindActionMap(_actionMapName).FindAction("move");
        InputManager.Instance.ActionsAsset.FindActionMap(_actionMapName).FindAction("jump").performed += OnJump;
    }

    private void Update()
    {
        Move(_moveAction.ReadValue<Vector2>());
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        Debug.Log("Jump!");
        _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
    }

    public void Move(Vector2 moveVector)
    {
        transform.Translate(moveVector.x * _moveSpeed * Time.deltaTime, 0, 0, Space.Self);

        // Keep object within x bounds
        float clampedX = Mathf.Clamp(transform.position.x, -_movementBounds.bounds.extents.x, _movementBounds.bounds.extents.x);
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
    }
}
