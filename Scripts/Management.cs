using UnityEngine;
using UnityEngine.UI;

public class Management : MonoBehaviour
{
    public Toggle keyboard, gyroscope, joystick;

    void Start()
    {
        keyboard.onValueChanged.AddListener((isOn) => SetControl(1));
        gyroscope.onValueChanged.AddListener((isOn) => SetControl(2));
        joystick.onValueChanged.AddListener((isOn) => SetControl(3));

        if (keyboard.isOn) SetControl(1);
        else if (gyroscope.isOn) SetControl(2);
        else if (joystick.isOn) SetControl(3);
    }

    void SetControl(int value)
    {
        Hero.control = value;
    }
}
