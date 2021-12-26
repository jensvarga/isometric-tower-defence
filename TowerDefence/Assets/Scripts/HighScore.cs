using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class HighScore : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI top1Label;
    [SerializeField] private TextMeshProUGUI top2Label;
    [SerializeField] private TextMeshProUGUI top3Label;
    [SerializeField] private TextMeshProUGUI top4Label;
    [SerializeField] private TextMeshProUGUI top5Label;

    public void GetHighScore(int level, int score)
    {
        string lev = level.ToString();
        top1Label.text = "1. " + PlayerPrefs.GetInt("Top1" + lev, 1000).ToString();
        if (PlayerPrefs.GetInt("Top1" + lev) == score) 
        {
            top1Label.color = Color.green;
        } else {
            top1Label.color = Color.white;
        }

        top2Label.text = "2. " + PlayerPrefs.GetInt("Top2" + lev, 800).ToString();
        if (PlayerPrefs.GetInt("Top2" + lev) == score) 
        {
            top2Label.color = Color.green;
        } else {
            top2Label.color = Color.white;
        }

        top3Label.text = "3. " + PlayerPrefs.GetInt("Top3" + lev, 700).ToString();
        if (PlayerPrefs.GetInt("Top3" + lev) == score) 
        {
            top3Label.color = Color.green;
        } else {
            top3Label.color = Color.white;
        }

        top4Label.text = "4. " + PlayerPrefs.GetInt("Top4" + lev, 600).ToString();
        if (PlayerPrefs.GetInt("Top4" + lev) == score) 
        {
            top4Label.color = Color.green;
        } else {
            top4Label.color = Color.white;
        }

        top5Label.text = "5. " + PlayerPrefs.GetInt("Top5" + lev, 500).ToString();
        if (PlayerPrefs.GetInt("Top5" + lev) == score) 
        {
            top5Label.color = Color.green;
        } else {
            top5Label.color = Color.white;
        }
    }

    public void NextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (SceneManager.sceneCountInBuildSettings > nextSceneIndex)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        
        gameObject.SetActive(false);
    }
}