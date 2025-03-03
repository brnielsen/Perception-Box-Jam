using Rive.Components;
using UnityEngine;

public class Tendril : ProjectileBase
{

    [SerializeField] private bool isActive = false;
    [SerializeField] private RectTransform _tendrilRectTransform;
    [SerializeField] private float maxDistance = 10f;

    /// <summary>
    /// Reduce rect transform by this amount when checking for overlap
    /// </summary>
    [SerializeField] private float _internalCushion = 10f;
    RiveWidget _riveWidget;

    public RectTransform _playerRectTransform;
    private float _initialHeight;



    public override void Update()
    {
        if (isActive)
        {
            Extend();
            CheckIfHitPlayer();
        }
    }

    public void Extend()
    {
        if (_tendrilRectTransform.rect.height > _initialHeight * maxDistance)
        {
            return;
        }
        Rect rect = _tendrilRectTransform.rect;
        _tendrilRectTransform.sizeDelta = new Vector2(_tendrilRectTransform.sizeDelta.x, _tendrilRectTransform.sizeDelta.y + speed * Time.deltaTime);

    }

    public override void Initialize(float speed, float lifeTime, float damage)
    {
        _initialHeight = _tendrilRectTransform.rect.height;
        _playerRectTransform = FindFirstObjectByType<PlayerBulletHeck>().RectTransform;
        base.Initialize(speed, lifeTime, damage);
        Activate();
    }

    public void Activate()
    {
        isActive = true;
    }

    public void CheckIfHitPlayer()
    {
        if (RectTransformOverlapChecker.IsOverLapping(_playerRectTransform, _tendrilRectTransform, _internalCushion))
        {
            Debug.Log("Hit Player");
            _playerRectTransform.GetComponent<PlayerBulletHeck>().PlayerHit();
        }
    }
}
