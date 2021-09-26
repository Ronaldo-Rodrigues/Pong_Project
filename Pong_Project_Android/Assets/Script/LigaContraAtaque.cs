using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LigaContraAtaque : MonoBehaviour
{
    public Collider2D colide;

    public bool ligaFogo, ligaAgua, ligaGrama;

    private void Start()
    {
        colide = GetComponent<BoxCollider2D>();
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            var bola = other.GetComponent<Ball>();
            if (bola.isAquaBall && bola.playerTouch == true)
            {
                combateAgua();
            }

        }
    }

    public void combateAgua()
    {
        ligaGrama = true;
        ligaFogo = false;
        ligaAgua = false;
    }
}
