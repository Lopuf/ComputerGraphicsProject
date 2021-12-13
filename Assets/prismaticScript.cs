using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class prismaticScript : MonoBehaviour
{
    public float moveSpeed = 0.1f;
    //private GameObject fixedCylinder = transform.GetChild(0).gameObject;
    //private GameObject movableCylinder = transform.GetChild(1).gameObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.Translate(Vector3.right * moveSpeed); //*moveSpeed * Time.deltaTime pr du metre par seconde
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.Translate(Vector3.left * moveSpeed);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.Translate(Vector3.forward * moveSpeed);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            transform.Translate(Vector3.back * moveSpeed);
        }


        if (Input.GetMouseButtonDown(2)) Debug.Log("Pressed middle click.");
    }
}
