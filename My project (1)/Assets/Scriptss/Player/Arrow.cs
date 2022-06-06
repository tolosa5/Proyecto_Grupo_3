using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    float timer;
    float vel = 15;
    GameObject enemyGO;
    Enemy enemyScript;
    GameObject playerGO;
    Player playerScript;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += 1 * Time.deltaTime;
        if (timer>=5)
        {
            Destroy(gameObject);
        }
        transform.Translate(new Vector3(0,0,1) * vel * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            enemyGO = other.gameObject;
            enemyScript = enemyGO.GetComponent<Enemy>();
            enemyScript.lifes--;
            Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag("Player"))
        {
            playerGO = other.gameObject;
            playerScript = playerGO.GetComponent<Player>();
            if (playerScript.shieldUp)
            {
                float dotProduct = Vector3.Dot(playerGO.transform.forward, transform.forward);

                if (dotProduct < 0 && playerScript.shieldUp)
                {
                    playerScript.Recoil(transform.forward);
                }
                else
                {
                    playerScript.TakeDamage(1);                   
                }
            }
            Destroy(gameObject);
        }
        Destroy(gameObject);
    }
}
