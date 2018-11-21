using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{

    public GameObject point1;
    public GameObject point2;
    public float speed = 0.5f;
    public Vector3 Rotation;

    public bool Movement;

	void Update ()
    {

        this.transform.Rotate(Rotation * Time.deltaTime);


        if (Movement == true)
        {
            transform.position = Vector3.Lerp(point1.transform.position, point2.transform.position, Mathf.PingPong(Time.time * speed, 1.0f));
        }
    }
}