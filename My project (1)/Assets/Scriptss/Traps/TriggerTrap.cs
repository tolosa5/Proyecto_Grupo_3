using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTrap : MonoBehaviour
{
    [SerializeField] GameObject[] traps;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("trampitas");
            ShootingTrap trap1 = traps[0].GetComponent<ShootingTrap>();
            trap1.trapOn1 = true;
            ShootingTrap2 trap2 = traps[1].GetComponent<ShootingTrap2>();
            trap2.trapOn2 = true;
            ShootingTrap3 trap3 = traps[2].GetComponent<ShootingTrap3>();
            trap3.trapOn3 = true;
        }
    }
}
