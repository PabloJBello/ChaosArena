using UnityEngine;
using ControllerSystem;

public abstract class BaseElementalAbility : MonoBehaviour
{
    protected FighterController fighterController;
    
    public virtual void Initialize(FighterController controller)
    {
        fighterController = controller;
    }
    
    public virtual void OnEquip() { }
    public virtual void OnUnequip() { }
    public abstract void UsePrimary();
    public abstract void UseSecondary();
}

