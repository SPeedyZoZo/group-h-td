using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    private GameObject turret;

    public Color hoverColor;
    public Vector3 positionOffset;
    private Color startColor;
    private Renderer rend;
    

    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
    }

    void OnMouseDown() // called when mouse is used
    {
        if (turret != null) {
            Debug.Log("cant place another turret");
            return;
        }

        GameObject turretToBuild = BuildManager.instance.getTurretToBuild();
        turret = (GameObject) Instantiate(turretToBuild, transform.position + positionOffset, transform.rotation);
    }

    void OnMouseEnter() // called when mouse enters collider
    {
        rend.material.color = hoverColor;
    }

    void OnMouseExit() // called when mouse enters collider
    {
        rend.material.color = startColor;
    }
}
