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

    Sword1 enemySword;

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
        
        enemySword = GetComponentInChildren<Sword1>();
    }

    void Update()
    {
        
        animInfo = anim.GetCurrentAnimatorStateInfo(0);

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
            anim.SetBool("BoolWalking", true);
            agent.SetDestination(target);

            if (agent.remainingDistance <= 2f )
            {
                Debug.Log("Attacking");
                target = playerGO.transform.position;
                agent.isStopped = true;
                anim.SetBool("BoolWalking", false);
                anim.SetBool("AttackingBool", true);
                //metodo ataque en el momento de la animacion, animation event
            }
            else if (agent.remainingDistance < distanceToReturn && agent.remainingDistance > 2) //5
            {
                anim.SetBool("AttackingBool", false);
                Debug.Log("Chasing");
                target = playerGO.transform.position;
                agent.isStopped = false;
                anim.SetBool("BoolWalking", false);
                onWaitingTimer = 2f;
            }
            
            if (agent.remainingDistance > distanceToReturn && agent.remainingDistance > 2) // > que 5.
            {
                anim.SetBool("AttackingBool", false);
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
            
            if (animInfo.IsName("ataque"))
            {
                agent.isStopped = true;
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
            
        enemySword.Atacking();
        
    }

    //anim event
    void StopAttack()
    {
        enemySword.coll.enabled = false;
    }

    public void TakeDamage(int enemyDamage)
    {
        lifes -= enemyDamage;
        Debug.Log(lifes);
        if (lifes <= 0)
        {
            Debug.Log("noooo chimuelooooo");
            StartCoroutine(Death());
        }
    }

    public void Recoil(Vector3 recoilDirection)
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position - recoilDirection, 3);
    }

    public IEnumerator Death()
    {
        anim.SetTrigger("DeathTrigger");
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
        

        //desaparecer, destruirse
    }
}
