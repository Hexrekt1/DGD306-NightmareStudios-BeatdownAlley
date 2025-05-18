using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{

    public AudioClip levelSong, bossSong, levelClearSong;

    private AudioSource audioS;


    // Start is called before the first frame update
    void Start()
    {
        audioS = GetComponent<AudioSource>();
        PlaySong(levelSong);
    }

    
    public void PlaySong(AudioClip clip)
    {
        audioS.clip = clip; 
        audioS.Play();
    }
}
