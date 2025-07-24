using UnityEngine;
using UnityEngine.UI;
public class BGM : MonoBehaviour
{
    public AudioSource backgroundMusicSource;
    public Slider volumeSlider;

    private void Start()
    {
        if (backgroundMusicSource != null && volumeSlider != null)
            volumeSlider.value = backgroundMusicSource.volume;
    }

    public void ChangeMusicVolume()
    {
        if (backgroundMusicSource != null && volumeSlider != null)
            backgroundMusicSource.volume = volumeSlider.value;
    }
}
