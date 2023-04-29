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
        Vector3 v3 = new Vector3(moveVector.x, moveVector.y, 0);
        _rb.AddForce(v3 * _moveSpeed, ForceMode.Impulse);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject != _movementBounds.gameObject) return;

        _rb.velocity = new Vector3(0, _rb.velocity.y, _rb.velocity.z);

        transform.position = other.ClosestPointOnBounds(transform.position);
    }
}
