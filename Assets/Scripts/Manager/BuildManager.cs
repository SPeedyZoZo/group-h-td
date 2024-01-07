using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

    private void Awake()
    {
        if (instance != null) {
            Debug.Log("More than one manager in scene");
            return;
        }
        instance = this;
    }
    private TurretBlueprint turretToBuild;

    public bool canBuild { get { return turretToBuild != null; } }
    public bool hasMoney { get { return LevelManager.money >= turretToBuild.cost; } }


    public void BuildTurretOn (Node node)
    {
        if (LevelManager.money < turretToBuild.cost)
            return;

        // Subtracting cost
        LevelManager.money -= turretToBuild.cost;

        GameObject turret = Instantiate(turretToBuild.prefab, node.GetBuildPosition(), Quaternion.identity);
        node.turret = turret;
    }
    public void SelectTurretToBuild(TurretBlueprint turret)
    {
        turretToBuild = turret;
    }
}