using UnityEngine;
using UnityEngine.Tilemaps;

public class PlatformerMapGenerator : MonoBehaviour
{
    [Header("Map Settings")]
    public int width = 50;
    public int height = 10;
    public float noiseScale = 0.1f; //Q : 이거 수치의 정도 알아야 함.
    public int platformNum = 5;

    [Header("Tilemap & Tiles")]
    public Tilemap groundTilemap;
    public Tilemap objectTilemap;
    public TileBase groundTile;
    public TileBase platformTile;
    public TileBase obstacleTile;

    private void Start()
    {
        GenerateMap();
        GeneratePlatform();
    }


    private void GenerateMap()
    {
        GenerateGround();
    }

    private void GenerateGround()
    {
        for (int x = 0; x < width; x++)
        {
            int groundHeight = (x == 0)? 1 : Mathf.FloorToInt(Mathf.PerlinNoise(x * noiseScale, 0) * height);

            for (int y = 0; y < groundHeight; y++)
            {
                groundTilemap.SetTile(new Vector3Int(x, y, 0), groundTile);
            }
        }

        GameObject obj = Resources.Load<GameObject>("Prefabs/Player");
        Instantiate(obj, new Vector3(1, 5, 0), Quaternion.identity);
    }

    private void GeneratePlatform()
    {
        for (int i = 0; i < platformNum; i++)
        {
            int x = Random.Range(5, width - 5);
            int y = Random.Range(height / 2, height); 

            for (int j = 0; j < Random.Range(2, 6); j++) // 플랫폼 길이 (2~5칸)
            {
                objectTilemap.SetTile(new Vector3Int(x + j, y, 0), platformTile);
            }
        }
    }

    private void GenerateObstacle() 
    {

    }

}
