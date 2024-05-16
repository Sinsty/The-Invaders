using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float Speed;
    public float JumpForce;
    [Header("Ground Check")]
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private float _checkBoxOffset = -0.7f;
    [SerializeField] private Vector3 _boxHalfSize = new Vector3(0.2f, 0.35f, 0.2f);
    [Header("Animations")]
    [SerializeField] private Animator _animator;
    [Header("Sounds")]
    [SerializeField] private AudioSource _audioSource;
    [Header("FootSteps")]
    [SerializeField] private float _stepsDelay = 0.25f;
    [SerializeField] private AudioClip[] _standartFootsteps;
    [SerializeField] private string _metalTag;
    [SerializeField] private AudioClip[] _metalFootsteps;

    private bool _isStepReload = true;
    private WaitForSeconds _cachedStepsDelay;

    private float _verticalInput;
    private float _horizontalInput;

    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _cachedStepsDelay = new WaitForSeconds(_stepsDelay);
    }

    private void Update()
    {
        Inputs();
        Jump();
        Animate();
        if (_isStepReload && IsGrounded()) StartCoroutine(FootstepsUpdate());
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
        Vector3 position = new Vector3(transform.position.x, transform.position.y + _checkBoxOffset, transform.position.z);
        bool result = Physics.CheckBox(position, _boxHalfSize, Quaternion.identity, _groundMask);
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Vector3 position = new Vector3(transform.position.x, transform.position.y + _checkBoxOffset, transform.position.z);
        Gizmos.DrawCube(position, _boxHalfSize * 2);
    }

    private void Animate()
    {
        _animator.SetFloat("Horizontal", _horizontalInput);
        _animator.SetFloat("Vertical", _verticalInput);

        _animator.SetBool("IsGrounded", IsGrounded());
        _animator.SetFloat("FallSpeed", _rb.velocity.y);
    }

    private IEnumerator FootstepsUpdate()
    {
        if (_horizontalInput != 0 || _verticalInput != 0)
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, Vector3.down, out hit, 2))
            {
                _audioSource.pitch = Random.Range(0.8f, 1.2f);

                if (hit.collider.tag == _metalTag)
                    _audioSource.PlayOneShot(_metalFootsteps[Random.Range(0, _metalFootsteps.Length)]);
                else
                    _audioSource.PlayOneShot(_standartFootsteps[Random.Range(0, _standartFootsteps.Length)]);
            }

            _isStepReload = false;

            yield return _cachedStepsDelay;

            _isStepReload = true;
        }
    }
}