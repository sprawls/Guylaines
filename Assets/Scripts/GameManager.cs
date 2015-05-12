using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    //components
    private ChunkManager chunkManager;
    private ShipControl player;
    public GameObject boss;
    
    //Attributes
    private bool inBossPhase = false;

	// Use this for initialization
    void Awake() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<ShipControl>();
    }
	void Start () {
        
        chunkManager = GameObject.FindGameObjectWithTag("ChunkManager").GetComponent<ChunkManager>();

        //Start
        StartCoroutine(TEMP_BossCooldown());
        chunkManager.CreateNewChuckSpecifier_Level(3, 3, 3);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Z) && !inBossPhase) {
            Debug.Log("Debug : Started Boss Phase");
            StopAllCoroutines();
            StartBossPhase();
        }
	}

    public void EndBossPhase() {
        inBossPhase = false;
        StartCoroutine(TEMP_BossCooldown());
        player.DeactivateTurretMode();
        chunkManager.CreateNewChuckSpecifier_Level(3, 3, 3);
    }

    public void StartBossPhase() {
        inBossPhase = true;
        StartCoroutine(StartBossCoroutine());
    }

    void StartSpawningBossTerrain() {
        chunkManager.CreateNewChuckSpecifier_Boss(1, 1, 1);
    }

    void SpawnBoss() {
        //instantiate boss
        //Vector3 targetPos = player.transform.position + new Vector3(0, 50, 250);
        Vector3 pos = player.transform.position;
        Instantiate(boss, pos, Quaternion.identity);
        //Activate turret
        player.ActivateTurretMode();
    }

    IEnumerator StartBossCoroutine() {
        StartSpawningBossTerrain();
        yield return new WaitForSeconds(5f);
        SpawnBoss();
    }

    IEnumerator TEMP_BossCooldown() {  //TEMP ::: StartCooldown till next boss, this will later be handled by a stat 
        yield return new WaitForSeconds(30f);
        StartCoroutine(StartBossCoroutine());
    }

}
