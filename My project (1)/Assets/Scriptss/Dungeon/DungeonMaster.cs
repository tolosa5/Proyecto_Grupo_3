using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonMaster : MonoBehaviour
{
    public static DungeonMaster sharedDM;

    GameObject dianaGO;
    Diana dianaScr;

    [Range(0, 3)]
    public int timesPressed;

    [SerializeField] GameObject wallGO;
    [SerializeField] GameObject bridgeGO;

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
        dianaScr = dianaGO.GetComponent<Diana>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timesPressed == 2)
        {
            ActivatedPreassure();
        }

        if (dianaScr.arrowDetected == true)
        {
            bridgeGO.SetActive(true);
        }
    }

    void ActivatedPreassure()
    {
        
    }
}
