using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerTexture", menuName = "Scriptables/PlayerTexture", order = 1)]
public class PlayerSpriteScriptable : ScriptableObject
{
    public RuntimeAnimatorController[] animators;
    public Sprite[] sprites;
}
