using UnityEngine;
using ControllerSystem;

[RequireComponent(typeof(FighterController))]
public class Ability : MonoBehaviour
{ 
    private BaseElementalAbility currentAbility;
    private FighterController fighterController;

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

        if (input.primary.GetPressedThisFrame())
            currentAbility?.UsePrimary();

        if (input.secondary.GetPressedThisFrame())
            currentAbility?.UseSecondary();
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

