using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionTower : MonoBehaviour
{
    public float buildTime;

    float startTime;
    int finalTower;
    string towerName;
    
    // Start is called before the first frame update
    public void Build(int _goalTower, string _name)
    {
        startTime = Time.time;
        finalTower = _goalTower;
        towerName = _name;
    }

    public void Finish()
    {
        BuildController.instance.FinishBuild(finalTower, towerName, transform);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - startTime >= buildTime)
        {
            Finish();
        }
    }
}
