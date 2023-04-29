using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float _moveSpeed = 4f;
    [SerializeField]
    private float _jumpForce = 10f;

    [SerializeField]
    private BoxCollider _movementBounds;

    private InputAction _moveAction;
    private Rigidbody _rb;
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

        if (_isXOutOfBounds)
        {
            // Get the closest point we're out of bounds to
            Vector3 closestPoint = _movementBounds.ClosestPoint(transform.position);

            // Move back towards the point on the bounds
            moveVector.x = Mathf.Sign(closestPoint.x - transform.position.x);
        }

        _rb.AddForce(new Vector3(moveVector.x, 0, 0) * _moveSpeed * Time.deltaTime, ForceMode.Impulse);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject != _movementBounds.gameObject) return;

        _isXOutOfBounds = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        _isXOutOfBounds = false;
    }
}
