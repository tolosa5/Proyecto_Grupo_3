using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    Collider coll;
    
    int damage = 1;
    
    [SerializeField] LayerMask isAttackable;

    private void Start()
    {
        coll = GetComponent<Collider>();
        coll.enabled = false;
    }

    public void Atacking()
    {
        if (Player.player.atacando)
        {
            coll.enabled = true;
        }
        else
        {
            coll.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("ey!");
        if (Player.player.atacando == true && other.gameObject.layer == 9)
        {
            Debug.Log("atacando");
            if (other.gameObject.CompareTag("Enemy"))
            {

                GameObject enemyGO = other.gameObject;
                Enemy enemyScr = enemyGO.GetComponent<Enemy>();
                Debug.Log("atacado enemigo");

                float dotProduct = Vector3.Dot(enemyGO.transform.forward, Player.player.transform.forward);

                if (dotProduct < 0 && enemyScr.shield)
                {
                    enemyScr.Recoil(transform.forward);
                }

                else
                {
                    enemyScr.TakeDamage(damage);
                    if (enemyScr.lifes <= 0)
                    {
                        enemyScr.Death();
                    }
                }

            }

            else if (other.gameObject.CompareTag("Setos"))
            {
                Debug.Log("seto: ay");
                GameObject setosGO = other.gameObject;
                Setos setosScr = setosGO.GetComponent<Setos>();
                setosScr.GetHit();
            }
        }
    }
}
