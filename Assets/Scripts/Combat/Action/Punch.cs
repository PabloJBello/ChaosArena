using UnityEngine;
using DamageSystem;
using ControllerSystem;
using ControllerSystem.Platformer2D;

[RequireComponent(typeof(FighterController))]
public class Punch : MonoBehaviour
{
    /* Hitbox Setup */
    [SerializeField] private Hitbox punchHitbox;
    [SerializeField] private Transform punchSpawnPoint;

    /* Punch Settings */
    [SerializeField] private float punchDuration = 0.15f;
    [SerializeField] private float cooldown = 0.25f;

    /* Hit Info */
    [SerializeField] private float damage = 5f;
    [SerializeField] private Vector2 baseKnockback = new Vector2(3f, 2f);
    [SerializeField] private float hitstop = 0.1f;
    [SerializeField] private Team team = Team.Player;

    private PlatformerAnimator platformerAnimator;
    private FighterController fighterController;
    private bool attacking;

    private void Awake()
    {
        fighterController = GetComponent<FighterController>();
        platformerAnimator = GetComponentInChildren<PlatformerAnimator>();
    }

    private void Update()
    {
        if (fighterController.InMovementState() && !attacking)
        {
            if (fighterController.Input.primary.GetPressedThisFrame())
            {
                StartCoroutine(PerformPunch());
            }
        }
    }

    private System.Collections.IEnumerator PerformPunch()
    {
        attacking = true;
        fighterController.UpdateState(FighterController.States.Ability);

        platformerAnimator?.OnPunch();
        
        // Build hit info
        HitInfo hitInfo = new HitInfo
        {
            damage = damage,
            knockback = fighterController.FacingLeft ? new Vector2(-baseKnockback.x, baseKnockback.y) : baseKnockback,
            hitStop = hitstop,
            team = team
        };

        // Determine facing direction
        Vector3 spawnPos = punchSpawnPoint.position;
        Vector3 scale = punchHitbox.transform.localScale;
        
        // Flip hitbox horizontally if facing left
        if (fighterController.FacingLeft)
        {
            scale.x = Mathf.Abs(scale.x) * -1f;
        }
        else
        {
            scale.x = Mathf.Abs(scale.x);
        }
        
        // Apply position and orientation
        punchHitbox.transform.position = spawnPos;
        punchHitbox.transform.localScale = scale;
        
        // Activate hitbox
        punchHitbox.Activate(hitInfo);

        // Deactivate after a short window
        yield return new WaitForSeconds(punchDuration);
        punchHitbox.Deactivate();

        // Small recovery delay
        yield return new WaitForSeconds(cooldown);

        fighterController.UpdateState(FighterController.States.Movement);
        attacking = false;
    }
}
