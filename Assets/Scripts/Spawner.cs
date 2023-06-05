using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    public GameObject[] groups;

    private void Start()
    {
        spawnNext();
    }

    public void spawnNext()
    {
        var i = Random.Range(0, groups.Length);

        // Spawn Group at current Position
        Instantiate(groups[i],
            transform.position,
            Quaternion.identity);
    }
}