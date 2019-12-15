using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tileManager : MonoBehaviour
{
  public GameObject[] tilePrefabs;
  private Transform playerTransform;
  [SerializeField] private float spawnZ = -10.0f;
  [SerializeField] private float tileLength = 36;
  [SerializeField]private int amntOfTiles = 3;
  [SerializeField] private float safeZone = 15.0f;
  [SerializeField] private int bufferLength = 2;
  [SerializeField] private int aheadBufferLength = 2;
  private int lastPrefabInd = 0;
  private GameManager gameManager;

  private List<GameObject> activeTiles;
    // Start is called before the first frame update
    private void Start()
    {
      gameManager = GameManager.Instance;
      
      activeTiles = new List<GameObject>();
      playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

      GameObject tile = null;
      for(int i = 0; i < aheadBufferLength; i++){
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
      if(playerTransform.position.z - safeZone > (spawnZ - aheadBufferLength*tileLength)){
        SpawnTile();
        DeleteTile();
      }
    }

    private GameObject SpawnTile(){
      GameObject go;
      go = Instantiate(tilePrefabs[RandomPrefabInd()]) as GameObject;
      go.transform.SetParent(transform);
      go.transform.position = Vector3.forward*spawnZ;
      spawnZ += tileLength;

      activeTiles.Add(go);

      return go;
    }

    private void DeleteTile(){
      if (activeTiles.Count > bufferLength)
      {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0); 
      }
    }

    private int RandomPrefabInd(){
      if(tilePrefabs.Length <= 1){
        return 0;
      }

      int randInd = lastPrefabInd;
      while(randInd == lastPrefabInd){
        randInd =  Random.Range(0, tilePrefabs.Length-1);
      }

      lastPrefabInd = randInd;
      return randInd;
    }

}
