using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreassurePlates : MonoBehaviour
{

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
