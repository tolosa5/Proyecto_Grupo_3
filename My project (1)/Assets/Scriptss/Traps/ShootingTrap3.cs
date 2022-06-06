using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingTrap3 : MonoBehaviour
{
    [SerializeField] GameObject fireball;
    float timer;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timer += 0.5f * Time.deltaTime;
        if (timer >= 1.5f)
        {
            Instantiate(fireball, transform.position, transform.rotation);
            timer = 0;
        }
    }
}
