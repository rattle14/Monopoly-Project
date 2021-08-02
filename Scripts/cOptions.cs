using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class cOptions : MonoBehaviour
{
    public AudioMixer audioPlayer;
    private GameManager gm;

    public Slider musicSlider;
    public Slider soundSlider;
    public Toggle animationToggle;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.gb;
        InitUI();
    }
    public void InitUI()
    {
        musicSlider.value = gm.musicValue;
        soundSlider.value = gm.soundValue;
      //  animationToggle.isOn = gm.isPlayAnimation;
    }

    public void OnBackButtonClicked()
    {
        Destroy(gameObject);
    }
    public void OnMusicSlider(float _value)
    {
        audioPlayer.SetFloat("MusicVol", Mathf.Log(_value)*20);
        gm.musicValue = musicSlider.value;
    }
    public void OnSoundSlider(float _value)
    {
        audioPlayer.SetFloat("SoundVol", Mathf.Log(_value) * 20);
        gm.soundValue = soundSlider.value;
    }
    public void OnTestButton()
    {
        gm.testButton.Play();
    }
    public void OnAniToggle()
    {
        gm.isPlayAnimation = animationToggle.isOn;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
