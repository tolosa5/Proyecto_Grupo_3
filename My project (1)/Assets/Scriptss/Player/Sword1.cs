using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword1 : MonoBehaviour
{
    public Collider coll;
    
    int damage = 1;
    
    [SerializeField] LayerMask isAttackable;

    GameObject enemyGO;

    private void Start()
    {
        coll = GetComponent<Collider>();
        coll.enabled = false;

        enemyGO = transform.parent.gameObject;
    }

    public void Atacking()
    {
        coll.enabled = true;
        Debug.Log("collider");
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("ey!");
        Debug.Log("atacando");
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("atacado enemigo");

            float dotProduct = Vector3.Dot(enemyGO.transform.forward, Player.player.transform.forward);

            if (dotProduct < 0 && Player.player.shieldUp)
            {
                return;
            }

            else
            {
                Debug.Log("hhhhhhhhhhhhh");
                Player.player.TakeDamage(damage);
            }
        }
    }
}
