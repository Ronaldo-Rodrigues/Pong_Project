using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingPoints : MonoBehaviour
{
    private void Start()
    {
        Destroy(this.gameObject, 1f);
        transform.localPosition += new Vector3(0, 2f, 0);
    }
}
