using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasButton : MonoBehaviour
{
     public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
    public void Exit()
    {
        Application.Quit();
    }
    
}
