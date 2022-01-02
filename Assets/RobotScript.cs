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
    private articulationRotoideScript jointScript1;
    private articulationPrismaticScript jointScript2;
    private articulationPrismaticScript jointScript3;
    private articulationRotoideScript jointScript4;
    private articulationRotoideScript jointScript5;
    private articulationRotoideScript jointScript6;

    private GameObject tip;
    private TrailRenderer trail;

    private bool drawLine;
    private bool drawCircle;
    private bool drawEnsisa;
    private float xDesired;
    private float yDesired;
    private float zDesired;

    private int iterations;

    public EnsisaPath ensisaScript;


    [System.Serializable]
    public struct Stats
    {
        [Header("Movement Settings")]
        [Tooltip("You can change the speed and the position of each body part")]
        [Range(-180, 180)]
        public float positionRotoide1;
        [Range(0, 12)]
        public float positionPrismatic1;
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
        jointScript1 = joint1.GetComponent<articulationRotoideScript>();
        jointScript2 = joint2.GetComponent<articulationPrismaticScript>();
        jointScript3 = joint3.GetComponent<articulationPrismaticScript>();
        jointScript4 = joint4.GetComponent<articulationRotoideScript>();
        jointScript5 = joint5.GetComponent<articulationRotoideScript>();
        jointScript6 = joint6.GetComponent<articulationRotoideScript>();


        tip = GameObject.Find("Tip");
        trail = tip.GetComponent<TrailRenderer>();
        //trail.widthMultiplier = 0f;
        trail.emitting = false;
        drawLine = false;
        drawCircle = false;
        drawEnsisa = false;
        iterations = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (jointsPositions.positionRotoide1 != jointScript1.angle)
        {
            jointScript1.angle = jointsPositions.positionRotoide1;
        }
        if (jointsPositions.positionPrismatic1 != jointScript2.distance)
        {
            jointScript2.distance = jointsPositions.positionPrismatic1;
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
            if(xDesired < 15)
            {
                xDesired += 0.02f;
                float[] Variables = CalculVariablesArticulaires(xDesired, yDesired, zDesired);
                // [jointScript1.angle, jointScript2.distance, jointScript3.angle, jointScript4.distance, jointScript5.angle]
                jointScript1.angle = Variables[0];
                jointScript2.distance = Variables[1];
                jointScript3.distance = Variables[2];
                jointScript4.angle = Variables[3];
                jointScript5.angle = Variables[4];
                jointScript6.angle = Variables[5];
                jointsPositions.positionRotoide1 = Variables[0];
                jointsPositions.positionPrismatic1 = Variables[1];
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
        if (drawCircle)
        {
            if (iterations < 1800)
            {
                xDesired = Mathf.Cos(iterations/5 * Mathf.Deg2Rad) *8;
                yDesired = Mathf.Sin(iterations/ 5 * Mathf.Deg2Rad) *8+7;
               
                float[] Variables = CalculVariablesArticulaires(xDesired, yDesired, zDesired);
                // [jointScript1.angle, jointScript2.distance, jointScript3.angle, jointScript4.distance, jointScript5.angle]
                jointScript1.angle = Variables[0];
                jointScript2.distance = Variables[1];
                jointScript3.distance = Variables[2];
                jointScript4.angle = Variables[3];
                jointScript5.angle = Variables[4];
                jointScript6.angle = Variables[5];
                jointsPositions.positionRotoide1 = Variables[0];
                jointsPositions.positionPrismatic1 = Variables[1];
                jointsPositions.positionPrismatic2 = Variables[2];
                jointsPositions.positionRotoide2 = Variables[3];
                jointsPositions.positionRotoide3 = Variables[4];
                jointsPositions.positionRotoide4 = Variables[5];
                iterations++;
            }
            else
            {
                drawCircle = false;
                iterations = 0;
                EndDraw();
            }
        }
        if (drawEnsisa)
        {
            if (iterations < ensisaScript.path.Length*4 / 3)
            {
                //Debug.Log(ensisaScript.path.Length); //ensisaScript.path[0,1] // ensisaScript.path.Length/3 !!

                xDesired = (float)ensisaScript.path[iterations/4, 0];
                yDesired = (float)ensisaScript.path[iterations/4, 1];
                if (ensisaScript.path[iterations/4, 2] == 1) trail.emitting = true;
                else trail.emitting = false;


                float[] Variables = CalculVariablesArticulaires(xDesired, yDesired, zDesired);
                jointScript1.angle = Variables[0];
                jointScript2.distance = Variables[1];
                jointScript3.distance = Variables[2];
                jointScript4.angle = Variables[3];
                jointScript5.angle = Variables[4];
                jointScript6.angle = Variables[5];
                jointsPositions.positionRotoide1 = Variables[0];
                jointsPositions.positionPrismatic1 = Variables[1];
                jointsPositions.positionPrismatic2 = Variables[2];
                jointsPositions.positionRotoide2 = Variables[3];
                jointsPositions.positionRotoide3 = Variables[4];
                jointsPositions.positionRotoide4 = Variables[5];
                iterations++;
            }
            else
            {
                drawEnsisa = false;
                iterations = 0;
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
    public void Draw()
    {
        trail.emitting = true;
    }
    public void Stop()
    {
        trail.emitting = false;
    }



    void inPositionToDrawLine()
    {
        //trail.widthMultiplier = 1f;
        trail.emitting = true;
        drawLine = true;
    }
    void inPositionToDrawCircle()
    {
        //trail.widthMultiplier = 1f;
        trail.emitting = true;
        drawCircle = true;
    }
    void inPositionToDrawEnsisa()
    {
        //trail.widthMultiplier = 1f;
        trail.emitting = true;
        drawEnsisa = true;
    }

    void StartDrawLine()
    {
        // ici on met CalculVariablesArticulaires(xDesired, yDesired, zDesired)
        xDesired = -15f;
        yDesired = 10f;
        zDesired = -7f;
        float[] Variables = CalculVariablesArticulaires(xDesired, yDesired, zDesired);
        jointScript1.angle = Variables[0];
        jointScript2.distance = Variables[1];
        jointScript3.distance = Variables[2];
        jointScript4.angle = Variables[3];
        jointScript5.angle = Variables[4];
        jointScript6.angle = Variables[5];
        jointsPositions.positionRotoide1 = Variables[0];
        jointsPositions.positionPrismatic1 = Variables[1];
        jointsPositions.positionPrismatic2 = Variables[2];
        jointsPositions.positionRotoide2 = Variables[3];
        jointsPositions.positionRotoide3 = Variables[4];
        jointsPositions.positionRotoide4 = Variables[5];
    }
    void StartDrawCircle()
    {
        // ici on met CalculVariablesArticulaires(xDesired, yDesired, zDesired)
        xDesired = 8f;
        yDesired = 7f;
        zDesired = -7f;
        float[] Variables = CalculVariablesArticulaires(xDesired, yDesired, zDesired);
        jointScript1.angle = Variables[0];
        jointScript2.distance = Variables[1];
        jointScript3.distance = Variables[2];
        jointScript4.angle = Variables[3];
        jointScript5.angle = Variables[4];
        jointScript6.angle = Variables[5];
        jointsPositions.positionRotoide1 = Variables[0];
        jointsPositions.positionPrismatic1 = Variables[1];
        jointsPositions.positionPrismatic2 = Variables[2];
        jointsPositions.positionRotoide2 = Variables[3];
        jointsPositions.positionRotoide3 = Variables[4];
        jointsPositions.positionRotoide4 = Variables[5];
    }
    void StartDrawEnsisa()
    {
        // ici on met CalculVariablesArticulaires(xDesired, yDesired, zDesired)
        xDesired = -9f;
        yDesired = 12f;
        zDesired = -7f; //-7f
        float[] Variables = CalculVariablesArticulaires(xDesired, yDesired, zDesired);
        jointScript1.angle = Variables[0];
        jointScript2.distance = Variables[1];
        jointScript3.distance = Variables[2];
        jointScript4.angle = Variables[3];
        jointScript5.angle = Variables[4];
        jointScript6.angle = Variables[5];
        jointsPositions.positionRotoide1 = Variables[0];
        jointsPositions.positionPrismatic1 = Variables[1];
        jointsPositions.positionPrismatic2 = Variables[2];
        jointsPositions.positionRotoide2 = Variables[3];
        jointsPositions.positionRotoide3 = Variables[4];
        jointsPositions.positionRotoide4 = Variables[5];
    }
    void EndDraw()
    {
        //trail.widthMultiplier = 0f;
        trail.emitting = false;
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

        Variables[0] = (Mathf.Acos(x / (Mathf.Sqrt(Mathf.Pow(x, 2) + Mathf.Pow(z, 2)))) - Mathf.Acos(1/ (Mathf.Sqrt(Mathf.Pow(x, 2) + Mathf.Pow(z, 2))))) * Mathf.Rad2Deg + 90; //90 pcq mauvais axes
        Variables[1] = y;
        Variables[2] = Mathf.Sqrt(Mathf.Pow(x, 2) + Mathf.Pow(z, 2) - 25);
        Variables[3] = 0;
        Variables[4] = 0;
        Variables[5] = 0;

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
