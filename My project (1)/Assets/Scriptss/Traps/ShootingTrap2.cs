using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingTrap2 : MonoBehaviour
{
    [SerializeField] GameObject fireball;
    float timer;
    public bool trapOn2 = false;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitUntil(() => trapOn2 == true);
        
        while (trapOn2 == true)
        {
            timer += 0.5f * Time.deltaTime;
            if (timer >= 1.25f)
            {
                Instantiate(fireball, transform.position, transform.rotation);
                timer = 0;
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
