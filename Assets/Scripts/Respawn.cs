using System.Net.NetworkInformation;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public static Vector3 PlayerRespawnPosition;

    [SerializeField] private PlayerHealth _player;
    [SerializeField] private Animator _characterAnimator;
    [SerializeField] private GameObject _deathScreen;
    [SerializeField] private GameObject _hud;
    public GameObject PauseGame2;

    private void Start()
    {
        PlayerRespawnPosition = _player.transform.position;
    }

    private void Update()
    {
        RespawnUpdate();
    }

    private void RespawnUpdate()
    {
        if (_deathScreen.activeInHierarchy)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                RespawnPlayer();
            }
        }
    }

    private void RespawnPlayer()
    {
        _player.enabled = true;
        _player.Heal(_player.MaxHealth);

        _player.GetComponent<PlayerController>().enabled = true;
        _player.GetComponent<CameraRotation>().enabled = true;
        _player.GetComponent<WeaponSwitch>().enabled = true;
        _player.GetComponent<Aiming>().enabled = true;

        _characterAnimator.SetBool("IsDead", false);

        _deathScreen.SetActive(false);
        _hud.SetActive(true);
        PauseGame2.SetActive(true);

        _player.transform.position = PlayerRespawnPosition;
    }

}
