using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRoomSpawner : MonoBehaviour
{

    public string name;
    public SpawnerData data;
    public GridController grid;

    void Start()
    {
        grid = GetComponentInParent<GridController>();
    }

    public void SpawnObjects ()
    {
        int randomIteration = Random.Range(data.minSpawn, data.maxSpawn + 1);
        
        for(int i = 0; i < randomIteration; i++)
        {
            int randomPos = Random.Range(0, grid.availablePoints.Count - 3);
            GameObject go = Instantiate(data.itemToSpawn, grid.availablePoints[randomPos], Quaternion.identity, transform) as GameObject;
            grid.availablePoints.RemoveAt(randomPos);
        }
    }

}
