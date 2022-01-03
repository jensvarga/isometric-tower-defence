using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class spawner : MonoBehaviour
{
    [SerializeField] private GameObject diamondSpider;
    [SerializeField] private GameObject larvaBot;
    [SerializeField] private GameObject wheelyBot;
    [SerializeField] private GameObject bossRobot;
    [SerializeField] private GameObject wallEbot;
    [SerializeField] private GameObject illuminatiBot;
    [SerializeField] private GameObject crawlBot;
    [SerializeField] private GameObject pinchRomba;

    [SerializeField] private GameObject spawnPoint;

    private float cooldownTimer;
    private float waveTimer;
    private ScoreKeeper scoreKeeper;
    private int maxWaves;
    private int currentWave;
    private int enemyCount = 0;
    private int enemiesInWave;

    private enum State
    {
        BeforeFirst,
        InWave,
        BetweenWaves,
        InfiniteMode
    };

    private State state;

    private struct WaveIndicator
    {
        public int enemies;
        public int level;
        public GameObject type;
        public bool armoured;

        public WaveIndicator(int enemies, int level, GameObject type, bool armoured)
        {
            this.enemies = enemies;
            this.level = level;
            this.type = type;
            this.armoured = armoured;
        }
    }

    private int level;

    private List<WaveIndicator> waves = new List<WaveIndicator>();

    void Start()
    {
        level = 1;

        if (level == 1)
        {
            for (int i = 0; i < 10; i++)
            {
                switch (i)
                {
                    case 0:
                        {
                            WaveIndicator wave = new WaveIndicator(5, i, diamondSpider, false);
                            waves.Add(wave);
                            break;
                        }
                    case 1:
                        {
                            WaveIndicator wave = new WaveIndicator(4, i, wheelyBot, false);
                            waves.Add(wave);
                            break;
                        }
                    case 2:
                        {
                            WaveIndicator wave = new WaveIndicator(3, i, pinchRomba, false);
                            waves.Add(wave);
                            break;
                        }
                    case 3:
                        {
                            WaveIndicator wave = new WaveIndicator(4, i, larvaBot, false);
                            waves.Add(wave);
                            break;
                        }
                    case 4:
                        {
                            WaveIndicator wave = new WaveIndicator(3, i, illuminatiBot, true);
                            waves.Add(wave);
                            break;
                        }
                    case 5:
                        {
                            WaveIndicator wave = new WaveIndicator(4, i, crawlBot, false);
                            waves.Add(wave);
                            break;
                        }
                    case 6:
                        {
                            WaveIndicator wave = new WaveIndicator(4, i, wallEbot, true);
                            waves.Add(wave);
                            break;
                        }
                    case 7:
                        {
                            WaveIndicator wave = new WaveIndicator(4, i, larvaBot, false);
                            waves.Add(wave);
                            break;
                        }
                    case 8:
                        {
                            WaveIndicator wave = new WaveIndicator(7, i, diamondSpider, false);
                            waves.Add(wave);
                            break;
                        }
                    case 9:
                        {
                            WaveIndicator wave = new WaveIndicator(1, i + 20, bossRobot, false);
                            waves.Add(wave);
                            break;
                        }
                    default:
                        {
                            WaveIndicator wave = new WaveIndicator(5, i, diamondSpider, false);
                            waves.Add(wave);
                            break;
                        }
                }
            }
        }

        var obj = GameObject.FindGameObjectWithTag("scoreKeeper");
        scoreKeeper = obj.GetComponent<ScoreKeeper>();

        currentWave = 0;
        maxWaves = waves.Count;
        scoreKeeper.maxWaves = maxWaves;

        state = State.BeforeFirst;
        float firstWaveCooldown = 15.0f;
        waveTimer = firstWaveCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        scoreKeeper.timer = waveTimer;
        scoreKeeper.currentWave = currentWave;

        switch (state)
        {
            case State.BeforeFirst:
                {
                    if (waveTimer > 0)
                    {
                        waveTimer -= Time.deltaTime;
                    }
                    else
                    {
                        TransitionToInWave();
                    }
                    break;
                }
            case State.InWave:
                {
                    int enemiesSpawned = enemyCount;

                    if (cooldownTimer > 0)
                    {
                        cooldownTimer -= Time.deltaTime;
                    }
                    else
                    {
                        if (enemiesSpawned < enemiesInWave)
                        {
                            // Spawn enemy
                            GameObject enemyType = waves[currentWave].type;
                            int level = waves[currentWave].level == 0 ? 1 : waves[currentWave].level;
                            GameObject enemyObj = Instantiate(enemyType, spawnPoint.transform.position, Quaternion.identity);
                            enemyObj.GetComponent<enemy>().Setup(level, waves[currentWave].armoured);
                            enemyCount++;
                            cooldownTimer = 5.0f;
                        }
                        else
                        {
                            // Check if all enemies are dead
                            GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");
                            if (enemies.ToList().Count.Equals(0) || enemies == null)
                            {
                                TransitionToBetweenWaves();
                            }
                        }
                    }
                    break;
                }
            case State.BetweenWaves:
                {
                    if (waveTimer > 0)
                    {
                        waveTimer -= Time.deltaTime;
                    }
                    else
                    {
                        if (currentWave < maxWaves)
                        {
                            currentWave++;
                            TransitionToInWave();
                        }
                        else
                        {
                            currentWave++;
                            TransitionToInfiniteMode();
                        }
                    }
                    break;
                }
            case State.InfiniteMode:
                {
                    if (cooldownTimer > 0)
                    {
                        cooldownTimer -= Time.deltaTime;
                    }
                    else
                    {

                        // Spawn enemy
                        int level = maxWaves + enemyCount;
                        GameObject enemyObj = Instantiate(RandomEnemyType(), spawnPoint.transform.position, Quaternion.identity);
                        enemyObj.GetComponent<enemy>().Setup(level, Random.Range(0, 1) == 0);
                        cooldownTimer = 5.0f;
                        enemyCount++;
                    }
                    break;
                }
        }
    }

    private void TransitionToInfiniteMode()
    {
        AudioPlayer audioPlayer = Resources.FindObjectsOfTypeAll<AudioPlayer>()[0];
        if (audioPlayer != null) audioPlayer.PlayWinSound();
        enemyCount = 0; // Used for enemy level in infinte mode
        scoreKeeper.nextButtonPosition.SetActive(true);
        state = State.InfiniteMode;
    }

    private void TransitionToInWave()
    {
        if (currentWave < waves.Count)
        {
            enemiesInWave = waves[currentWave].enemies;
        }
        else
        {
            enemiesInWave = 0;
        }
        state = State.InWave;
    }

    private void TransitionToBetweenWaves()
    {
        enemyCount = 0;
        waveTimer = 5.0f;
        state = State.BetweenWaves;
    }

    private GameObject RandomEnemyType()
    {
        int randonInt = Random.Range(0, 100);

        if (randonInt < 5)
        {
            return bossRobot;
        }
        else if (randonInt < 25)
        {
            return larvaBot;
        }
        else if (randonInt < 45)
        {
            return wheelyBot;
        }
        else if (randonInt < 55)
        {
            return wallEbot;
        }
        else if (randonInt < 65)
        {
            return illuminatiBot;
        }
        else if (randonInt < 75)
        {
            return crawlBot;
        }
        else if (randonInt < 85)
        {
            return pinchRomba;
        }
        else
        {
            return diamondSpider;
        }
    }
}