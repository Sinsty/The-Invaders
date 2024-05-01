using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class Gun : MonoBehaviour
{
    public float Damage;
    public float MaxDistance;
    public float FireRate;

    [SerializeField] string _aimAnimationTag = "Aim";
    [SerializeField] private Camera _camera;
    [SerializeField] private ParticleSystem _muzzleFlashEffect;
    [SerializeField] private ParticleSystem _impactEffect;
    [SerializeField] private AudioClip _shotAudio;
    [SerializeField] private Animator _animator;

    private float _nextTimeToFire;
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) && Time.time >= _nextTimeToFire && _animator.GetCurrentAnimatorStateInfo(1).IsTag(_aimAnimationTag))
        {
            _nextTimeToFire = Time.time + 1f / FireRate;
            Shoot();
        }

    }

    private void Shoot()
    {
        _muzzleFlashEffect.Play();
        _audioSource.PlayOneShot(_shotAudio);

        RaycastHit hit;
        Ray ray = _camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        if (Physics.Raycast(ray, out hit, MaxDistance))
        {
            OnHit(hit);
        }
    }

    private void OnHit(RaycastHit hit)
    {
        Instantiate(_impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
        Debug.Log("Hitted object in: " + hit.point);

        EnemyHealth enemyHealth = hit.collider.gameObject.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            enemyHealth.DealDamage(Damage);
        }
    }
}