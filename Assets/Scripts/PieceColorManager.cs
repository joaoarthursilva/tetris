using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceColorManager : MonoBehaviour
{
    [SerializeField] private Color color;
    private void Start()
    {
        foreach (var spriteRenderer in gameObject.GetComponentsInChildren<SpriteRenderer>())
        {
            spriteRenderer.color = color;
        }
    }
}
