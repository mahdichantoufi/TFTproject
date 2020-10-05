using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    public ChampionPrepController championPrepController;
    public List<Vector3> SpawnPositions = new List<Vector3>();

    TerrainCollider terrainCollider;
    Vector3 worldPosition;
    Ray ray;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("start()");
       terrainCollider = Terrain.activeTerrain.GetComponent<TerrainCollider>();
        getSpawningPoints();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)){
            Debug.Log("Pressed primary button." + Input.mousePosition);
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitData;

            if(terrainCollider.Raycast(ray, out hitData, Mathf.Infinity)){
                worldPosition = hitData.point;
            }
            Debug.Log("RLC : "+ worldPosition);
        } else if (Input.GetMouseButtonUp(0)){
            Debug.Log("Released primary button." + Input.mousePosition);
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitData;

            if(terrainCollider.Raycast(ray, out hitData, 1000)){
                worldPosition = hitData.point;
            }
            Debug.Log("RLC : "+ worldPosition);
        }
    }
    public void setController(ChampionPrepController ChampionPrepController){
        this.championPrepController = ChampionPrepController;
    }

    private void getSpawningPoints(){
        Transform Spawner;
        Transform AllySpawnPositions = transform.Find("SpawnPositions");
        foreach (Transform AllySP in AllySpawnPositions)
        {
            Spawner = null;
            Spawner = AllySP.gameObject.transform;
            if (Spawner != null){
                SpawnPositions.Add(Spawner.position);
            }
        }
        // Debug.Log("SpawnPositions.forEach");
        // SpawnPositions.ForEach((pos) => Debug.Log(pos));
    }
}
