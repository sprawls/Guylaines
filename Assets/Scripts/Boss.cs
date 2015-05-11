using UnityEngine;
using System.Collections;

public class Boss : MonoBehaviour {

    //Particles /Animations
    public GameObject bossDeathParticles;

    //Attributes
    private int health = 5;

	// Use this for initialization
	void Start () {
	
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
        ChunkManager chunkM = GameObject.FindGameObjectWithTag("ChunkManager").GetComponent<ChunkManager>();
        chunkM.BossDeath();

        Destroy(gameObject);
    }
}
