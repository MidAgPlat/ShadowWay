using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot_Controller : MonoBehaviour
{
    public float lifeTime;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DeathDelay());
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator DeathDelay()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Enemy" || col.tag == "BOSS")
        {
            col.gameObject.GetComponent<EnemyController>().MinusHP();
            if (col.gameObject.GetComponent<EnemyController>().GetHP() == 0)
            {
                col.gameObject.GetComponent<EnemyController>().Death();
            }
        }
    }
}