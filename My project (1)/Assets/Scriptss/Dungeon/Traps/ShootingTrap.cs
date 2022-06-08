using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingTrap : MonoBehaviour
{
    [SerializeField] GameObject fireball;
    [SerializeField] float timer;
    float timerThreshold;
    public bool trapOn = false;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitUntil(() => trapOn == true);
        Debug.Log("aaaaa");
        while (trapOn == true)
        {
            Debug.Log("eeeeee");
            timerThreshold += 0.5f * Time.deltaTime;
            if (timerThreshold >= timer)
            {
                Debug.Log("iiiiii");
                Instantiate(fireball, transform.position, transform.rotation);
                timerThreshold = 0;
            }
            Debug.Log("ooooo");
            yield return new WaitForEndOfFrame();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
