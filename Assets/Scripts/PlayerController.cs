using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float Speed;
    public float JumpForce;
    [Header("Ground Check")]
    [SerializeField] private LayerMask _gorundMask;
    [SerializeField] private float _playerHeight = 2;
    [SerializeField] private float _checkGorundDistance = 0.01f;
    [Header("Animations")]
    [SerializeField] private Animator _animator;

    private float _verticalInput;
    private float _horizontalInput;

    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Inputs();
        Jump();
        Animate();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Inputs()
    {
        _verticalInput = Input.GetAxis("Vertical");
        _horizontalInput = Input.GetAxis("Horizontal");
    }

    private bool IsGrounded()
    {
        Vector3 position = new Vector3(transform.position.x, transform.position.y - _playerHeight / 2, transform.position.z);
        bool result = Physics.CheckSphere(position, _checkGorundDistance, _gorundMask);
        return result;
    }

    private void Move()
    {
        Vector3 moveDirection = _verticalInput * transform.forward + _horizontalInput * transform.right;
        Vector3 velocity = moveDirection * Speed + _rb.velocity.y * Vector3.up;
        _rb.velocity = velocity;
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            _animator.SetTrigger("Jump");
            _rb.velocity = new Vector3(_rb.velocity.x, JumpForce, _rb.velocity.z);
        }
    }

    private void Animate()
    {
        _animator.SetFloat("Horizontal", _horizontalInput);
        _animator.SetFloat("Vertical", _verticalInput);

        _animator.SetBool("IsGrounded", IsGrounded());
        _animator.SetFloat("FallSpeed", _rb.velocity.y);
    }
}
