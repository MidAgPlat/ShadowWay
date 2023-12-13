using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    public Room room;

    public Grid grid;

    public List<Vector2> availablePoints = new List<Vector2>();

    public float verticalOffset, horizontalOffset;

    public ItemRoomSpawner spawner;

    public struct Grid
    {
        public int colomns, rows;

    }

    void Awake()
    {
        room = GetComponentInParent<Room>();
        grid.colomns = room.width - 5;
        grid.rows = room.height - 4;
        GenerateGrid();
    }

    public void GenerateGrid()
    {
        verticalOffset += room.transform.localPosition.y;
        horizontalOffset += room.transform.localPosition.x;

        for(int y = 0; y < grid.rows; y++)
        {
            for (int x = 0; x < grid.colomns; x++)
            {
                availablePoints.Add(new Vector2(x - (grid.colomns - horizontalOffset), y - (grid.rows - verticalOffset)));
            }
        }
        spawner.SpawnObjects();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
