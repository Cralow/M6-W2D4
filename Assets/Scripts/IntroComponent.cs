using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class IntroComponent : MonoBehaviour
{
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private float delay = 2f;

    private bool audioStarted = false;

    void Update()
    {
        if (!audioStarted && videoPlayer.isPlaying && videoPlayer.time >= delay)
        {
            audioSource.Play();
            audioStarted = true;
        }
    }
}