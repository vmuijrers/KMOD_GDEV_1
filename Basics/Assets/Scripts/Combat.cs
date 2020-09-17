using System.Collections.Generic;
using UnityEngine;
namespace SickCombatGame
{

    public interface IArmor
    {
        int ArmorPoints { get; }
    }

    public interface IDamageable
    {
        int Health { get; }
        void TakeDamage(int damage);
    }

    public interface IWeapon
    {
        int Weight { get; set; }
        int Damage { get; set; }
        int GetDamage();
    }

    public interface ICombatant : IDamageable
    {
        int GetDamage();
    }

    public class Combat
    {

        public void Fight(ICombatant fighterOne, ICombatant fighterTwo, out ICombatant winner)
        {
            fighterOne.TakeDamage(fighterTwo.GetDamage());
            fighterTwo.TakeDamage(fighterOne.GetDamage());
            winner = fighterOne.Health > fighterTwo.Health ? fighterOne : fighterTwo;
        }
    }

    public class Sword : IWeapon
    {
        private int _speedModifier = 3;

        public int Weight { get; set; }
        public int Damage { get; set; }

        public Sword(int weight, int damage)
        {
            Weight = weight;
            Damage = damage;
        }

        public int GetDamage()
        {
            return Weight * Damage * _speedModifier;
        }
    }


    public class Fighter : ICombatant
    {
        public int Health { get; private set; }

        protected IWeapon _weapon;
        public Fighter(IWeapon weapon)
        {
            this._weapon = weapon;
        }

        public virtual int GetDamage()
        {
            return _weapon.GetDamage();
        }

        public virtual void TakeDamage(int damage)
        {
            Health -= damage;
        }
    }


    public class Player : Fighter, IArmor
    {
        public int Level { get; set; }

        public int ArmorPoints { get; private set; }

        public Player(IWeapon weapon, int armorPoints) : base(weapon)
        {
            ArmorPoints = armorPoints;
        }

        public override int GetDamage()
        {
            return base.GetDamage() * Level;
        }

        public override void TakeDamage(int damage)
        {
            damage -= ArmorPoints;
            damage = Mathf.Max(damage, 0);
            base.TakeDamage(damage);
        }
    }

    public class GameController2 : MonoBehaviour
    {
        private List<ICombatant> enemies = new List<ICombatant>();
        private int numberOfEnemies = 10;
        private ICombatant player;

        private Combat combatManager;
        public void Awake()
        {
            combatManager = new Combat();
            for (int i = 0; i < numberOfEnemies; i++)
            {
                enemies.Add(new Fighter(new Sword(10, 20)));
            }

            player = new Player(null, 3);
        }

        public void FixedUpdate()
        {
            ICombatant winner = null;
            combatManager.Fight(enemies[Random.Range(0, enemies.Count)], player, out winner);
        }

    }
}

