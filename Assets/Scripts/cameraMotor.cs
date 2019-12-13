using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMotor : MonoBehaviour
{
  private Transform lookAt;
  private Vector3 startOffest;
    // Start is called before the first frame update
    void Start()
    {
      lookAt = GameObject.FindGameObjectWithTag("Player").transform;
      startOffest = transform.position - lookAt.position;
    }

    // Update is called once per frame
    void Update()
    {
      transform.position = lookAt.position;
    }
}
