using UnityEngine;
using System.Collections;

public class SetBeamValues : MonoBehaviour {

    LineRenderer lineRenderer;
    private ShipControl ship;

    //Attributes
    private Vector3 curStartPos = Vector3.zero;
    private Vector3 curEndPos = Vector3.zero;
    
    //Additionnal options
    public bool BeamFollowPlayerSpeed = false;

	// Use this for initialization
	void Awake () {
        lineRenderer = GetComponent<LineRenderer>();
	}

    void Start() {
        ship = GameObject.FindGameObjectWithTag("Player").GetComponent<ShipControl>();
    }
	
	// Update is called once per frame
	void Update () {
        if (BeamFollowPlayerSpeed && ship != null) {
            float speed = (ship.forwardSpeed + ship.additionalSpeed) * Time.deltaTime * ship.SpeedDeltaTimeAdjuster;
            //Add speed to vectors
            curStartPos += new Vector3(0,0,speed);
            curEndPos += new Vector3(0, 0, speed);

            lineRenderer.SetPosition(0, curStartPos);
            lineRenderer.SetPosition(1, curEndPos);
        }
        
        //Debug.Log(lineRenderer);
	}

    public void SetBeamPositions(Vector3 start, Vector3 end) {
        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, end);
        curStartPos = start;
        curEndPos = end;
    }

    public void SetBeamColors(Color start, Color end) {
        lineRenderer.SetColors(start,end);
    }

    public void SetBeamWidth(float start, float end) {
        lineRenderer.SetWidth(start, end);
    }

}
