using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMotor : MonoBehaviour

{
  private float speed = 15.0f;
  public CharacterController controller;
    // Start is called before the first frame update
    void Start()
    {
      controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
      controller.Move((Vector3.forward*speed)*Time.deltaTime);
    }
}
