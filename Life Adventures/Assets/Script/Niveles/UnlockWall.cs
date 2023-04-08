using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockWall : MonoBehaviour
{
    public GameObject llave;

    private void Update()
    {
        if (llave.GetComponent<EstatuaControl>().activate)
        {
            Destroy(gameObject);
        }
    }
}
