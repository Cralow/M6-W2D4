using System.Collections;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private VideoPlayer fristVideoDuration;
    [SerializeField] private GameObject objectToActivate;
    [SerializeField] private VideoPlayer introVideoDuration;

    [SerializeField] private string sceneNameMenu;
    [SerializeField] private string sceneNameCastle;
    [SerializeField] private string sceneNameLevel;

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

        StartCoroutine(WaitForIntroVideoEnd());
    }

    private IEnumerator WaitForIntroVideoEnd()
    {
        // aspetta inizio
        while (!introVideoDuration.isPlaying)
            yield return null;

        // Aspetta fine
        while (introVideoDuration.isPlaying)
            yield return null;


        LoadScene(sceneNameCastle);
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}