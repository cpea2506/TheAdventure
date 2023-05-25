using UnityEngine;

public enum SFX
{
    CoinPickUp,
    TakeDamage,
    HeartPickup,
    Jump,
    Death,
    CrateCollide,
    Pipe,
    EnemyDeath,
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    private AudioSource audioSource;

    [SerializeField]
    private AudioClip[] sfxSounds;

    private void Awake()
    {
        instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(SFX soundFx, float volumeScale = 1f)
    {
        audioSource.clip = sfxSounds[(int)soundFx];
        audioSource.PlayOneShot(audioSource.clip, volumeScale);
    }
}
