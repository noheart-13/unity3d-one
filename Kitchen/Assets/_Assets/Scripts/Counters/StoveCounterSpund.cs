using UnityEngine;

public class StoveCounterSpund : MonoBehaviour
{
    [SerializeField] private StoveCounter stoveCounter;

    private AudioSource audioSource;
    private SoundManager soundManager;
    private float baseVolume;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        baseVolume = audioSource.volume;
    }

    private void Start()
    {
        stoveCounter.OnStateChanged += StoveCounter_OnStateChanged;

        soundManager = SoundManager.Instance;
        if (soundManager != null)
        {
            soundManager.OnVolumeChanged += ApplyVolume;
            ApplyVolume(soundManager.GetVolume());
        }
    }

    private void OnDestroy()
    {
        if (stoveCounter != null)
        {
            stoveCounter.OnStateChanged -= StoveCounter_OnStateChanged;
        }
        if (soundManager != null)
        {
            soundManager.OnVolumeChanged -= ApplyVolume;
        }
    }

    private void ApplyVolume(float volume)
    {
        audioSource.volume = baseVolume * volume;
    }

    private void StoveCounter_OnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e)
    {
        bool playSound = e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried;
        if (playSound) 
        { 
            audioSource.Play();
        }
        else
        {
            audioSource.Stop();
        }
    }
}
