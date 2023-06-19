using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] groups;

    private void Start()
    {
        SpawnNext();
    }

    public void SpawnNext() // instancia uma peça
    {
        var i = Random.Range(0, groups.Length);

        Instantiate(groups[i],
            transform.position,
            Quaternion.identity);
    }
}