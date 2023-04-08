using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelector : MonoBehaviour
{
    
    private int actualCharacter;
    [SerializeField] GameObject [] characters;

    [SerializeField] RawImage rawImage;
    [SerializeField] Texture [] backgroundCharacter;

    [SerializeField] SpriteRenderer ground;
    [SerializeField] Sprite [] groundCharacter;

    [SerializeField] ButtonTextures [] listButtons;

    [Header("Sonidos de menu")]
    [SerializeField] AudioSource menuMusic;
    [SerializeField] AudioSource soundOption;
    [SerializeField] AudioSource soundSelect;
    SceneController scene = new SceneController();
    private void Awake()
    {
        menuMusic.Play();
        Load();
        ChangeCharacter();
    }
    private void Load()
    {     
        actualCharacter = PlayerPrefs.GetInt("tipoPersonaje");        
    }
    private void Save()
    {     
        PlayerPrefs.SetInt("tipoPersonaje", actualCharacter);
    }

    //SelectorCharacters
    public void nextCharacter()
    {        
        if (actualCharacter == 2)
            actualCharacter = 0;
        else
            actualCharacter++;
        soundOption.Play();
        Save();
        ChangeCharacter();
    }

    public void previousCharacter()
    {       
        if (actualCharacter == 0)
            actualCharacter = 2;
        else
            actualCharacter--;
        soundOption.Play();
        Save();
        ChangeCharacter();
    }

    public void SelectCharacter()
    {
        soundSelect.Play();
        scene.ChangeScreen("MainMenu");
    }

    private void ChangeCharacter()
    {
        characters[0].gameObject.SetActive(actualCharacter==0);
        characters[1].gameObject.SetActive(actualCharacter==1);
        characters[2].gameObject.SetActive(actualCharacter==2);
        rawImage.texture = backgroundCharacter[actualCharacter];
        ground.sprite = groundCharacter[actualCharacter];

        foreach (ButtonTextures button in listButtons)
            button.ChangeSpriteButton(actualCharacter);
    }

}
