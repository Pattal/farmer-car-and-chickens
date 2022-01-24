using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TileManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> availableTiles = new List<GameObject>();
    [SerializeField] private List<Tile> currentTiles = new List<Tile>();
    [SerializeField] private List<Tile> hardTiles = new List<Tile>();

    private float durationOfDestroingCycle = 6.0f;
    private float durationOfSpawningCycle = 6.0f;

    private float tileLength = 12.0f;

    private void Start()
    {
        StartCoroutine(SpawnNewTile());
        StartCoroutine(DestroyTileCycle());
    }

    private void SpawnTile()
    {
        var pos = currentTiles.Last().gameObject.transform.position + Vector3.forward * tileLength;

        if(Tile.NumberOfTiles % 15 == 0)
        {
            var rand = Random.Range(0, 100) % hardTiles.Count;
            var obj = Instantiate(hardTiles[rand], pos, Quaternion.identity);
            currentTiles.Add(obj.GetComponent<Tile>());
        }
        else
        {
            var rand = Random.Range(0, 100) % availableTiles.Count;
            var obj = Instantiate(availableTiles[rand], pos, Quaternion.identity);
            currentTiles.Add(obj.GetComponent<Tile>());
        }
        
    }

    private void DestroyTile(Tile tile)
    {
        currentTiles.Remove(tile);
        Destroy(tile.gameObject);
    }


    IEnumerator SpawnNewTile()
    {
        while(true)
        {
            SpawnTile();
            yield return new WaitForSeconds(durationOfSpawningCycle);
        }
        
    }

    IEnumerator DestroyTileCycle()
    {
        while (true)
        {
            DestroyTile(currentTiles.First());
            yield return new WaitForSeconds(durationOfDestroingCycle);
        }

    }



}
