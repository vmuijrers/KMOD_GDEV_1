using UnityEngine;

[CreateAssetMenu(fileName = "FloatValue", menuName = "VariableObjects")]
public class FloatObject : ScriptableObject
{
    public event System.Action<float, float> OnValueChanged;

    [SerializeField] private float value;
    public float Value { get
        {
            return value;
        }
        set
        {
            if(this.value!= value)
            {
                OnValueChanged?.Invoke(this.value, value);
            }
            this.value = value;
        }
    }
}
