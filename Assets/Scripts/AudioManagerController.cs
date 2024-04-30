using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class AudioManagerController : MonoBehaviour
{
    public static AudioManagerController Instance { get; private set; }
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private AudioSource musicAudioSource;
    [SerializeField] private AudioSource sfxAudioSource;
    [SerializeField] private AudioClip[] musicClips;
    [SerializeField] private AudioClip[] sfxClips;
    [SerializeField] private AudioSettings audioSettings;
    [SerializeField] private bool load_volume_settings;   
    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        AudioSource[] audioSources = GetComponentsInChildren<AudioSource>();
        musicAudioSource = audioSources[0];
        sfxAudioSource = audioSources[1];
    }
    /*
    private void Start()
    {
        if (load_volume_settings == true)
        {
            LoadAudioSettings();
        }
    }
    */
    public AudioSource MusicAudioSource
    {
        get
        {
            return musicAudioSource;
        }
    }
    public AudioSource SfxAudioSource
    {
        get
        {
            return sfxAudioSource;
        }
    }
    public AudioClip[] SfxClips
    {
        get
        {
            return sfxClips;
        }
    }
    
    public void SaveAudioSettings(GameObject Sliders)
    {
        Slider[] sliders = Sliders.GetComponentsInChildren<Slider>();       
        audioSettings.musicVolume = sliders[0].value;
        audioSettings.sfxVolume = sliders[2].value;
        audioSettings.masterVolume = sliders[1].value;
    }
/*
    public void LoadAudioSettings()
    {
        Instance.musicConfiguration.value = Instance.audioSettings.musicVolume;
        Instance.sfxConfiguration.value = Instance.audioSettings.sfxVolume;
        Instance.masterConfiguration.value = Instance.audioSettings.masterVolume;

        SetVolumeOfMusic();
        SetVolumeOfSfx();
        SetVolumeOfMaster();
    }
  */  
    public void SetVolumeOfMusic(Slider musicConfiguration)
    {
        audioMixer.SetFloat("music",Mathf.Log10(musicConfiguration.value) * 20f);
    }
    public void SetVolumeOfSfx(Slider sfxConfiguration)
    {
        audioMixer.SetFloat("sfx", Mathf.Log10(sfxConfiguration.value) * 20f);
    }
    public void SetVolumeOfMaster(Slider masterConfiguration)
    {
        audioMixer.SetFloat("master", Mathf.Log10(masterConfiguration.value) * 20f);
    }
    public void PlayMusic(int index)
    {
        Instance.musicAudioSource.Stop();
        Instance.musicAudioSource.clip = Instance.musicClips[index];
        Instance.musicAudioSource.Play();
    }
    public void PlaySfx(int index)
    {
        Instance.sfxAudioSource.PlayOneShot(Instance.sfxClips[index]);
    }
}
