using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotScript : MonoBehaviour
{
    
    private GameObject joint1;
    private GameObject joint2;
    private GameObject joint3;
    private GameObject joint4;
    private GameObject joint5;
    private articulationRotoideScript jointScript1;
    private articulationPrismaticScript jointScript2;
    private articulationRotoideScript jointScript3;
    private articulationPrismaticScript jointScript4;
    private articulationRotoideScript jointScript5;

    private bool drawLine;
    private bool drawCircle;
    private bool drawEnsisa;
    private float xDesired;
    private float yDesired;
    private float zDesired;


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
        [Range(-180, 180)]
        public float positionRotoide3;
    }
    public RobotScript.Stats jointsPositions = new RobotScript.Stats
    {
        positionRotoide1 = 0,
        positionPrismatic1 = 0,
        positionRotoide2 = 0,
        positionPrismatic2 = 0,
        positionRotoide3 = 0,
    };
    // Start is called before the first frame update
    void Start()
    {
        joint1 = GameObject.Find("Articulation1");
        joint2 = GameObject.Find("Articulation2");
        joint3 = GameObject.Find("Articulation3");
        joint4 = GameObject.Find("Articulation4");
        joint5 = GameObject.Find("Articulation5");
        jointScript1 = joint1.GetComponent<articulationRotoideScript>();
        jointScript2 = joint2.GetComponent<articulationPrismaticScript>();
        jointScript3 = joint3.GetComponent<articulationRotoideScript>();
        jointScript4 = joint4.GetComponent<articulationPrismaticScript>();
        jointScript5 = joint5.GetComponent<articulationRotoideScript>();
        drawLine = false;
        drawCircle = false;
        drawEnsisa = false;
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
        if (jointsPositions.positionRotoide3 != jointScript5.angle)
        {
            jointScript5.angle = jointsPositions.positionRotoide3;
        }

        if (drawLine)
        {
            if(xDesired < 10)
            {
                xDesired += 0.1f;
                float[] Variables = CalculVariablesArticulaires(xDesired, yDesired, zDesired);
                // [jointScript1.angle, jointScript2.distance, jointScript3.angle, jointScript4.distance, jointScript5.angle]
                jointScript1.angle = Variables[0];
                jointScript2.distance = Variables[1];
                jointScript3.angle = Variables[2];
                jointScript4.distance = Variables[3];
                jointScript5.angle = Variables[4];
                jointsPositions.positionRotoide1 = Variables[0];
                jointsPositions.positionPrismatic1 = Variables[1];
                jointsPositions.positionRotoide2 = Variables[2];
                jointsPositions.positionPrismatic2 = Variables[3];
                jointsPositions.positionRotoide3 = Variables[4];
            }
            else
            {
                drawLine = false;
                EndDraw();
            }
        }
    }

    public void DrawLine()
    {
        StartDrawLine();
        Invoke("inPositionToDrawLine", 2);
    }
    public void DrawCircle()
    {
        StartDrawCircle();
        Invoke("inPositionToDrawCircle", 2);
    }
    public void DrawEnsisa()
    {
        StartDrawEnsisa();
        Invoke("inPositionToDrawEnsisa", 2);
    }



    void inPositionToDrawLine()
    {
        drawLine = true;
    }
    void inPositionToDrawCircle()
    {
        drawCircle = true;
    }
    void inPositionToDrawEnsisa()
    {
        drawEnsisa = true;
    }

    void StartDrawLine()
    {
        // ici on met CalculVariablesArticulaires(xDesired, yDesired, zDesired)
        xDesired = -6.5f;
        yDesired = 10.3f;
        zDesired = -22.3f;
        jointsPositions.positionRotoide1 = 100;
        jointsPositions.positionPrismatic1 = 5;
        jointsPositions.positionRotoide2 = 6;
        jointsPositions.positionPrismatic2 = 5;
        jointsPositions.positionRotoide3 = 20;
    }
    void StartDrawCircle()
    {
        // ici on met CalculVariablesArticulaires(xDesired, yDesired, zDesired)
        xDesired = -6.5f;
        yDesired = 10.3f;
        zDesired = -22.3f;
        jointsPositions.positionRotoide1 = 100;
        jointsPositions.positionPrismatic1 = 5;
        jointsPositions.positionRotoide2 = 6;
        jointsPositions.positionPrismatic2 = 5;
        jointsPositions.positionRotoide3 = 20;
    }
    void StartDrawEnsisa()
    {
        // ici on met CalculVariablesArticulaires(xDesired, yDesired, zDesired)
        xDesired = -6.5f;
        yDesired = 10.3f;
        zDesired = -22.3f;
        jointsPositions.positionRotoide1 = 100;
        jointsPositions.positionPrismatic1 = 5;
        jointsPositions.positionRotoide2 = 6;
        jointsPositions.positionPrismatic2 = 5;
        jointsPositions.positionRotoide3 = 20;
    }
    void EndDraw()
    {
        jointsPositions.positionRotoide1 = 0;
        jointsPositions.positionPrismatic1 = 0;
        jointsPositions.positionRotoide2 = 0;
        jointsPositions.positionPrismatic2 = 0;
        jointsPositions.positionRotoide3 = 0;
    }



    float[] CalculVariablesArticulaires(float x, float y, float z)
    {

        // matrice de modele geometrique inverse
        float[] Variables = new float[5];
        Variables[0] = 100-5*x;
        Variables[1] = 5;
        Variables[2] = 4;
        Variables[3] = 5;
        Variables[4] = 10;

        return Variables;
    }

    public void setRotoide1(float newPos)
    {
        jointsPositions.positionRotoide1 = newPos;
        
    }
    public void setPrismatic1(float newPos)
    {
        jointsPositions.positionPrismatic1 = newPos;

    }
    public void setRotoide2(float newPos)
    {
        jointsPositions.positionRotoide2 = newPos;

    }
    public void setPrismatic2(float newPos)
    {
        jointsPositions.positionPrismatic2 = newPos;

    }
    public void setRotoide3(float newPos)
    {
        jointsPositions.positionRotoide3 = newPos;

    }
}
