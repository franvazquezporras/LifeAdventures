using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnd : MonoBehaviour
{
    [SerializeField] GameObject boss;
    [SerializeField] GameObject endPoint;


    private void Update()
    {
        if (!boss.activeInHierarchy)
            endPoint.SetActive(true);      

        
    }
}
