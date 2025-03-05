using System;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using PixelCrushers.DialogueSystem;
using UnityEngine.Rendering;

public class AngerMonsterController : BulletHellCore
{

    [Header("Dialogue References")]
    [SerializeField][VariablePopup(true)] private string _finishedGame;

    [Header("Gameplay references")]
    public PlayerBulletHeck playerBulletHeck;
    public int maxHits = 3;

    public bool CanWin = false;

    public RectTransform rectTransform;

    [Header("Monster Stats")]

    public float growthRate = 0.5f;
    public float maxSize = 4f;
    public float minSize = 0.5f;
    public float growthStep = 0.1f;

    [Header("UI References")]

    public Image FadeImageWorldSpace;
    public Image FadeImageScreenSpace;
    public Image FadeImageScreenSpaceWin;
    [Header("Scene References")]
    [SerializeField] private string _overworldScene = "Overworld";
    [SerializeField] private string _breathingScene = "Breathing";

    private int hitCount = 0;
    void Start()
    {
        if (FadeImageScreenSpaceWin != null)
        {
            FadeImageScreenSpaceWin.enabled = false;
        }
        playerBulletHeck = FindFirstObjectByType<PlayerBulletHeck>();
        playerBulletHeck.OnPlayerHit += PlayerBulletHeck_OnPlayerHit;

        StartCoroutine(GameOpening());

    }

    private void PlayerBulletHeck_OnPlayerHit(object sender, EventArgs e)
    {
        if (CanWin == true)
        {
            TakeHit();
        }
        else
        {
            StartCoroutine(LoseGame());

        }
    }

    [ContextMenu("Take Hit")]
    private void TakeHit()
    {
        hitCount++;
        if (hitCount >= maxHits)
        {
            StartCoroutine(LoseGame());
        }
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
            DialogueLua.SetVariable(_finishedGame, true);

            Debug.Log("Game over");
            SceneManager.LoadScene(_breathingScene);
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

        DialogueLua.SetVariable(_finishedGame, true);

        FadeImageScreenSpaceWin.enabled = true;
        yield return FadeCoroutine(FadeImageScreenSpaceWin, 2f, 1f);
        Debug.Log("Win");
        SceneManager.LoadScene(_overworldScene);
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