using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartGameButton : MonoBehaviour
{
    public Button yourButton;

    void Start()
    {

    }

    public void TaskOnClick()
    {
        Debug.Log("You have clicked the button!");
        SceneManager.LoadScene("ShadowTown", LoadSceneMode.Single);
        
    }

    public void Exit()
    {
        #if UNITY_EDITOR
             UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
    }
}
