using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PauseButton : MonoBehaviour
{

    public void ClickedResumeButton()
    {
        GameObject girdObj = GameObject.FindGameObjectWithTag("levelGrid");
        if (girdObj != null)
        {
            LevelGrid levelGrid = girdObj.GetComponent<LevelGrid>();
            if (levelGrid != null) levelGrid.ResumeGame();
        }
    }

    public void ClickedRestart()
    {
        GameObject soundObject = GameObject.FindGameObjectWithTag("audioPlayer");
        if (soundObject != null)
        {
            AudioPlayer audioPlayer = soundObject.GetComponent<AudioPlayer>();
            if (audioPlayer != null)
            {
                audioPlayer.StopMusic();
                audioPlayer.StartMusic();
            }
        }
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneIndex);
    }

    public void ClickedExitButton()
    {
        Debug.Log("Exit game");
        Application.Quit();
    }
}
