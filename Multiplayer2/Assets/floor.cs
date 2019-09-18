using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floor : MonoBehaviour
{
    static public floor instance;
   
    private void Start()
    {
        if(instance== null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void SetColor(Color color)
    {
        gameObject.GetComponent<Renderer>().material.color = color;
    }
}
