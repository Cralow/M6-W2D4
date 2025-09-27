using System.Collections;
using UnityEngine;
using UnityEngine.Video;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private VideoPlayer fristVideoDuration;
    [SerializeField] private GameObject objectToActivate;
    [SerializeField] private VideoPlayer introVideoDuration;


    private Coroutine intro;
    [SerializeField] private KeyCode skipKeyCode= KeyCode.Escape;

    private void Start()
    {
        StartCoroutine(WaitForVideoEnd());
    }

    private IEnumerator WaitForVideoEnd()
    {
        // Aspetta che il primo video inizi
        while (!fristVideoDuration.isPlaying)
            yield return null;

        // Aspetta che il primo video termini
        while (fristVideoDuration.isPlaying)
            yield return null;

        // Disattiva il video e attiva l'oggetto
        fristVideoDuration.gameObject.SetActive(false);
        objectToActivate?.SetActive(true);
    }

    public void StartGame()
    {
        if (introVideoDuration == null) return;

        objectToActivate.SetActive(false);
        introVideoDuration.gameObject.SetActive(true);
        introVideoDuration.Play();

        intro = StartCoroutine(WaitForIntroVideoEnd());
    }

    private IEnumerator WaitForIntroVideoEnd()
    {
        // aspetta inizio
        while (!introVideoDuration.isPlaying)
            yield return null;

        // Aspetta fine
        while (introVideoDuration.isPlaying)
        {
            if (Input.GetKeyDown(skipKeyCode))
            {
                StopCoroutine(intro);
                GameManager.Instance.Load_CastelScene();
            }

            yield return null;

        }


        GameManager.Instance.Load_CastelScene();
    }

    public void LoadScene(string sceneName)
    {
        GameManager.Instance.Load_MainMenu();
    }
}