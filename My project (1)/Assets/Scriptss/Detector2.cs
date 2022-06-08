using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector2 : MonoBehaviour
{
    [SerializeField] bool interruptor;
    [SerializeField] GameObject numChest;
    bool chest;
    bool enemy;
    GameObject puertaGO;
    GameObject muroGO;
    GameObject dungeonMaster;
    DungeonMaster dMScrt;
    // Start is called before the first frame update
    void Start()
    {
        //Pilla la puerta
        puertaGO = transform.GetChild(0).gameObject;

        muroGO = GameObject.FindGameObjectWithTag("Wall");

        dungeonMaster = GameObject.FindGameObjectWithTag("DungeonMaster");
        dMScrt = dungeonMaster.GetComponent<DungeonMaster>();
    }
    private void Update()
    {

      
    }
    private void OnTriggerStay(Collider other)
    {
        //Si detecta al player y al enemigo (independientemente del número de enemigos),
        //activa la puerta
        if (other.gameObject.CompareTag("Enemy"))
        {
            enemy = true;
        }
        if (other.gameObject.CompareTag("Player") && enemy == true)
        {
            puertaGO.SetActive(true);
            Debug.Log("David Cage");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        //Si no detecta al enemigo, se desactiva la puerta.
        //IMPORTANTE HACER QUE EL ENEMIGO SALGA DEL TRIGGER ANTES DE DESTRUIRLO
        if (other.gameObject.CompareTag("Enemy"))
        {
            enemy = false;
        }
        if (other.gameObject.CompareTag("Enemy") && interruptor == true)
        {
            muroGO.SetActive(false);
            numChest.SetActive(true);
        }
        if (other.gameObject.CompareTag("Enemy") && interruptor == false)
        {
            Debug.Log("David fugitive");
            enemy = false;
            puertaGO.SetActive(false);
        }
    }
}
