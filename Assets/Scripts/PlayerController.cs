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
    [SerializeField]
    private float _groundedDistance;

    private Rigidbody _rb;
    private InputAction _moveAction;
    private string _actionMapName = "gameplay";
    private Vector2 _moveVector;
    private bool _isXOutOfBounds;
    private bool _isGrounded;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();

        _moveAction = InputManager.Instance.ActionsAsset.FindActionMap(_actionMapName).FindAction("move");
        InputManager.Instance.ActionsAsset.FindActionMap(_actionMapName).FindAction("jump").performed += OnJump;
    }

    private void Update()
    {
        Move(_moveAction.ReadValue<Vector2>());

        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, _groundedDistance)) {
            _isGrounded = true;
        } else
        {
            _isGrounded = false;
        }
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        Debug.Log("Pressed Jump!");

        if (!_isGrounded) return;

        _rb.velocity = new Vector3(_rb.velocity.x, 0, _rb.velocity.z);

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
