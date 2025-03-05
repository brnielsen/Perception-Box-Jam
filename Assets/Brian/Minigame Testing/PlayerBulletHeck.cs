using System;
using System.Collections;
using UnityEngine;

public class PlayerBulletHeck : MonoBehaviour
{
    public RectTransform RectTransform;

    public event EventHandler OnPlayerHit;

    public float IFrames = 1f;

    private bool isInvincible = false;

    public bool YogaMode;

    void Awake()
    {
        RectTransform ??= GetComponent<RectTransform>();
    }

    public void PlayerHit()
    {
        if (isInvincible == true)
        {
            return;
        }
        Debug.Log("Player Hit");
        StartCoroutine(TakeHitWithIFrames());
        OnPlayerHit?.Invoke(this, EventArgs.Empty);
    }

    private IEnumerator TakeHitWithIFrames()
    {
        isInvincible = true;
        yield return new WaitForSeconds(IFrames);
        isInvincible = false;
    }
}
