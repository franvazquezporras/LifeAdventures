using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTextures : MonoBehaviour
{
    private int player;

    [SerializeField] private PlayerSpriteScriptable playerTextures;
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
        this.GetComponent<Animator>().runtimeAnimatorController = playerTextures.animators[valueSprite];
        this.GetComponent<SpriteRenderer>().sprite = playerTextures.sprites[valueSprite];
    }
}
