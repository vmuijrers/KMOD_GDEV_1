using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEventListener : MonoBehaviour
{
    public EventType type;
    private Text text;
    // Start is called before the first frame update
    private void Awake()
    {
        text = GetComponent<Text>();
    }

    private void OnEnable()
    {
        EventManager<int>.AddListener(type, UpdateUI);
    }

    private void OnDisable()
    {
        EventManager<int>.RemoveListener(type, UpdateUI);
    }

    public void UpdateUI(int number)
    {
        text.text = "Health: " + number;
    }
}
