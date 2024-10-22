using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager Instance;

    public enum SFX
    {
        PLAYER_HIT,
        ENEMY_HIT,
        XP,
        PLAYER_SHOOT,
        ENEMY_SHOOT
    };

    // Start is called before the first frame update
    [SerializeField] public AudioSource audioSource;

    [SerializeField] private AudioClip[] playerHit;
    [SerializeField] private AudioClip[] enemyHit;
    [SerializeField] private AudioClip[] xp;
    [SerializeField] private AudioClip[] playerShoot;
    [SerializeField] private AudioClip[] enemyShoot;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();

        if (Instance != null && Instance != this) {
            Destroy(this);
        } else {
            Instance = this;
        }

        DontDestroyOnLoad(this);
    }

    public void PlayRandomSound(SFX sfx)
    {
        AudioClip[] clips = sfx switch
        {
            SFX.PLAYER_HIT => playerHit,
            SFX.ENEMY_HIT => enemyHit,
            SFX.XP => xp,
            SFX.PLAYER_SHOOT => playerShoot,
            SFX.ENEMY_SHOOT => playerShoot,
            _ => throw new System.NotImplementedException()
        };
        audioSource.PlayOneShot(clips[Random.Range(0, clips.Length)]);
    }

    public void PlayAudioClip(AudioClip aClip)
    {
        audioSource.PlayOneShot(aClip);
	}
}
