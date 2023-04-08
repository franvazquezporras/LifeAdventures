using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicController : MonoBehaviour
{

    private bool muteMusica = false;
    private bool muteSound = false;
    [SerializeField] GameObject musicOff;
    GameObject[] sounds;
    GameObject music;
    


    private void Awake()
    {
        checkPrefabs();
        Load();
        sounds = GameObject.FindGameObjectsWithTag("Sounds");
        music = GameObject.FindGameObjectWithTag("Music");
    }

    private void Start()
    {        
        checkStatus();
    }
    private void checkPrefabs()
    {
        if (!PlayerPrefs.HasKey("muted"))
            PlayerPrefs.SetInt("muted", 0);
        if (!PlayerPrefs.HasKey("mutedS"))
            PlayerPrefs.SetInt("mutedS", 0);
    }

    private void checkStatus()
    {

        if (gameObject.name == "BMusic")
        {
            ActiveMute(muteMusica);
            if (muteMusica)  
                music.GetComponent<AudioSource>().volume = 0;
            else   
                music.GetComponent<AudioSource>().volume = PlayerPrefs.GetInt("MusicVol");
        }            
        else if (gameObject.name == "BSound")
        {
            ActiveMute(muteSound);
            if (muteSound)   
                foreach (GameObject iSound in sounds)
                    iSound.GetComponent<AudioSource>().volume = 0;
            else
                foreach (GameObject iSound in sounds)
                    iSound.GetComponent<AudioSource>().volume = PlayerPrefs.GetInt("SoundVol");
        }
            
    }

    public void muteMusic()
    {
        if (!muteMusica)
        {
            muteMusica = true;
            music.GetComponent<AudioSource>().volume = 0;
        }
        else
        {
            
            muteMusica = false;
            music.GetComponent<AudioSource>().volume = (PlayerPrefs.GetInt("MusicVol")/10f) ;
        }
        ActiveMute(muteMusica);
        Save();
    }

    public void muteSounds()
    {
        if (!muteSound)
        {
            
            muteSound = true;
            foreach (GameObject iSound in sounds)
                iSound.GetComponent<AudioSource>().volume = 0;
            
        }
        else
        {
            
            muteSound = false;
            foreach (GameObject iSound in sounds)
                iSound.GetComponent<AudioSource>().volume = (PlayerPrefs.GetInt("SoundVol")/10f) ;
        }
        ActiveMute(muteSound);
        Save();
    }


    private void ActiveMute(bool mute)
    {
        if (musicOff != null)
        {
            musicOff.SetActive(mute);
        }
    }
    private void Load()
    {
        muteMusica = PlayerPrefs.GetInt("muted") == 1;
        muteSound = PlayerPrefs.GetInt("mutedS") == 1;
    }

    private void Save()
    {
        PlayerPrefs.SetInt("muted", muteMusica ? 1 : 0);
        PlayerPrefs.SetInt("mutedS", muteSound ? 1 : 0);
    }

  


}
