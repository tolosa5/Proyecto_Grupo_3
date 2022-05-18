using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player player;


    [SerializeField] LayerMask isInteractable;
    [SerializeField] Transform attackSpot;
    
    Sword swordScr;
    [SerializeField] GameObject swordGO;
    bool sword;
    
    Shield shieldScr;
    [SerializeField] GameObject shieldGO;
    bool shield;

    [SerializeField] GameObject hookGO;
    bool hook;
    [SerializeField] GameObject bowGO;
    bool bow;
    [SerializeField] GameObject spearGO;
    bool spear;
    
    Animator anim;
    public AnimatorStateInfo animState;
    [HideInInspector] public bool shieldUp;

    public bool atacando;
    bool key;
    bool bossKey;

    
    void Awake()
    {
        //singleton
        if (player == null)
        {
            player = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        anim = GetComponent<Animator>();

        swordScr = GetComponentInChildren<Sword>();
        shieldScr = GetComponentInChildren<Shield>();
    }

    
    void Update()
    {
        Inputs();
    }

    void Inputs()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, 5, isInteractable))
            {
                //COGER ARMAS
                if (hit.collider.gameObject.CompareTag("PickSword"))
                {
                    swordGO.SetActive(true);
                    sword = true;
                }
                else if (hit.collider.gameObject.CompareTag("PickShield"))
                {
                    shieldGO.SetActive(true);
                    shield = true;
                }
                else if (hit.collider.gameObject.CompareTag("PickHook"))
                {
                    hookGO.SetActive(true);
                    hook = true;
                }
                else if (hit.collider.gameObject.CompareTag("PickBow"))
                {
                    bowGO.SetActive(true);
                    bow = true;
                }
                else if (hit.collider.gameObject.CompareTag("PickSpear"))
                {
                    spearGO.SetActive(true);
                    spear = true;
                }

                //COFRE
                else if (hit.collider.gameObject.CompareTag("Chest"))
                {
                    Debug.Log("Abre");
                    GameObject chestGO = hit.collider.gameObject;
                    Chest chestScr = chestGO.GetComponent<Chest>();
                    chestScr.OpenAnim();
                }
                
                //COSAS DE LAS LLAVES
                else if (hit.collider.gameObject.CompareTag("MiniKey"))
                {
                    Debug.Log("llaveChikita");
                    key = true;
                }
                else if (hit.collider.gameObject.CompareTag("BossKey"))
                {
                    Debug.Log("llaveGrande");
                    bossKey = true;
                }
                else if (hit.collider.gameObject.CompareTag("Door"))
                {
                    if (key)
                    {
                        key = false;
                        GameObject doorGO = hit.collider.gameObject;
                        //pillar script, abrirla, animacion
                    }
                }
                else if (hit.collider.gameObject.CompareTag("BossDoor"))
                {
                    if (bossKey)
                    {
                        bossKey = false;
                        GameObject bossDoorGO = hit.collider.gameObject;
                        //pillar script, abrirla, animacion
                    }
                }


            }
        }

        //ATACAR
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            anim.SetTrigger("TriggerAttack");
            swordScr.Atacking();
            atacando = true;
        }

        //DEFENDERSE
        if (Input.GetKey(KeyCode.DownArrow))
        {
            anim.SetBool("BoolDefense", true);
            shieldUp = true;
        }
        else if(Input.GetKeyUp(KeyCode.DownArrow))
        {
            anim.SetBool("BoolDefense", false);
            shieldUp = false;
        }

        //ARCO
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            anim.SetBool("BoolBowCharge", true);
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            anim.SetTrigger("TriggerBowShot");
        }
        
    }

    //animation event para que al acabar la animacion de ataque se ponga a false
    public void AtackingFalse()
    {
        atacando = false;
    }

    public void Recoil(Vector3 recoilDirection)
    {

    }

    public void TakeDamage(int damageTaken)
    {

    }
}
