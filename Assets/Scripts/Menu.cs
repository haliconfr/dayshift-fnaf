using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class Menu : MonoBehaviour
{
    public AudioMixer music;
    public AudioMixer sfx;
    public float sfxVol, musicVol;
    public AudioSource select;
    public void StartGame()
    {
        select.Play();
        SceneManager.LoadScene("Loading");
    }
    public void Options()
    {
        
    }
    public void Quit()
    {
        Application.Quit(1);
    }
    public void initialiser()
    {
        select.Play();
        PlayerPrefs.SetFloat("SoundEffectsVolume", sfxVol);
        PlayerPrefs.SetFloat("Music", musicVol);
        SceneManager.LoadScene("Menu");
    }
    public void SetSfx(float AudioLevel){
        sfx.SetFloat("SoundEffectsVolume", AudioLevel);
        sfxVol = AudioLevel;
    }
    public void SetMusic(float AudioLevel){
        music.SetFloat("Music", AudioLevel);
        musicVol = AudioLevel;
    }
}