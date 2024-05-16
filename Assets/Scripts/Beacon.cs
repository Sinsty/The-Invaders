using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

public class Beacon : MonoBehaviour
{
    public static UnityEvent OnUsed = new UnityEvent();

    [SerializeField] private float _turningOffSpeed = 1;
    [SerializeField] private GameObject _light;
    [SerializeField] private BeaconRotation _rotatingPanels;
    [Header("Sounds")]
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _turningOffSound;

    public bool IsUsed { get; private set; } = false;

    private void Update()
    {
        if (IsUsed)
        {
            TurningOffProcessUpdate();
        }
    }

    private void TurningOffProcessUpdate()
    {
        if (_light.transform.localScale.magnitude >= 0.05 || _rotatingPanels.RotationSpeed >= 0.05f || _audioSource.volume >= 0.001f)
        {
            _light.transform.localScale = Vector3.Lerp(_light.transform.localScale, Vector3.up * 0.0001f, _turningOffSpeed * Time.deltaTime);
            _rotatingPanels.RotationSpeed = Mathf.Lerp(_rotatingPanels.RotationSpeed, 0.01f, _turningOffSpeed * Time.deltaTime);
            _audioSource.volume = Mathf.Lerp(_rotatingPanels.RotationSpeed, 0.01f, _turningOffSpeed * Time.deltaTime);
        }
        else if (_light.activeInHierarchy)
        {
            _light.SetActive(false);
        }
        else if (_rotatingPanels.RotationSpeed != 0)
        {
            _rotatingPanels.RotationSpeed = 0;
        }
        else if (_audioSource.volume != 0)
        {
            _audioSource.volume = 0;
        }
    }

    public void Use()
    {
        if (IsUsed == false)
        {
            IsUsed = true;
            OnUsed.Invoke();
            _audioSource.PlayOneShot(_turningOffSound);
        }
    }
}
