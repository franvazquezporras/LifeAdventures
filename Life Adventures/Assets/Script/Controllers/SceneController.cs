using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{

    
    public void ChangeScreen(string scene)
    {
        if (Time.timeScale == 0)
            Time.timeScale = 1;
        SceneManager.LoadScene(scene);
    }

    public void NextLevel()
    {
        if (SceneManager.GetActiveScene().name == "Nivel1")
            ChangeScreen("Nivel2");
        else if (SceneManager.GetActiveScene().name == "Nivel2")
            ChangeScreen("Nivel3");
        else if (SceneManager.GetActiveScene().name == "Nivel3")
            ChangeScreen("Credits");
    }
}
