using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    public ChampionPrepController championPrepController;
    public List<Vector3> SpawnPositions = new List<Vector3>();
    
    private TerrainCollider terrainCollider;
    private PlacementData playerPlacementData;
    private GameObject DraggedInstance;
    private string nameSpawnFrom = "";
    private string nameSpawnTo = "";
    private bool isDragging = false;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("start()");
        terrainCollider = Terrain.activeTerrain.GetComponent<TerrainCollider>();
        getSpawningPoints();
        playerPlacementData = GameObject.FindWithTag("GameManager")
            .transform.GetComponent<GameManager>()
                .GetPlayer().GetPlacementData();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isDragging){
            Vector3 clickWorldCoord = getWorldCoordonatesFromClick(Input.mousePosition);
            if ( clickWorldCoord.x != -1 && clickWorldCoord.y != -1 && clickWorldCoord.z != -1 )
            {
                this.isDragging = true;
                this.nameSpawnFrom = checkNearestActiveSpawner(clickWorldCoord).name;
                // TODO : PlacementData.getSpawnActiveInstance()
                // this.DraggedInstance = playerPlacementData.getSpawnActiveInstance();
            } //else we do nothing 
        } else if (Input.GetMouseButtonUp(0) && isDragging){
            this.isDragging = false;
            Vector3 clickWorldCoord = getWorldCoordonatesFromClick(Input.mousePosition);
            if ( clickWorldCoord.x != -1 && clickWorldCoord.y != -1 && clickWorldCoord.z != -1 )
            {
                this.nameSpawnTo = checkNearestActiveSpawner(clickWorldCoord).name;
                playerPlacementData.switchChampionsInstances(nameSpawnFrom, nameSpawnTo);
            } else
                Debug.Log("button up but out of bounds. ");
                // Button down out of bounds && Input.GetMouseButtonDown(0) && !isDragging
                // TODO : Get character back to its original place
        } else if (isDragging){
            Vector3 clickWorldCoord = getWorldCoordonatesFromClick(Input.mousePosition);
            if ( clickWorldCoord.x != -1 && clickWorldCoord.y != -1 && clickWorldCoord.z != -1 )
            {
                // TODO : Move character while dragging
                // this.DraggedInstance.transform.position = clickWorldCoord;
                //Debug.Log("move character");
            }
        }
    }
    private Vector3 getWorldCoordonatesFromClick(Vector3 mousePosition){
        Ray ray;
        ray = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit hitData;

        if(terrainCollider.Raycast(ray, out hitData, Mathf.Infinity)){
            return hitData.point;
        }
        return new Vector3(-1, -1, -1);
    }
    
	GameObject checkNearestActiveSpawner(Vector3 clickPos)
	{
    	float distanceToClosestSpawnPoint = 10f;
        Transform closestSpawner = null;
        Transform allySubsSpawnPositions = transform.Find("SubsSpawnPositions");
        foreach (Transform AllySSP in allySubsSpawnPositions)
        {
            float distanceToclick = Vector3.Distance (AllySSP.position, clickPos);
            if (distanceToclick < distanceToClosestSpawnPoint) {
                ChampionSpawner championSpawner = AllySSP.gameObject.GetComponent<ChampionSpawner>();
                if (championSpawner != null && championSpawner.spawnedChampionIsDraggable())
                {
                    distanceToClosestSpawnPoint = distanceToclick;
                    closestSpawner = AllySSP;
                }
            }
        }
        Transform allySpawnPositions = transform.Find("SpawnPositions");
        foreach (Transform AllySP in allySpawnPositions)
        {
            float distanceToclick = Vector3.Distance (AllySP.position, clickPos);
            if (distanceToclick < distanceToClosestSpawnPoint) {
                ChampionSpawner championSpawner = AllySP.gameObject.GetComponent<ChampionSpawner>();
                if (championSpawner != null && championSpawner.spawnedChampionIsDraggable())
                {
                    distanceToClosestSpawnPoint = distanceToclick;
                    closestSpawner = AllySP;
                }
            }

        }
        return (closestSpawner == null ? null : closestSpawner.gameObject);
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
