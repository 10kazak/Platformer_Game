using UnityEngine;

public class ActiveButton : MonoBehaviour
{
    public GameObject jost;
    public GameObject buttonJump;

    void Update()
    {
        if (Hero.control == 1)
        {
            jost.SetActive(false);
            buttonJump.SetActive(false);
        }

        if (Hero.control == 2)
        {
            jost.SetActive(false);
        }
    }

}
