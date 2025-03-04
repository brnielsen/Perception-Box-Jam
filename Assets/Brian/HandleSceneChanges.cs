using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HandleSceneChanges : MonoBehaviour
{
    public Image FadeImageScreenSpace;

    void Start()
    {
        StartCoroutine(FadeCoroutine(FadeImageScreenSpace, 2f, 0f));
    }
    public void SceneChange(string sceneName)
    {
        StartCoroutine(SceneChangeWithFade(sceneName));
    }

    public IEnumerator SceneChangeWithFade(string sceneName)
    {
        yield return FadeCoroutine(FadeImageScreenSpace, 2f, 1f);
        SceneManager.LoadScene(sceneName);
    }

    public IEnumerator FadeCoroutine(Image image, float fadeDuration, float targetAlpha)
    {
        Color color = image.color;
        float startAlpha = color.a;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / fadeDuration);
            image.color = new Color(color.r, color.g, color.b, newAlpha);
            yield return null;
        }

        image.color = new Color(color.r, color.g, color.b, targetAlpha);
    }
}
