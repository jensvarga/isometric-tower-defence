using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGrid : MonoBehaviour
{
    private char[,] levelArray = new char[10, 10] {
        {'x', 'x', 'g', 'x', 'x', 'x', 'x', 'x', 'x', 'x'},
        {'x', 'x', 'i', 'x', 'x', 'x', 'x', 'x', 'x', 'x'},
        {'x', 'x', 'i', 'x', 'x', 'x', 'x', 'x', 'x', 'x'},
        {'x', 'x', 'p', 'i', 'i', 'i', 'p', 'x', 'x', 'x'},
        {'x', 'x', 'x', 'x', 'x', 'x', 'i', 'x', 'x', 'x'},
        {'x', 'x', 'x', 'x', 'x', 'x', 'i', 'x', 'x', 'x'},
        {'x', 'x', 'x', 'x', 'x', 'x', 'i', 'x', 'x', 'x'},
        {'x', 'x', 'x', 'p', 'i', 'i', 'p', 'x', 'x', 'x'},
        {'x', 'x', 'x', 'i', 'x', 'x', 'x', 'x', 'x', 'x'},
        {'x', 'x', 'x', 's', 'x', 'x', 'x', 'x', 'x', 'x'}
    };
    public List<Vector3> path;
    public GameObject buildTile;
    public GameObject enemyTile;
    public GameObject spawnTile;
    public GameObject goalTile;
    public GameObject underGroundTile;
    public GameObject slopedEnemyTile;

    List<List<string>> levelList;

    public float tileSize = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        List<List<string>> example = new List<List<string>>();
        example.Add(new List<string>() { "x", "x", "x", "x", "x", "x", "x", "x", "x", "x" });
        example.Add(new List<string>() { "x", "x", "x", "x", "x", "x", "x", "x", "x", "x" });
        example.Add(new List<string>() { "x", "x", "x", "x", "x", "x", "x", "x", "x", "x" });
        example.Add(new List<string>() { "x", "x", "x", "x", "x", "x", "x", "x", "x", "x" });
        example.Add(new List<string>() { "x", "x", "x", "x", "x", "x", "x", "x", "x", "x" });
        example.Add(new List<string>() { "x", "x", "x", "x", "x", "x", "x", "x", "x", "x" });
        example.Add(new List<string>() { "x", "x", "x", "x", "x", "x", "x", "x", "x", "x" });
        example.Add(new List<string>() { "x", "x", "x", "x", "x", "x", "x", "x", "x", "x" });
        example.Add(new List<string>() { "x", "x", "x", "x", "x", "x", "x", "x", "x", "x" });
        example.Add(new List<string>() { "x", "x", "x", "x", "x", "x", "x", "x", "x", "x" });
        example.Add(new List<string>() { "x", "x", "x", "x", "x", "x", "x", "x", "x", "x" });
        example.Add(new List<string>() { "x", "x", "x", "x", "x", "x", "x", "x", "x", "x" });
        example.Add(new List<string>() { "x", "x", "x", "x", "x", "x", "x", "x", "x", "x" });
        example.Add(new List<string>() { "x", "x", "x", "x", "x", "x", "x", "x", "x", "x" });
        example.Add(new List<string>() { "x", "x", "x", "x", "x", "x", "x", "x", "x", "x" });

        List<List<string>> level1 = new List<List<string>>();
        level1.Add(new List<string>() { "x", "x", "g", "x", "x", "x", "x", "x", "x", "x" });
        level1.Add(new List<string>() { "x", "x", "i", "x", "x", "x", "x", "x", "x", "x" });
        level1.Add(new List<string>() { "x", "x", "i", "x", "x", "x", "x", "x", "x", "x" });
        level1.Add(new List<string>() { "x", "x", "p", "i", "i", "i", "p", "x", "x", "x" });
        level1.Add(new List<string>() { "x", "x", "x", "x", "x", "x", "i", "x", "x", "x" });
        level1.Add(new List<string>() { "x", "x", "x", "x", "x", "x", "i", "x", "x", "x" });
        level1.Add(new List<string>() { "x", "x", "x", "x", "x", "x", "i", "x", "x", "x" });
        level1.Add(new List<string>() { "x", "x", "x", "p", "i", "i", "p", "x", "x", "x" });
        level1.Add(new List<string>() { "x", "x", "x", "i", "x", "x", "x", "x", "x", "x" });
        level1.Add(new List<string>() { "x", "x", "x", "s", "x", "x", "x", "x", "x", "x" });

        List<List<string>> level2 = new List<List<string>>();
        level2.Add(new List<string>() { "x", "g", "x", "x", "x", "x", "x", "x", "x", "x2" });
        level2.Add(new List<string>() { "x", "p", "i", "i", "i", "i", "i", "i", "p", "x2" });
        level2.Add(new List<string>() { "x", "x", "x", "x", "x", "x", "x", "x", "i", "x2" });
        level2.Add(new List<string>() { "x", "x", "x", "x", "x", "x", "x", "x", "i", "x2" });
        level2.Add(new List<string>() { "x", "x", "x", "x", "x", "x", "x", "x", "i", "x2" });
        level2.Add(new List<string>() { "x", "x", "x", "x", "x", "x", "p", "i", "p", "x2" });
        level2.Add(new List<string>() { "x", "x", "x", "x", "x", "p", "p", "x2", "x2", "x2" });
        level2.Add(new List<string>() { "x", "x", "x", "x", "x", "i", "x", "x", "x", "x" });
        level2.Add(new List<string>() { "x2", "x", "x", "x", "x", "i", "x", "x", "x", "x" });
        level2.Add(new List<string>() { "x2", "x2", "x2", "x", "x", "p", "x", "x", "x", "x" });
        level2.Add(new List<string>() { "x2", "p2", "sr2", "sr", "p", "p", "x", "x", "x", "x" });
        level2.Add(new List<string>() { "x2", "i2", "x2", "x2", "x2", "x2", "x2", "x2", "x2", "x2" });
        level2.Add(new List<string>() { "x2", "p2", "i2", "p2", "i2", "p2", "i2", "p2", "x2", "x2" });
        level2.Add(new List<string>() { "x2", "x2", "x2", "x2", "x2", "x2", "x2", "i2", "x2", "x2" });
        level2.Add(new List<string>() { "x2", "x2", "x2", "x2", "x2", "x2", "x2", "s2", "x2", "x2" });

        List<List<string>> level3a = new List<List<string>>();
        level3a.Add(new List<string>() { "x", "g", "x", "x", "x", "o", "o", "o", "o", "o" });
        level3a.Add(new List<string>() { "x", "p", "x", "x", "x", "o", "o", "o", "o", "o" });
        level3a.Add(new List<string>() { "x", "p", "x", "x", "x", "o", "o", "o", "o", "o" });
        level3a.Add(new List<string>() { "x", "p", "x", "x", "x", "o", "o", "o", "o", "o" });
        level3a.Add(new List<string>() { "x", "p", "x", "x", "x", "o", "o", "o", "o", "o" });
        level3a.Add(new List<string>() { "x", "p", "x", "x", "x", "o", "o", "o", "o", "o" });
        level3a.Add(new List<string>() { "x", "p", "p", "p", "p", "o", "o", "o", "o", "o" });
        level3a.Add(new List<string>() { "x", "x", "x", "x", "p", "o", "o", "o", "o", "o" });
        level3a.Add(new List<string>() { "x", "x", "x", "x", "p", "o", "o", "o", "o", "o" });
        level3a.Add(new List<string>() { "x", "x", "x", "x", "p", "o", "o", "o", "o", "o" });
        level3a.Add(new List<string>() { "x", "x", "x", "x", "p", "o", "o", "o", "o", "o" });
        level3a.Add(new List<string>() { "x", "p", "p", "p", "p", "o", "o", "o", "o", "o" });
        level3a.Add(new List<string>() { "x", "p", "x", "x", "x", "o", "o", "o", "o", "o" });
        level3a.Add(new List<string>() { "x", "p", "x", "x", "x", "o", "o", "o", "o", "o" });
        level3a.Add(new List<string>() { "x", "s", "x", "x", "x", "o", "o", "o", "o", "o" });

        List<List<string>> level3b = new List<List<string>>();
        level3b.Add(new List<string>() { "o", "o", "o", "o", "o", "x2", "x", "x", "s", "x" });
        level3b.Add(new List<string>() { "o", "o", "o", "o", "o", "x2", "x", "x", "p", "x" });
        level3b.Add(new List<string>() { "o", "o", "o", "o", "o", "x2", "x", "x", "p", "x" });
        level3b.Add(new List<string>() { "o", "o", "o", "o", "o", "x2", "x", "x", "p", "x" });
        level3b.Add(new List<string>() { "o", "o", "o", "o", "o", "x2", "x", "x", "p", "x" });
        level3b.Add(new List<string>() { "o", "o", "o", "o", "o", "x2", "x", "x", "p", "x" });
        level3b.Add(new List<string>() { "o", "o", "o", "o", "o", "x2", "p", "p", "p", "x" });
        level3b.Add(new List<string>() { "o", "o", "o", "o", "o", "x2", "p", "x", "x", "x" });
        level3b.Add(new List<string>() { "o", "o", "o", "o", "o", "x2", "p", "x", "x", "x" });
        level3b.Add(new List<string>() { "o", "o", "o", "o", "o", "x2", "p", "x", "x", "x" });
        level3b.Add(new List<string>() { "o", "o", "o", "o", "o", "x2", "p", "x", "x", "x" });
        level3b.Add(new List<string>() { "o", "o", "o", "o", "o", "x2", "p", "p", "p", "x" });
        level3b.Add(new List<string>() { "o", "o", "o", "o", "o", "x2", "x", "x", "p", "x" });
        level3b.Add(new List<string>() { "o", "o", "o", "o", "o", "x2", "x", "x", "p", "x" });
        level3b.Add(new List<string>() { "o", "o", "o", "o", "o", "x2", "x", "x", "g", "x" });

        // Set what level to play
        var mapList = new List<List<List<string>>>() { level3a, level3b };

        foreach (List<List<string>> map in mapList)
        {
            BuildMap(map);
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
