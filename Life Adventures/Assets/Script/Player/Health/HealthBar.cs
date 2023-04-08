using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    [SerializeField] private Image totalHealth;
    [SerializeField] private Image currentHealth;
    

    private void Start()
    {
       
        if (playerHealth.gameObject.layer == 11)
            totalHealth.fillAmount = playerHealth.maxHealth / 10;

        else
            totalHealth.fillAmount = playerHealth.maxHealth / playerHealth.maxHealth;

    }
    private void Update()
    {
        if (playerHealth.gameObject.layer == 11)
        {
            totalHealth.fillAmount = playerHealth.maxHealth / 10;
            currentHealth.fillAmount = playerHealth.currentHealth / 10;
        }
        else
        {
            totalHealth.fillAmount = playerHealth.maxHealth / playerHealth.maxHealth;
            currentHealth.fillAmount = playerHealth.currentHealth / playerHealth.maxHealth;
        }
     
            
    }
}
