using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : GenericSingleton<GameManager>
{
    [SerializeField] private GameObject pauseCanvas;

    [SerializeField] private string sceneNameMenu;
    [SerializeField] private string sceneNameCastle;
    [SerializeField] private string sceneNameCinematics;
    [SerializeField] private string sceneNameGameScene;

    [SerializeField] private bool isMainMenu;
    [SerializeField] private bool isCastelScene;
    [SerializeField] private bool isCinematicIntro;
    [SerializeField] private bool isGameScene;
    [SerializeField] private bool canPause;


    protected override void Awake()
    {
        base.Awake();
    }


    public void Load_MainMenu()
    {
        SceneManager.LoadScene(sceneNameMenu);
        ResetFlags();
        isMainMenu = true;
    }
    public void Load_CastelScene()
    {
        SceneManager.LoadScene(sceneNameCastle);
        ResetFlags();
        isCastelScene = true;
        canPause = true;
    }
    public void Load_CinematicScene()
    {
        SceneManager.LoadScene(sceneNameCinematics);
        ResetFlags();
        isCinematicIntro = true;
    }
    public void Load_GameScene()
    {
        SceneManager.LoadScene(sceneNameGameScene);
        ResetFlags();
        isGameScene = true;
        canPause = true;
    }
   
    private void ResetFlags()//ALSO RESET CANPAUSE
    {
        canPause = false;
        isMainMenu=false;
        isCastelScene=false;
        isCinematicIntro=false;
        isGameScene=false;
    }

    private void Update()
    {
        if (canPause&& Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseCanvas.activeInHierarchy == true)
            {
                pauseCanvas.SetActive(false);
                Time.timeScale = 1;
            }
            else
            {
                pauseCanvas.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }
}
