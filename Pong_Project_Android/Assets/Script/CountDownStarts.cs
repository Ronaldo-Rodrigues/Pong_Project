using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountDownStarts : MonoBehaviour
{
    public GameManager gm;

    public void StartBola()
    {
        gm = GameObject.Find("Game Manager").GetComponent<GameManager>();
        gm.ForcaInicial();
       
    }
}
