using UnityEngine;
using ControllerSystem;

[RequireComponent(typeof(FighterController))]
public class Ability : MonoBehaviour
{ 
    [SerializeField] private float primaryCooldown = 1f;
    [SerializeField] private float secondaryCooldown = 3f;

    private BaseElementalAbility currentAbility;
    private FighterController fighterController;
    
    private float lastPrimaryTime = Mathf.NegativeInfinity;
    private float lastSecondaryTime = Mathf.NegativeInfinity;

    private void Awake()
    {
        fighterController = GetComponent<FighterController>();
    }

    private void Start()
    {
        // TEMP for testing input
        var testAbility = GetComponentInChildren<EarthAbility>();
        if (testAbility != null)
        {
            EquipAbility(testAbility);
        }
    }

    private void Update()
    {
        if (fighterController == null || !fighterController.InMovementState())
            return;
        
        var input = fighterController.Input;
        if (input == null) return;

        if (input.primary.GetPressedThisFrame() && CanUsePrimary())
        {
            currentAbility?.UsePrimary();
            lastPrimaryTime = Time.time;
        }

        if (input.secondary.GetPressedThisFrame() && CanUseSecondary())
        {
            currentAbility?.UseSecondary();
            lastSecondaryTime = Time.time;
        }
    }

    private bool CanUsePrimary()
    {
        return Time.time >= lastPrimaryTime + primaryCooldown;
    }

    private bool CanUseSecondary()
    {
        return Time.time >= lastSecondaryTime + secondaryCooldown;
    }

    public void EquipAbility(BaseElementalAbility newAbility)
    {
        if (currentAbility != null)
            currentAbility.OnUnequip();
        
        currentAbility = newAbility;
        currentAbility.Initialize(fighterController);
        currentAbility.OnEquip();
    }
    
    public void UnequipAbility()
    {
        if (currentAbility == null)
            return;

        currentAbility.OnUnequip();
        currentAbility = null;
    }
}

