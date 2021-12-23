using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blueprint : MonoBehaviour
{
    public int buildCost = 15;
    private ScoreKeeper scoreKeeper;

    void Start()
    {
        var obj = GameObject.FindGameObjectWithTag("scoreKeeper");
        scoreKeeper = obj.GetComponent<ScoreKeeper>();
    }

    void OnMouseOver()
    {
        scoreKeeper.cost = buildCost;
    }

    void OnMouseExit()
    {
        scoreKeeper.cost = 0;
    }
}
