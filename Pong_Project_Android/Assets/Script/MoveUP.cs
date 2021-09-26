using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveUP : MonoBehaviour
{
    public GameObject _player;
    public Button thisbtn;

    private void Start()
    {
        thisbtn = GetComponent<Button>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            _player.GetComponent<Player_Paddle>().MouseUp();
        }
    }

    private void OnMouseDown()
    {
        _player.GetComponent<Player_Paddle>().MoveUP();

    }

}
