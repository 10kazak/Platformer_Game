using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform Hero;
    private Vector3 coordinate;

    private void Awake()
    {
        if (!Hero)
        {
            Hero = FindObjectOfType<Hero>().transform;
        }
    }
    private void Update()
    {
        coordinate = Hero.position;
        coordinate.z = -10f;


        transform.position = Vector3.Lerp(transform.position, coordinate, Time.deltaTime);
    }
}
