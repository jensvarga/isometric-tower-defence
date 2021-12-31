using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class LevelGrid : MonoBehaviour
{
    public int levelNumber;

    [HideInInspector] public List<Vector3> path;
    [SerializeField] private GameObject buildTile;
    [SerializeField] private GameObject enemyTile;
    [SerializeField] private GameObject spawnTile;
    [SerializeField] private GameObject goalTile;
    [SerializeField] private GameObject underGroundTile;
    [SerializeField] private GameObject slopedEnemyTile;

    [SerializeField] private GameObject pauseMenu;
    private bool paused = false;

    private List<List<string>> levelList;
    private float tileSize = 1.0f;

    private struct Level
    {
        public int index;
        public List<List<List<string>>> subLevels;

        public Level(int index, List<List<List<string>>> subLevels)
        {
            this.index = index;
            this.subLevels = subLevels;
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
        paused = false;

        List<List<string>> sub1 = new List<List<string>>();
        sub1.Add(new List<string>() { "x", "x", "g", "x", "x", "x", "x", "x", "x", "x" });
        sub1.Add(new List<string>() { "x", "x", "i", "x", "x", "x", "x", "x", "x", "x" });
        sub1.Add(new List<string>() { "x", "x", "i", "x", "x", "x", "x", "x", "x", "x" });
        sub1.Add(new List<string>() { "x", "x", "i", "x", "x", "x", "x", "x", "x", "x" });
        sub1.Add(new List<string>() { "x", "x", "i", "x", "x", "x", "x", "x", "x", "x" });
        sub1.Add(new List<string>() { "x", "x", "p", "i", "i", "p", "x", "x", "x", "x" });
        sub1.Add(new List<string>() { "x", "x", "x", "x", "x", "i", "x", "x", "x", "x" });
        sub1.Add(new List<string>() { "x", "x", "x", "x", "x", "i", "x", "x", "x", "x" });
        sub1.Add(new List<string>() { "x", "x", "x", "x", "x", "i", "x", "x", "x", "x" });
        sub1.Add(new List<string>() { "x", "x", "x", "x", "x", "i", "x", "x", "x", "x" });
        sub1.Add(new List<string>() { "x", "x", "x", "x", "x", "p", "i", "i", "p", "x" });
        sub1.Add(new List<string>() { "x", "x", "x", "x", "x", "x", "x", "x", "i", "x" });
        sub1.Add(new List<string>() { "x", "x", "x", "x", "x", "x", "x", "x", "i", "x" });
        sub1.Add(new List<string>() { "x", "x", "x", "x", "x", "x", "x", "x", "i", "x" });
        sub1.Add(new List<string>() { "x", "x", "x", "x", "x", "x", "x", "x", "s", "x" });

        var sublevels1 = new List<List<List<string>>>() { sub1 };
        Level level1 = new Level(1, sublevels1);

        List<List<string>> sub2 = new List<List<string>>();
        sub2.Add(new List<string>() { "x", "g", "x", "x", "x", "x", "x", "x", "x", "x2" });
        sub2.Add(new List<string>() { "x", "p", "i", "i", "i", "i", "i", "i", "p", "x2" });
        sub2.Add(new List<string>() { "x", "x", "x", "x", "x", "x", "x", "x", "i", "x2" });
        sub2.Add(new List<string>() { "x", "x", "x", "x", "x", "x", "x", "x", "i", "x2" });
        sub2.Add(new List<string>() { "x", "x", "x", "x", "x", "x", "x", "x", "i", "x2" });
        sub2.Add(new List<string>() { "x", "x", "x", "x", "x", "x", "p", "i", "p", "x2" });
        sub2.Add(new List<string>() { "x", "x", "x", "x", "x", "p", "p", "x2", "x2", "x2" });
        sub2.Add(new List<string>() { "x", "x", "x", "x", "x", "i", "x", "x", "x", "x" });
        sub2.Add(new List<string>() { "x2", "x", "x", "x", "x", "i", "x", "x", "x", "x" });
        sub2.Add(new List<string>() { "x2", "x2", "x2", "x", "x", "p", "x", "x", "x", "x" });
        sub2.Add(new List<string>() { "x2", "p2", "sr2", "sr", "p", "p", "x", "x", "x", "x" });
        sub2.Add(new List<string>() { "x2", "i2", "x2", "x2", "x2", "x2", "x2", "x2", "x2", "x2" });
        sub2.Add(new List<string>() { "x2", "p2", "i2", "p2", "i2", "p2", "i2", "p2", "x2", "x2" });
        sub2.Add(new List<string>() { "x2", "x2", "x2", "x2", "x2", "x2", "x2", "i2", "x2", "x2" });
        sub2.Add(new List<string>() { "x2", "x2", "x2", "x2", "x2", "x2", "x2", "s2", "x2", "x2" });

        var sublevels2 = new List<List<List<string>>>() { sub2 };
        Level level2 = new Level(2, sublevels2);

        List<List<string>> sub3a = new List<List<string>>();
        sub3a.Add(new List<string>() { "x", "g", "x", "x", "x", "o", "o", "o", "o", "o" });
        sub3a.Add(new List<string>() { "x", "p", "x", "x", "x", "o", "o", "o", "o", "o" });
        sub3a.Add(new List<string>() { "x", "p", "x", "x", "x", "o", "o", "o", "o", "o" });
        sub3a.Add(new List<string>() { "x", "p", "x", "x", "x", "o", "o", "o", "o", "o" });
        sub3a.Add(new List<string>() { "x", "p", "x", "x", "x", "o", "o", "o", "o", "o" });
        sub3a.Add(new List<string>() { "x", "p", "x", "x", "x", "o", "o", "o", "o", "o" });
        sub3a.Add(new List<string>() { "x", "p", "p", "p", "p", "o", "o", "o", "o", "o" });
        sub3a.Add(new List<string>() { "x", "x", "x", "x", "p", "o", "o", "o", "o", "o" });
        sub3a.Add(new List<string>() { "x", "x", "x", "x", "p", "o", "o", "o", "o", "o" });
        sub3a.Add(new List<string>() { "x", "x", "x", "x", "p", "o", "o", "o", "o", "o" });
        sub3a.Add(new List<string>() { "x", "x", "x", "x", "p", "o", "o", "o", "o", "o" });
        sub3a.Add(new List<string>() { "x", "p", "p", "p", "p", "o", "o", "o", "o", "o" });
        sub3a.Add(new List<string>() { "x", "p", "x", "x", "x", "o", "o", "o", "o", "o" });
        sub3a.Add(new List<string>() { "x", "p", "x", "x", "x", "o", "o", "o", "o", "o" });
        sub3a.Add(new List<string>() { "x", "s", "x", "x", "x", "o", "o", "o", "o", "o" });

        List<List<string>> sub3b = new List<List<string>>();
        sub3b.Add(new List<string>() { "o", "o", "o", "o", "o", "x", "x", "x", "s", "x" });
        sub3b.Add(new List<string>() { "o", "o", "o", "o", "o", "x", "x", "x", "p", "x" });
        sub3b.Add(new List<string>() { "o", "o", "o", "o", "o", "x", "x", "x", "p", "x" });
        sub3b.Add(new List<string>() { "o", "o", "o", "o", "o", "x", "x", "x", "p", "x" });
        sub3b.Add(new List<string>() { "o", "o", "o", "o", "o", "x", "x", "x", "p", "x" });
        sub3b.Add(new List<string>() { "o", "o", "o", "o", "o", "x", "x", "x", "p", "x" });
        sub3b.Add(new List<string>() { "o", "o", "o", "o", "o", "x", "p", "p", "p", "x" });
        sub3b.Add(new List<string>() { "o", "o", "o", "o", "o", "x", "p", "x", "x", "x" });
        sub3b.Add(new List<string>() { "o", "o", "o", "o", "o", "x", "p", "x", "x", "x" });
        sub3b.Add(new List<string>() { "o", "o", "o", "o", "o", "x", "p", "x", "x", "x" });
        sub3b.Add(new List<string>() { "o", "o", "o", "o", "o", "x", "p", "x", "x", "x" });
        sub3b.Add(new List<string>() { "o", "o", "o", "o", "o", "x", "p", "p", "p", "x" });
        sub3b.Add(new List<string>() { "o", "o", "o", "o", "o", "x", "x", "x", "p", "x" });
        sub3b.Add(new List<string>() { "o", "o", "o", "o", "o", "x", "x", "x", "p", "x" });
        sub3b.Add(new List<string>() { "o", "o", "o", "o", "o", "x", "x", "x", "g", "x" });

        var sublevels3 = new List<List<List<string>>>() { sub3a, sub3b };
        Level level3 = new Level(3, sublevels3);

        List<List<string>> sub4a = new List<List<string>>();
        sub4a.Add(new List<string>() { "x", "s", "x", "x", "x", "x", "x", "x", "x", "x" });
        sub4a.Add(new List<string>() { "x", "p", "x", "x", "x", "x", "x", "x", "x", "x" });
        sub4a.Add(new List<string>() { "x", "p", "x", "x", "x", "x", "x", "x", "x", "x" });
        sub4a.Add(new List<string>() { "x", "p", "x", "x", "x", "x", "x", "x", "x", "x" });
        sub4a.Add(new List<string>() { "x", "p", "x", "x", "x", "x", "x", "x", "x", "x" });
        sub4a.Add(new List<string>() { "x", "p", "x", "x", "x", "x", "x", "x", "x", "x" });
        sub4a.Add(new List<string>() { "x", "p", "p", "p", "p", "x", "x", "x", "x", "x" });
        sub4a.Add(new List<string>() { "x", "x", "x", "x", "i", "x", "x", "x", "x", "x" });
        sub4a.Add(new List<string>() { "x", "x", "x", "x", "p", "p", "p", "p", "p", "g" });
        sub4a.Add(new List<string>() { "x", "x", "x", "x", "o", "x", "x", "x", "x", "x" });
        sub4a.Add(new List<string>() { "x", "x", "x", "x", "o", "x", "x", "x", "x", "x" });
        sub4a.Add(new List<string>() { "x", "x", "x", "x", "o", "x", "x", "x", "x", "x" });
        sub4a.Add(new List<string>() { "x", "x", "x", "x", "o", "o", "o", "x", "x", "x" });
        sub4a.Add(new List<string>() { "x", "x", "x", "x", "x", "x", "o", "x", "x", "x" });
        sub4a.Add(new List<string>() { "x", "x", "x", "x", "x", "x", "o", "x", "x", "x" });

        List<List<string>> sub4b = new List<List<string>>();
        sub4b.Add(new List<string>() { "o", "o", "o", "o", "o", "o", "o", "o", "o", "o" });
        sub4b.Add(new List<string>() { "o", "o", "o", "o", "o", "o", "o", "o", "o", "o" });
        sub4b.Add(new List<string>() { "o", "o", "o", "o", "o", "o", "o", "o", "o", "o" });
        sub4b.Add(new List<string>() { "o", "o", "o", "o", "o", "o", "o", "o", "o", "o" });
        sub4b.Add(new List<string>() { "o", "o", "o", "o", "o", "o", "o", "o", "o", "o" });
        sub4b.Add(new List<string>() { "o", "o", "o", "o", "o", "o", "o", "o", "o", "o" });
        sub4b.Add(new List<string>() { "o", "o", "o", "o", "o", "o", "o", "o", "o", "o" });
        sub4b.Add(new List<string>() { "o", "o", "o", "o", "o", "o", "o", "o", "o", "o" });
        sub4b.Add(new List<string>() { "o", "o", "o", "o", "o", "o", "o", "o", "o", "o" });
        sub4b.Add(new List<string>() { "o", "o", "o", "o", "p", "o", "o", "o", "o", "o" });
        sub4b.Add(new List<string>() { "o", "o", "o", "o", "i", "o", "o", "o", "o", "o" });
        sub4b.Add(new List<string>() { "o", "o", "o", "o", "p", "o", "o", "o", "o", "o" });
        sub4b.Add(new List<string>() { "o", "o", "o", "o", "p", "p", "p", "o", "o", "o" });
        sub4b.Add(new List<string>() { "o", "o", "o", "o", "o", "o", "p", "o", "o", "o" });
        sub4b.Add(new List<string>() { "o", "o", "o", "o", "o", "o", "s", "o", "o", "o" });

        var sublevels4 = new List<List<List<string>>>() { sub4a, sub4b };
        Level level4 = new Level(4, sublevels4);

        var levels = new List<Level>() { level1, level2, level3, level4 };

        foreach (Level level in levels)
        {
            if (level.index == levelNumber)
            {
                foreach (var sublevel in level.subLevels)
                {
                    BuildMap(sublevel);
                }

            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape) && !paused)
        {
            // Game paused
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
            StartCoroutine(Buffer());
        }

        if (Input.GetKeyUp(KeyCode.Escape) && paused)
        {
            ResumeGame();
        }
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        StartCoroutine(Buffer());
    }

    private IEnumerator Buffer()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        if (paused) paused = false;
        else paused = true;

        GameObject[] groundTiles = GameObject.FindGameObjectsWithTag("groundTile");
        foreach (GameObject tile in groundTiles)
        {
            GridUnit gridUnit = tile.GetComponent<GridUnit>();
            if (gridUnit != null) gridUnit.paused = paused;
        }
    }

    void BuildMap(List<List<string>> levelList)
    {
        int rowIndex = 0;
        foreach (List<string> row in levelList)
        {
            int colIndex = 0;
            foreach (string col in row)
            {
                float x = colIndex * tileSize;
                float z = rowIndex * tileSize;
                float y = 0 * tileSize;
                Vector3 position = new Vector3(x, 0.0f, z);
                Vector3 l2position = new Vector3(x, 1.0f * tileSize, z);
                if (col == "x")
                {
                    // Place build tile
                    var bTile = GameObject.Instantiate(buildTile, position, Quaternion.identity);
                }
                if (col == "x2")
                {
                    // Place build tile
                    var bTile = GameObject.Instantiate(buildTile, l2position, Quaternion.identity);
                    var ugTile = GameObject.Instantiate(underGroundTile, position, Quaternion.identity);
                }
                if (col == "i")
                {
                    // Place enemy tile
                    var eTile = GameObject.Instantiate(enemyTile, position, Quaternion.identity);
                }
                if (col == "i2")
                {
                    // Place enemy tile
                    var eTile = GameObject.Instantiate(enemyTile, l2position, Quaternion.identity);
                }
                if (col == "sr2") // Sloped right
                {
                    Vector3 slopeRot = new Vector3(0.0f, 90f, -180);
                    Vector3 slopeScale = new Vector3(50f, 25f, 50f);
                    var tile = GameObject.Instantiate(slopedEnemyTile, l2position, Quaternion.Euler(slopeRot));
                    tile.transform.localScale = slopeScale;
                    path.Add(new Vector3(position.x - 0.5f * tileSize, 1.25f * tileSize, position.z));
                }
                if (col == "sr") // Sloped right
                {
                    Vector3 slopeRot = new Vector3(0.0f, 90f, -180);
                    Vector3 slopeScale = new Vector3(50f, 25f, 50f);
                    Vector3 bottomSlopePos = new Vector3(x, 0.5f, z);
                    var tile = GameObject.Instantiate(slopedEnemyTile, bottomSlopePos, Quaternion.Euler(slopeRot));
                    tile.transform.localScale = slopeScale;
                    path.Add(new Vector3(position.x + 0.3f * tileSize, 0.3f * tileSize, position.z));
                }
                if (col == "sl") // Sloped left
                {
                    Vector3 slopeRot = new Vector3(0.0f, 270f, -180);
                    Vector3 slopeScale = new Vector3(50f, 25f, 50f);
                    var tile = GameObject.Instantiate(slopedEnemyTile, l2position, Quaternion.Euler(slopeRot));
                    tile.transform.localScale = slopeScale;
                }
                if (col == "su") // Sloped up
                {
                    Vector3 slopeRot = new Vector3(0.0f, 0f, -180);
                    Vector3 slopeScale = new Vector3(50f, 25f, 50f);
                    var tile = GameObject.Instantiate(slopedEnemyTile, l2position, Quaternion.Euler(slopeRot));
                    tile.transform.localScale = slopeScale;
                }
                if (col == "sd") // Sloped down
                {
                    Vector3 slopeRot = new Vector3(0.0f, 180f, -180);
                    Vector3 slopeScale = new Vector3(50f, 25f, 50f);
                    var tile = GameObject.Instantiate(slopedEnemyTile, l2position, Quaternion.Euler(slopeRot));
                    tile.transform.localScale = slopeScale;
                }
                if (col == "s")
                {
                    // Place start tile
                    var bTile = GameObject.Instantiate(spawnTile, position, Quaternion.identity);
                }
                if (col == "s2")
                {
                    // Place start tile
                    var bTile = GameObject.Instantiate(spawnTile, l2position, Quaternion.identity);
                }
                if (col == "p")
                {
                    // Place enemy tile in path
                    var eTile = GameObject.Instantiate(enemyTile, position, Quaternion.identity);
                    path.Add(new Vector3(position.x, 0.25f, position.z));
                }
                if (col == "p2")
                {
                    // Place enemy tile in path
                    var eTile = GameObject.Instantiate(enemyTile, l2position, Quaternion.identity);
                    path.Add(new Vector3(position.x, 1.25f, position.z));
                }
                if (col == "g")
                {
                    // Place goal tile
                    var eTile = GameObject.Instantiate(goalTile, position, Quaternion.identity);

                    path.Add(new Vector3(position.x, 0.25f, position.z));
                }
                colIndex++;
            }
            rowIndex++;
        }
    }

    void OnDrawGizmos()
    {
        // Green
        Gizmos.color = new Color(0.0f, 1.0f, 0.0f);
        foreach (var position in path)
        {
            Gizmos.DrawSphere(position, 0.1f);
        }
    }
}
