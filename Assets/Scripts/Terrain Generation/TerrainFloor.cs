using UnityEngine;
using System.Collections;

public class TerrainFloor : Floor {

    Terrain ter;
    public float mean;
    public float offset;
	
    // Use this for initialization

    void Awake()
    {
        
    }

	void Start () {
        ter = GetComponentInChildren<Terrain>();
        transform.position = new Vector3(middle.x, 0, middle.y);
        ter.transform.localPosition = new Vector3(-size.x / 2, 0, -size.y / 2);

        TerrainData td = Instantiate(ter.terrainData) as TerrainData;

        ter.terrainData = td;
        GetComponentInChildren<TerrainCollider>().terrainData = td; ;

        Vector3 scale = td.size;
        scale.x = this.size.x;
        scale.z = this.size.y;
        td.size = scale;

        updateTerrainData();
        
	}

    private void generateHeightMap()
    {
        TerrainData terData = ter.terrainData;

        int Tw = terData.heightmapWidth;
        int Th = terData.heightmapHeight;
        float[,] heightMap = terData.GetHeights(0, 0, Tw, Th);

        float acc = 0;
        int cpt = 0;
        for (int i = 0; i < Tw; i++)
        {
            for(int j = 0 ;j < Th ; j++)
            {
                if(i < 2 || i > Tw - 3 || j < 2 || j > Th -3)
                {
                    acc += heightMap[i, j];
                    cpt++;
                }
            }
        }

        mean = acc / cpt;
        for (int i = 0; i < Tw; i++)
        {
            for (int j = 0; j < Th; j++)
            {
                if (i < 2 || i > Tw - 3 || j < 2 || j > Th - 3)
                {
                    heightMap[i, j] = mean;
                }
            }
        }

        terData.SetHeights(0, 0, heightMap);
        offset = -mean * terData.size.y;

        Vector3 pos = ter.transform.position;
        pos.y = offset;
        ter.transform.position = pos;
    }
	
    private void updateTerrainData()
    {
            TerrainToolkit tk = GetComponentInChildren<TerrainToolkit>();

            tk.PerlinGenerator(5, 0.5f, 4, 1);
            generateHeightMap();
            tk.SmoothTerrain(1, 0.3f);
            generateHeightMap();
    }
}
