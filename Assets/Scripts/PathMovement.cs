using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public  class PathMovement : MonoBehaviour
{
    public float speed;

    private bool moving = false;
    private bool goingUp = true;
    private int objective = 0;

    private GameObject go;
    private List<PathPoint> checkpoint;
    void Start()
    {
        go = GetComponentInChildren<MeshRenderer>().gameObject as GameObject;
        checkpoint = gameObject.GetComponentsInChildren<PathPoint>().ToList();
        checkpoint.Sort(
            delegate(PathPoint a, PathPoint b)
            {
                if(a.position < b.position)
                {
                    return -1;
                }
                if(a.position == b.position)
                {
                    return 0;
                }
                return 1;
            });


    }

    void Update()
    {
        if(!moving)
        {
            moving = true;
            StartCoroutine(Goto());
        }
    }

    IEnumerator Goto()
    {
        Vector3 original = MeshPosition;
        Vector3 final = currentObjective;
        
        float distance = 1/ (Vector3.Distance(original, final) / speed );

        float step = 0;
       
        while (step < 1f)
        {
            step += distance * Time.deltaTime;

            MeshPosition = Vector3.Lerp(original, final, step);

            yield return null;
        }
        //complete movement
        MeshPosition = final;
        moving = false;
        NextObjective();
    }

    void NextObjective()
    {
        if(goingUp)
        {
            if (objective == checkpoint.Count-1)
            {
                objective--;
                goingUp = false;
            }
            else
            {
                objective++;
            }
        }
        else
        {
            if(objective == 0)
            {
                objective++;
                goingUp = true;
            }
            else
            {
                objective--;
            }
        }
    }

    public Vector3 MeshPosition
    {
        get
        {
            return go.transform.position;
        }
        set
        {
            go.transform.position = value;
        }
    }
    public Vector3 currentObjective { get { return checkpoint[objective].transform.position; } }

}

