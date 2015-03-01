using UnityEngine;
using System.Collections;

public class TerrainFloor : Floor {

    Terrain ter;
	// Use this for initialization
	void Start () {
        ter = GetComponentInChildren<Terrain>();
        transform.position = new Vector3(middle.x,0,middle.y);
        ter.transform.localPosition = new Vector3(- size.x /2, 0,  - size.y/2);

        TerrainData td = Instantiate(ter.terrainData) as TerrainData;
        Vector3 scale = td.size;
        scale.x = this.size.x;
        scale.z = this.size.y;
        td.size = scale;

        updateTerrainData(ref td);

        ter.terrainData = td;
        GetComponentInChildren<TerrainCollider>().terrainData = td; ;
	}

    private void generateHeightMap()
    {
        TerrainData terData = ter.terrainData;

        int Tw = terData.heightmapWidth;
        int Th = terData.heightmapHeight;
        float[,] heightMap = terData.GetHeights(0, 0, Tw, Th);

        float acc = 0;
        int cpt = 0;
        for(int i = 0; i < Tw; i++)
        {
            acc += heightMap[i, 0];
            acc += heightMap[i, Th - 1];
            cpt += 2;
        }
        for(int i = 1; i < Th-1; i++)
        {
            acc += heightMap[0, i];
            acc += heightMap[Tw - 1, i];
            cpt += 2;
        }
        float mean = acc / cpt;

        for (int i = 0; i < Tw; i++)
        {
            heightMap[i, 0] = mean;
            heightMap[i, Th - 1] = mean;
        }
        for (int i = 1; i < Th - 1; i++)
        {
            heightMap[0, i] = mean;
            heightMap[Tw - 1, i] = mean;
        }

        terData.SetHeights(0, 0, heightMap);
        float offset = -mean * ter.terrainData.heightmapHeight;
        Debug.Log("Offset: " + offset.ToString());

        Vector3 pos = ter.transform.localPosition;
        pos.y = -mean * ter.terrainData.heightmapHeight;
        ter.transform.localPosition = pos;
    }
	
    private void updateTerrainData(ref TerrainData terData)
    {
            TerrainToolkit tk = GetComponentInChildren<TerrainToolkit>();

            tk.PerlinGenerator(5, 0.5f, 4, 1);
            generateHeightMap();
            tk.SmoothTerrain(1, 0.3f);
            generateHeightMap();
    }
}
