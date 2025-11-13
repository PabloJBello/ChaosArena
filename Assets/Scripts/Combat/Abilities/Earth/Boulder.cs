using UnityEngine;
using DamageSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Boulder : MonoBehaviour
{
    [SerializeField] private float damage = 10f;
    [SerializeField] private Vector2 knockback = new Vector2(5f, 2f);
    [SerializeField] private float hitstop = 0.1f;
    [SerializeField] private Team team = Team.Player;
    [SerializeField] private float lifetime = 5f;
    
    private Rigidbody2D rb;
    private bool hasCollided = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (hasCollided) return;
        hasCollided = true;

        GameObject other = collision.gameObject;

        if (other.TryGetComponent(out Hurtbox hurtbox))
        {
            if (hurtbox.Team == team)
            {
                DestroySelf();
                return;
            }

            // Build hit info
            HitInfo hit = new HitInfo
            {
                damage = damage,
                knockback = GetKnockbackDirection(other.transform),
                hitStop = hitstop,
                team = team
            };

            hurtbox.TakeDamage(hit);

            DestroySelf();
            return;
        }

        if (other.CompareTag("Climbable") || other.layer == LayerMask.NameToLayer("Ground"))
        {
            DestroySelf();
            return;
        }

        if (other.CompareTag("Untagged") || other.layer == LayerMask.NameToLayer("Default"))
        {
            DestroySelf();
            return;
        }
    }

    private Vector2 GetKnockbackDirection(Transform target)
    {
        Vector2 direction = (target.position - transform.position).normalized;
        direction.y = Mathf.Max(direction.y, 0.2f); // always some upward knockback
        return new Vector2(direction.x * knockback.x, direction.y * knockback.y);
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }
}

