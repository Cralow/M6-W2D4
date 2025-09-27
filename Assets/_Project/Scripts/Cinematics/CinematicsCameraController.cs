using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CinematicsCameraController : MonoBehaviour
{
    [SerializeField] private float animationTime;
    [SerializeField] private GameObject cameraObj1;
    [SerializeField] private GameObject cameraObj2;

    [SerializeField] private string lethalLavaLandSceneName;
    private Coroutine coroutine1;

    private void Awake()
    {
        coroutine1= StartCoroutine(AnimationRoutine());
    }
    public IEnumerator AnimationRoutine()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(AnimationRoutine2());
        }
        yield return new WaitForSeconds(animationTime);

    }


    public IEnumerator AnimationRoutine2()
    {
        StopCoroutine(coroutine1);
        cameraObj1.SetActive(false);
        cameraObj2.SetActive(true);
        yield return new WaitForSeconds(animationTime);
    }
    public void LoadPlayScene()
    {
        GameManager.Instance.Load_GameScene();
    }
    //riferimento da Ui
    public void UI_StartAnimation2()
    {
        StartCoroutine(AnimationRoutine2());
    }
}
