using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundsManager : MonoBehaviour
{
    public static SoundsManager instance { get; private set; }
    private AudioSource source;   


    private void Awake()
    {
        instance = this;
        source = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip soundParam)
    {
        source.PlayOneShot(soundParam);
    }
   

  


}
