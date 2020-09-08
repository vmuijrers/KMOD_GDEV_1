using System;
using System.Collections.Generic;

public class ObjectPool<T> where T : IPoolable
{
    private List<T> _activePool = new List<T>();
    private List<T> _inactivePool = new List<T>();

    public ObjectPool()
    {
        AddNewItemToPool();
    }

    public T RequestObject()
    {
        if(_inactivePool.Count > 0)
        {
            return ActivateItem(_inactivePool[0]);
        }
        return ActivateItem(AddNewItemToPool());
    }

    public T ActivateItem(T item)
    {
        item.OnEnableObject();
        item.Active = true;
        if (_inactivePool.Contains(item))
        {
            _inactivePool.Remove(item);
        }
        _activePool.Add(item);
        return item;
    }

    public void ReturnObjectToPool(T item)
    {
        if (_activePool.Contains(item))
        {
            _activePool.Remove(item);
        }
        item.OnDisableObject();
        item.Active = false;
        _inactivePool.Add(item);
    }

    private T AddNewItemToPool()
    {
        T instance = (T)Activator.CreateInstance(typeof(T));
        _inactivePool.Add(instance);
        UnityEngine.Debug.Log("A New Item was added to the pool");
        return instance;
    }
}

public interface IPoolable
{
    bool Active { get; set; }
    void OnEnableObject();
    void OnDisableObject();
}



