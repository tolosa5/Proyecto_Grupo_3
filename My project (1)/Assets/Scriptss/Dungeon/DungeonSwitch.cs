using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonSwitch : MonoBehaviour
{
    [SerializeField] GameObject detector;
    GameObject puertaGO;
    // Start is called before the first frame update
    void Start()
    {
        puertaGO = detector.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("aaaaa");
        if (collision.gameObject.CompareTag("Spear"))
        {
            Debug.Log("caca");
            puertaGO.SetActive(false);
        }
    }
}
