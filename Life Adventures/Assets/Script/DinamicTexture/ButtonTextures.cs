using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonTextures : MonoBehaviour
{
    private int player;
    
    [SerializeField] private ButtonTextureScriptable buttonTextures;
    private void Awake()
    {
        player = PlayerPrefs.GetInt("tipoPersonaje");
    }
    void Start()
    {
        ChangeSpriteButton(player);        
    }
    
    public void ChangeSpriteButton(int valueSprite)
    {
        this.GetComponent<Image>().sprite = buttonTextures.textures[valueSprite];        
    }
}
