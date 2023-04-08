using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SignalInfo : MonoBehaviour
{
    [SerializeField] private GameObject mensajeInfo;
    private SpriteRenderer spr;
    [SerializeField] private Text text;
    [SerializeField] private string textInfo;
    private void Start()
    {
        
        spr = mensajeInfo.GetComponent<SpriteRenderer>();
        Color color = spr.material.color;
        Color textColor = text.color;
        color.a = 0f;
        textColor.a = 0f;
        spr.material.color = color;
        text.color = textColor;
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.gameObject.layer == Layers.PLAYER)
        {
            text.text = textInfo;
            StartCoroutine("ActiveInfo");
        }
            

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == Layers.PLAYER)
            StartCoroutine("InactiveInfo");
    }


    IEnumerator ActiveInfo()
    {
        for(float i = 0.0f; i <= 1; i += 0.02f)
        {
            Color textColor = text.color;
            Color color = spr.material.color;
            textColor.a = i;
            color.a = i;
            text.color = textColor;
            spr.material.color = color;
            yield return (0.05f);
        }
    }

    IEnumerator InactiveInfo()
    {
        for (float i =1f; i >= 0; i -= 0.02f)
        {
            Color textColor = text.color;
            Color color = spr.material.color;
            textColor.a = i;
            color.a = i;
            text.color = textColor;
            spr.material.color = color;
            yield return (0.05f);
        }
    }
}
