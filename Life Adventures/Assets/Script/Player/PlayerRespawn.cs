using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    
    

    void Start()
    {
        if (PlayerPrefs.GetFloat("PositionX") != 0)
            transform.position = (new Vector2(PlayerPrefs.GetFloat("PositionX"), PlayerPrefs.GetFloat("PositionY")));
    }

    public void ActivedCheckPoint(float x, float y)
    {
        PlayerPrefs.SetFloat("PositionX",x);
        PlayerPrefs.SetFloat("PositionY",y);
    }

}
