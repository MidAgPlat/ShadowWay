using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeBuilding : MonoBehaviour
{
    int i = 0;
    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            this.gameObject.transform.GetChild(2).gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    void Update()
    {
        if (i==0 && Input.GetAxis("Jump") > 0)
        {
            i++;
            this.gameObject.transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().enabled = true;
            Destroy(this.gameObject.transform.GetChild(0).gameObject);
            Destroy(this.gameObject.transform.GetChild(2).gameObject);
        }
        
    }
}
