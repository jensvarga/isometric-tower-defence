using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridUnit : MonoBehaviour
{
    public bool built = false;
    public GameObject lazerTower;
    public GameObject missileTower;
    public GameObject electricTower;
    public GameObject buildAnchor;
    [SerializeField] private GameObject selectionIndicator;
    [SerializeField] private GameObject dollarSign;
    [SerializeField] private GameObject[] availableBuildings;

    private GameObject towerObject;
    private TowerBase tower;
    private GameObject lazerBp;
    private GameObject missileBp;
    private GameObject electricBp;
    private GameObject upgradeBP;
    private GameObject dollar;
    private ScoreKeeper scoreKeeper;

    [HideInInspector] public bool dead = false;
    [HideInInspector] public bool paused = false;

    private enum State
    {
        Unselected,
        Selected,
    };
    State state;

    private enum SelectedState
    {
        Unbuilt,
        Built
    };
    SelectedState selectedState;

    // Start is called before the first frame update
    void Start()
    {
        dead = false;
        paused = false;

        var obj = GameObject.FindGameObjectWithTag("scoreKeeper");
        scoreKeeper = obj.GetComponent<ScoreKeeper>();

        selectionIndicator.SetActive(false);
        state = State.Unselected;
        selectedState = SelectedState.Unbuilt;
        Vector3 dollarPos = new Vector3(buildAnchor.transform.position.x,
                                        buildAnchor.transform.position.y + 0.5f,
                                        buildAnchor.transform.position.z + 1.0f);
        dollar = Instantiate(dollarSign, dollarPos, Quaternion.identity);
        dollar.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        DetectInput();

        switch (state)
        {
            case State.Selected:
                {
                    SelectedUpdate();
                    break;
                }
            case State.Unselected:
                {
                    UnselectedUpdate();
                    break;
                }
        }

    }

    void SelectedUpdate()
    {
        switch (selectedState)
        {
            case SelectedState.Built:
                {
                    BuiltUpdate();
                    break;
                }
            case SelectedState.Unbuilt:
                {
                    UnbuiltUpdate();
                    break;
                }
        }
    }

    void BuiltUpdate()
    {
        if (upgradeBP) upgradeBP.transform.Rotate(0f, 50f * Time.deltaTime, 0f);
        dollar.transform.Rotate(0f, 50f * Time.deltaTime, 0f);
    }

    void UnbuiltUpdate()
    {
        lazerBp.transform.Rotate(0f, 50f * Time.deltaTime, 0f);
        missileBp.transform.Rotate(0f, 50f * Time.deltaTime, 0f);
        electricBp.transform.Rotate(0f, 50f * Time.deltaTime, 0f);
    }

    void UnselectedUpdate()
    {

    }

    void TransitionToUnbuilt()
    {
        // Show building blueprints
        int index = 0;
        foreach (var blueprint in availableBuildings)
        {
            float xx = 0f;
            float zz = 0f;

            switch (index)
            {
                case 0:
                    xx = 1f;
                    zz = 0f;
                    break;
                case 1:
                    xx = 0f;
                    zz = 0f;
                    break;
                case 2:
                    xx = -1f;
                    zz = 0f;
                    break;
            }
            Vector3 bpPos = new Vector3(buildAnchor.transform.position.x + xx,
                                        buildAnchor.transform.position.y + 0.5f,
                                        buildAnchor.transform.position.z + zz);
            switch (index)
            {
                case 0:
                    lazerBp = Instantiate(blueprint, bpPos, Quaternion.identity);
                    break;
                case 1:
                    missileBp = Instantiate(blueprint, bpPos, Quaternion.identity);
                    break;
                case 2:
                    electricBp = Instantiate(blueprint, bpPos, Quaternion.identity);
                    break;
            }

            index++;
        }
        selectedState = SelectedState.Unbuilt;
    }

    void TransitionToBuilt()
    {
        RemoveBlueprints();
        selectionIndicator.SetActive(false);
        state = State.Unselected;
        selectedState = SelectedState.Built;
    }

    void TransitionToSelected()
    {
        selectionIndicator.SetActive(true);
        if (selectedState == SelectedState.Unbuilt)
        {
            TransitionToUnbuilt();
        }
        if (selectedState == SelectedState.Built)
        {
            showUpgrades();
        }
        state = State.Selected;
    }

    void showUpgrades()
    {
        if (tower.upgradeBp)
        {
            upgradeBP = tower.upgradeBp;
            // Show upgrade blueprints or sell
            Vector3 bpPos = new Vector3(buildAnchor.transform.position.x + 1.0f,
                                        buildAnchor.transform.position.y + 0.5f,
                                        buildAnchor.transform.position.z);

            upgradeBP = Instantiate(upgradeBP, bpPos, Quaternion.identity);
        }

        dollar.SetActive(true);
    }

    void TransitionToUnselected()
    {
        RemoveBlueprints();
        selectionIndicator.SetActive(false);
        state = State.Unselected;
    }

    private void RemoveBlueprints()
    {
        if (lazerBp != null)
        {
            Object.Destroy(lazerBp);
        }
        if (missileBp != null)
        {
            Object.Destroy(missileBp);
        }
        if (electricBp != null)
        {
            Object.Destroy(electricBp);
        }
        if (upgradeBP != null)
        {
            Object.Destroy(upgradeBP);
        }
        dollar.SetActive(false);
        scoreKeeper.cost = 0;
    }

    private void DetectInput()
    {
        if (paused || dead) return;
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit = new RaycastHit();
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                int action = 0;
                if (hit.collider.gameObject == this.gameObject) action = 1;
                if (hit.collider.gameObject == lazerBp) action = 2;
                if (hit.collider.gameObject == missileBp) action = 3;
                if (hit.collider.gameObject == electricBp) action = 4;
                if (hit.collider.gameObject == upgradeBP) action = 5;
                if (hit.collider.gameObject == dollar) action = 6;

                switch (action)
                {
                    case 1:
                        {
                            if (state == State.Unselected) TransitionToSelected();
                            break;
                        }
                    case 2:
                        {
                            int cost = lazerBp.GetComponent<Blueprint>().buildCost;
                            if (scoreKeeper.money >= cost)
                            {
                                towerObject = GameObject.Instantiate(lazerTower, buildAnchor.transform.position, Quaternion.identity);
                                tower = towerObject.GetComponent<TowerBase>();
                                scoreKeeper.money -= cost;
                                TransitionToBuilt();
                            }
                            else
                            {
                                scoreKeeper.FlashCostRed();
                            }
                            break;
                        }
                    case 3:
                        {
                            int cost = missileBp.GetComponent<Blueprint>().buildCost;
                            if (scoreKeeper.money >= cost)
                            {
                                towerObject = GameObject.Instantiate(missileTower, buildAnchor.transform.position, Quaternion.identity);
                                tower = towerObject.GetComponent<TowerBase>();
                                scoreKeeper.money -= cost;
                                TransitionToBuilt();
                            }
                            else
                            {
                                scoreKeeper.FlashCostRed();
                            }
                            break;
                        }
                    case 4:
                        {
                            int cost = electricBp.GetComponent<Blueprint>().buildCost;
                            if (scoreKeeper.money >= cost)
                            {
                                towerObject = GameObject.Instantiate(electricTower, buildAnchor.transform.position, Quaternion.identity);
                                tower = towerObject.GetComponent<TowerBase>();
                                scoreKeeper.money -= cost;
                                TransitionToBuilt();
                            }
                            else
                            {
                                scoreKeeper.FlashCostRed();
                            }
                            break;
                        }
                    case 5:
                        {
                            int cost = upgradeBP.GetComponent<Blueprint>().buildCost;
                            if (scoreKeeper.money >= cost)
                            {
                                var upgradedTower = tower.upgradedTower;
                                Object.Destroy(tower);
                                Object.Destroy(towerObject);
                                towerObject = GameObject.Instantiate(upgradedTower, buildAnchor.transform.position, Quaternion.identity);
                                tower = towerObject.GetComponent<TowerBase>();
                                scoreKeeper.money -= cost;
                                TransitionToBuilt();
                            }
                            else
                            {
                                scoreKeeper.FlashCostRed();
                            }
                            break;
                        }
                    case 6:
                        {
                            Object.Destroy(tower);
                            Object.Destroy(towerObject);
                            selectedState = SelectedState.Unbuilt;
                            scoreKeeper.money += tower.sellReward;
                            TransitionToUnselected();
                            break;
                        }
                    default:
                        {
                            TransitionToUnselected();
                            break;
                        }
                }
            }
            else
            {
                TransitionToUnselected();
            }
        }
    }
}
