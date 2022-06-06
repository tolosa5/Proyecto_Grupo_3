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
        puertaGO = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && enemy == false)
        {
            puertaGO.SetActive(true);
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            enemy = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            puertaGO.SetActive(false);
            enemy = false;
        }
    }


}
