using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ArenaGen : MonoBehaviour
{
    public int width = 20;
    public int height = 20;
    [Range(0f, 1f)] public float obstacleChance = 0.1f;
    [SerializeField] GameObject[] bases;
    [SerializeField] GameObject[] exp;
    public TileBase wallTile;
    public TileBase obstacleTile;
    public TileBase floorTile;
    Tilemap tilemap;

    void Start()
    {
        tilemap = GetComponent<Tilemap>();
        GenerateArena();
        AddColliders();
        for (int i = 0; i < 4; i++)
        {
            Vector3 spawnPosition = GetSpawnPosition(i + 1);
            SpawnBase(i + 1, spawnPosition);
        }
    }

    void GenerateArena()
    {
        for (int x = -width / 2; x <= width / 2; x++)
        {
            for (int y = -height / 2; y <= height / 2; y++)
            {
                Vector3Int pos = new Vector3Int(x, y, 0);

                bool isCenter = (x == 0 && y == 0);

                if (x == -width / 2 || x == width / 2 || y == -height / 2 || y == height / 2)
                {
                    tilemap.SetTile(pos, wallTile);
                }
                else if (isCenter)
                {
                    tilemap.SetTile(pos, floorTile);
                }
                else
                {
                    if (Random.value < obstacleChance)
                        tilemap.SetTile(pos, obstacleTile);
                    else if (floorTile != null)
                        tilemap.SetTile(pos, floorTile);
                }
            }
        }
    }

    void AddColliders()
    {
        TilemapCollider2D tilemapCollider = this.GetComponent<TilemapCollider2D>();
        CompositeCollider2D composite = this.GetComponent<CompositeCollider2D>();
        Rigidbody2D rb = this.GetComponent<Rigidbody2D>();

        tilemapCollider.compositeOperation = TilemapCollider2D.CompositeOperation.Merge;
        tilemapCollider.usedByComposite = true;

        composite.geometryType = CompositeCollider2D.GeometryType.Polygons;
        composite.generationType = CompositeCollider2D.GenerationType.Synchronous;

        rb.bodyType = RigidbodyType2D.Static;
        rb.simulated = true;
    }
    Vector3 GetSpawnPosition(int teamNumber)
    {
        Vector3 spawnPosition = Vector3.zero;
        int attempts = 0;

        while (attempts < 10)
        {
            switch (teamNumber)
            {
                case 1:
                    spawnPosition = new Vector3(Random.Range(width / 4, width / 2) - 1, Random.Range(height / 4, height / 2), 0);
                    break;
                case 2:
                    spawnPosition = new Vector3(Random.Range(-width / 2, -width / 4) + 1, Random.Range(height / 4, height / 2), 0);
                    break;
                case 3:
                    spawnPosition = new Vector3(Random.Range(-width / 2, -width / 4) + 1, Random.Range(-height / 2, -height / 4) + 1, 0);
                    break;
                case 4:
                    spawnPosition = new Vector3(Random.Range(width / 4, width / 2) - 1, Random.Range(-height / 2, -height / 4) + 1, 0);
                    break;
            }
            attempts++;
        }

        return spawnPosition;
    }



    void SpawnBase(int i, Vector3 position)
    {
        GameObject basePrefab = bases[i - 1];
        GameObject newBase = Instantiate(basePrefab, position, Quaternion.identity);
        ClearPathToCenter(position);

        BillionBase baseScript = newBase.GetComponent<BillionBase>();
        if (baseScript != null)
        {
            switch (i)
            {
                case 1: 
                    baseScript.color = "green";
                    break;
                case 2:
                    baseScript.color = "blue";
                    break;
                case 3:
                    baseScript.color = "red";
                    break;
                case 4:
                    baseScript.color = "yellow";
                    break;
            }
        }

        GameObject expPrefab = exp[i - 1];
        if (expPrefab != null)
        {
            switch (i)
            {
                case 1:
                    baseScript.color = "green";
                    break;
                case 2:
                    baseScript.color = "green";
                    break;
                case 3:
                    baseScript.color = "green";
                    break;
                case 4:
                    baseScript.color = "green";
                    break;
            }
        }
    }
    void ClearPathToCenter(Vector3 startPosition)
    {
        Vector3Int start = tilemap.WorldToCell(startPosition);
        Vector3Int center = new Vector3Int(0, 0, 0);

        int x = start.x;
        int y = start.y;

        while (x != center.x)
        {
            tilemap.SetTile(new Vector3Int(x, y, 0), floorTile);
            x += (center.x > x) ? 1 : -1;
        }
        while (y != center.y)
        {
            tilemap.SetTile(new Vector3Int(x, y, 0), floorTile);
            y += (center.y > y) ? 1 : -1;
        }
    }
}
