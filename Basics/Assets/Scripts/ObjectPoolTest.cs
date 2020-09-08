using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ObjectPoolTest : MonoBehaviour
{
    private ObjectPool<Enemy> _enemyPool;
    public void Awake()
    {
        _enemyPool = new ObjectPool<Enemy>();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Enemy enemy = _enemyPool.RequestObject();
            enemy.OnDie += OnEnemyDead;
            /*
             * Use the enemy here
             */
            enemy.Die();
        }
    }

    public void OnEnemyDead(Enemy enemy)
    {
        _enemyPool.ReturnObjectToPool(enemy);
    }

    public class Enemy : IPoolable
    {
        public bool Active { get; set; }
        public event Action<Enemy> OnDie;

        public void Die()
        {
            OnDie?.Invoke(this);
        }

        public void OnEnableObject()
        {
            Debug.Log("Enemy Activated!");
        }

        public void OnDisableObject()
        {
            Debug.Log("Enemy DeActivated!");

            //Clear the event, because we want to reuse this object later
            OnDie = null;
        }
    }
}
