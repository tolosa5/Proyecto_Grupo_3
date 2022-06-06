using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    Rigidbody rb;

    bool isPushed;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void FixedUpdate() 
    {
        if (isPushed)
        {
            rb.AddForce(Player.player.transform.forward * 2);
            Debug.Log("pushed");
        }
    }

    public void GetPushed()
    {
        isPushed = true;
    }

    public void StopPushed()
    {
        isPushed = false;
    }
}
