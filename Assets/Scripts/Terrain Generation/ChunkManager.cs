using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChunkManager : MonoBehaviour {

    public TerrainGenerator generator;
    public List<Chunk> PossibleChunks;
    public List<Chunk> PossibleBossChunks;

    //TEMP :: 
    public GameObject boss;
    private ShipControl player;

	// Use this for initialization
	void Start () {

        //TEMP  
        player = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<ShipControl>();
        StartCoroutine(TEMP_BossCooldown());
        CreateNewChuckSpecifier_Level(3, 3, 3);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    /// <summary>
    /// Creates a new list of chunks to be added to the terrain generator
    /// </summary>
    /// <param name="amt_pull">Amount of pull from the possible chunks. This get overridden if result doesn't fit min or max parameters</param>
    /// <param name="min">Minimum Amount of different chunks possible</param>
    /// <param name="max">Maximum Amount of different chunks possible</param>
 
    public void CreateNewChuckSpecifier_Level(int amt_pull, int min, int max) { 
        List<Chunk> newChunkList = new List<Chunk>();
        int attempt = 0;
        //Create new chunk list based on parameters
        while((attempt < amt_pull || newChunkList.Count < min) && newChunkList.Count < max) {
            attempt++;
            AddChunckToList(ref newChunkList, PossibleChunks);
        }
        //Add newly created ChunkList to generator
        generator.ChunkSpecifier = newChunkList;

        //TEMP
        generator.ChunkSpecifier = PossibleChunks;
    }

    public void CreateNewChuckSpecifier_Boss(int amt_pull, int min, int max) {
        List<Chunk> newChunkList = new List<Chunk>();
        int attempt = 0;
        //Create new chunk list based on parameters
        while ((attempt < amt_pull || newChunkList.Count < min) && newChunkList.Count < max) {
            attempt++;
            AddChunckToList(ref newChunkList, PossibleBossChunks);
        }
        //Add newly created ChunkList to generator
        generator.ChunkSpecifier = newChunkList;

    }


    void AddChunckToList(ref List<Chunk> newList, List<Chunk> allChunks) {
        int n = Random.Range(0,allChunks.Count-1);
        newList.Add(allChunks[n]);
    }

    public void BossDeath() {
        //TEMP ::: StartCooldown till next boss, this will later be handled by a stat 
        StartCoroutine(TEMP_BossCooldown());
        player.DeactivateTurretMode();
        CreateNewChuckSpecifier_Level(3, 3, 3);
    }

    IEnumerator TEMP_BossCooldown() {
        yield return new WaitForSeconds(30f);
        CreateNewChuckSpecifier_Boss(1, 1, 1);
        yield return new WaitForSeconds(5f);
        //instantiate boss
        Vector3 pos = player.transform.position + new Vector3(0, 50, 150);
        Instantiate(boss,pos,Quaternion.identity);
        //Activate turret
        player.ActivateTurretMode();
    }
}
