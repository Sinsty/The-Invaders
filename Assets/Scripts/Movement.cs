using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour
{
    [Header("Settings")]
    public float Speed;
    public float JumpForce;
    [Header("Ground Check")]
    [SerializeField] private LayerMask _groundMAsk;
    [SerializeField] private float _distanceToCheckGround;
    [SerializeField] private float _playerHeight;

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
    }

    private void FixedUpdate()
    {
        Move();
    }

    private bool IsGorunded()
    {
        Vector3 position = new Vector3(transform.position.x, transform.position.y - (_playerHeight / 2), transform.position.z);
        bool result = Physics.CheckSphere(position, _distanceToCheckGround, _groundMAsk);
        return result;
    }

    private void Inputs()
    {
        _verticalInput = Input.GetAxis("Vertical");
        _horizontalInput = Input.GetAxis("Horizontal");
    }

    private void Move()
    {
        Vector3 moveDirection = transform.forward * _verticalInput + transform.right * _horizontalInput;
        Vector3 velocity = new Vector3(moveDirection.x * Speed, _rb.velocity.y, moveDirection.z * Speed);

        _rb.velocity = velocity;
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsGorunded())
        {
            Vector3 velcity = new Vector3(_rb.velocity.x, JumpForce, _rb.velocity.z);
            _rb.velocity = velcity;
        }
    }
}
