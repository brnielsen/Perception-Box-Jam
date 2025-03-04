using System;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AngerMonsterController : BulletHellCore
{
    public PlayerBulletHeck playerBulletHeck;

    public RectTransform rectTransform;

    public float growthRate = 0.5f;
    public float maxSize = 4f;
    public float minSize = 0.5f;
    public float growthStep = 0.1f;

    public Image FadeImageWorldSpace;
    public Image FadeImageScreenSpace;
    public Image FadeImageScreenSpaceWin;
    void Start()
    {
        FadeImageScreenSpaceWin.enabled = false;
        playerBulletHeck = FindFirstObjectByType<PlayerBulletHeck>();
        playerBulletHeck.OnPlayerHit += PlayerBulletHeck_OnPlayerHit;

        StartCoroutine(GameOpening());

    }

    private void PlayerBulletHeck_OnPlayerHit(object sender, EventArgs e)
    {
        StartCoroutine(LoseGame());
    }

    private IEnumerator GameOpening()
    {
        yield return FadeCoroutine(FadeImageScreenSpace, 2f, 0f);
        yield return StartCoroutine(FadeCoroutine(FadeImageWorldSpace, 2f, 0f));
        StartCoroutine(GrowMonster());
        BeginAttacking();
        Debug.Log("Game opening");
        //Begin game logic here
    }

    private IEnumerator LoseGame()
    {
        if (SceneManager.GetActiveScene().name.Contains("Lose"))
        {
            yield return FadeCoroutine(FadeImageWorldSpace, 2f, 1f);
            Debug.Log("Game over");
            SceneManager.LoadScene("Overworld");
        }
        else
        {
            yield return FadeCoroutine(FadeImageWorldSpace, 2f, 1f);
            Debug.Log("Reset");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

    }

    public IEnumerator WinGame()
    {
        FadeImageScreenSpaceWin.enabled = true;
        yield return FadeCoroutine(FadeImageScreenSpaceWin, 2f, 1f);
        Debug.Log("Win");
        SceneManager.LoadScene("Overworld");
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

    [ContextMenu("Init")]
    public void BeginAttacking()
    {
        StartCoroutine(Attack());
    }

    public IEnumerator Attack()
    {
        foreach (ProjectileSpawner spawner in projectileSpawners)
        {
            yield return new WaitForSeconds(fireRate);
            spawner.Fire();
        }
        yield return null;
    }

    public IEnumerator GrowMonster()
    {
        while (rectTransform.localScale.x < maxSize || rectTransform.localScale.x > minSize)
        {
            rectTransform.localScale *= growthStep;
            yield return new WaitForSeconds(growthRate);
            
            if (rectTransform.localScale.x <= minSize)
            {
                StartCoroutine(WinGame());
            }
        }
    }

}