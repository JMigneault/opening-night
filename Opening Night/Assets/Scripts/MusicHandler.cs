using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicHandler : MonoBehaviour
{
    private AudioSource Audio;
    [SerializeField]
    private AudioClip gameplayMusic;
    [SerializeField]
    private AudioClip titleMusic;

    private bool gameplayPlaying = false;

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        Audio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if(SceneManager.GetActiveScene().buildIndex == 2 && !gameplayPlaying)
        {
            gameplayPlaying = true;
            Audio.Stop();
            Audio.clip = gameplayMusic;
            Audio.Play();
        }
        else if(SceneManager.GetActiveScene().buildIndex != 2 && gameplayPlaying)
        {
            gameplayPlaying = false;
            Audio.Stop();
            Audio.clip = titleMusic;
            Audio.Play();
        }

    }
}
