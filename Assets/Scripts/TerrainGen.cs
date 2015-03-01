using UnityEngine;
using System.Collections;

public class TerrainGen : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Terrain tc = GetComponent<Terrain>();
        

        TerrainData td = tc.terrainData;

        updateTerrainData(ref td);

        tc.terrainData = td;
	}

    private void generateHeightMap()
    {
        Terrain ter = (Terrain)GetComponent(typeof(Terrain));
        if (ter == null)
        {
            return;
        }
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
        Vector3 pos = transform.position;
        pos.y = -mean * ter.terrainData.heightmapHeight;
        transform.position = pos;
    }
	
    private void updateTerrainData(ref TerrainData terData)
    {
            TerrainToolkit tk = GetComponent<TerrainToolkit>();

            tk.PerlinGenerator(5, 0.5f, 4, 1);
            generateHeightMap();
            tk.SmoothTerrain(1, 0.3f);
            generateHeightMap();
    }

	// Update is called once per frame
	void Update () {
	
	}
}
