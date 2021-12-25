using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextButton : MonoBehaviour
{
    private bool swap = true;
    private ScoreKeeper scoreKeeper;

    void Start()
    {
        var obj = GameObject.FindGameObjectWithTag("scoreKeeper");
        scoreKeeper = obj.GetComponent<ScoreKeeper>();
    }
    // Update is called once per frame
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

    void OnMouseDown()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (SceneManager.sceneCountInBuildSettings > nextSceneIndex)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
    }
}
