using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioBacgroundController : MonoBehaviour
{
    [SerializeField] private AudioClip[] musics;

    private AudioSource musicSource;

    public static AudioBacgroundController Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        musicSource = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        if (musicSource.isPlaying == false)
            PlayRandomMusic();
    }

    public void SetPauseMusic(bool pause)
    {
        if(pause) musicSource.Pause();
        else musicSource.UnPause();
    }

    private void PlayRandomMusic()
    {
        musicSource.PlayOneShot(musics[Random.Range(0, musics.Length)]);
    }


}
