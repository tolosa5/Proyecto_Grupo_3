using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingTrap : MonoBehaviour
{
    [SerializeField] GameObject fireball;
    float timer;
    public bool trapOn1 = false;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitUntil(() => trapOn1 == true);
        Debug.Log("aaaaa");
        while (trapOn1 == true)
        {
            Debug.Log("eeeeee");
            timer += 0.5f * Time.deltaTime;
            if (timer >= 1f)
            {
                Debug.Log("iiiiii");
                Instantiate(fireball, transform.position, transform.rotation);
                timer = 0;
            }
            Debug.Log("ooooo");
            yield return new WaitForSeconds(0.1f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
