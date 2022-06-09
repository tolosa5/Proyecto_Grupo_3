using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    [SerializeField] GameObject arrow;
    float timer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            //Debug.Break();
            //Instantiate(arrow, transform.position + transform.forward, Quaternion.Euler(Player.player.prota.transform.eulerAngles));
        }
    }
}
