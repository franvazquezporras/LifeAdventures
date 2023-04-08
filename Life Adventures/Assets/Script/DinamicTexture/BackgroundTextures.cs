using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundTextures : MonoBehaviour
{
    private int player;

    [SerializeField] private ButtonTextureScriptable backgroundTextures;
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
        this.GetComponent<SpriteRenderer>().sprite = backgroundTextures.textures[valueSprite];
    }
}
