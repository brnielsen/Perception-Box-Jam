using System.Collections.Generic;
using UnityEngine;

public class BulletHellCore : MonoBehaviour
{
    [System.Serializable]
    public class ProjectileSpawner
    {
        public GameObject projectilePrefab;
        public float projectileSpeed = 10f;
        public float projectileLifeTime = 10f;
        public float projectileDamage = 10f;

        public Transform spawnPoint;

        public void Fire()
        {
            GameObject projectileGameObject = Instantiate(projectilePrefab, spawnPoint.position, Quaternion.identity);
            ProjectileBase projectile = projectileGameObject.GetComponent<ProjectileBase>();
            if (projectile != null)
            {
                projectile.Initialize(projectileSpeed, projectileLifeTime, projectileDamage);
            }
        }
    }

    public List<ProjectileSpawner> projectileSpawners;
    public float fireRate = 1f;


}
