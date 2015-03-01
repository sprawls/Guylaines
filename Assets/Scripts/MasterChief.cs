using UnityEngine;
using System.Collections;

public class MasterChief : FallingObject {

    public float speed;
    private Vector3 angle;
    private Vector3 target;

    public override void init()
    {
        target = transform.position;
        angle = new Vector3(Random.Range(-10,10), -10, Random.Range(-10,5));

        transform.rotation = Quaternion.FromToRotation(-transform.up, angle);
       
        transform.position = target - (angle.normalized * speed);
    }

	public override void DoSomethingWhenNear()
    {
        Debug.Log("Hey test");
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
