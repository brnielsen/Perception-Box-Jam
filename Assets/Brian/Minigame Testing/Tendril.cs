using UnityEngine;

public class Tendril : ProjectileBase
{
    
    [SerializeField] private bool isActive = false;

    public override void Update()
    {
        if (isActive)
        {
            Extend();
        }
    }

    public void Extend()
    {
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z + speed * Time.deltaTime);

    }

    public override void Initialize(float speed, float lifeTime, float damage)
    {
        base.Initialize(speed, lifeTime, damage);
        Activate();
    }

    public void Activate()
    {
        isActive = true;
    }
}
