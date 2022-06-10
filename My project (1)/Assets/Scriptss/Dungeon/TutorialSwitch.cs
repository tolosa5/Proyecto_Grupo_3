using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSwitch : MonoBehaviour
{
    [SerializeField] Animator animBridge;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Spear"))
        {
            Debug.Log("caca");
            animBridge.SetTrigger("TriggerOn");
        }
    }
}
