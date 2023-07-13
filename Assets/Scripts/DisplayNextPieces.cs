using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayNextPieces : MonoBehaviour
{
    [SerializeField] private List<Sprite> pieceSprites;
    [SerializeField] private List<Image> nextUiImages;
    private Queue<int> _nextPieces;
    private Spawner _spawner;

    private void Awake()
    {
        _spawner = FindObjectOfType<Spawner>();
    }

    private void Start()
    {
        UpdateNextPieceSprites();
    }

    public void UpdateNextPieceSprites()
    {
        _nextPieces = _spawner.GetQueue();
        var arr = _nextPieces.ToArray();
        for (var i = 0; i < arr.Length; i++)
        {
            var currImg = nextUiImages[i];
            var currNxtPiece = arr[i];
            currImg.sprite = pieceSprites[currNxtPiece];
        }
    }
}