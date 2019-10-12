using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private int damage;

    private void OnTriggerEnter(Collider other)
    {
        gameObject.SetActive(false);
    }

    public void SetDamage(int _damage)
    {
        damage = _damage;
    }

    public int GetDmage()
    {
        return damage;
    }
}
