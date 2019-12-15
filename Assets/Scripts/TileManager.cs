using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public GameObject[] tilePrefabs;
    private Transform playerTransform;
    [SerializeField] private float spawnZ = -10.0f;
    [SerializeField] private float tileLength = 36;
    [SerializeField] private int amntOfTiles = 3;
    [SerializeField] private float safeZone = 15.0f;
    [SerializeField] private int bufferLength = 2;
    [SerializeField] private int aheadBufferLength = 2;
    private int lastPrefabInd = 0;
    private GameManager gameManager;

    private List<GameObject> activeTiles;
    private TileGrid tileGrid;

    // Start is called before the first frame update
    private void Start()
    {
        gameManager = GameManager.Instance;

        activeTiles = new List<GameObject>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        tileGrid = new TileGrid(3, 9, tileLength);

        GameObject tile = null;
        for (int i = 0; i < aheadBufferLength; i++)
        {
            tile = SpawnTile();
        }

        AssignSpawnedTiles(tile);
    }

    private void AssignSpawnedTiles(GameObject go)
    {
        if (go == null)
        {
            return;
        }

        int i = 0;
        foreach (Transform child in go.gameObject.transform)
        {
            gameManager.tracksPositions[i] = child.position;
            i++;

            if (i > 2)
            {
                break;
            }
        }

        //TODO hack XD
//      var tmp = gameManager.tracksPositions[0];
//      gameManager.tracksPositions[0] = gameManager.tracksPositions[1];
//      gameManager.tracksPositions[1] = tmp;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTransform.position.z - safeZone > (spawnZ - aheadBufferLength * tileLength))
        {
            SpawnTile();
            DeleteTile();
        }
    }

    private GameObject SpawnTile()
    {
        GameObject go;
        go = Instantiate(tilePrefabs[RandomPrefabInd()]) as GameObject;
        go.transform.SetParent(transform);
        go.transform.position = Vector3.forward * spawnZ;
        spawnZ += tileLength;

        activeTiles.Add(go);

        SpawnObjectsOnTile(tileGrid);
        
        return go;
    }

    private void DeleteTile()
    {

        if (tileItems.ContainsKey(activeTiles[0]))
        {
            foreach (var item in tileItems[activeTiles[0]])
            {
                Destroy(item);   
            }

            tileItems.Remove(activeTiles[0]);    
        }
            
        if (activeTiles.Count > bufferLength)
        {
            Destroy(activeTiles[0]);
            activeTiles.RemoveAt(0);
        }
    }

    private int RandomPrefabInd()
    {
        if (tilePrefabs.Length <= 1)
        {
            return 0;
        }

        int randInd = lastPrefabInd;
        while (randInd == lastPrefabInd)
        {
            randInd = Random.Range(0, tilePrefabs.Length - 1);
        }

        lastPrefabInd = randInd;
        return randInd;
    }

    #region ObjectsOnTile

    private Dictionary<GameObject, List<GameObject>> tileItems = new Dictionary<GameObject, List<GameObject>>();

    [SerializeField] private GameObject[] spawnItemPrefabs;
    public class TileGrid
    {
        [SerializeField] private float tileXOffset = -3.5f;
        
        public Vector3[,] posArray;
        public TileGrid(int x, int z, float tileLength)
        {
            float tileDiff = tileLength / z;
            
            posArray = new Vector3[x, z];
            for (int i = 0; i < posArray.GetLength(0); i++)
            {
                for (int j = 0; j < posArray.GetLength(1); j++)
                {
                    posArray [i, j] = new Vector3(
                        (i-1)*tileXOffset,
                        0 ,
                        tileDiff/2 - (tileDiff - j*tileDiff));
                }
            }
            
        }
    }
    
    private void SpawnObjectsOnTile(TileGrid tileGrid)
    {
        GameObject goPrefab = new GameObject();
    
        for (int j = 0; j < tileGrid.posArray.GetLength(1); j+=3)
        {
            //choose random track
            int i = Random.Range(0, tileGrid.posArray.GetLength(0));
            
            //choose random item to spawn
            GameObject randomItem = spawnItemPrefabs[Random.Range(0, spawnItemPrefabs.Length)];

            GameObject newObj = Instantiate(randomItem);

            newObj.transform.position = new Vector3(
                tileGrid.posArray[i, j + 1].x,
                newObj.transform.position.y,
                (spawnZ - tileLength) + tileGrid.posArray[i, j].z);
            
            //not very tidy - buut this is fast programming - assign object to the tile
            var tile = activeTiles.Last();
            if (tileItems.ContainsKey(tile))
            {
                tileItems[tile].Add(newObj);
            }
            else
            {
                tileItems.Add(tile, new List<GameObject>());
                tileItems[tile].Add(newObj);
            }
        }
    }

    #endregion
}