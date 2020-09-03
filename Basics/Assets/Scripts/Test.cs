using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public delegate void MyDelegate();
    public MyDelegate wave;

    public delegate void MyIntDelegate(int num);
    public event MyIntDelegate someEvent;
    private Enemy enemy;

    public void Start()
    {
        //SomeClass.OnGameStart += SpawnOrc;
        //AddEnemiesToWave(3, SpawnOrc); 
        //AddEnemiesToWave(5, SpawnGoblin);
        ////wave += SpawnOrc;
        ////wave = null;
        ////This executes the wave
        //wave();

        //someEvent += ShowTheNumber;
        //someEvent(3);
        enemy = new Enemy(100);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            enemy.TakeDamage(10);
        }
    }

    public void ShowTheNumber(int someNumber)
    {
        Debug.Log(someNumber);
    }

    public void AddEnemiesToWave(int num, MyDelegate func)
    {
        for (int i = 0; i < num; i++)
        {
            wave += func;
        }
    }

    public void SpawnOrc()
    {
        Debug.Log("We spawn an Orc");
    }

    public void SpawnGoblin()
    {
        Debug.Log("We spawn a goblin");
    }
}

public class ScoreManager
{
    private int score = 0;
    public void Init()
    {
        Enemy.OnDie += AddScore;
        EventManager.AddListener(EventType.ON_ENEMY_KILLED, AddPoint);
        EventManager<IPointable>.AddListener(EventType.ON_ENEMY_KILLED, AddScore);

    }

    public void AddScore(IPointable pointable)
    {
        score += pointable.Points;
    }

    public void AddPoint()
    {
        score++;
    }
}

public interface IPointable
{
    int Points { get; set; }
}

public class Enemy : IPointable
{
    public int Points { get; set; }
    public delegate void MyDelegate(Enemy enemy);
    public static event MyDelegate OnDie;
    private float health;
    public float Health
    {
        get => health;
        set
        {
            if (health > 0 && value <= 0)
            {
                //OnDie?.Invoke(this);
                EventManager.RaiseEvent(EventType.ON_ENEMY_KILLED);
                EventManager<IPointable>.RaiseEvent(EventType.ON_ENEMY_KILLED, this);
                EventManager<Enemy>.RaiseEvent(EventType.ON_ENEMY_KILLED, this);
            }
            health = value;
        }
    }
    public System.Action SomeAction;
    public Enemy(float health)
    {
        Health = health;
        SomeAction += DoSomething;
    }

    public void DoSomething()
    {

    }
    public void TakeDamage(int damage)
    {
        Health -= damage;
        EventManager<int>.RaiseEvent(EventType.ON_ENEMY_HIT, (int)Health);
    }

}



public class SomeClass
{
    public delegate void VoidDelegate();

    public static event VoidDelegate OnGameStart;
    public void Init(Test test)
    {
        test.someEvent += DoSomething;
        //test.someEvent = null; //This is not possible because it is an event
        test.wave = null; //This is possible because wave is a delegate
        OnGameStart();
    }
    public void DoSomething(int something)
    {

    }
}

//public class EventManager
//{
//    //Add here all the events needed for the game
//    public enum EventEnum
//    {
//        ON_GAME_START = 0
//    }

//    public delegate void VoidDelegate();
//    private Dictionary<EventEnum, VoidDelegate> allEvents = new Dictionary<EventEnum, VoidDelegate>();
//    public void SubscribeToEvent(EventEnum eventType, VoidDelegate func)
//    {
//        if (allEvents.ContainsKey(eventType))
//        {
//            allEvents[eventType] += func;
//        }
//    }

//    public void RemoveListener(EventEnum eventType, VoidDelegate func)
//    {
//        if (allEvents.ContainsKey(eventType) && allEvents[eventType] != null)
//        {
//            allEvents[eventType] -= func;
//        }
//    }

//    public void RaiseEvent(EventEnum eventType)
//    {
//        if(allEvents.ContainsKey(eventType) && allEvents[eventType] != null)
//        {
//            allEvents[eventType].Invoke();
//        }
//    }
//}

//        AudioManager.Instance.enabled = false;
//public interface IDamageable
//{
//    int Health { get; set; }
//    void TakeDamage(int dam);
//}

//public abstract class ActorBase : IDamageable
//{
//    public ActorBase(int health)
//    {
//        Health = health;
//    }

//    public int Health { get; set; }

//    public abstract void TakeDamage(int dam);

//    public virtual void Move()
//    {
//        //Do some movement
//    }
//}

//public class Player : ActorBase
//{
//    public Player(int health) : base(health)
//    {

//    }
//    public override void TakeDamage(int dam)
//    {
//        Health -= dam;
//    }
//}

//public class EnemyOne : ActorBase
//{
//    public EnemyOne(int health) : base(health)
//    {
//        Health *= 2;
//    }

//    public override void TakeDamage(int dam)
//    {
//        Health -= dam;
//    }
//}

//public class Wall : IDamageable
//{
//    public int Health { get; set; }

//    public Wall(int health)
//    {
//        Health = health;
//    }

//    public void TakeDamage(int dam)
//    {
//        Health -= dam;
//    }
//}