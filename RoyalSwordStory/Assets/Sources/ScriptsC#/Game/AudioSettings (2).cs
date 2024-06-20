using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider audioEffectsVolumeSlider;
    [Space]
    [SerializeField] private string nameMusicVolumeExpose = "Music";
    [SerializeField] private string nameAudioEffectsVolumeExpose = "SFX";
    [SerializeField] private AudioMixer audioMixer;


    private void OnEnable()
    {
        DataPlayer.GetDataEvent += LoadSettings;
    }

    private void OnDisable()
    {
        DataPlayer.GetDataEvent -= LoadSettings;
    }


    private void Start()
    {
        if(DataPlayer.SDKEnabled)
        LoadSettings();
    }

    public void SetVolumeMusic(float value)
    {
        DataPlayer.GetData().MusicVolume = value;
        audioMixer.SetFloat(nameMusicVolumeExpose, value);
        Save();
    }

    public void SetVolumeAudioEffects(float value)
    {
        DataPlayer.GetData().AudioEffectsVolume = value;
        audioMixer.SetFloat(nameAudioEffectsVolumeExpose, value);
        Save();
    }

    private void Save()
    {
        DataPlayer.SaveData();
    }

    private void LoadSettings()
    {
        var data = DataPlayer.GetData();

        musicVolumeSlider.value = data.MusicVolume;
        audioEffectsVolumeSlider.value = data.AudioEffectsVolume;  

        audioMixer.SetFloat(nameMusicVolumeExpose, data.MusicVolume);
        audioMixer.SetFloat(nameAudioEffectsVolumeExpose, data.AudioEffectsVolume);
    }
}
