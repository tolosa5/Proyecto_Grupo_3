using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreassurePlates : MonoBehaviour
{

    void Update()
    {

    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Rock"))
        {
            DungeonMaster.sharedDM.timesPressed++;
            Debug.Log("llegue");
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Rock"))
        {
            DungeonMaster.sharedDM.timesPressed--;
        }
    }
}
