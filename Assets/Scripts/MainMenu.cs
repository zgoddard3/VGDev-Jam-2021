using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject optionsMenu;
    public AudioMixer mixer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            CloseOptions();
        }
    }

    public void StartGame() {
        SceneManager.LoadScene("Level 1");
    }

    public void OpenOptions() {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void CloseOptions() {
        mainMenu.SetActive(true);
        optionsMenu.SetActive(false);
    }

    public void SetMusicVolume(float value) {
        value = (value - 1f) * 15;
        mixer.SetFloat("MusicVolume", value);
    }

    public void SetSFXVolume(float value) {
        value = (value - 1f) * 15;
        mixer.SetFloat("SFXVolume", value);
    }

    public void Exit() {
        Application.Quit();
    }
}
