using UnityEngine;
using System.Collections;

public class SupplyDropSpawner : MonoBehaviour
{
    [SerializeField] private GameObject supplyDropPrefab;
    [SerializeField] private float spawnInterval = 15f;
    [SerializeField] private Vector2 spawnXRange = new Vector2(-8f, 8f);
    [SerializeField] private float spawnYAboveCamera = 10f;

    private void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    private IEnumerator SpawnLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            float x = Random.Range(spawnXRange.x, spawnXRange.y);
            float y = Camera.main.transform.position.y + spawnYAboveCamera;
            Instantiate(supplyDropPrefab, new Vector3(x, y, 0f), Quaternion.identity);
        }
    }
}