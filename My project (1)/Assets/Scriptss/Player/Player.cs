using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
using TMPro;

//IMPORTANTE, ANIMACIONES SE HACEN EN PROTA

public class Player : MonoBehaviour
{
    [TextArea]
    [SerializeField] string[] phrases;
    
    [SerializeField] GameObject box;
    [SerializeField] TextMeshProUGUI chat;
    int contPhrases = -1;
    [HideInInspector] public bool talking;

    public static Player player;
    GameObject door;
    public GameObject prota;
    public bool bowShooting;
    float lasthit;

    public int lifes = 3;
    int totalLifes = 3;
    int recargaVidas;

    [HideInInspector] public Vector3 lastCheckPoint;

    //CANVAS
    [SerializeField] GameObject lifesGO;
    [SerializeField] GameObject weapons;
    [SerializeField] Image[] lifesImage;

    [SerializeField] GameObject arrow;

    RaycastHit hit;

    [SerializeField] LayerMask isInteractable;
    [SerializeField] Transform attackSpot;
    
    Sword swordScr;
    [SerializeField] GameObject swordGO;
    [SerializeField] bool sword;
    
    Shield shieldScr;
    [SerializeField] GameObject shieldGO;
    bool shield;
    float timer;
    bool recoiling;

    [SerializeField] GameObject hookGO;
    bool hook;
    [SerializeField] GameObject bowGO;
    [SerializeField] bool bow;
    [SerializeField] GameObject spearGO;
    bool spear;
    
    Animator anim;
    public AnimatorStateInfo animState;
    [HideInInspector] public bool shieldUp;

    [HideInInspector] public bool atacando;
    [HideInInspector] public int key;
    [HideInInspector] public bool bossKey;

    [HideInInspector] public bool busy;
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
        swordScr = GetComponentInChildren<Sword>();
    }

    private void Start()
    {
        prota = GameObject.FindGameObjectWithTag("Prota");
    }

    void Update()
    {
        Inputs();
        lasthit += Time.deltaTime;

    }

    void Inputs()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Collider[] colls = Physics.OverlapSphere(transform.position + transform.forward, 1, isInteractable);
            if (colls.Length > 0)
            {

            }
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
                    shield = true;
                }
                else if (hit.collider.gameObject.CompareTag("PickBow")) //no va y ni idea de por que
                {
                    hit.collider.gameObject.SetActive(false);
                    bow = true;
                    Bow bowScr = GetComponentInChildren<Bow>();
                    Debug.Log("get cogido");
                }
                else if (hit.collider.gameObject.CompareTag("PickSpear"))
                {
                    hit.collider.gameObject.SetActive(false);
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
                    key++;
                }
                
                //COSAS DE LAS LLAVES
                else if (hit.collider.gameObject.CompareTag("MiniKey"))
                {
                    Debug.Log("llaveChikita");
                    key++;
                }
                else if (hit.collider.gameObject.CompareTag("BossKey"))
                {
                    Debug.Log("llaveGrande");
                    bossKey = true;
                }
                if (hit.collider.gameObject.CompareTag("Door"))
                {
                    Debug.Log("open");
                    if (key >= 1)
                    {
                        key--;
                        GameObject doorGO = hit.collider.gameObject;
                        doorGO.SetActive(false);
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
        if (Input.GetKeyDown(KeyCode.I) && sword && lasthit > 2)
        {
            lasthit = 0;
            anim.SetTrigger("TriggerSword");
            atacando = true;
            swordScr.Atacking();
        }

        //DEFENDERSE
        if (Input.GetKey(KeyCode.K))
        {

            anim.SetBool("BoolShield", true);
            shieldUp = true;
        }
        else if(Input.GetKeyUp(KeyCode.K))
        {
            anim.SetBool("BoolShield", false);
            shieldUp = false;
        }

        //ARCO
        if (Input.GetKeyDown(KeyCode.J) && bow)
        {
            swordGO.SetActive(false);
            shieldGO.SetActive(false);

            bowGO.SetActive(true);
            anim.SetTrigger("TriggerBow");
        }

        //LANZA
        if (Input.GetKeyDown(KeyCode.L) && spear)
        {
            swordGO.SetActive(false);
            shieldGO.SetActive(false);

            spearGO.SetActive(true);
            anim.SetTrigger("TriggerSpear");
        }
    }

    public void Talk()
    {
        GoBusy();

        box.SetActive(true);
        NextPhrase();
    }

    IEnumerator WritePhrase()
    {
        talking = true;
        chat.text = "";
        char[] chars = phrases[contPhrases].ToCharArray();

        for (int i = 0; i < chars.Length; i++)
        {
            chat.text += chars[i];
            yield return new WaitForSeconds(0.05f);
        }
        talking = false;
    }

    public void AutoComplete()
    {
        StopAllCoroutines();
        chat.text = "";
        chat.text = phrases[contPhrases];
        talking = false;
    }
    
    void NextPhrase()
    {
        contPhrases++;
        if (contPhrases == phrases.Length)
        {
            box.SetActive(false);
            contPhrases = -1;
            Player.player.GoFree();
        }
        else
        {
            StartCoroutine(WritePhrase());
        }
    }

    void BowShoot()
    {
        Instantiate(arrow, transform.position + transform.forward, Quaternion.Euler(transform.eulerAngles));
    }

    //anim event
    void BowOff()
    {
        swordGO.SetActive(true);
        shieldGO.SetActive(true);
        bowGO.SetActive(false);
    }

    //anim event
    void SpearOff()
    {
        swordGO.SetActive(true);
        shieldGO.SetActive(true);
        spearGO.SetActive(false);
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


        if (other.gameObject.CompareTag("Preassure"))
        {
            DungeonMaster.sharedDM.timesPressed++;
        }

        //---------------------
        else if (other.gameObject.CompareTag("Corazon"))
        {
            recargaVidas = totalLifes - lifes;
            lifes += recargaVidas;
        }
        //---------------------

        else if (other.gameObject.CompareTag("DeathZone"))
        {
            lifes--;
            transform.position = lastCheckPoint;
        }

        else if (other.gameObject.CompareTag("PickBow"))
        {
            other.gameObject.SetActive(false);
            bow = true;
            Bow bowScr = GetComponentInChildren<Bow>();
            Debug.Log("get cogido");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Preassure"))
        {
            DungeonMaster.sharedDM.timesPressed--;
        }
    }

    private void OnCollisionEnter(Collision other) 
    {
        if (other.gameObject.CompareTag("Door") && key > 0)
        {
            other.gameObject.SetActive(false);
            key--;
        }
    }

    private void OnCollisionStay(Collision collision) 
    {
        if (collision.gameObject.CompareTag("Rock"))
        {
            Debug.Log("venite");
            GameObject rockGO = collision.gameObject;
            Rock rockScr = rockGO.GetComponent<Rock>();
            rockScr.GetPushed();
            //SetActive(false) a las armas y demas
        }
    }

    private void OnCollisionExit(Collision collision) 
    {
        if (collision.gameObject.CompareTag("Rock"))
        {
            GameObject rockGO = collision.gameObject;
            Rock rockScr = rockGO.GetComponent<Rock>();
            rockScr.StopPushed();
            //SetActive(false) a las armas y demas
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
        //animacion
        transform.position = lastCheckPoint;
    }
}
