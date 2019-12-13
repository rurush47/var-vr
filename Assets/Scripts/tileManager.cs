using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tileManager : MonoBehaviour
{
  public GameObject[] tilePrefabs;
  private Transform playerTransform;
  private float spawnZ = -10.0f;
  private float tileLength = 10.0f;
  private int amntOfTiles = 3;
  private float safeZone = 15.0f;
  private int lastPrefabInd = 0;

  private List<GameObject> activeTiles;
    // Start is called before the first frame update
    private void Start()
    {
      activeTiles = new List<GameObject>();
      playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

      for(int i = 0; i < amntOfTiles; i++){
        SpawnTile();
      }

    }

    // Update is called once per frame
    void Update()
    {
      if(playerTransform.position.z - safeZone > (spawnZ - amntOfTiles*tileLength)){
        SpawnTile();
        DeleteTile();
      }
    }

    private void SpawnTile(){
      GameObject go;
      go = Instantiate(tilePrefabs[RandomPrefabInd()]) as GameObject;
      go.transform.SetParent(transform);
      go.transform.position = Vector3.forward*spawnZ;
      spawnZ += tileLength;

      activeTiles.Add(go);
    }

    private void DeleteTile(){
      Destroy(activeTiles[0]);
      activeTiles.RemoveAt(0);
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
