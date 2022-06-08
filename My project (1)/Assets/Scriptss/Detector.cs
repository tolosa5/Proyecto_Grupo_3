using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour
{
    bool enemy;
    GameObject puertaGO;
    // Start is called before the first frame update
    void Start()
    {
        //Pilla la puerta
        puertaGO = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        //Si detecta al player y al enemigo (independientemente del número de enemigos),
        //activa la puerta
        if (other.gameObject.CompareTag("Player") && enemy == true)
        {
            puertaGO.SetActive(true);
            Debug.Log("David Cage");
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            enemy = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        //Si no detecta al enemigo, se desactiva la puerta.
        //IMPORTANTE HACER QUE EL ENEMIGO SALGA DEL TRIGGER ANTES DE DESTRUIRLO
        if (other.gameObject.CompareTag("Enemy"))
        {
            puertaGO.SetActive(false);
            Debug.Log("David fugitive");
            enemy = false;
        }
    }
}
