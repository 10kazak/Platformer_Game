using UnityEngine;
using UnityEngine.UI;

public class KeyText : Entity
{
    public static int Key;
    Text text;
    void Start()
    {
        text = GetComponent<Text>();
    }

    void Update()
    {
        text.text = Key.ToString() + "/5";
        
    }
}
