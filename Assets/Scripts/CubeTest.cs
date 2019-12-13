using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeTest : MonoBehaviour
{
    private Material material;
    void Start()
    {
        material = GetComponent<MeshRenderer>().material;
    }

    public void SetColor(string color)
    {
        Color uColor = Color.red;
        switch (color)
        {
            case "blue":
                uColor = Color.blue;
                break;
            case "yellow":
                uColor = Color.yellow;
                break;
        }
        
        material.color = uColor;
    }
}
