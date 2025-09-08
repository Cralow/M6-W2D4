using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;


public class GameOverController : MonoBehaviour
{
    [SerializeField] private string sceneNameMainMenu;

    [SerializeField] private float levelTimer = 30f;
    [SerializeField] private Text uiTimer;

    [SerializeField] private GameObject playerCam;       
    [SerializeField] private GameObject playeroObj;
    [SerializeField] private GameObject playerCanvasDeath;
    [SerializeField] private GameObject playerCanvasWin;   
    [SerializeField] private GameObject playerCanvasLose;
    [SerializeField] private LifeController lifeController;
    AudioSource sound;

    //endlevel
    [SerializeField] private Transform endLevelCheckPoint;
    [SerializeField] private float checkRadius = 1f;
    [SerializeField] private LayerMask playerLayer;
    private bool gameEnded = false;


    void Start()
    {
        sound = GetComponent<AudioSource>();
        StartCoroutine(LevelTimerRoutine());
    }
    private void Update()
    {
        if (!gameEnded && Physics.CheckSphere(endLevelCheckPoint.position, checkRadius, playerLayer))
        {
            gameEnded = true;
            HandleVictory();
        }


    }
    private IEnumerator LevelTimerRoutine()
    {
        while (levelTimer > 0f)
        {
            levelTimer -= Time.deltaTime;
            uiTimer.text = levelTimer.ToString("F2");
            yield return null;
        }

        // Clamp
        levelTimer = 0f;
        uiTimer.text = "0";
        HandleDefeat();
    }
    public void HandleDeath()
    {
        sound.enabled = false;
        playeroObj.SetActive(false);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        playerCanvasDeath.SetActive(true);
    }
    public void HandleVictory()
    {
        sound.enabled = false;
        playeroObj.SetActive(false);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        playerCanvasWin.SetActive(true);
    }

    public void HandleDefeat()
    {
        sound.enabled = false;
        playeroObj.SetActive(false);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        playerCanvasLose.SetActive(true);
    }
   //pulsante per il meniu
    public void LoadMainMenu()
    {
        SceneManager.LoadScene(sceneNameMainMenu);
    }
    //pulsante per restart 
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}