using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DamageSpellDecorator
{
    public int Damage { get; set; }
    public DamageSpellDecorator(int damage) 
    {
        Damage = damage;
    }

    public abstract ISpell Decorate(ISpell spell);
}

public class FireDecorator : DamageSpellDecorator
{
    public FireDecorator(int damage) : base(damage) { }

    public override ISpell Decorate(ISpell spell)
    {
        Debug.Log("Add Some Fire To it!");
        spell.Damage += Damage;
        spell.SpellTypes |= SpellType.Fire;
        return spell;
    }
}

public class IceDecorator : DamageSpellDecorator
{
    public IceDecorator(int damage) : base(damage) { }

    public override ISpell Decorate(ISpell spell)
    {
        Debug.Log("Add Some Ice To it!");
        spell.SpellTypes |= SpellType.Ice;
        spell.Damage += Damage;
        return spell;
    }
}

public class DecoratorTest
{
    public void Setup()
    {
        //Create the spell
        ISpell someSpell = new Spell(5);

        //Decorate it with fire damage
        FireDecorator fireDecorator = new FireDecorator(20);
        someSpell = fireDecorator.Decorate(someSpell);

        //Decorate some more with ice damage
        IceDecorator iceDecorator = new IceDecorator(10);
        someSpell = iceDecorator.Decorate(someSpell);

        //Cast the decorated spell (dealing 35 damage with effects: Normal, Fire and Ice)
        someSpell.Cast();
    }
}

[System.Flags]
public enum SpellType
{
    Normal =    1 << 0,
    Fire =      1 << 1,
    Ice =       1 << 2,
    Poison =    1 << 3
}

public interface ISpell
{
    int Damage { get; set; }
    SpellType SpellTypes { get; set; }
    void Cast();
}

public class Spell : ISpell
{
    public int Damage { get; set; }
    public SpellType SpellTypes { get; set; } = SpellType.Normal;

    public Spell(int damage)
    {
        Damage = damage;
    }

    public void Cast()
    {
        Debug.Log("Do the damage: " + Damage + " " + SpellTypes);
    }
}