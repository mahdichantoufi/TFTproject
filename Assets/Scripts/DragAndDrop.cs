using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    public ChampionPrepController championPrepController;
    
    private TerrainCollider terrainCollider;
    private PlacementData playerPlacementData;
    private GameObject DraggedInstance;
    private bool isDragging = false;
    private GameObject fromSpawn;
    private GameObject toSpawn;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("start()");
        terrainCollider = Terrain.activeTerrain.GetComponent<TerrainCollider>();
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
                Debug.Log("button down but in bounds. " + clickWorldCoord);
                fromSpawn = checkNearestActiveSpawner(clickWorldCoord);
                if (fromSpawn != null)
                {
                    this.isDragging = true;
                }
                // TODO A : PlacementData.getSpawnActiveInstance()
                // this.DraggedInstance = playerPlacementData.getSpawnActiveInstance();
            } //else we do nothing 
        } else if (Input.GetMouseButtonUp(0) && isDragging){
            Vector3 clickWorldCoord = getWorldCoordonatesFromClick(Input.mousePosition);
            if ( clickWorldCoord.x != -1 && clickWorldCoord.y != -1 && clickWorldCoord.z != -1 )
            {
                toSpawn = checkNearestSpawner(clickWorldCoord);
                if (toSpawn != null)
                {
                    Debug.Log("button up but in bounds. " + clickWorldCoord);
                    Debug.Log(fromSpawn.name + " here 'from' position : " + fromSpawn.transform.position);
                    Debug.Log(toSpawn.name + " here 'To' position : " + toSpawn.transform.position);
                    championPrepController.switchChampionsInstances(fromSpawn, toSpawn);
                } else {
                    // TODO B2 : Bring character back to original position
                }
            } else
                // TODO B2 : Bring character back to original position
                Debug.Log("button up but out of bounds. ");
            this.isDragging = false;
            resetAfterSwitch();
                
        } else if (isDragging){
            Vector3 clickWorldCoord = getWorldCoordonatesFromClick(Input.mousePosition);
            if ( clickWorldCoord.x != -1 && clickWorldCoord.y != -1 && clickWorldCoord.z != -1 )
            {
                // TODO B1 : Move character while dragging
                // this.DraggedInstance.transform.position = clickWorldCoord;
                //Debug.Log("move character");
            }
        }
    }
    private void resetAfterSwitch(){
        this.fromSpawn = null;
        this.toSpawn = null;
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
    	GameObject checkNearestSpawner(Vector3 clickPos)
	{
    	float distanceToClosestSpawnPoint = 1000f;
        Transform closestSpawner = null;
        Transform allySpawnPositions = transform.Find("SpawnPositions");
        foreach (Transform AllySP in allySpawnPositions)
        {
            float distanceToclick = Vector3.Distance (AllySP.position, clickPos);
            if (distanceToclick < distanceToClosestSpawnPoint) {
                distanceToClosestSpawnPoint = distanceToclick;
                closestSpawner = AllySP;
            }

        }
        Transform allySubsSpawnPositions = transform.Find("SubsSpawnPositions");
        foreach (Transform AllySSP in allySubsSpawnPositions)
        {
            float distanceToclick = Vector3.Distance (AllySSP.position, clickPos);
            if (distanceToclick < distanceToClosestSpawnPoint) {
                distanceToClosestSpawnPoint = distanceToclick;
                closestSpawner = AllySSP;
            }
        }
        return (closestSpawner == null ? null : closestSpawner.gameObject);
	}
    public void setController(ChampionPrepController ChampionPrepController){
        this.championPrepController = ChampionPrepController;
    }
}
