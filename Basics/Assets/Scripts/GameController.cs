using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {
        
    }
}

//public T DoSomething<T>() where T : new()
//{
//    return new T();
//}

//public abstract class Singleton<T> : MonoBehaviour
//{
//    private static T instance;
//    public static T Instance {
//        get {
//            return instance;
//        }
//        set {
//            if(instance == null) {
//                instance = value;
//            } else {
//                Debug.Log("Two instances detected for singleton!");
//            }
//        }
//    }
//}

//public class AudioManager : Singleton<AudioManager>
//{
//    void Awake()
//    {
//        Instance = this;
//    }
//}

//public class SomeGenericClass<T>
//{
//    public T someGenericVariable;
//    public T SomeGenericFunction<T>()
//    {
//        return default(T);
//    }
//}

////usage: new SomeGenericClass<float>();

//public abstract class SomeAbstractClass<T>
//{
//    public Dictionary<T, float> someDictionary = new Dictionary<T, float>();

//    public static float FindAValue(T key)
//    {
//        return someDictionary[key];
//    }
//    //This method has no body and MUST be overriden by childclasses
//    public abstract void DoSomethingVeryCool();

//    //This method has a base implementation
//    public virtual void DoSomethingCool()
//    {
//        Debug.Log("Hello");
//    }
//}

//public class SomeStaticMethodClass {

//    public static float someSharedFloat;

//    public static void SomeStaticFunction() {

//    }
//}
////Usage: SomeStaticMethodClass.SomeStaticFunction()

//public interface IState
//{
//    void OnEnter();
//    void OnUpdate();
//    void OnExit();
//}

//public class StateMachine
//{
//    private IState currentState;
//    private Dictionary<System.Type, IState> states = new Dictionary<System.Type, IState>();

//    public void OnStart(){
//        AddState(new IdleState(this));
//        AddState(new AttackState(this));
//    }

//    public void OnUpdate(){
//        currentState?.OnUpdate();
//    }

//    public void SwitchState(System.Type type){
//        currentState?.OnExit();
//        currentState = states[type];
//        currentState?.OnEnter();
//    }

//    public void AddState(IState state){
//        states.Add(state.GetType(), state);
//    }
//}

//public abstract class State : IState
//{
//    public State(StateMachine owner)
//    {
//        this.owner = owner;
//    }
//    protected StateMachine owner;
//    public abstract void OnEnter();
//    public abstract void OnUpdate();
//    public abstract void OnExit();
//}
