using System.Collections;

using UnityEngine;

public class AngerMonsterController : BulletHellCore
{
    void Start()
    {
        Debug.Log("Start");
        Invoke("BeginAttacking", 1f);
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

}