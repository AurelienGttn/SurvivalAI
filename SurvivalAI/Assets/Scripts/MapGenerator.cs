using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MapGenerator : MonoBehaviour {

    public Vector2 mapSize;
    public Transform tilePrefab;
    public Transform navmeshFloor;

    [SerializeField] private int freeSpaceFromCenter = 10;
    
    public Transform treePrefab;
    public int treeCount;
    public Transform bushPrefab;
    public int bushCount;
    public Transform rockPrefab;
    public int rockCount;
    
    List<Coord> allTileCoords;
    Queue<Coord> shuffledTileCoords;

    public int seed;

    private void Awake()
    {
        GenerateMap();
    }

    public void GenerateMap()
    {
        seed = Random.Range(0, 100);
        allTileCoords = new List<Coord>();
        for (int x = 0; x < mapSize.x; x++)
        {
            for (int y = 0; y < mapSize.y; y++)
            {
                allTileCoords.Add(new Coord(x, y));
            }
        }
        shuffledTileCoords = new Queue<Coord>(Utility.ShuffleArray(allTileCoords.ToArray(), seed));

        string holderName = "Generated Map";

        if (transform.Find(holderName))
            DestroyImmediate(transform.Find(holderName).gameObject);

        Transform mapHolder = new GameObject(holderName).transform;
        mapHolder.parent = transform;

        // Create ground with only one tile
        // The grid is still here but it's lighter for the CPU
        Transform ground = Instantiate(tilePrefab, Vector3.zero, Quaternion.Euler(Vector3.right * 90));
        ground.localScale = new Vector3(mapSize.x, mapSize.y, 1);
        ground.parent = mapHolder;
        
        /* ###### TOO HEAVY ###### //
        // Create the base map
                
                for (int x = 0; x < mapSize.x; x++)
                {
                    for (int y = 0; y < mapSize.y; y++)
                    {
                        Vector3 tilePosition = CoordToPosition(x, y);
                        Transform newTile = Instantiate(tilePrefab, tilePosition, Quaternion.Euler(Vector3.right * 90)) as Transform;
                        newTile.localScale = Vector3.one;
                        newTile.parent = mapHolder;
                    }
                }
        ###### TOO HEAVY ###### */


        // Place resources
        // Wood
        for (int i = 0; i < treeCount; i++)
        {
            Coord randomCoord = GetRandomCoord();
            // Make sure they don't go in the center
            while (randomCoord.x > Mathf.Floor(mapSize.x / 2) - freeSpaceFromCenter && randomCoord.x < Mathf.Floor(mapSize.x / 2) + freeSpaceFromCenter
                && randomCoord.y > Mathf.Floor(mapSize.y / 2) - freeSpaceFromCenter && randomCoord.y < Mathf.Floor(mapSize.y / 2) + freeSpaceFromCenter)
            {
                randomCoord = GetRandomCoord();
            }

            Vector3 obstaclePosition = CoordToPosition(randomCoord.x, randomCoord.y);
            Transform newTree = Instantiate(treePrefab, obstaclePosition + Vector3.up * 0.75f, Quaternion.identity) as Transform;
            newTree.parent = mapHolder;
        }

        // Berries
        for (int i = 0; i < bushCount; i++)
        {
            Coord randomCoord = GetRandomCoord();
            // Make sure they don't go in the center
            while (randomCoord.x > Mathf.Floor(mapSize.x / 2) - freeSpaceFromCenter && randomCoord.x < Mathf.Floor(mapSize.x / 2) + freeSpaceFromCenter
                && randomCoord.y > Mathf.Floor(mapSize.y / 2) - freeSpaceFromCenter && randomCoord.y < Mathf.Floor(mapSize.y / 2) + freeSpaceFromCenter)
            {
                randomCoord = GetRandomCoord();
            }
            Vector3 obstaclePosition = CoordToPosition(randomCoord.x, randomCoord.y);
            Transform newBush = Instantiate(bushPrefab, obstaclePosition + Vector3.up * 0.5f, Quaternion.identity) as Transform;
            newBush.parent = mapHolder;
        }

        // Rock
        for (int i = 0; i < rockCount; i++)
        {
            Coord randomCoord = GetRandomCoord();
            // Make sure they don't go in the center
            while (randomCoord.x > Mathf.Floor(mapSize.x / 2) - freeSpaceFromCenter && randomCoord.x < Mathf.Floor(mapSize.x / 2) + freeSpaceFromCenter
                && randomCoord.y > Mathf.Floor(mapSize.y / 2) - freeSpaceFromCenter && randomCoord.y < Mathf.Floor(mapSize.y / 2) + freeSpaceFromCenter)
            {
                randomCoord = GetRandomCoord();
            }
            Vector3 obstaclePosition = CoordToPosition(randomCoord.x, randomCoord.y);
            Transform newRock = Instantiate(rockPrefab, obstaclePosition, Quaternion.identity) as Transform;
            newRock.parent = mapHolder;
        }

        navmeshFloor.localScale = new Vector3(mapSize.x, mapSize.y, 1);
    }

    Vector3 CoordToPosition(int x, int y)
    {
        return new Vector3(-mapSize.x / 2 + 0.5f + x, 0, -mapSize.y / 2 + 0.5f + y);
    }

    public Coord GetRandomCoord()
    {
        Coord randomCoord = shuffledTileCoords.Dequeue();
        shuffledTileCoords.Enqueue(randomCoord);

        return randomCoord;
    }

    public struct Coord
    {
        public int x;
        public int y;

        public Coord(int _x, int _y) {
            x = _x;
            y = _y;
        }
    }
}
