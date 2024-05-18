using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float _health;
    public float Health
    {
        get { return _health; }
        private set { _health = SetHealth(value); }
    }



    [SerializeField] private float _maxHealth = 100;
    [SerializeField] private float _timeToStartHealing = 10;
    [SerializeField] private float _healingSpeed;
    [Header("Damage Effects")]
    [SerializeField] private AudioClip _damageTakeAudio;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private Animator _damageTakeAnimator;
    [Header("UI")]
    [SerializeField] private Image _hpBar;
    [SerializeField] private GameObject gameplayUI;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject GamePause;

    private float _timerToHealing = 10;

    private float _hpBarFillAmmount;

    private void Awake()
    {
        Health = _maxHealth;
        UpdateHealthBar();
    }

    private void Update()
    {
        if (Time.time > _timerToHealing)
        {
            Heal(_healingSpeed * Time.deltaTime);
        }

        _hpBar.fillAmount = Mathf.Lerp(_hpBar.fillAmount, _hpBarFillAmmount, Time.deltaTime * 5);
    }

    public void Heal(float health)
    {
        health = Mathf.Abs(health);
        Health += health;

        UpdateHealthBar();
    }

    public void DealDamage(float damage)
    {
        damage = Mathf.Abs(damage);
        Health -= damage;

        _timerToHealing = Time.time + _timeToStartHealing;

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

    private float SetHealth(float value)
    {
        value = Mathf.Clamp(value, 0, _maxHealth);
        return value;
    }
}