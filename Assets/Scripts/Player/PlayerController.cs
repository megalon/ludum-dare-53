using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
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
    [SerializeField]
    private UnityEvent _onSplashDown;
    [SerializeField]
    private UnityEvent _onJump;

    private bool _isGrounded;
    public bool IsGrounded { get => _isGrounded; }

    private Rigidbody _rb;
    private InputAction _moveAction;
    private string _actionMapName = "gameplay";
    private Damageable _damageable;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _damageable = GetComponent<Damageable>();

        _moveAction = InputManager.Instance.ActionsAsset.FindActionMap(_actionMapName).FindAction("move");
        InputManager.Instance.ActionsAsset.FindActionMap(_actionMapName).FindAction("jump").performed += OnJump;
    }

    private void Update()
    {
        Move(_moveAction.ReadValue<Vector2>());

        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, _groundedDistance)) {
            if (!_isGrounded)
            {
                _onSplashDown.Invoke();
            }
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

        _onJump.Invoke();
    }

    public void Move(Vector2 moveVector)
    {
        transform.Translate(moveVector.x * _moveSpeed * Time.deltaTime, 0, 0, Space.Self);

        // Keep object within x bounds
        float clampedX = Mathf.Clamp(transform.position.x, -_movementBounds.bounds.extents.x, _movementBounds.bounds.extents.x);
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.tag.Equals("enemy")) return;

        _damageable.Hurt(1, Damageable.Team.ENEMY);
    }
}
