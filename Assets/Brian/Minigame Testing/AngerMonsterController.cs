using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngerMonsterController : MonoBehaviour
{
    [SerializeField] private List<Tendril> tendrils = new List<Tendril>();

    [SerializeField] private float offsetRate = 0.2f;

   
    [ContextMenu("Init")]
    public void BeingAttacking()
    {
        StartCoroutine(Attack());
    }

    public IEnumerator Attack()
    {
        foreach (Tendril tendril in tendrils)
        {
            yield return new WaitForSeconds(offsetRate);
            tendril.Activate();
        }
        yield return null;
    }
}