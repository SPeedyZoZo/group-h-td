using UnityEngine;

public class shop : MonoBehaviour
{
    public TurretBlueprint standardTurret;     // references abstract class TurretBlueprint
    BuildManager buildManager;

    void Start()
    {
        buildManager = BuildManager.instance;
    }
    public void selectStandardTurret()
    {
        Debug.Log("Standard Turret Selected");
        buildManager.SelectTurretToBuild(standardTurret);
    }
}