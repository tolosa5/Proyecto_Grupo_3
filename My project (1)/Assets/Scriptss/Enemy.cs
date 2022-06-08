using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public int lifes = 3;
    public int damage = 1;

    float distanceToReturn = 5;
    float onWaitingTimer = 2f;
    Vector3 posInicial;
    Vector3 target;
    //mano para ver si contacta con el player al atacar
    [SerializeField] GameObject sword;

    NavMeshAgent agent;

    GameObject playerGO;
    Player playerScr;

    Animator anim;
    AnimatorStateInfo animInfo;

    public bool shield;

    bool atacando;

    //para el overlap, saber si pilla algo de player
    [SerializeField] LayerMask isPlayer;

    enum States { Idle, Chase, Return};
    States currentState;
    
    void Start()
    {
        currentState = States.Idle;
        posInicial = transform.position;
        agent = GetComponent<NavMeshAgent>();

        playerGO = GameObject.FindGameObjectWithTag("Player");

        anim = GetComponent<Animator>();
    }

    void Update()
    {
        //animInfo = anim.GetCurrentAnimatorStateInfo(0);
        if(currentState == States.Idle)
        {
            //Para que me empiece a perseguir tras el puente.
            if(Vector3.Distance(playerGO.transform.position, transform.position) < 4)
            {
                Debug.Log("Chase");
                target = playerGO.transform.position;
                agent.SetDestination(target);
                currentState = States.Chase;
            }

        }
        else if(currentState == States.Chase)
        {
            
            agent.SetDestination(target);

            if (agent.remainingDistance <= 1f )
            {
                Debug.Log("Attacking");
                target = playerGO.transform.position;
                agent.isStopped = true;
                anim.SetBool("AttackingBool", true);
                //metodo ataque en el momento de la animacion, animation event
            }
            else if(agent.remainingDistance < distanceToReturn) //5
            {
                Debug.Log("Chasing");
                target = playerGO.transform.position;
                agent.isStopped = false;
                onWaitingTimer = 2f;
                
            }
            else if (agent.remainingDistance > distanceToReturn) // > que 5.
            {
                Debug.Log("Waiting");
                target = posInicial;
                agent.isStopped = true;
                onWaitingTimer -= Time.deltaTime;
                if (onWaitingTimer < 0)
                {
                    agent.isStopped = false;
                    Debug.Log("Returning");

                }

            }
            else
            {
                //if (!animInfo.IsName("Enemy@Attack"))
                //{
                    //agent.isStopped = false;
                    //anim.SetBool("AttackingBool", false);
                //}
            }

        }
    }

    //se ejecuta desde evento de animaci�n, np
    void Attack()
    {
        anim.SetTrigger("TriggerAttack");
        atacando = true;
        sword.GetComponent<BoxCollider>().enabled = true;

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

    //anim event
    void StopAttack()
    {
        
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
        anim.SetTrigger("DeathTrigger");
        //desaparecer, destruirse
    }
}
