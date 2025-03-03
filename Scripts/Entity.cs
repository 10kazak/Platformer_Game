using UnityEngine;
using UnityEngine.SceneManagement;

public class Entity : MonoBehaviour
{
    protected int lives;
    protected int keys;

    public virtual void GetDamage()
    {
        lives--;
        if (lives < 1)
        {
            Die();
        }
    }

    public virtual void GetKey()
    {
        keys++;
        if (keys < 4)
        {
            SceneManager.LoadScene(1);
        }
    }

    public virtual void Die()
    {
        Destroy(this.gameObject);
        SceneManager.LoadScene(1);
    }
}
