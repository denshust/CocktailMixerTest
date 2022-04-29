using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesHandler : MonoBehaviour
{
    [SerializeField] private int _maxLvls=3;
    
    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
    
    public void GoNextLvl()
    {
        Level.LevelIndex++;
        Level.LevelIndex %= _maxLvls;
        SceneManager.LoadScene(0);
    }
}
