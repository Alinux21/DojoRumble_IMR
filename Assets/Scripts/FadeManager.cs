using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class FadeManager : MonoBehaviour
{
    private static FadeManager instance;
    public static FadeManager Instance => instance;

    private CanvasGroup fadeCanvasGroup;
    private float fadeDuration = 1f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            fadeCanvasGroup = GetComponentInChildren<CanvasGroup>();
            fadeCanvasGroup.alpha = 0f;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void FadeAndLoadScene(string sceneName)
    {
        StartCoroutine(FadeAndLoadRoutine(sceneName));
    }

    private IEnumerator FadeAndLoadRoutine(string sceneName)
    {
        // Fade to black
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            fadeCanvasGroup.alpha = elapsedTime / fadeDuration;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        fadeCanvasGroup.alpha = 1f;

        // Load the scene
        SceneManager.LoadScene(sceneName);

        // Fade back in
        elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            fadeCanvasGroup.alpha = 1f - (elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        fadeCanvasGroup.alpha = 0f;
    }
}