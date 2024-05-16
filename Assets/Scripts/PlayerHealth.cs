using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float Health { get; private set; }

    [SerializeField] private float _maxHealth = 100;
    [SerializeField] private Image _hpBar;

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

        UpdateHealthBar();

        if (Health <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        Destroy(gameObject);
    }

    private void UpdateHealthBar()
    {
        _hpBarFillAmmount = Health / _maxHealth;
    }
}