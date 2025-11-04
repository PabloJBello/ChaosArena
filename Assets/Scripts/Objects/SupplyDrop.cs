using UnityEngine;
using InputManagement;

[RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
public class SupplyDrop : MonoBehaviour
{
    private InputManager inputManager;
    
    /* Drop Settings */
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float landOnFirstChance = 0.5f;
    [SerializeField] private float fallThroughTime = 0.25f;
    
    /* Interaction */
    [SerializeField] private float interactRange = 1.5f;
    
    /* Visuals & Effects */
    // [SerializeField] private SpriteRenderer highlightSprite;
    // [SerializeField] private ParticleSystem landingEffect;
    
    private Rigidbody2D rb;
    private BoxCollider2D collider;
    private Transform player;
    private bool landed;
    private bool canInteract;
    private bool allowCollision = true;

    private void Awake()
    {
        inputManager = FindObjectOfType<InputManager>();
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (!player)
        {
            var playerObj =  GameObject.FindGameObjectWithTag("Player");
            if (playerObj)
                player = playerObj.transform;
        }

        if (!player || !landed)
            return;
        
        canInteract = Vector2.Distance(player.position, transform.position) <= interactRange;
        /* if (highlightSprite)
            highlightSprite.enabled = canInteract; */
        
        if (canInteract && inputManager.Input.interact.GetPressedThisFrame())
            OnPickup(player.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (landed || !allowCollision)
            return;
        
        // Check if grounded
        if (((1 << collision.gameObject.layer) & groundLayer) != 0)
        {
            bool shouldLandHere = Random.value < landOnFirstChance;

            if (!shouldLandHere && rb.linearVelocity.y > 0)
            {
                // If moving downward, fall through
                Physics2D.IgnoreCollision(collider, collision.collider, true);
                StartCoroutine(ReenableCollision(collision.collider, fallThroughTime));
            }
            else
            {
                Land(collision.contacts[0].point);
            }
        }
    }
    
    private System.Collections.IEnumerator ReenableCollision(Collider2D platform, float delay)
    {
        allowCollision = false;
        yield return new WaitForSeconds(delay);
        Physics2D.IgnoreCollision(collider, platform, false);
        allowCollision = true;
    }
    
    private void Land(Vector2 point)
    {
        landed = true;
        rb.bodyType = RigidbodyType2D.Static;
        // rb.linearVelocity = Vector2.zero;
        // landingEffect?.Play();
    }
    
    private void OnPickup(GameObject playerObj)
    {
        Debug.Log("Supply drop collected!");

        //var inventory = playerObj.GetComponent<PlayerInventory>();
        //if (inventory != null)
        //    inventory.GiveRandomWeapon();

        Destroy(gameObject);
    }
}
