using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotScript : MonoBehaviour
{

    private GameObject joint1;
    private GameObject joint2;
    private GameObject joint3;
    private GameObject joint4;
    private articulationRotoideScript jointScript1;
    private articulationPrismaticScript jointScript2;
    private articulationRotoideScript jointScript3;
    private articulationPrismaticScript jointScript4;


[System.Serializable]
    public struct Stats
    {
        [Header("Movement Settings")]
        [Tooltip("You can change the speed and the position of each body part")]
        [Range(-180, 180)]
        public float positionRotoide1;
        [Range(0, 12)]
        public float positionPrismatic1;
        [Range(-180, 180)]
        public float positionRotoide2;
        [Range(0, 12)]
        public float positionPrismatic2;
    }
    public RobotScript.Stats jointsPositions = new RobotScript.Stats
    {
        positionRotoide1 = 0,
        positionPrismatic1 = 0,
        positionRotoide2 = 0,
        positionPrismatic2 = 0,
    };
    // Start is called before the first frame update
    void Start()
    {
        joint1 =  GameObject.Find("Articulation1");
        joint2 = GameObject.Find("Articulation2");
        joint3 = GameObject.Find("Articulation3");
        joint4 = GameObject.Find("Articulation4");
        jointScript1 = joint1.GetComponent<articulationRotoideScript>();
        jointScript2 = joint2.GetComponent<articulationPrismaticScript>();
        jointScript3 = joint3.GetComponent<articulationRotoideScript>();
        jointScript4 = joint4.GetComponent<articulationPrismaticScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if(jointsPositions.positionRotoide1 != jointScript1.angle)
        {
            jointScript1.angle = jointsPositions.positionRotoide1;
        }
        if (jointsPositions.positionPrismatic1 != jointScript2.distance)
        {
            jointScript2.distance = jointsPositions.positionPrismatic1;
        }
        if (jointsPositions.positionRotoide2 != jointScript3.angle)
        {
            jointScript3.angle = jointsPositions.positionRotoide2;
        }
        if (jointsPositions.positionPrismatic2 != jointScript4.distance)
        {
            jointScript4.distance = jointsPositions.positionPrismatic2;
        }
    }
}
