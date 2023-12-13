using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TowerEnterMenu : MonoBehaviour
{
    public Button yourButton;

    void Start()
    {

    }

    public void Yes()
    {
        Debug.Log("AAAAAA");
        SceneManager.LoadScene("Basement Main", LoadSceneMode.Single);
    }

    public void No()
    {
        Destroy(this);
    }
}
