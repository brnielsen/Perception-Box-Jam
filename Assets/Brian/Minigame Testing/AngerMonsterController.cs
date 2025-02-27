using System.Collections;

using UnityEngine;

public class AngerMonsterController : BulletHellCore
{


    [ContextMenu("Init")]
    public void BeingAttacking()
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