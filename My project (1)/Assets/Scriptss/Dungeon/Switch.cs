using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    [SerializeField] GameObject referencedGO;
    [SerializeField] Animator bridgeAnim;

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
            //activar puente y asi, animacion y tal
        }

        else if (referencedGO.CompareTag("DungeonMechanism"))
        {
            //activar lo que haga, dividir?
        }
    }

}
