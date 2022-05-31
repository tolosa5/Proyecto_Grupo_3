using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setos : MonoBehaviour
{
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void GetHit()
    {
        Debug.Log("rompiendo");
        anim.SetTrigger("TriggerBreak");
        Destroy(gameObject, 2.5f);
    }
}
