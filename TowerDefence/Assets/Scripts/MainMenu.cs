using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject blackScreen;

    public void NewGameClicked()
    {
        StartCoroutine(FadeOut(2f));
    }

    private IEnumerator FadeOut(float waitTime)
    {
        // Do stuff
        GameObject soundObject = GameObject.FindGameObjectWithTag("audioPlayer");
        AudioPlayer audioPlayer = soundObject.GetComponent<AudioPlayer>();
        audioPlayer.FadeOutMusic();

        CanvasGroup canvasGroup = blackScreen.GetComponent<CanvasGroup>();

         while (canvasGroup.alpha < 1) {
            canvasGroup.alpha += Time.unscaledDeltaTime / waitTime;
 
            yield return null;
        }

        yield return new WaitForSecondsRealtime(waitTime);
        // Start game
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
