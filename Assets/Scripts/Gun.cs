using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]

public class Gun : MonoBehaviour
{
    public static UnityEvent<Vector3> OnHit = new UnityEvent<Vector3>();

    public float Damage;
    public float MaxDistance;
    public float FireRate;

    [SerializeField] private Camera _camera;
    [Header("Visuals")]
    [SerializeField] string _aimAnimationTag = "Aim";
    [SerializeField] private ParticleSystem _muzzleFlashEffect;
    [SerializeField] private ParticleSystem _impactEffect;
    [SerializeField] private Animator _animator;
    [Header("Sounds")]
    [SerializeField] private AudioClip _shotAudio;
    [Header("OverHeat")]
    [SerializeField] private Image _overheatBar;
    [SerializeField] private GameObject _overheatWarningText;
    [SerializeField] private float _overheatIncreaseSpeed = 0.07f;
    [SerializeField] private float _overheatDecreaseSpeed = 0.7f;
    [SerializeField] private float _coolingDownSpeed = 0.3f;
    [SerializeField] private AudioClip _coolingDownSound;

    private bool _isCoolingDown = false;

    private float _overheatLevel;

    private float _disableTime;

    public float OverheatLevel
    {
        get { return _overheatLevel; }
        set { _overheatLevel = Mathf.Clamp(value, 0, 1); }
    }

    private float _nextTimeToFire;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        UpdateOverheatOnEnable();
    }

    private void Update()
    {
        ShootUpdate();
        UpdateOverheatBar();
    }

    private void ShootUpdate()
    {
        if (OverheatLevel >= 1)
        {
            _isCoolingDown = true;
            _overheatWarningText.SetActive(true);
            _audioSource.PlayOneShot(_coolingDownSound);
        }
        else if (OverheatLevel <= 0)
        {
            _isCoolingDown = false;
            _overheatWarningText.SetActive(false);
        }

        if (_isCoolingDown == false)
        {
            if (Input.GetKey(KeyCode.Mouse0) && OverheatLevel < 1 && Time.time >= _nextTimeToFire && _animator.GetCurrentAnimatorStateInfo(1).IsTag(_aimAnimationTag))
            {
                _nextTimeToFire = Time.time + 1f / FireRate;
                Shoot();
            }
            else if (Input.GetKey(KeyCode.Mouse0) == false)
            {
                OverheatLevel -= _overheatDecreaseSpeed * Time.deltaTime;
            }
        }
        else
        {
            OverheatLevel -= _coolingDownSpeed * Time.deltaTime;
        }
    }

    private void Shoot()
    {
        _muzzleFlashEffect.Play();
        _audioSource.PlayOneShot(_shotAudio);
        OverheatLevel += _overheatIncreaseSpeed;

        RaycastHit hit;
        Ray ray = _camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        if (Physics.Raycast(ray, out hit, MaxDistance))
        {
            Hitting(hit);
            OnHit.Invoke(hit.point);
        }
    }

    private void Hitting(RaycastHit hit)
    {
        Instantiate(_impactEffect, hit.point, Quaternion.LookRotation(hit.normal));

        EnemyHealth enemyHealth = hit.collider.gameObject.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            enemyHealth.DealDamage(Damage);
        }
    }

    private void UpdateOverheatBar()
    {
        _overheatBar.fillAmount = Mathf.Lerp(_overheatBar.fillAmount, OverheatLevel, Time.deltaTime * 5);
    }

    private void UpdateOverheatOnEnable()
    {
        if (_isCoolingDown)
        {
            _overheatWarningText.SetActive(true);
        }
        else
        {
            _overheatWarningText.SetActive(false);
        }

        float decreaseValue = 0;
        if (_isCoolingDown)
            decreaseValue = (Time.time - _disableTime) * _coolingDownSpeed;
        else
            decreaseValue = (Time.time - _disableTime) * _overheatDecreaseSpeed;

        OverheatLevel -= decreaseValue;
        _overheatBar.fillAmount = OverheatLevel;
    }

    private void OnDisable()
    {
        _disableTime = Time.time;
    }
}