using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    static GameManager singleton;
    [SerializeField] private GameObject deathScreen;
    private SpriteRenderer sprDeathScreen;
    public static bool gameOn = false;
    public static bool deathPlayer;
    private int coins;
    [SerializeField] private Text countCoint;
    [SerializeField] private GameObject pauseScreen;
    
    private void Awake()
    {
        singleton = this;
        deathScreen.SetActive(true); 
    }

    public static void getCoin()
    {
        singleton.coins++;
        if (singleton.coins < 10)
            singleton.countCoint.text = "0" + singleton.coins;
        else
            singleton.countCoint.text = singleton.coins.ToString();
    }
    private void Start()
    {
        sprDeathScreen = deathScreen.GetComponent<SpriteRenderer>();
        Invoke("DeathScreenOff", 0.5f);
    }
    private void Update()
    {
        if (deathPlayer)
        {            
            StartCoroutine("DSOn");
            deathPlayer = false;
        }        
    }

    public void ControlPause(int i)
    {
        if(i == 0)
        {
            pauseScreen.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            pauseScreen.SetActive(false);
            Time.timeScale = 1;
        }
    }
    public static void reloadScene()
    {        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void DeathScreenOff()
    {
        StartCoroutine("DSOff");
    }

    IEnumerator DSOff()
    {
        for(float alpha = 1f; alpha >= 0; alpha -= Time.deltaTime * 2f)
        {
            sprDeathScreen.material.color = new Color(sprDeathScreen.material.color.r, sprDeathScreen.material.color.g, sprDeathScreen.material.color.b, alpha);
            yield return null;
        }
        gameOn = true;
    }
    IEnumerator DSOn()
    {
        for (float alpha = 0f; alpha <= 1; alpha += Time.deltaTime * 2f)
        {
            sprDeathScreen.material.color = new Color(sprDeathScreen.material.color.r, sprDeathScreen.material.color.g, sprDeathScreen.material.color.b, alpha);
            yield return null;
        }
        reloadScene();
    }

}
