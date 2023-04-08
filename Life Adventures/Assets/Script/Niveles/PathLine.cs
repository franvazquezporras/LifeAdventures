using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathLine : MonoBehaviour
{
    [SerializeField] private Transform salida;
    [SerializeField] private Transform destino;

    private void OnDrawGizmosSelected()
    {
        if (salida != null && destino != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(salida.position, destino.position);

        }
    }
}
