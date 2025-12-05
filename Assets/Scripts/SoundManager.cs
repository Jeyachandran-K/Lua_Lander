using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance {  get; private set; }

    private static int soundVolume = 6;
    private int soundVolumeMax = 10;

    [SerializeField] private AudioClip fuelPickUpAudioClip;
    [SerializeField] private AudioClip coinPickUpAudioClip;
    [SerializeField] private AudioClip successLandingAudioClip;
    [SerializeField] private AudioClip crashLandingAudioClip;

    private void Start()
    {
        Lander.Instance.OnCoinPickUp += Instance_OnCoinPickUp;
        Lander.Instance.OnFuelPickUp += Instance_OnFuelPickUp;
        Lander.Instance.OnLanding += Instance_OnLanding;
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Instance_OnLanding(object sender, Lander.OnLandingEventArgs e)
    {
        if (e.landingType == Lander.LandingType.Success)
        {
            AudioSource.PlayClipAtPoint(successLandingAudioClip,Camera.main.transform.position, GetSoundVolumeNormalize());
        }
        else
        {
            AudioSource.PlayClipAtPoint(crashLandingAudioClip,Camera.main.transform.position, GetSoundVolumeNormalize());
        }
    }

    private void Instance_OnFuelPickUp(object sender, System.EventArgs e)
    {
        AudioSource.PlayClipAtPoint(fuelPickUpAudioClip,Camera.main.transform.position,GetSoundVolumeNormalize());
    }

    private void Instance_OnCoinPickUp(object sender, System.EventArgs e)
    {
        AudioSource.PlayClipAtPoint(coinPickUpAudioClip, Camera.main.transform.position,GetSoundVolumeNormalize());
    }
    public  void ChangeSoundVolume()
    {
        
        soundVolume = (soundVolume + 1) % soundVolumeMax;
    }
    public int GetSoundVolume()
    {
        return soundVolume;
    }
    public float GetSoundVolumeNormalize()
    {
        return ((float)soundVolume) / soundVolumeMax;
    }
}
