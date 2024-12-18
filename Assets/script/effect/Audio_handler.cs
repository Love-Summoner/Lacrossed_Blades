using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Audio_handler : MonoBehaviour
{
    public Slider volume_slider;
    private float volume = 1.0f;

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Update is called once per frame
    void Update()
    {
        if (volume_slider == null && GameObject.Find("Volume") != null)
        {
            volume_slider = GameObject.Find("Volume").GetComponent<Slider>();
            volume = volume_slider.value;
            set_volume();
        } 
        else if (volume_slider != null && volume_slider.value != volume)
        {
            volume = volume_slider.value;
            set_volume();
        }
    }
    void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, LoadSceneMode mode)
    {
        if (GameObject.FindGameObjectsWithTag("audio_manager").Length > 1) { 
            Destroy(gameObject);
        }
        set_volume();
    }
    private void set_volume()
    {
        GameObject[] sound_abjects = GameObject.FindGameObjectsWithTag("audio_source");
        foreach (GameObject sound in sound_abjects)
        {
            sound.GetComponent<AudioSource>().volume = volume;
        }
    }
}
