using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] public AudioMixer audioMixer;

    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    [SerializeField] private TextMeshProUGUI masterText;
    [SerializeField] private TextMeshProUGUI musicText;
    [SerializeField] private TextMeshProUGUI sfxText;

    private void Start()
    {
        float masterVolume = PlayerPrefs.GetFloat("MasterVolume", 1);
        float musicVolume = PlayerPrefs.GetFloat("MusicVolume", 1);
        float sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 1);

        SetMasterVolume(masterVolume);
        SetMusicVolume(musicVolume);
        SetSFXVolume(sfxVolume);

        if (masterSlider) masterSlider.value = masterVolume;
        if (musicSlider) musicSlider.value = musicVolume;
        if (sfxSlider) sfxSlider.value = sfxVolume;

        UpdateText();
    }

    private void Awake()
    {
        if (instance == null) instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public void SetMasterVolume(float masterVolume)
    {
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(masterVolume) * 20);
        PlayerPrefs.SetFloat("MasterVolume", masterVolume);
        PlayerPrefs.Save();
        UpdateText();
    }

    public void SetMusicVolume(float musicVolume)
    {
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(musicVolume) * 20);
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
        PlayerPrefs.Save();
        UpdateText();
    }

    public void SetSFXVolume(float sfxVolume)
    {
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(sfxVolume) * 20);
        PlayerPrefs.SetFloat("SFXVolume", sfxVolume);
        PlayerPrefs.Save();
        UpdateText();
    }

    private void UpdateText()
    {
        if (masterText) masterText.text = "" + Mathf.Round(masterSlider.value * 100);
        if (musicText) musicText.text = "" + Mathf.Round(musicSlider.value * 100);
        if (sfxText) sfxText.text = "" + Mathf.Round(sfxSlider.value * 100);
    }
}
