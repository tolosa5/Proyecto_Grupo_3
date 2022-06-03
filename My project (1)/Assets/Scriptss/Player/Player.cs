using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    public static Player player;

    int lifes = 3;

    //CANVAS
    [SerializeField] GameObject lifesGO;
    [SerializeField] GameObject weapons;
    [SerializeField] Image[] lifesImage;

    RaycastHit hit;

    [SerializeField] LayerMask isInteractable;
    [SerializeField] Transform attackSpot;
    
    Sword swordScr;
    [SerializeField] GameObject swordGO;
    bool sword;
    
    Shield shieldScr;
    [SerializeField] GameObject shieldGO;
    bool shield;
    float timer;
    bool recoiling;

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

    public bool busy;
    [SerializeField] PlayableDirector bridgeCinematic;
    [SerializeField] GameObject cinematicTrigger;
    
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

        shieldScr = GetComponentInChildren<Shield>();
    }

    private void Start()
    {
        
    }

    void Update()
    {
        Inputs();
    }

    void Inputs()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            
            if (Physics.Raycast(transform.position, transform.forward, out hit, 5, isInteractable))
            {
                //COGER ARMAS
                if (hit.collider.gameObject.CompareTag("PickSword"))
                {
                    hit.collider.gameObject.SetActive(false);
                    swordGO.SetActive(true);
                    sword = true;
                    swordScr = GetComponentInChildren<Sword>();
                }
                else if (hit.collider.gameObject.CompareTag("PickShield"))
                {
                    hit.collider.gameObject.SetActive(false);
                    shieldGO.SetActive(true);
                    shield = true;
                }
                else if (hit.collider.gameObject.CompareTag("PickHook"))
                {
                    hit.collider.gameObject.SetActive(false);
                    hookGO.SetActive(true);
                    hook = true;
                }
                else if (hit.collider.gameObject.CompareTag("PickBow"))
                {
                    hit.collider.gameObject.SetActive(false);
                    bowGO.SetActive(true);
                    bow = true;
                }
                else if (hit.collider.gameObject.CompareTag("PickSpear"))
                {
                    hit.collider.gameObject.SetActive(false);
                    spearGO.SetActive(true);
                    spear = true;
                }

                //NPC
                else if (hit.collider.gameObject.CompareTag("NPC"))
                {
                    Talking();
                }
                else if (hit.collider.gameObject.CompareTag("Smith"))
                {
                    Talking();
                    //poner un cofre como interactuable para abrirlo y pillar la llave
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

                //INTERACCIONES DUNGEON
                else if (hit.collider.gameObject.CompareTag("Switch"))
                {
                    GameObject switchGO = hit.collider.gameObject;
                    Switch switchScr = switchGO.GetComponent<Switch>();
                    switchScr.Activate();
                }
            }
        }

        //ATACAR
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            anim.SetTrigger("TriggerAttack");
            atacando = true;
            swordScr.Atacking();
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

    void Talking()
    {
        NPC npcScr = hit.collider.gameObject.GetComponent<NPC>();
        if (!npcScr.talking)
        {
            npcScr.Talk();
        }
        else
        {
            npcScr.AutoComplete();
        }
    }

    //animation event para que al acabar la animacion de ataque se ponga a false
    public void AtackingFalse()
    {
        atacando = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("BridgeCinematic"))
        {
            GoBusy();
            bridgeCinematic.Play();
            cinematicTrigger.SetActive(false);
        }
    }

    public void GoBusy()
    {
        busy = true;
    }

    public void GoFree()
    {
        busy = false;
    }

    public void Recoil(Vector3 recoilDirection)
    {
        while (recoiling)
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position - recoilDirection, 3);
            timer += Time.deltaTime;
            if (timer >= 1)
            {
                recoiling = false;

            }
        }
    }

    public void TakeDamage(int damageTaken)
    {
        lifes -= damageTaken;
        if (lifes <= 0)
        {
            Death();
        }
    }

    void Death()
    {
        //chimuelo
    }
}
