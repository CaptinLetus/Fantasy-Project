using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuilderUI : MonoBehaviour
{
    public static BuilderUI instance;

    public TextMeshProUGUI title;
    public Canvas canvas;

    int currentTowerIndex;
    Transform currentPad;
    bool isUpgrade;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        transform.LookAt(Camera.main.transform.position, Vector3.up);
    }
    public void Upgrade1()
    {
        if (isUpgrade) // upgrade
        {
            Debug.Log("This index is " + currentTowerIndex);
            int level = currentPad.GetComponent<Tower>().towerLevel.value;

            BuildController.instance.Construct(level + 1, "Archer", currentPad);
            Destroy(currentPad.gameObject);
        }
        else // new tower
        {
            BuildController.instance.Construct(currentTowerIndex, "Archer", currentPad);
        }
        
        canvas.enabled = false;
    }

    public void OpenFromPad(Transform pad)
    {
        title.text = "BUILD";
        currentPad = pad;
        isUpgrade = false;

        transform.position = pad.position;
        currentTowerIndex = 1;
        canvas.enabled = true;
    }

    public void OpenFromTower(Transform tower, string name, int index)
    {
        title.text = "UPGRADE";
        isUpgrade = true;
        currentPad = tower;

        transform.position = tower.position;
        currentTowerIndex = index;
        canvas.enabled = true;
    }

    public void Close()
    {
        Debug.Log("Close");
        canvas.enabled = false;
    }
}
