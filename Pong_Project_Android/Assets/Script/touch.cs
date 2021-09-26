using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class touch : MonoBehaviour
{
    public bool isTouch = false, isGoingUP = false, isGoingDown = false;
    private void Update()
    {
       /* float moveY = Input.GetAxis("Mouse Y");
        if(isTouch == true)
        {
            if(moveY <= -0.3f)
            {
                isGoingDown = true;
                isGoingUP = false;
            }
            if(moveY >= 0.3f)
            {
                isGoingUP = true;
                isGoingDown = false;

            }
        }
        else
        { 
            isGoingDown = false;
            isGoingUP = false;
        }*/
    }

   

    private void OnMouseDown()
    {
        isTouch = true;
    }
    private void OnMouseUp()
    {
        isTouch = false;
    }
}
