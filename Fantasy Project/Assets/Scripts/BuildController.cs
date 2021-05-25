using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildController : MonoBehaviour
{
    public GameObject[] archerTowers;
    public GameObject[] mageTowers;
    public GameObject[] cannonTowers;
    public GameObject[] barracksTowers;

    // Allow other objects in the game to reference the build controller
    public static BuildController instance;

    int getLevelFromIndex(int index)
    {
        return index / 2 + 1;
    }

    int getIndexFromLevel(int level)
    {
        return (level * 2) - 1;
    }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one BuildController has been created");
            return;
        }
        instance = this;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MouseDown();
        }
    }
    public GameObject Construct(int level, string name, Transform pad)
    {

        int index = getIndexFromLevel(level);
        Debug.Log("Construct at Level " + level + " index: " + index);

        GameObject newTower = Build(index, name, pad);

        newTower.GetComponent<ConstructionTower>().Build(index - 1, name);

        return newTower;
    }

    public GameObject FinishBuild(int index, string name, Transform pad)
    {
        GameObject newTower = Build(index, name, pad);

        // update name and level
        Tower tower = newTower.transform.GetComponent<Tower>();
        index++;
        int level = getLevelFromIndex(index);
        Debug.Log("Level " + level + " index: " + index);

        tower.towerLevel.value = level;
        tower.towerName.value = name;

        return newTower;
    }

    public GameObject Build(int index, string name, Transform pad)
    {
        GameObject newTower = GameObject.Instantiate(archerTowers[index]);

        newTower.transform.rotation = pad.rotation;
        newTower.transform.position = pad.position;

        return newTower;
    }

    private void MouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log(hit.transform.tag);

            if (hit.transform.tag == "BuildingPad")
            {
                BuilderUI.instance.OpenFromPad(hit.transform);
            }
            else if (hit.transform.tag == "Tower")
            {
                Debug.Log("from tower");
                Tower tower = hit.transform.GetComponent<Tower>();
                int level = tower.towerLevel.value;
                string name = tower.towerName.value;

                BuilderUI.instance.OpenFromTower(hit.transform, name, level);
            }
        }
    }
}
