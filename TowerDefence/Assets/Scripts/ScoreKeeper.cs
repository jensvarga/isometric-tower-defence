using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreKeeper : MonoBehaviour
{
    public int money = 0;
    public int score = 0;
    public int cost = 0;
    public string info = "";
    public int currentWave = 0;
    public int maxWaves = 0;
    public float timer;
    public bool playerDead = false;
    private bool paused;
    private bool shaking = false;

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
        ResumeGame();
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
                    UpdateOneLife();
                    break;
                }
            case State.TwoLives:
                {
                    UpdateTwoLifes();
                    break;
                }
            case State.ThreeLives:
                {
                    UpdateThreeLifes();
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
            costLabel.text = info;
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

    private void UpdateThreeLifes()
    {
        if (damageTaken == 0)
        {
            DisplayDamage();
            return;
        }
        if (damageTaken == 1)
        {
            DisplayDamage();
            TakeDamage();
            state = State.TwoLives;
        }
        if (damageTaken == 2)
        {
            DisplayDamage();
            TakeDamage();
            state = State.OneLife;
        }
        if (damageTaken == 3)
        {
            DisplayDamage();
            TakeDamage();
            state = State.Dead;
        }
    }

    private void UpdateTwoLifes()
    {
        if (damageTaken == 1)
        {
            return;
        }
        if (damageTaken == 0)
        {
            DisplayDamage();
            state = State.ThreeLives;
            return;
        }
        if (damageTaken == 2)
        {
            DisplayDamage();
            TakeDamage();
            state = State.OneLife;
        }
        if (damageTaken == 3)
        {
            DisplayDamage();
            TakeDamage();
            state = State.Dead;
        }
    }

    private void UpdateOneLife()
    {
        if (damageTaken == 2)
        {
            return;
        }
        if (damageTaken == 0)
        {
            DisplayDamage();
            state = State.ThreeLives;
            return;
        }
        if (damageTaken == 1)
        {
            DisplayDamage();
            TakeDamage();
            state = State.TwoLives;
        }
        if (damageTaken == 3)
        {
            DisplayDamage();
            TakeDamage();
            state = State.Dead;
        }
    }

    public void SetHighScore(int score, int level)
    {
        string lev = level.ToString();
        int top1 = PlayerPrefs.GetInt("Top1" + lev, 0);
        int top2 = PlayerPrefs.GetInt("Top2" + lev, 0);
        int top3 = PlayerPrefs.GetInt("Top3" + lev, 0);
        int top4 = PlayerPrefs.GetInt("Top4" + lev, 0);
        int top5 = PlayerPrefs.GetInt("Top5" + lev, 0);

        if (score > top1)
        {
            PlayerPrefs.SetInt("Top5" + lev, top4);
            PlayerPrefs.SetInt("Top4" + lev, top3);
            PlayerPrefs.SetInt("Top3" + lev, top2);
            PlayerPrefs.SetInt("Top2" + lev, top1);
            PlayerPrefs.SetInt("Top1" + lev, score);
        }
        else if (score > top2)
        {
            PlayerPrefs.SetInt("Top5" + lev, top4);
            PlayerPrefs.SetInt("Top4" + lev, top3);
            PlayerPrefs.SetInt("Top3" + lev, top2);
            PlayerPrefs.SetInt("Top2" + lev, score);
        }
        else if (score > top3)
        {
            PlayerPrefs.SetInt("Top5" + lev, top4);
            PlayerPrefs.SetInt("Top4" + lev, top3);
            PlayerPrefs.SetInt("Top3" + lev, score);
        }
        else if (score > top4)
        {
            PlayerPrefs.SetInt("Top5" + lev, top4);
            PlayerPrefs.SetInt("Top4" + lev, score);
        }
        else if (score > top5)
        {
            PlayerPrefs.SetInt("Top5" + lev, score);
        }
    }

    private void TakeDamage()
    {
        GameObject cameraObject = GameObject.FindGameObjectWithTag("cameraObject");
        shakeGameObject(cameraObject, 0.5f, 0.4f, false);
        DisplayDamage();
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


    protected void shakeGameObject(GameObject objectToShake, float shakeDuration, float decreasePoint, bool objectIs2D = false)
    {
        if (shaking)
        {
            return;
        }
        shaking = true;
        StartCoroutine(shakeGameObjectCOR(objectToShake, shakeDuration, decreasePoint, objectIs2D));
    }

    protected IEnumerator shakeGameObjectCOR(GameObject objectToShake, float totalShakeDuration, float decreasePoint, bool objectIs2D = false)
    {
        if (decreasePoint >= totalShakeDuration)
        {
            Debug.LogError("decreasePoint must be less than totalShakeDuration...Exiting");
            yield break; //Exit!
        }

        //Get Original Pos and rot
        if (objectToShake != null)
        {
            Transform objTransform = objectToShake.transform;
            Vector3 defaultPos = objTransform.position;
            Quaternion defaultRot = objTransform.rotation;

            float counter = 0f;

            //Shake Speed
            const float speed = 0.05f;

            //Angle Rotation(Optional)
            const float angleRot = 2;

            //Do the actual shaking
            while (counter < totalShakeDuration)
            {
                counter += Time.unscaledDeltaTime;
                float decreaseSpeed = speed;
                float decreaseAngle = angleRot;

                //Shake GameObject
                if (objectIs2D)
                {
                    //Don't Translate the Z Axis if 2D Object
                    Vector3 tempPos = defaultPos + UnityEngine.Random.insideUnitSphere * decreaseSpeed;
                    tempPos.z = defaultPos.z;
                    objTransform.position = tempPos;

                    //Only Rotate the Z axis if 2D
                    objTransform.rotation = defaultRot * Quaternion.AngleAxis(UnityEngine.Random.Range(-angleRot, angleRot), new Vector3(0f, 0f, 1f));
                }
                else
                {
                    objTransform.position = defaultPos + UnityEngine.Random.insideUnitSphere * decreaseSpeed;
                    objTransform.rotation = defaultRot * Quaternion.AngleAxis(UnityEngine.Random.Range(-angleRot, angleRot), new Vector3(1f, 1f, 1f));
                }
                yield return null;


                //Check if we have reached the decreasePoint then start decreasing  decreaseSpeed value
                if (counter >= decreasePoint)
                {
                    //Reset counter to 0 
                    counter = 0f;
                    while (counter <= decreasePoint)
                    {
                        counter += Time.unscaledDeltaTime;
                        decreaseSpeed = Mathf.Lerp(speed, 0, counter / decreasePoint);
                        decreaseAngle = Mathf.Lerp(angleRot, 0, counter / decreasePoint);

                        //Shake GameObject
                        if (objectIs2D)
                        {
                            //Don't Translate the Z Axis if 2D Object
                            Vector3 tempPos = defaultPos + UnityEngine.Random.insideUnitSphere * decreaseSpeed;
                            tempPos.z = defaultPos.z;
                            objTransform.position = tempPos;

                            //Only Rotate the Z axis if 2D
                            objTransform.rotation = defaultRot * Quaternion.AngleAxis(UnityEngine.Random.Range(-decreaseAngle, decreaseAngle), new Vector3(0f, 0f, 1f));
                        }
                        else
                        {
                            objTransform.position = defaultPos + UnityEngine.Random.insideUnitSphere * decreaseSpeed;
                            objTransform.rotation = defaultRot * Quaternion.AngleAxis(UnityEngine.Random.Range(-decreaseAngle, decreaseAngle), new Vector3(1f, 1f, 1f));
                        }
                        yield return null;
                    }

                    //Break from the outer loop
                    break;
                }
            }
            objTransform.position = defaultPos; //Reset to original postion
            objTransform.rotation = defaultRot;//Reset to original rotation
        }

        shaking = false; //So that we can call this function next time
    }
}
