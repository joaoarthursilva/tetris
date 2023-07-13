using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] groups;
    private Queue<int> _nextPieces;
    [SerializeField] private int numMaxDeProx = 5;
    private DisplayNextPieces _displayNextPieces;

    private void Awake()
    {
        _displayNextPieces = FindObjectOfType<DisplayNextPieces>();
        _nextPieces = new Queue<int>();
        for (var j = 0; j < numMaxDeProx; j++)
        {
            _nextPieces.Enqueue(Random.Range(0, groups.Length));
        }
    }

    private void Start()
    {
        SpawnNext();
    }

    public void SpawnNext() // instancia uma peça
    {
        Instantiate(groups[_nextPieces.Dequeue()],
            transform.position,
            Quaternion.identity);
        _nextPieces.Enqueue(Random.Range(0, groups.Length));
        _displayNextPieces.UpdateNextPieceSprites();
    }

    public Queue<int> GetQueue()
    {
        return _nextPieces;
    }
}