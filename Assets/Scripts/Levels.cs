using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Levels : MonoBehaviour
{
    public List<GameObject> ListLevels = new();

    private void Awake()
    {
        foreach (Transform child in transform)
        {
            if (child.GetComponent<Player>() == null)
                ListLevels.Add(child.gameObject);
        }
    }
}
