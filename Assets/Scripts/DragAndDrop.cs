using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private ChampionPrepController championPrepController;
    private Transform transform;

    private TerrainCollider terrainCollider;
    private PlacementData playerPlacementData;
    private GameObject DraggedInstance;
    private Vector3 DraggedChampOriginalPosition;
    private bool isDragging = false;
    private GameObject fromSpawn;
    private GameObject toSpawn;

    // Start is called before the first frame update
    void Start()
    {
        championPrepController = GameManager.instance.GetChampionPrepController();
        transform = GameManager.instance.transform;
        terrainCollider = Terrain.activeTerrain.GetComponent<TerrainCollider>();
        playerPlacementData = GameManager.instance.GetPlayer().GetPlacementData();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isDragging){
            Vector3 clickWorldCoord = getWorldCoordonatesFromClick(Input.mousePosition);
            if ( clickWorldCoord.x != -1 && clickWorldCoord.y != -1 && clickWorldCoord.z != -1 )
            {
                fromSpawn = checkNearestActiveSpawner(clickWorldCoord);
                if (fromSpawn != null)
                {
                    this.isDragging = true;
                    this.DraggedInstance = playerPlacementData.GetSpawnActiveInstance(fromSpawn.name);
                    this.DraggedChampOriginalPosition = this.DraggedInstance.transform.position;
                }
            } //else we do nothing 
        } 
        else if (Input.GetMouseButtonUp(0) && isDragging){
            this.isDragging = false;
            Vector3 clickWorldCoord = getWorldCoordonatesFromClick(Input.mousePosition);
            if ( clickWorldCoord.x != -1 && clickWorldCoord.y != -1 && clickWorldCoord.z != -1 )
            {
                toSpawn = checkNearestSpawner(clickWorldCoord);
                if (toSpawn != null)
                {
                    if (!championPrepController.switchChampionsInstances(fromSpawn, toSpawn)){
                        this.DraggedInstance.transform.position = this.DraggedChampOriginalPosition;
                    }
                } else {
                    this.DraggedInstance.transform.position = this.DraggedChampOriginalPosition;
                }
            } else
                this.DraggedInstance.transform.position = this.DraggedChampOriginalPosition;
            resetAfterSwitch();
                
        } 
        else if (isDragging){
            Vector3 clickWorldCoord = getWorldCoordonatesFromClick(Input.mousePosition);
            if ( clickWorldCoord.x != -1 && clickWorldCoord.y != -1 && clickWorldCoord.z != -1 )
            {
                this.DraggedInstance.transform.position = clickWorldCoord;
            }
        }
    }
    private void resetAfterSwitch(){
        this.fromSpawn = null;
        this.toSpawn = null;
        this.DraggedChampOriginalPosition = new Vector3(-1, -1, -1);
    }
    private Vector3 getWorldCoordonatesFromClick(Vector3 mousePosition){
        Ray ray;
        ray = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit hitData;

        if(terrainCollider != null && terrainCollider.Raycast(ray, out hitData, Mathf.Infinity)){
            return hitData.point;
        }
        return new Vector3(-1, -1, -1);
    }
	private GameObject checkNearestActiveSpawner(Vector3 clickPos)
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
    private GameObject checkNearestSpawner(Vector3 clickPos)
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
