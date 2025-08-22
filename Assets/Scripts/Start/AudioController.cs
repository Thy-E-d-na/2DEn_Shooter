using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    private const string VOLUME_BGM = "VolumeBGM";
    private const string VOLUME_SFX = "VolumeSFX";

    [Header("BGM")]
    [SerializeField] AudioClip backgroundMusic;
    [SerializeField] AudioClip gameoverMusic;
    [SerializeField] GameObject btnOffBGM, btnOnBGM;

    [Header("SFX")]
    [SerializeField] AudioClip buttonClip;
    [SerializeField] AudioClip fireClip, deathClip, lvUpClip;
    [SerializeField] AudioClip ChickenLayClip, EggBreakClip, EatingClip;
    [SerializeField] List<AudioClip> ChickenHurtClips;
    [SerializeField] List<AudioClip> ChickenDeathClips;
    [SerializeField] GameObject btnOffSFX, btnOnSFX;
    private AudioSource bgm, sfx;

    public static AudioController Instance;
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        bgm = transform.GetChild(0).GetComponent<AudioSource>();
        sfx = transform.GetChild(1).GetComponent<AudioSource>();

        LoadSetting();
        PlayBackgroundMusic();
    }
    public void LoadSetting()
    {
        bgm.volume = PlayerPrefs.GetInt(VOLUME_BGM, 1);
        btnOffBGM.SetActive(bgm.volume == 0);
        btnOnBGM.SetActive(bgm.volume == 1);

        sfx.volume = PlayerPrefs.GetInt(VOLUME_SFX, 1);
        btnOffSFX.SetActive(sfx.volume == 0);
        btnOnSFX.SetActive(sfx.volume == 1);
    }
    public void SetBGM(int volume)
    {
        bgm.volume = volume;
        PlayerPrefs.SetInt(VOLUME_BGM, volume);
        btnOffBGM.SetActive(volume == 0);
        btnOnBGM.SetActive(volume == 1);
    }
    public void SetSFX(int volume)
    {
        sfx.volume = volume;
        PlayerPrefs.SetInt(VOLUME_SFX, volume);
        btnOffSFX.SetActive(volume == 0);
        btnOnSFX.SetActive(volume == 1);
    }

    #region BGM
    public void PlayBackgroundMusic()
    {
        bgm.clip = backgroundMusic;
        bgm.loop = true;
        bgm.Play();
    }
    public void PlayGameOverMusic()
    {
        bgm.clip = gameoverMusic;
        bgm.loop = false;
        bgm.Play();
    }
    #endregion

    #region SFX
    public void PlayButtonClip() => sfx.PlayOneShot(buttonClip);
    public void PlayFireClip() => sfx.PlayOneShot(fireClip);
    public void PlayDeathClip() => sfx.PlayOneShot(deathClip);
    public void PlayLvUpClip() => sfx.PlayOneShot(lvUpClip);
    public void PlayChickenLayClip() => sfx.PlayOneShot(ChickenLayClip);
    public void PlayEggBreakClip() => sfx.PlayOneShot(EggBreakClip);
    public void PlayEatingClip() => sfx.PlayOneShot(EatingClip);
    public void PlayChickenDeathClip() => sfx.PlayOneShot(ChickenDeathClips[Random.Range(0, ChickenDeathClips.Count)]);
    public void PlayChickenHurtClip() => sfx.PlayOneShot(ChickenDeathClips[Random.Range(0, ChickenHurtClips.Count)]);
    #endregion
}
