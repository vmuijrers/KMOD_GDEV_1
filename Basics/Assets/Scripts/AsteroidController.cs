using System;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{

    private System.Action SomeAction;
    Asteroid someAsteroid;
    // Start is called before the first frame update
    void Start()
    {
        someAsteroid = new Asteroid();
        someAsteroid.OnDeadEvent += OnAsteroidDestroyed;

        SomeAction += FixedUpdate;
    }

    private void FixedUpdate()
    {
        someAsteroid.Update();
    }

    private void OnAsteroidDestroyed(Asteroid asteroid)
    {
        //Add Score 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public interface IEvent
{
    event System.Action<Asteroid> OnDeadEvent;
}

public class Asteroid : IEvent
{
    public event Action<Asteroid> OnDeadEvent;

    public Asteroid()
    {
    }
    public void Update() { } 
    public void OnDestroy()
    {
        OnDeadEvent?.Invoke(this);
    }
}