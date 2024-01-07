using UnityEngine;

public class Shop : MonoBehaviour
{
    public TurretBlueprint basic;
    public TurretBlueprint gatling;
    public TurretBlueprint sniper;

    public void SelectBasicTurret()
    {
        BuildManager.instance.SelectTurretToBuild(basic);
    }

    public void SelectGatlingTurret()
    {
        BuildManager.instance.SelectTurretToBuild(gatling);
    }

    public void SelectSniperTurret()
    {
        BuildManager.instance.SelectTurretToBuild(sniper);
    }
}