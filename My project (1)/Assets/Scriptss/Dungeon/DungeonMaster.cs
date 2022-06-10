using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonMaster : MonoBehaviour
{
    public static DungeonMaster sharedDM;

    bool done;

    GameObject dianaGO;
    DianaDungeon dianaScr;

    GameObject detectorGO;
    //Detector detectorScr;

    public int timesPressed;

    [SerializeField] GameObject wallGO;
    [SerializeField] GameObject bridgeGO;
    [SerializeField] BoxCollider[] preassureColls;
    [SerializeField] GameObject[] chests;

    [SerializeField] GameObject door, miniBridge;

    // Start is called before the first frame update

    private void Awake()
    {
        if (sharedDM == null)
        {
            sharedDM = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        dianaGO = GameObject.FindGameObjectWithTag("Diana");
        dianaScr = dianaGO.GetComponent<DianaDungeon>();

        detectorGO = GameObject.FindGameObjectWithTag("Detector");
        //detectorScr = detectorGO.GetComponent<Detector>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(timesPressed);
        
        if (timesPressed == 2)
        {
            if (!done)
            {
                ActivatedPreassure();

            }
        }

        else if (timesPressed == 3)
        {
            ActivatedPreassure2();
        }

        if (dianaScr.arrowDetected == true)
        {
            Debug.Log("KKDVAK");
            bridgeGO.SetActive(true);
        }
    }

    void ActivatedPreassure()
    {
        done = true;
        for (int i = 0; i < 1; i++)
        {
            preassureColls[i].enabled = false;
        }
        chests[0].SetActive(true);
        timesPressed = 0;
    }

    void ActivatedPreassure2()
    {
        for (int i = 2; i < 4; i++)
        {
            preassureColls[i].enabled = false;
        }
        miniBridge.SetActive(true);
        door.SetActive(false);
        chests[4].SetActive(true);
    }

    public void KeyChest()
    {
        //chests[detectorScr.numChest].SetActive(true);
    }
}
