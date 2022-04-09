using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public Transform collectibleRoot;
    private List<Transform> collectibles;
    public List<Vector3> collectibleDefaultPositions;
    public void Start()
    {
        FindCollectibles();
    }

    private void FindCollectibles()
    {
        collectibles = new List<Transform>();
        collectibleDefaultPositions = new List<Vector3>();
        for (int i = 0; i < collectibleRoot.childCount; i++)
        {
            collectibles.Add(collectibleRoot.GetChild(i).transform);
            collectibleDefaultPositions.Add(collectibleRoot.GetChild(i).transform.position);
        }
    }
    public void ResetLevel()
    {
        for (int i = 0; i < collectibles.Count; i++)
        {
            collectibles[i].position = collectibleDefaultPositions[i];
            collectibles[i].SetParent(transform);
            collectibles[i].gameObject.SetActive(true);
        }
    }

    void Update()
    {
        
    }
}
