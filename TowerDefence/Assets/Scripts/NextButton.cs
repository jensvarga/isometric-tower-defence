using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextButton : MonoBehaviour
{
    private bool swap = true;
    private ScoreKeeper scoreKeeper;
    private HighScore highScore;

    void Awake()
    {
        var obje = GameObject.FindGameObjectWithTag("highScore");
        if (obje != null) highScore = obje.GetComponent<HighScore>();

        var obj = GameObject.FindGameObjectWithTag("scoreKeeper");
        if (obj != null) scoreKeeper = obj.GetComponent<ScoreKeeper>();
    }

    void Update()
    {
        if (this.transform.rotation.z > 0.7 && swap)
        {
            swap = false;
        }

        if (this.transform.rotation.z < 0.3f && !swap)
        {
            swap = true;
        }

        if (swap)
        {
            this.transform.Rotate(0f, 0f, -10f * Time.unscaledDeltaTime);
        }
        else
        {
            this.transform.Rotate(0f, 0f, 10f * Time.unscaledDeltaTime);
        }
    }

    void OnMouseOver()
    {
        scoreKeeper.info = "Go to next level";
    }

    void OnMouseExit()
    {
        scoreKeeper.info = "";
    }

    public void ClickNextButton()
    {
        int score = scoreKeeper.score;

        GameObject soundObject = GameObject.FindGameObjectWithTag("audioPlayer");
        if (soundObject != null)
        {
            AudioPlayer audioPlayer = soundObject.GetComponent<AudioPlayer>();
            if (audioPlayer != null) audioPlayer.FadeOutMusic();
        }

        highScore.gameObject.SetActive(true);
        scoreKeeper.SetHighScore(score, SceneManager.GetActiveScene().buildIndex);
        highScore.GetHighScore(SceneManager.GetActiveScene().buildIndex, score);
        Time.timeScale = 0;
    }
}
