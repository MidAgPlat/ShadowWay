using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public int width;
    public int height;
    public int X;
    public int Y;
    //public Vector3 pos;

    public Door leftDoor;
    public Door rightDoor;
    public Door upDoor;
    public Door downDoor;

    List<Door> doors = new List<Door>();
    // Start is called before the first frame update
    void Start()
    {
        if(RoomController.instance == null)
        {
            Debug.Log("WrongScene");
            return;
        }

        Door[] ds = GetComponentsInChildren<Door>();
        foreach(Door d in ds)
        {
            doors.Add(d);
            switch(d.doorType)
            {
                case Door.DoorType.right:
                    rightDoor = d;
                    break;
                case Door.DoorType.left:
                    leftDoor = d;
                    break;
                case Door.DoorType.up:
                    upDoor = d;
                    break;
                case Door.DoorType.down:
                    downDoor = d;
                    break;
            }
        }

        RoomController.instance.RegisterRoom(this);
    }

    public void RemoveUnconnectedDoors()
    {
        foreach(Door door in doors)
        {
            switch (door.doorType)
            {
                case Door.DoorType.right:
                    if(GetRight() == null)
                    {
                        door.gameObject.GetComponent<SpriteRenderer>().GetComponent<Renderer>().enabled = false;
                        door.gameObject.AddComponent<BoxCollider2D>();
                        
                    }
                    break;
                case Door.DoorType.left:
                    if (GetLeft() == null)
                    {
                        door.gameObject.GetComponent<SpriteRenderer>().GetComponent<Renderer>().enabled = false;
                        door.gameObject.AddComponent<BoxCollider2D>();
                    }
                    break;
                case Door.DoorType.up:
                    if (GetUp() == null)
                    {
                        door.gameObject.GetComponent<SpriteRenderer>().GetComponent<Renderer>().enabled = false;
                        door.gameObject.AddComponent<BoxCollider2D>();
                    }
                    break;
                case Door.DoorType.down:
                    if (GetDown() == null)
                    {
                        door.gameObject.GetComponent<SpriteRenderer>().GetComponent<Renderer>().enabled = false;
                        door.gameObject.AddComponent<BoxCollider2D>();
                    }
                    break;
            }
        }
        
        //pos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }


    public Room GetRight()
    {
        if (RoomController.instance.DoesRoomExist(X + 1, Y))
        {
            return RoomController.instance.FindRoom(X + 1, Y);
        }
        else
        {
            return null;
        }
    }
    public Room GetLeft()
    {
        if (RoomController.instance.DoesRoomExist(X - 1, Y))
        {
            return RoomController.instance.FindRoom(X - 1, Y);
        }
        else
        {
            return null;
        }
    }
    public Room GetUp()
    {
        if (RoomController.instance.DoesRoomExist(X, Y + 1))
        {
            return RoomController.instance.FindRoom(X, Y + 1);
        }
        else
        {
            return null;
        }
    }
    public Room GetDown()
    {
        if (RoomController.instance.DoesRoomExist(X, Y - 1))
        {
            return RoomController.instance.FindRoom(X, Y - 1);
        }
        else
        {
            return null;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height, 0));
    }

    public Vector3 GetRoomCentre()
    {
        return new Vector3(X * width, Y * height);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" && (other.transform.position.x != 0 && other.transform.position.y != 0))
        {
            RoomController.instance.OnPlayerEnterRoom(this);
        }

        Debug.Log("WTF" + other.tag + other);
    }
}

