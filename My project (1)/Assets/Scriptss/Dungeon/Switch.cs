using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    [SerializeField] GameObject referencedGO;

    private void OnCollisionEnter(Collision collision) 
    {
        if (collision.gameObject.CompareTag("as"))
        {
            Activate();
        }
    }

    public void Activate()
    {
        if (referencedGO.CompareTag("BridgeMechanims"))
        {

        }

        else if (referencedGO.CompareTag("DungeonMechanim"))
        {
            
        }
    }

}
