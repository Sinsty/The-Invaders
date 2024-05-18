using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float Health { get; private set; }


    [SerializeField] private float _maxHealth = 100;
    [SerializeField] private Image _hpBar;
    [Header("Damage Effects")]
    [SerializeField] private AudioClip _damageTakeAudio;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private Animator _damageTakeAnimator;
    [Header("UI")]
    [SerializeField] private GameObject gameplayUI;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject GamePause;

    private float _hpBarFillAmmount;

    private void Awake()
    {
        Health = _maxHealth;
        UpdateHealthBar();
    }

    private void Update()
    {
        _hpBar.fillAmount = Mathf.Lerp(_hpBar.fillAmount, _hpBarFillAmmount, Time.deltaTime * 5);
    }

    public void DealDamage(float damage)
    {
        damage = Mathf.Abs(damage);
        Health -= damage;

        _audioSource.pitch = Random.Range(0.8f, 1.2f);
        _audioSource.PlayOneShot(_damageTakeAudio);
        _damageTakeAnimator.SetTrigger("DamageTake");

        UpdateHealthBar();

        if (Health <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        gameplayUI.SetActive(false);
        GamePause.SetActive(false);
        gameOverScreen.SetActive(true);

        GetComponent<PlayerController>().enabled = false;
        GetComponent<CameraRotation>().enabled = false;
        GetComponent<WeaponSwitch>().enabled = false;
        GetComponent<Aiming>().enabled = false;
        GetComponent<Gun>().enabled = false;
    }

    private void UpdateHealthBar()
    {
        _hpBarFillAmmount = Health / _maxHealth;
    }
}