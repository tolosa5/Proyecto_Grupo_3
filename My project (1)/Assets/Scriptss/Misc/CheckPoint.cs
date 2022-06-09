using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] Vector3 myCheckPoint;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Player.player.lastCheckPoint = myCheckPoint;
            Debug.Log("pillado");
            Debug.Log(Player.player.lastCheckPoint);
        }
    }
}
