using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class articulationPrismaticScript : MonoBehaviour
{
    public float moveSpeed = 0.05f;


    [Range(0, 12)]
    public float distance;
    private float tempDistance;
    // Start is called before the first frame update
    void Start()
    {
        transform.Translate(Vector3.up * distance);
        tempDistance = distance;
    }

    // Update is called once per frame
    void Update()
    {

        if (tempDistance > distance + moveSpeed)
        {
            transform.Translate(Vector3.down * moveSpeed);
            tempDistance -= moveSpeed;
        }
        else if (tempDistance < distance - moveSpeed)
        {
            transform.Translate(Vector3.up * moveSpeed);
            tempDistance += moveSpeed;
        }
        //transform.Rotate(Vector3.back, rotationSpeed);
    }
    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            transform.Translate(Vector3.up * moveSpeed);
        }
        if (Input.GetMouseButtonDown(1))
        {
            transform.Translate(Vector3.down * moveSpeed);
        }
    }
}
