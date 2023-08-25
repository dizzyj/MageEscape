using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    bool hitDetected;
    List<GameObject> hitList = new List<GameObject>();
    public int damage;
    public bool getHit()
    {
        return hitDetected;
    }
    public void DetectHit()
    {
        RaycastHit[] hits;
        hits = Physics.SphereCastAll(transform.position, .5f, transform.forward, 0.0001f);
        foreach (var hit in hits)
        {
            if (hit.transform.CompareTag("Enemy"))
            {
                hitDetected = true;
                print("Hit detected on: "+ hit.transform.gameObject.name);
                hitList.Add(hit.transform.gameObject);
            }
        }
    }
    public void Damage()
    {
        foreach (var enemy in hitList)
        {
            enemy.GetComponent<Enemy>().TakeDamage(damage);
            
        }
        hitList.Clear();
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, .5f);
    }
}
