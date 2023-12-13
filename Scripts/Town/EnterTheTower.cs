using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterTheTower : MonoBehaviour
{
   /* public GameObject TowerEnter;
    public GameObject entityToSpawn;

    public MenuSO spawnManagerValues;*/

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("okokoklalala");
            /*GameObject currentEntity = Instantiate(entityToSpawn, spawnManagerValues.pos, Quaternion.identity, transform) as GameObject;
            currentEntity.name = spawnManagerValues.prefabName;*/
            SceneManager.LoadScene("BasementMain", LoadSceneMode.Single);

        }
    }
}
