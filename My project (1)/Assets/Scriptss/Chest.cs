using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenAnim()
    {
        gameObject.isStatic = false;
        anim.SetTrigger("TriggerOpen");
        Debug.Log("abriendo");
    }

    //Se le llama en la animacion
    public IEnumerator Drop()
    {
        yield return new WaitForSeconds(1f);
        Debug.Log("Get Dropped");
        yield return new WaitForSeconds(2f);
        CloseAnim();
    }
    
    void CloseAnim()
    {
        anim.SetTrigger("TriggerClose");
    }
}
