using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonMaster : MonoBehaviour
{
    public static DungeonMaster sharedDM;

    [HideInInspector]
    public int timesPressed;

    [SerializeField] GameObject[] preassurePlates;

    [SerializeField] GameObject[] chests; //ACTIVARLOS SEGUN LA ZONA QUE SE SUPERE, LINEA 67 DE REFERENCIA

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
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(timesPressed);
        if (timesPressed == 2)
        {
            ActivatedPreassure();

            
        }
        else if (timesPressed == 3)
        {
            ActivatedPreassure2();
            
        }
    }

    void ActivatedPreassure()
    {

        timesPressed = 0;
        for (int i = 0; i < 1; i++)
        {
            preassurePlates[i].GetComponent<BoxCollider>().enabled = false;

        }
        chests[0].SetActive(true);
    }

    void ActivatedPreassure2()
    {

        timesPressed = 0;
        for (int i = 2; i < 5; i++)
        {
            preassurePlates[i].GetComponent<BoxCollider>().enabled = false;
        }
        chests[2].SetActive(true);
    }
}
