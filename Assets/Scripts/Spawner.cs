using UnityEngine;

public class Spawner : MonoBehaviour
{
    private Transform[] path;

    private void Awake()
    {
        path = new Transform[transform.childCount];
        for (int i = 0; i < path.Length; i++)
            path[i] = transform.GetChild(i);
    }
}
