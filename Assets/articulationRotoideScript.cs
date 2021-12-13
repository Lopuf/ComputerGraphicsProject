using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class articulationRotoideScript : MonoBehaviour
{
    public float rotationSpeed = 0.2f;
    // Start is called before the first frame update


    [Range(-180, 180)]
    public float angle;
    private float tempAngle;
    // Start is called before the first frame update
    void Start()
    {
        transform.Rotate(Vector3.back, angle);
        tempAngle = angle;
    }

    // Update is called once per frame
    void Update()
    {

        if (tempAngle > angle + rotationSpeed) {
            transform.Rotate(Vector3.forward, rotationSpeed);
            tempAngle -= rotationSpeed;
        }
        else if (tempAngle < angle - rotationSpeed)
        {
            transform.Rotate(Vector3.back, rotationSpeed);
            tempAngle += rotationSpeed;
        }
        //transform.Rotate(Vector3.back, rotationSpeed);
    }
    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            transform.Rotate(Vector3.back, rotationSpeed);
        }
        if (Input.GetMouseButtonDown(1))
        {
            transform.Rotate(Vector3.forward, rotationSpeed);
        }

    }
}
