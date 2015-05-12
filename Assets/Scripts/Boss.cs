using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Boss : MonoBehaviour {

    //Particles /Animations
    public GameObject bossDeathParticles;
    public GameObject ModelTranslate;
    public GameObject ModelRotate;

    //Attributes
    private int health = 5;
    private Vector3 StartPosition = new Vector3(0, 50, 250);
    private Vector3 SpawnDistance = new Vector3(0, 20, -100);
    private bool isInitialized = false;

	// Use this for initialization
	void Start () {
        StartCoroutine(IntroAnimation());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Boss_hit() {
        health -= 1;
        CheckDeath();
    }

    public void Boss_WeakSpothit() {
        health -= 5;
        CheckDeath();
    }

    void CheckDeath() {
        if (health <= 0) {
            Kill();
        }
    }

    void Kill() {
        if (bossDeathParticles != null) Instantiate(bossDeathParticles,transform.position,Quaternion.identity);

        GameManager gameM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        gameM.EndBossPhase();

        StartCoroutine(DeathAnimation());
        
    }


    IEnumerator IntroAnimation() {
        //initial positions
        ModelRotate.transform.localEulerAngles = new Vector3(20, 180, 0);
        ModelTranslate.transform.localPosition = SpawnDistance;
        //Start mouvements
        ModelTranslate.transform.DOLocalMove(StartPosition, 4f, false);
        ModelRotate.transform.DOLocalRotate(new Vector3(0, 0, 0), 6f);

        yield return new WaitForSeconds(6f);
        isInitialized = true;

    }

    IEnumerator DeathAnimation() {

        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

}
