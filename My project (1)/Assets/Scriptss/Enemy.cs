using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public int lifes = 3;
    public int damage = 1;

    //mano para ver si contacta con el player al atacar
    [SerializeField] Transform handAttack;

    NavMeshAgent agent;

    GameObject playerGO;
    Player playerScr;

    Animator anim;
    AnimatorStateInfo animInfo;

    public bool shield;

    //para el overlap, saber si pilla algo de player
    [SerializeField] LayerMask isPlayer;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        playerGO = GameObject.FindGameObjectWithTag("Player");

        anim = GetComponent<Animator>();
    }

    void Update()
    {
        agent.SetDestination(playerGO.transform.position);

        //si esta a menos de 2f el stoppingDistance del navMesh, pararse DE GOLPE
        if (agent.remainingDistance <= 1f)
        {
            agent.isStopped = true;
            anim.SetBool("AttackingBool", true);
            //metodo ataque en el momento de la animacion, animation event
        }
        else
        {
            if (!animInfo.IsName("Enemy@Attack"))
            {
                agent.isStopped = false;
                anim.SetBool("AttackingBool", false);
            }
        }
    }

    //se ejecuta desde evento de animaci�n, np
    void Attack()
    {
        //overlap ahi
        Collider[] colls = Physics.OverlapSphere(handAttack.position, 0.3f, isPlayer);
        if (colls.Length >= 1)
        {
            //si detecta player, bajar vidas
            playerScr = playerGO.GetComponent<Player>();
            float dotProduct = Vector3.Dot(playerGO.transform.forward, transform.forward);
            
            if(dotProduct < 0 && playerScr.shieldUp)
            {
                playerScr.Recoil(transform.forward);
            }
            
            else
            {
                playerScr.TakeDamage(damage);

            }
        }
    }

    public void TakeDamage(int enemyDamage)
    {
        lifes -= enemyDamage;
        Debug.Log(lifes);
        if (lifes <= 0)
        {
            Debug.Log("noooo chimuelooooo");
            Death();
        }
    }

    public void Recoil(Vector3 recoilDirection)
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position - recoilDirection, 3);
    }

    public void Death()
    {
        
    }
}
