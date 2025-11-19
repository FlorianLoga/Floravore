using UnityEngine;

public class AudioControllerScript : MonoBehaviour
{
    public static AudioControllerScript Instance;

    public AudioSource pause;
    public AudioSource resume;
    public AudioSource enemySlain;
    public AudioSource selectUpgrade;
    public AudioSource areaWeaponSpawn;
    public AudioSource gameOver;
    public AudioSource hitSound;
    public AudioSource wateringWeaponSpawn;
    public AudioSource projectileWeaponSpawn;
    private void Awake()
    {
        if(Instance !=null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;    
        }
    }
    public void PlaySound(AudioSource sound)
    {
        sound.Stop();
        sound.Play();
    }
    public void PlayModifiedSound(AudioSource sound)
    {
        sound.pitch=Random.Range(0.7f, 1.3f);
        sound.Stop();
        sound.Play();
    }
}
