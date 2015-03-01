using UnityEngine;
using System.Collections;

public class MasterChief : FallingObject {

    public float speed;
    private Vector3 angle;
    private Vector3 target;

    public GameObject RedAlert;

    public override void init()
    {
        target = transform.position;
        angle = new Vector3(Random.Range(-5,5), -10, Random.Range(-5,5));

        transform.rotation = Quaternion.FromToRotation(-transform.up, angle);
       
        transform.position = target - (angle.normalized * speed);

        GameObject go = Instantiate(RedAlert) as GameObject;
        go.transform.position = target;
        //go.transform.parent = transform;
    }

	public override void DoSomethingWhenNear()
    {
        StartCoroutine(Goto());
    }

    public IEnumerator Goto()
    {
        Vector3 original = transform.position;
        Vector3 final = target; 

        float distance = 1 / (Vector3.Distance(original, final) / speed);

        float step = 0;

        while (step < 1f)
        {
            step += distance * Time.deltaTime;

            transform.position = Vector3.Lerp(original, final, step);

            yield return null;
        }
        //complete movement
        transform.position = final;
    }
}
