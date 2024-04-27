using UnityEngine;

public class ArmConsoleTakeEvent : MonoBehaviour
{
    [SerializeField] private AudioClip _takeSoundEffect;
    [SerializeField] private AudioSource _audioSource;

    private void TakeArmConsole()
    {
        _audioSource.PlayOneShot(_takeSoundEffect);
    }
}
