using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void NewGameClicked()
    {
        Debug.Log("New Game");
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (SceneManager.sceneCountInBuildSettings > nextSceneIndex)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
    }

    public void SelectLevelClicked()
    {
        
    }

    public void OptionsClicked()
    {
        
    }

    public void ExitClicked()
    {
        Debug.Log("Exit game");
        Application.Quit();
    }
}
