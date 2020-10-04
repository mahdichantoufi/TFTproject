using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    public ChampionPrepController championPrepController;
    public List<Vector3> SpawnPositions = new List<Vector3>();

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Spawner.position");
        getSpawningPoints();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)){
            Debug.Log("Pressed primary button.");
            Debug.Log(Input.mousePosition);
        } else if (Input.GetMouseButtonUp(0)){
            Debug.Log("Released primary button.");
            Debug.Log(Input.mousePosition);
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
