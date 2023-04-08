using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MenuController : MonoBehaviour
{
    [Header("Opciones Generale")]
    [SerializeField] GameObject screenMenu;
    [SerializeField] GameObject screenOption;
    private float timeChangeOption = 0.5f;
    private int screen;    
    private int volMusic;
    private int volSound;

    [Header("Elementos de opciones")]
    [SerializeField] Sprite musicON;
    [SerializeField] Sprite musicOFF;
    [SerializeField] Sprite soundsON;
    [SerializeField] Sprite soundsOFF;    
    [SerializeField] ButtonTextureScriptable volTexturesON;
    [SerializeField] ButtonTextureScriptable volTexturesOFF;
    [SerializeField] Button back;
    [SerializeField] SpriteRenderer[] musicSpr;
    [SerializeField] SpriteRenderer[] soundSpr;
    [SerializeField] SpriteRenderer music;
    [SerializeField] SpriteRenderer sound;
    Sprite spriteActiveON;
    Sprite spriteActiveOFF;

    [Header("Elementos de menu")]
    [SerializeField] Transform[] Buttons;
    [SerializeField] GameObject selectorRight;
    [SerializeField] GameObject selectorLeft;

    [Header("Sonidos de menu")]    
    [SerializeField] private AudioClip soundOption;
    [SerializeField] private AudioClip soundSelect;

    private int actualCharacter;
    private int optionMenu;
    private int optionOptions, lastOptionOptions;
    private bool clicked;
    private float vertical, horizontal;
    private float timeVertical,timeHorizontal;
    private float volButton = 0;

    SceneController scene = new SceneController();

    private void Awake()
    {
       volMusic = PlayerPrefs.GetInt("MusicVol");       
       volSound = PlayerPrefs.GetInt("SoundVol");
      
    }

    private void Start()
    {   
        screen = 0;
        timeVertical = 0;
        timeHorizontal = 0;
        optionMenu =  1;        
        actualCharacter = PlayerPrefs.GetInt("tipoPersonaje");
        SpritesPlayer();
        SetupOptions();
    }
  
    //Metodo para controlar las texturas de los botones segun el player seleccionado
    private void SpritesPlayer()
    {
        if (actualCharacter == 0)
        {
            spriteActiveON = volTexturesON.textures[0];
            spriteActiveOFF = volTexturesOFF.textures[0];
        }else if(actualCharacter == 1)
        {
            spriteActiveON = volTexturesON.textures[1];
            spriteActiveOFF = volTexturesOFF.textures[1];
        }else if(actualCharacter== 2)
        {
            spriteActiveON = volTexturesON.textures[2];
            spriteActiveOFF = volTexturesOFF.textures[2];
        }            
    }

    
    private void SetupOptions()
    {
        SetupMusicGame();
        SetupSoundGame();
    }

    //activa el menu principal y desactiva el menu de opciones
    public void MainMenuScreen()
    {
        clicked = true;
        SoundsManager.instance.PlaySound(soundSelect);
        screen = 0;
        screenOption.SetActive(false);
        screenMenu.SetActive(true);
    }

    //activa el menu de opciones y desactiva el menu principal
    public void OptionMenuScreen()
    {        
        clicked = true;
        SoundsManager.instance.PlaySound(soundSelect); 
        screenMenu.SetActive(false);
        screen = 1;
        optionOptions = lastOptionOptions = 1;
        music.sprite = musicON;
        sound.sprite = soundsOFF;
        screenOption.SetActive(true);
    }


    private void Update()
    {       
            vertical = Input.GetAxisRaw("Vertical");
            horizontal = Input.GetAxisRaw("Horizontal");
            if (Input.GetButtonUp("Submit")) clicked = false;
            if (vertical == 0) timeVertical = 0;
            if (screen == 0) MainMenu();
            if (screen == 1) OptionMenu();   
    }
  
    
    //control de desplazamiento por teclado de las opciones del menu principal
    private void MainMenu()
    {       
        if(vertical != 0)
        {
            if(timeVertical == 0 || timeVertical > timeChangeOption)
            {
                if (vertical == 1 && optionMenu > 1) SelectMenu(optionMenu - 1);
                if (vertical == -1 && optionMenu < 5) SelectMenu(optionMenu + 1);
                if (timeVertical > timeChangeOption) timeVertical = 0;
            }
            timeVertical += Time.deltaTime;
        }        
        doAction();
    }

    //control de desplazamiento por teclado de las opciones del menu de opciones
    private void OptionMenu()
    {
        if (vertical != 0)
        {
            if (timeVertical == 0 || timeVertical > timeChangeOption)
            {
                if (vertical == 1 && optionOptions > 1) SelectOption(optionOptions - 1);
                if (vertical == -1 && optionOptions < 3) SelectOption(optionOptions + 1);
                if (timeVertical > timeChangeOption) timeVertical = 0;
            }
            timeVertical += Time.deltaTime;
        }
        if (horizontal == 0) timeHorizontal = 0;
        else 
        {
            if ((timeHorizontal == 0 || timeHorizontal > timeChangeOption) && (optionOptions == 1 || optionOptions == 2))
            {
                if(optionOptions == 1 && ((horizontal<0 && volMusic >0) || (horizontal >0 && volMusic < 10))){
                    volMusic += (int)horizontal;
                    SetupMusicGame(); 
                }
                if (optionOptions == 2 && ((horizontal < 0 && volSound > 0) || (horizontal > 0 && volSound < 10)))
                {
                    volSound += (int)horizontal;
                    SetupSoundGame();
                    SoundsManager.instance.PlaySound(soundOption);
                }
                if (timeHorizontal > timeChangeOption) timeHorizontal = 0;
            }
            timeHorizontal += Time.deltaTime;
        }        
        if (Input.GetButtonDown("Submit") && optionOptions == 3 && !clicked) MainMenuScreen();
       
    }
  
    //Control de volumen de la musica
    public void SetVolMusicButton(int valueVol)
    {
        volButton = valueVol;
        if(optionOptions != 1)
        {
            optionOptions = 1;
            SelectOption(optionOptions);
        }        
        if (volButton == 0) timeHorizontal = 0;
        else
        {
            if ((timeHorizontal == 0 || timeHorizontal > timeChangeOption) && (optionOptions == 1))
            {
                if (optionOptions == 1 && ((volButton < 0 && volMusic > 0) || (volButton > 0 && volMusic < 10)))
                {
                    volMusic += (int)volButton;
                    SetupMusicGame();
                    PlayerPrefs.SetInt("MusicVol", volMusic);
                }                
                if (timeHorizontal > timeChangeOption) timeHorizontal = 0;
            }
            timeHorizontal += Time.deltaTime;
        }
        volButton = 0;
    }
    //Control de volumen de los sonidos
    public void SetVolSoundButton(int valueVol)
    {
        volButton = valueVol;
        if (optionOptions != 2)
        {
            optionOptions = 2;
            SelectOption(optionOptions);
        }
        if (volButton == 0) timeHorizontal = 0;
        else
        {
            if ((timeHorizontal == 0 || timeHorizontal > timeChangeOption) && (optionOptions == 2))
            {               
                if (optionOptions == 2 && ((volButton < 0 && volSound > 0) || (volButton > 0 && volSound < 10)))
                {
                    volSound += (int)volButton;
                    SetupSoundGame();
                    SoundsManager.instance.PlaySound(soundOption);
                    PlayerPrefs.SetInt("SoundVol", volSound);
                }
                if (timeHorizontal > timeChangeOption) timeHorizontal = 0;
            }
            timeHorizontal += Time.deltaTime;
        }
        volButton = 0;
    }

    //actualiza los sprites del volumen de la musica
    private void SetupMusicGame()
    {
        if (volMusic == 0)
            musicSpr[0].enabled = true;
        else
            musicSpr[0].enabled = false;

        for (int i = 1; i <= 10; i++)
        {
            if (i <= volMusic) musicSpr[i].sprite = spriteActiveON;
            else musicSpr[i].sprite = spriteActiveOFF;
        }
        
    }

    //actualiza los sprites del volumen de los sonidos
    private void SetupSoundGame()
    {
        if (volSound == 0) soundSpr[0].enabled = true;
        else soundSpr[0].enabled = false;

        for (int i = 1; i <= 10; i++)
        {
            if (i <= volSound) soundSpr[i].sprite = spriteActiveON;
            else soundSpr[i].sprite = spriteActiveOFF;
        }
        GameObject[] sounds = GameObject.FindGameObjectsWithTag("Sounds");
        foreach (GameObject iSound in sounds)
            iSound.GetComponent<AudioSource>().volume = (volSound / 10f);
    }


    
    private void doAction()
    {
        if (Input.GetButtonDown("Submit") && !clicked)
        {
            SoundsManager.instance.PlaySound(soundSelect);
            switch (optionMenu)
            {
                case 1: scene.ChangeScreen("Nivel1"); break;
                case 2: scene.ChangeScreen("CharacterSelector"); break;                
                case 3: scene.ChangeScreen("Ranking"); break;
                case 4: scene.ChangeScreen("Credits"); break;
                case 5: Quit(); break;
            }
        }
    }

    
    private void SelectMenu(int op)
    {
        SoundsManager.instance.PlaySound(soundOption);
        optionMenu = op;
        selectorRight.transform.position = new Vector3(selectorRight.transform.position.x,Buttons[op - 1].position.y, selectorRight.transform.position.z);
        selectorLeft.transform.position = new Vector3(selectorLeft.transform.position.x, Buttons[op - 1].position.y, selectorLeft.transform.position.z);
    }


    private void SelectOption(int op)
    {
        SoundsManager.instance.PlaySound(soundOption);
        optionOptions = op;
        if (op == 1) music.sprite = musicON;
        if (op == 2) sound.sprite = soundsON;
        if (lastOptionOptions == 1) music.sprite = musicOFF;
        if (lastOptionOptions == 2) sound.sprite = soundsOFF;      
        lastOptionOptions = op;
    }
    public void Quit(){Application.Quit();}

}
