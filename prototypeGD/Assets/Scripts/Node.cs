using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    [Header("Optional")]
    public GameObject turret;

    public Color hoverColor;
    public Vector3 positionOffset;
    private Color startColor;
    public Color noMoneyColor;
    private Renderer rend;
    
    BuildManager buildManager;

    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;

        buildManager = BuildManager.instance;
    }

    void OnMouseDown() // called when mouse is used
    {

        if (!buildManager.canBuild)
        {
            return;
        }

        if (turret != null) {
            Debug.Log("cant place another turret");
            return;
        }

        buildManager.BuildTurretOn(this);
    }

    public Vector3 GetBuildPosition() 
    {
        return transform.position + positionOffset;
    }

    void OnMouseEnter() // called when mouse enters collider
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (!buildManager.canBuild)
        {
            return;
        }

        if (buildManager.hasMoney) 
        {
            rend.material.color = hoverColor;
        } 
        else
        {
            rend.material.color = noMoneyColor;
        }

    }

    void OnMouseExit() // called when mouse exits collider
    {
        rend.material.color = startColor;
    }
}
