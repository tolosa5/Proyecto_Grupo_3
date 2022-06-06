using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreassurePlates : MonoBehaviour
{


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Rock"))
        {
            DungeonMaster.sharedDM.timesPressed++;
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            DungeonMaster.sharedDM.timesPressed++;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Rock"))
        {
            DungeonMaster.sharedDM.timesPressed--;
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            DungeonMaster.sharedDM.timesPressed--;
        }
    }
}
