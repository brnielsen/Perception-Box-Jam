using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    [SerializeField] protected float speed = 10f;
    [SerializeField] protected float lifeTime = 10f;

    [SerializeField] protected float damage = 10f;

    public virtual void Initialize(float speed, float lifeTime, float damage)
    {
        this.speed = speed;
        this.lifeTime = lifeTime;
        this.damage = damage;
    }

    public virtual void Update()
    {
        Move();
    }

    public virtual void Move()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other)
        {
            Debug.Log("Hit " + other.gameObject.name);
        }
    }

}
