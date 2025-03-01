using UnityEngine;

public class Key : Entity
{

     void Start()
    {
        
    }

    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Hero"))
        {
            
            KeyText.Key += 1;

            Hero.Instance.GetKey();
            Destroy(gameObject);  // Удаляем ключ
        }
        if (KeyText.Key == 5)
        {
            KeyText.Key -= 5;
        }
    }


}
