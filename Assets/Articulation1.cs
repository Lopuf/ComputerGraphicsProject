using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Articulation1 : MonoBehaviour
{
    [Range(-1, 1)]
    public float Rotoide1;
    // Start is called before the first frame update
    void Start()
    {
        transform.Rotate(Vector3.back, Rotoide1);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.back, Rotoide1);
    }
}
