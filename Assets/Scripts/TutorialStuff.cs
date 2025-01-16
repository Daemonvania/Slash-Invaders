using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class TutorialStuff : MonoBehaviour
{
    private IAbility currentAbility = new DelayDecorator(new Fireball());

    void UseAbility()
    {
        currentAbility.Activate();
    }
    
}

public interface IAbility
{
    void Activate();
}

public class SequenceComposite : IAbility
{
    private List<IAbility> abilities = new List<IAbility>();

    public void AddAbilies(List<IAbility> abilities)
    {
        this.abilities = abilities;
    }
    
    public void Activate()
    {
        foreach (IAbility ability in abilities)
        {
            ability.Activate();
        }
    }
}

public class DelayDecorator : IAbility
{
    private IAbility wrappedAbility;

    public DelayDecorator(IAbility ability)
    {
        this.wrappedAbility = ability;
    }

    public void Activate()
    {
        // DELAYYY
        wrappedAbility.Activate();
    }
}

public class Fireball : IAbility
{
    public void Activate()
    {
        Debug.Log("Ability One Activated");
    }
}

public class Heal : IAbility
{
    public void Activate()
    {
        Debug.Log("Ability Two Activated");
    }
}

public class Speed : IAbility
{
    public void Activate()
    {
        Debug.Log("Ability Three Activated");
    }
}