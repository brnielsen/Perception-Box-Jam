using System;
using UnityEngine;

public class PlayerBulletHeck : MonoBehaviour
{
    public RectTransform RectTransform;

    public event EventHandler OnPlayerHit;

    void Awake()
    {
        RectTransform ??= GetComponent<RectTransform>();
    }

    public void PlayerHit()
    {
        Debug.Log("Player Hit");
        OnPlayerHit?.Invoke(this, EventArgs.Empty);
    }
}
