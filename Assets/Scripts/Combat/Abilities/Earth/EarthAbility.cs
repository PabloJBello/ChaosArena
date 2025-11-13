using System;
using UnityEngine;

public class EarthAbility : BaseElementalAbility
{
    [SerializeField] private GameObject boulderPrefab;
    [SerializeField] private GameObject rockWallPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float boulderForce = 10f;
    [SerializeField] private float wallDistance = 2f;
    [SerializeField] private float wallHeightOffset = -0.8f;

    private AbilitySpawner abilitySpawner;
    private FighterController fighterController;

    private void Awake()
    {
        abilitySpawner = GetComponentInParent<AbilitySpawner>();
        fighterController = GetComponentInParent<FighterController>();

        if (spawnPoint == null)
            spawnPoint = transform;
    }

    public override void UsePrimary()
    {
        SpawnBoulder();
    }

    public override void UseSecondary()
    {
        SpawnWall();
    }

    private void SpawnBoulder()
    {
        if (boulderPrefab == null || abilitySpawner == null)
        {
            Debug.LogWarning("Boulder prefab or spawner missing!");
            return;
        }

        // Determine facing direction
        float direction = fighterController != null && fighterController.FacingLeft ? -1f : 1f;
        Vector2 launchDir = Vector2.right * direction;
        
        GameObject boulder = abilitySpawner.Spawn(boulderPrefab, spawnPoint.position, Quaternion.identity);

        // Flip visual direction if needed
        Vector3 scale = boulder.transform.localScale;
        scale.x = Mathf.Abs(scale.x) * direction;
        boulder.transform.localScale = scale;

        // Add physics force
        if (boulder.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb))
        {
            rb.AddForce(launchDir * boulderForce, ForceMode2D.Impulse);
        }

        Debug.Log($"EarthAbility: Spawned Boulder facing {(direction > 0 ? "Right" : "Left")}");
    }

    private void SpawnWall()
    {
        if (rockWallPrefab == null || abilitySpawner == null)
        {
            Debug.LogWarning("RockWall prefab or spawner missing!");
            return;
        }

        // Determine facing direction
        float direction = fighterController != null && fighterController.FacingLeft ? -1f : 1f;

        // Compute wall position
        Vector2 wallPos = (Vector2)spawnPoint.position + Vector2.right * wallDistance * direction;
        wallPos.y += wallHeightOffset;
        
        GameObject wall = abilitySpawner.Spawn(rockWallPrefab, wallPos, Quaternion.identity);

        // Flip wall visually if necessary
        Vector3 scale = wall.transform.localScale;
        scale.x = Mathf.Abs(scale.x) * direction;
        wall.transform.localScale = scale;

        Debug.Log($"EarthAbility: Spawned Rock Wall facing {(direction > 0 ? "Right" : "Left")} at {wallPos}");
    }
}
