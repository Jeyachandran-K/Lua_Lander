using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private AudioSource musicAudioSource;
    private static float musicTime;

    private void Awake()
    {
        musicAudioSource = GetComponent<AudioSource>();
        musicAudioSource.time = musicTime;
    }

    private void Update()
    {
        musicTime=musicAudioSource.time;
    }
}
