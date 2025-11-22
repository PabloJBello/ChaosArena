using UnityEngine;

/* Secondary attack (stationary wall that can block or be hit). */
[RequireComponent(typeof(BoxCollider2D))]
public class RockWall : MonoBehaviour
{
    [SerializeField] private float maxHealth = 50f;
    [SerializeField] private float lifetime = 10f;
    
    private WallHealth wallHealth;

    private void Awake()
    {
        wallHealth = GetComponent<WallHealth>();
        if (wallHealth != null)
        {
            wallHealth.Initialize(maxHealth);
        }
    }

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }
}
