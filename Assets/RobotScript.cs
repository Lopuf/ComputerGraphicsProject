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
    private GameObject joint6;
    private articulationPrismaticScript jointScript1;
    private articulationRotoideScript jointScript2;
    private articulationPrismaticScript jointScript3;
    private articulationRotoideScript jointScript4;
    private articulationRotoideScript jointScript5;
    private articulationRotoideScript jointScript6;

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
        [Range(0, 12)]
        public float positionPrismatic1;
        [Range(-180, 180)]
        public float positionRotoide1;
        [Range(0, 12)]
        public float positionPrismatic2;
        [Range(-180, 180)]
        public float positionRotoide2;
        [Range(-180, 180)]
        public float positionRotoide3;
        [Range(-180, 180)]
        public float positionRotoide4;
    }
    public RobotScript.Stats jointsPositions = new RobotScript.Stats
    {
        positionPrismatic1 = 0,
        positionRotoide1 = 0,
        positionPrismatic2 = 0,
        positionRotoide2 = 0,
        positionRotoide3 = 0,
        positionRotoide4 = 0,
    };
    // Start is called before the first frame update
    void Start()
    {
        joint1 = GameObject.Find("Articulation1");
        joint2 = GameObject.Find("Articulation2");
        joint3 = GameObject.Find("Articulation3");
        joint4 = GameObject.Find("Articulation4");
        joint5 = GameObject.Find("Articulation5");
        joint6 = GameObject.Find("Articulation6");
        jointScript1 = joint1.GetComponent<articulationPrismaticScript>();
        jointScript2 = joint2.GetComponent<articulationRotoideScript>();
        jointScript3 = joint3.GetComponent<articulationPrismaticScript>();
        jointScript4 = joint4.GetComponent<articulationRotoideScript>();
        jointScript5 = joint5.GetComponent<articulationRotoideScript>();
        jointScript6 = joint6.GetComponent<articulationRotoideScript>();
        drawLine = false;
        drawCircle = false;
        drawEnsisa = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (jointsPositions.positionPrismatic1 != jointScript1.distance)
        {
            jointScript1.distance = jointsPositions.positionPrismatic1;
        }
        if (jointsPositions.positionRotoide1 != jointScript2.angle)
        {
            jointScript2.angle = jointsPositions.positionRotoide1;
        }
        if (jointsPositions.positionPrismatic2 != jointScript3.distance)
        {
            jointScript3.distance = jointsPositions.positionPrismatic2;
        }
        if (jointsPositions.positionRotoide2 != jointScript4.angle)
        {
            jointScript4.angle = jointsPositions.positionRotoide2;
        }
        if (jointsPositions.positionRotoide3 != jointScript5.angle)
        {
            jointScript5.angle = jointsPositions.positionRotoide3;
        }
        if (jointsPositions.positionRotoide4 != jointScript6.angle)
        {
            jointScript6.angle = jointsPositions.positionRotoide4;
        }

        if (drawLine)
        {
            if(xDesired < 10)
            {
                xDesired += 0.1f;
                float[] Variables = CalculVariablesArticulaires(xDesired, yDesired, zDesired);
                // [jointScript1.angle, jointScript2.distance, jointScript3.angle, jointScript4.distance, jointScript5.angle]
                jointScript1.distance = Variables[0];
                jointScript2.angle = Variables[1];
                jointScript3.distance = Variables[2];
                jointScript4.angle = Variables[3];
                jointScript5.angle = Variables[4];
                jointScript6.angle = Variables[5];
                jointsPositions.positionPrismatic1 = Variables[0];
                jointsPositions.positionRotoide1 = Variables[1];
                jointsPositions.positionPrismatic2 = Variables[2];
                jointsPositions.positionRotoide2 = Variables[3];
                jointsPositions.positionRotoide3 = Variables[4];
                jointsPositions.positionRotoide4 = Variables[5];
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
        jointsPositions.positionPrismatic1 = 5;
        jointsPositions.positionRotoide1 = 100;
        jointsPositions.positionPrismatic2 = 5;
        jointsPositions.positionRotoide2 = 6;
        jointsPositions.positionRotoide3 = 20;
        jointsPositions.positionRotoide4 = 20;
    }
    void StartDrawCircle()
    {
        // ici on met CalculVariablesArticulaires(xDesired, yDesired, zDesired)
        xDesired = -6.5f;
        yDesired = 10.3f;
        zDesired = -22.3f;
        jointsPositions.positionPrismatic1 = 5;
        jointsPositions.positionRotoide1 = 100;
        jointsPositions.positionPrismatic2 = 5;
        jointsPositions.positionRotoide2 = 6;
        jointsPositions.positionRotoide3 = 20;
        jointsPositions.positionRotoide4 = 20;
    }
    void StartDrawEnsisa()
    {
        // ici on met CalculVariablesArticulaires(xDesired, yDesired, zDesired)
        xDesired = -6.5f;
        yDesired = 10.3f;
        zDesired = -22.3f;
        jointsPositions.positionPrismatic1 = 5;
        jointsPositions.positionRotoide1 = 100;
        jointsPositions.positionPrismatic2 = 5;
        jointsPositions.positionRotoide2 = 6;
        jointsPositions.positionRotoide3 = 20;
        jointsPositions.positionRotoide4 = 20;
    }
    void EndDraw()
    {
        jointsPositions.positionPrismatic1 = 0;
        jointsPositions.positionRotoide1 = 0;
        jointsPositions.positionPrismatic2 = 0;
        jointsPositions.positionRotoide2 = 0;
        jointsPositions.positionRotoide3 = 0;
        jointsPositions.positionRotoide4 = 0;
    }



    float[] CalculVariablesArticulaires(float x, float y, float z)
    {

        // matrice de modele geometrique inverse
        float[] Variables = new float[6];

        Variables[0] = 5;
        Variables[1] = 100-5*x;
        Variables[2] = 8;
        Variables[3] = 6;
        Variables[4] = 10;
        Variables[5] = 10;

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
    public void setRotoide4(float newPos)
    {
        jointsPositions.positionRotoide4 = newPos;

    }
}
