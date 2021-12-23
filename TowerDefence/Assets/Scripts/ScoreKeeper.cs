using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreKeeper : MonoBehaviour
{
    public int money = 0;
    public int score = 0;
    public int cost = 0;
    public int currentWave = 0;
    public int maxWaves = 0;
    public float timer;
    public bool playerDead = false;
    private bool paused;

    public int lives = 3;

    [SerializeField] private TextMeshProUGUI scoreLabel;
    [SerializeField] private TextMeshProUGUI moneyLabel;
    [SerializeField] private TextMeshProUGUI costLabel;
    [SerializeField] private TextMeshProUGUI waveLabel;
    [SerializeField] private TextMeshProUGUI timeLabel;
    [SerializeField] public GameObject nextButtonPosition;

    [SerializeField] private GameObject heart1;
    [SerializeField] private GameObject heart2;
    [SerializeField] private GameObject heart3;

    private enum State
    {
        Dead,
        OneLife,
        TwoLives,
        ThreeLives
    };

    private State state;
    public int damageTaken;

    void Start()
    {
        money = 20;
        nextButtonPosition.SetActive(false);
        damageTaken = 0;
        state = State.ThreeLives;
    }

    // Update is called once per frame
    void Update()
    {
        if (!paused && playerDead)
        {
            PauseGame();
        }

        if (paused && !playerDead)
        {
            ResumeGame();
        }
        
        switch (state)
        {
            case State.Dead:
                {
                    break;
                }
            case State.OneLife:
                {
                    DisplayDamage();
                    break;
                }
            case State.TwoLives:
                {
                    DisplayDamage();
                    break;
                }
            case State.ThreeLives:
                {
                    DisplayDamage();
                    break;
                }
        }

        if (state != State.Dead)
        {
            moneyLabel.text = "$ " + money.ToString();
            scoreLabel.text = "Score: " + score.ToString();
        }

        if (cost == 0)
        {
            costLabel.text = "";
        }
        else
        {
            costLabel.text = "$" + cost.ToString();
        }
        if (currentWave <= maxWaves)
        {
            waveLabel.text = currentWave.ToString() + "/" + maxWaves.ToString();
            int time = (int)timer;
            timeLabel.text = time.ToString();
        }
        else
        {
            waveLabel.text = "Inf";
            timeLabel.text = "Level completed";
        }
    }

    private void DisplayDamage()
    {
        if (damageTaken == 1)
        {
            heart3.SetActive(false);
            state = State.TwoLives;
        }
        if (damageTaken == 2)
        {
            heart3.SetActive(false);
            heart2.SetActive(false);
            state = State.OneLife;
        }
        if (damageTaken > 2)
        {
            heart3.SetActive(false);
            heart2.SetActive(false);
            heart1.SetActive(false);
            moneyLabel.color = Color.red;
            scoreLabel.color = Color.red;
            playerDead = true;
            state = State.Dead;
        }
    }

    public void FlashCostRed()
    {
        StartCoroutine(FlashRed());
    }

    private IEnumerator FlashRed()
    {
        costLabel.color = Color.red;
        yield return new WaitForSeconds(1f);
        costLabel.color = Color.white;
    }

    void PauseGame()
    {
        Time.timeScale = 0;
    }

    void ResumeGame()
    {
        Time.timeScale = 1;
    }
}
