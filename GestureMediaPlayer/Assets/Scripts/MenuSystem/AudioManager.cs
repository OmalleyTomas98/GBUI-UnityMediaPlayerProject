using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{

    public AudioClip[] musicClips;
    private int currentTrack;
    private AudioSource source;



    // Start is called before the first frame update
    void Start()
    {
            source = GetComponent<AudioSource>();

            // Begin to Play Music
            PlayMusic();
    }


    public void PlayMusic()
    {
        if(source.isPlaying){

            return;
        }

        currentTrack--;
        if(currentTrack<0){
            currentTrack = musicClips.Length -1;
        }

        StartCoroutine("WaitForMusicEnd");

    }
    IEnumerator WaitForMusicEnd()
    {
        while(source.isPlaying)
        {
            yield return null;

        }
        NextTitle();

    }

    public void NextTitle()
    {
        source.Stop();
        currentTrack++;
        if (currentTrack > musicClips.Length-1)
        {
            currentTrack =0;

        }
        source.clip = musicClips[currentTrack];
        source.Play();


        // Show title
        StartCoroutine("WaitForMusicEnd");
    }




    public void PreviousTitle()
    {
        source.Stop();
        currentTrack--;
        if(currentTrack <0)
        {
            currentTrack = musicClips.Length -1;
        }
        source.clip= musicClips[currentTrack];
        StartCoroutine("WaitForMusicEnd");
    }

    public void StopMusic()
    {
        StopCoroutine("WaitForMusicEnd");
        source.Stop();
    }

    public void MuteMusic()
    {
        source.mute = !source.mute;

    }
}
