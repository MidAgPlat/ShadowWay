using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Door : MonoBehaviour
{
    public enum DoorType
    {
        up,
        left,
        down,
        right
    };

    public DoorType doorType;

}
