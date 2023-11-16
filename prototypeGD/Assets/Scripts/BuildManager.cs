using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;  // stires reference to itself

    void Awake()
    {
        if (instance != null) {
            Debug.Log("More than one manager in scene");
            return;
        }
        instance = this;
    }
    private TurretBlueprint turretToBuild;

    public GameObject standardTurretPrefab;

    public bool canBuild { get { return turretToBuild != null; } }  // property 
    public bool hasMoney { get { return PlayerStats.Money >= turretToBuild.cost; } }  // property 


    public void BuildTurretOn (Node node)
    {
        if (PlayerStats.Money < turretToBuild.cost)
        {
            Debug.Log("Not enough money");
            return;
        }
        // Subtracting cost
        PlayerStats.Money -= turretToBuild.cost;

        GameObject turret = (GameObject)Instantiate(turretToBuild.prefab, node.GetBuildPosition(), Quaternion.identity);
        node.turret = turret;
        Debug.Log("Turret built. Money left --> " + PlayerStats.Money);
    }
    public void SelectTurretToBuild(TurretBlueprint turret)
    {
        turretToBuild = turret;
    }
}
