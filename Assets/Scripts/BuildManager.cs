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
    private GameObject turretToBuild;

    public GameObject standardTurretPrefab;

    void Start()
    {
        turretToBuild = standardTurretPrefab;
    }

    public GameObject getTurretToBuild()
    {
        return turretToBuild;
    }
}
