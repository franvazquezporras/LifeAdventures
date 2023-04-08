using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundParallax : MonoBehaviour
{
    [SerializeField]
    private Vector2 parallaxEfectoMultiplicador;
    [SerializeField]
    private bool infinitoX;
    [SerializeField]
    private bool infinitoY;

    private Transform camPosition;
    private Vector3 lastPositionCam;
    private float tamTexturaX;
    private float tamTexturaY;
    private bool ready= false;


    private void Start()
    {
        StartCoroutine("chargeBackground");
    }

    private void LateUpdate()
    {
        if (ready)
        {
            Vector3 movimiento = camPosition.position - lastPositionCam;
            transform.position += new Vector3(movimiento.x * parallaxEfectoMultiplicador.x, movimiento.y * parallaxEfectoMultiplicador.y);
            lastPositionCam = camPosition.position;
            if (infinitoX)
            {
                if (Mathf.Abs(camPosition.position.x - transform.position.x) >= tamTexturaX)
                {
                    float cambiarPosicionX = (camPosition.position.x - transform.position.x) % tamTexturaX;
                    transform.position = new Vector3(camPosition.position.x + cambiarPosicionX, transform.position.y);
                }
            }
            if (infinitoY)
            {
                if (Mathf.Abs(camPosition.position.y - transform.position.y) >= tamTexturaY)
                {
                    float cambiarPosicionY = (camPosition.position.y - transform.position.y) % tamTexturaY;
                    transform.position = new Vector3(transform.position.x, camPosition.position.y, cambiarPosicionY);
                }
            }
        }       
    }

    IEnumerator chargeBackground()
    {
        yield return new WaitForSeconds(2);
        camPosition = Camera.main.transform;
        lastPositionCam = camPosition.position;
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        Texture2D textura = sprite.texture;
        tamTexturaX = textura.width / sprite.pixelsPerUnit;
        tamTexturaY = textura.height / sprite.pixelsPerUnit;
        ready = true;
    }

}
