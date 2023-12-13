using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomInfo
{
    public string name;
    public int X;
    public int Y;
}

public class RoomController : MonoBehaviour
{
    public static RoomController instance;
    string currentWorldName = "Basement";
    RoomInfo currentLoadRoomData;
    Queue<RoomInfo> loadRoomQueue = new Queue<RoomInfo>();
    public List<Room> loadedRooms = new List<Room>();
    bool isLoadingRoom = false;
    public Room currRoom;
    public Room RoomStart;
    public Room RoomEnd;
    public Room RoomEmpty;
    public Room St;

    void Awake()
    {
        instance = this;
    }

    void FixedUpdate()
    {
        UpdateRoomQueue();
    }

    void UpdateRoomQueue()
    {
        if(isLoadingRoom)
        {
            return;
        }

        if(loadRoomQueue.Count == 0)
        {
            DoorCheck();
            return;
        }

        currentLoadRoomData = loadRoomQueue.Dequeue();
        foreach(Room room in loadedRooms)
        {
            if (currentLoadRoomData.X == room.X && currentLoadRoomData.Y == room.Y)
            {
                return;
            }
        }
        isLoadingRoom = true;
        StartCoroutine(LoadRoomRoutine(currentLoadRoomData));
    }

    void Start()
    {
        //LoadRoom("Start", 0, 0);
        //LoadRoom("Empty", 1, 0);
    }

    public void LoadRoom(string name, int x, int y)
    {
        if(DoesRoomExist(x,y))
        {
            return;
        }

        RoomInfo newRoomData = new RoomInfo();
        newRoomData.name = name;
        newRoomData.X = x;
        newRoomData.Y = y;

        loadRoomQueue.Enqueue(newRoomData);
    }

    IEnumerator LoadRoomRoutine(RoomInfo info)
    {
        string roomName = currentWorldName + info.name;

        Vector3 pos = new Vector3(info.X, info.Y, -5f);

        //AsyncOperation LoadRoom = SceneManager.LoadSceneAsync(roomName, LoadSceneMode.Additive);
        if(info.name == "Start")
        {
            St = (Room)GameObject.Instantiate(RoomStart, pos, Quaternion.identity);
            
        }
        else if(info.name == "End")
        {
            Instantiate(RoomEnd, pos, Quaternion.identity);
        }
        else
        {
            Instantiate(RoomEmpty, pos, Quaternion.identity);
        }
        CameraController.instance.currRoom = St;
        yield return null;
    }

    public void RegisterRoom(Room room)
    {

        if(!DoesRoomExist(currentLoadRoomData.X, currentLoadRoomData.Y))
        {
            room.transform.position = new Vector3(
            currentLoadRoomData.X * room.width, currentLoadRoomData.Y * room.height, 0);

            room.X = currentLoadRoomData.X;
            room.Y = currentLoadRoomData.Y;
            room.name = currentWorldName + "-" + currentLoadRoomData.name + " " + room.X + ", " + room.Y;

            room.transform.parent = transform;

            isLoadingRoom = false;

            if (loadedRooms.Count == 0 && currentLoadRoomData.name == "Start") //loadedRooms.Count == 0 &&
            {
                CameraController.instance.currRoom = room;
                Debug.Log(room.name + " x y " + room.X + room.Y);
                
            }

            loadedRooms.Add(room);
        }
        else
        {
            Destroy(room.gameObject);
            isLoadingRoom = false;
        }
    }

    public bool DoesRoomExist(int x, int y)
    {
        return loadedRooms.Find(item => item.X == x && item.Y == y) != null; 
    }

    public Room FindRoom(int x, int y)
    {
        return loadedRooms.Find(item => item.X == x && item.Y == y);
    }

    public void OnPlayerEnterRoom(Room room)
    {
        
        CameraController.instance.currRoom = room;
        currRoom = room;
        
    }

    public void DoorCheck()
    {
        
        for (int i = 0; i < loadedRooms.Count; i++)
        {
            loadedRooms[i].RemoveUnconnectedDoors();
        }
    }
        
}
