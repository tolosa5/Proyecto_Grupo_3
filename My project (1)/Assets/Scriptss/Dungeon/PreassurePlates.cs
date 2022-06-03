using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreassurePlates : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
