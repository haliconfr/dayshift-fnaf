using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class SoundVolume : MonoBehaviour
{
    private Scene scene;
    public AudioMixer music;
    public AudioMixer sfx;
    private void Awake()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneLoaded += OnSceneLoaded;
        DontDestroyOnLoad(gameObject);
        scene = SceneManager.GetActiveScene();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        sfx.SetFloat("SoundEffectsVolume", PlayerPrefs.GetFloat("SoundEffectsVolume"));
        music.SetFloat("Music", PlayerPrefs.GetFloat("Music"));
        Debug.Log("sfx = " + PlayerPrefs.GetFloat("SoundEffectsVolume"));
        Debug.Log("music = " + PlayerPrefs.GetFloat("Music"));
    }
}