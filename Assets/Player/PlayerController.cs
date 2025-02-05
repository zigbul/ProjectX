using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private CharacterController _player;
    [SerializeField] private Transform _camera;
    [SerializeField] private Transform _groundChecker;
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private Animator _swordAnimator;

    [Header("Settings")]
    [SerializeField] private float _checkRaduis = 0.2f;
    [SerializeField] private float _gravity = -9.8f;
    [SerializeField] private float _speed = 4f;
    [SerializeField] private float _runSpeed = 7f;
    [SerializeField] private float _jumpHeight = 1f;

    [Range(1, 100)]
    [SerializeField] private float _sensitivity = 50f;

    private float _rotationX;
    private bool _isGrounded;
    private bool _isRunning = false;
    private Vector3 _velocity;
    private Vector3 _move;

    public bool IsGrounded => _isGrounded;
    public bool IsRunning => _isRunning;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        CheckGround();

        if (_isGrounded)
        {
            Jump();
        }

        Move();
        Rotate();

        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }

    private void Rotate()
    {
        float mouseX = Input.GetAxis("Mouse X") * _sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * _sensitivity * Time.deltaTime;

        _rotationX -= mouseY;
        _rotationX = Mathf.Clamp(_rotationX, -90f, 90f);

        _camera.localRotation = Quaternion.Euler(_rotationX, 0, 0);

        transform.Rotate(0, mouseX, 0);
    }

    private void Move()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        _move = (transform.forward * moveY) + (transform.right * moveX);

        if (Input.GetKey(KeyCode.LeftShift) && (moveX != 0 || moveY != 0))
        {
            _isRunning = true;
            _player.Move(_move * _runSpeed * Time.deltaTime);
        }
        else
        {
            _isRunning = false;
            _player.Move(_move * _speed * Time.deltaTime);
        }
    }

    private void CheckGround()
    {
        _isGrounded = Physics.CheckSphere(_groundChecker.position, _checkRaduis, _groundMask);

        if (_isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f;
        }

        _velocity.y += Time.deltaTime * _gravity;

        _player.Move(_velocity * Time.deltaTime);
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            _velocity.y = Mathf.Sqrt(_jumpHeight * -2f * _gravity);
        }

        _player.Move(_velocity * Time.deltaTime);
    }

    private void Attack()
    {
        _swordAnimator.Play("SwordAttack");
    }
}
