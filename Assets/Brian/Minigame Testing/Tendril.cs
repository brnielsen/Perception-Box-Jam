using Rive.Components;
using UnityEngine;

public class Tendril : ProjectileBase
{

    [SerializeField] private bool isActive = false;
    [SerializeField] private RectTransform _tendrilRectTransform;
    [SerializeField] private float maxDistance = 10f;
    RiveWidget _riveWidget;

    private float _initialHeight;



    public override void Update()
    {
        if (isActive)
        {
            Extend();
        }
    }

    public void Extend()
    {
        if(_tendrilRectTransform.rect.height > _initialHeight * maxDistance){
            return;
        }
        Rect rect = _tendrilRectTransform.rect;
        _tendrilRectTransform.sizeDelta = new Vector2(_tendrilRectTransform.sizeDelta.x, _tendrilRectTransform.sizeDelta.y + speed* Time.deltaTime);

    }

    public override void Initialize(float speed, float lifeTime, float damage)
    {
        _initialHeight = _tendrilRectTransform.rect.height;
        base.Initialize(speed, lifeTime, damage);
        Activate();
    }

    public void Activate()
    {
        isActive = true;
    }
}
