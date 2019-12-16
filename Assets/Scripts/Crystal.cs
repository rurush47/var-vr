using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : MonoBehaviour
{
    private float randomRotation;
    void Start()
    {
        randomRotation = Random.Range(-3, 3);
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y + randomRotation, 0);
    }
}
