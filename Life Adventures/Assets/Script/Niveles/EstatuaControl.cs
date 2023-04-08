using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstatuaControl : MonoBehaviour
{
    public bool activate = false;
    
    private void Update()
    {
        if (activate)
            gameObject.transform.GetChild(1).gameObject.SetActive(true);
        
    }

}
