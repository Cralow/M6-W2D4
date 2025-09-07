using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraOnPaint : MonoBehaviour
{
    Painting painting;
    [SerializeField] private UiFade fade;
    [SerializeField] private GameObject playerObject;
    [SerializeField] private string LethalLavaLandSceneName;
    [SerializeField] private Camera paintCam;

    private void Awake()
    {
        painting = GetComponent<Painting>();
        fade = GetComponent<UiFade>();

        paintCam.enabled = false;
    }

    void Update()
    {
        if (painting.isInPlayerInPainting)
        {
            StartCoroutine(EnterPaintingSequence());
        }
    }

    public IEnumerator EnterPaintingSequence()
    {
        playerObject.SetActive(false);
        paintCam.enabled = true;
        fade.SetFade(1f, 1f);

        yield return new WaitForSeconds(3f);

        SceneManager.LoadScene(LethalLavaLandSceneName);
    }
}
