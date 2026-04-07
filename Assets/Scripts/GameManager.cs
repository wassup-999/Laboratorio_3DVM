using UnityEngine;
using UnityEngine.Windows;
using static UnityEngine.InputSystem.InputAction;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public float GamePlayTime = 180;
    public float GamePlayCounter;
    public ThirdPersonController player;
    public Bomb Bombprefab;
    public float BombExploteCounter;
    public float BombExploteValue = 5;
    

    public void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    void Start()
    {
        
    }


    void Update()
    {
        Victory();
        Defeat();
        GamePlayCounter += Time.deltaTime;

    }
    
    public void Victory()
    {
        if(GamePlayCounter >= GamePlayTime)
        {
            SceneManager.LoadScene(1);
        }        
    }
    public void Defeat()
    {
        if (player == null)
        {

            SceneManager.LoadScene(2);
        }
    }
    
}
