using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTrap : MonoBehaviour
{
    [SerializeField] GameObject[] traps;
    ShootingTrap trap1, trap2, trap3;
    // Start is called before the first frame update
    void Start()
    {
         trap1 = traps[0].GetComponent<ShootingTrap>();
         trap2 = traps[1].GetComponent<ShootingTrap>();
         trap3 = traps[2].GetComponent<ShootingTrap>();
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
            trap1.trapOn = true;
            trap2.trapOn = true;
            trap3.trapOn = true;
        }
    }
}
