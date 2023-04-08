using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathPlataform : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float velocidad;
    private Vector3 inicio, final;

    void Start()
    {
        //quitar la herencia del target
        if (target != null)
        {
            target.parent = null;
            inicio = transform.position;
            final = target.position;
        }
    }

    private void FixedUpdate()
    {
        //desplazamiento de plataforma
        if (target != null)
        {
            float fixedVelocidad = velocidad * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target.position, fixedVelocidad);
        }


        //comprueba final de tramo de desplazamiento y cambia el target
        if (transform.position == target.position)
        {
            target.position = (target.position == inicio) ? final : inicio;
        }
    }
}
