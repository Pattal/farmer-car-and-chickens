using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public static int NumberOfTiles;

    [SerializeField] private GameObject fuelPrefab;
    [SerializeField] private int id;

    private void OnEnable()
    {
        IncementNumberOfTiles();
        id = NumberOfTiles;
    }

    void Start()
    {
        SpawnFuel();
    }

    private static void IncementNumberOfTiles()
    {
        NumberOfTiles++;
    }

    private void SpawnFuel()
    {
        if(NumberOfTiles % 2 == 0)
        {
            var rndLength = Random.Range(0, 100) % 20;
            var rndWidth = Random.Range(-100, 100) % 2;
            var newPos = new Vector3(transform.position.x + rndWidth, 0.3f, transform.position.z + rndLength);

            Instantiate(fuelPrefab, newPos, Quaternion.identity);
        }
        
    }
}
