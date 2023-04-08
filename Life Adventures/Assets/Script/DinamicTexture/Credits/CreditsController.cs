using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsController : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine("WaitEndCredits");
    }
    
    IEnumerator WaitEndCredits()
    {
        yield return new WaitForSeconds(18);        
        SceneManager.LoadScene("MainMenu");
    }

}
