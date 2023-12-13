using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            Move_WASD.SetShot();
            Destroy(gameObject);
        }
    }
}
