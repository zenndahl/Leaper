using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public int side;
    
    void Start()
    {
        if(side == 0)
            transform.position = FindObjectOfType<Camera>().ViewportToWorldPoint(Vector3.zero);
        else
            transform.position = FindObjectOfType<Camera>().ViewportToWorldPoint(Vector3.one);
    }

}
