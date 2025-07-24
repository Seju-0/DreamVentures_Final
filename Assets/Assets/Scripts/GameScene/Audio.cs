using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Audio : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;
    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
    }
}
