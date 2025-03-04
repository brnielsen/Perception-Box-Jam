using System;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class AngerMonsterController : BulletHellCore
{
    public PlayerBulletHeck playerBulletHeck;

    public RectTransform rectTransform;

    public float growthRate = 0.5f;
    public float maxSize = 4f;
    public float growthStep = 0.1f;

    public Image FadeImage;
    void Start()
    {
        Debug.Log("Start");
        playerBulletHeck = FindFirstObjectByType<PlayerBulletHeck>();
        playerBulletHeck.OnPlayerHit += PlayerBulletHeck_OnPlayerHit;

        StartCoroutine(FadeCoroutine(FadeImage, 2f, 0f));
        StartCoroutine(GrowMonster());

        Invoke("BeginAttacking", 1f);
    }

    private void PlayerBulletHeck_OnPlayerHit(object sender, EventArgs e)
    {
        StartCoroutine(EndGame());
    }

    private IEnumerator EndGame()
    {
        yield return FadeCoroutine(FadeImage, 2f, 1f);
        Debug.Log("Game over");
        //Return to previous scene logic here
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
        while (rectTransform.localScale.x < 4)
        {
            rectTransform.localScale *= growthStep;
            yield return new WaitForSeconds(growthRate);
        }
    }

}