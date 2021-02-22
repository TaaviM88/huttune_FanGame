using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayRandomSound : MonoBehaviour
{
    public List<AudioClip> soundClips = new List<AudioClip>();
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RandomSound()
    {
        if (!audioSource.isPlaying)
        {
            int rndIndex = UnityEngine.Random.Range(0, soundClips.Count - 1);

            audioSource.PlayOneShot(soundClips[rndIndex]);
        }

    }

    public bool IsPlayingSound()
    {
        return audioSource.isPlaying;
    }
}
