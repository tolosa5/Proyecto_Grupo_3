using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{
    public static Cam cam;

    // Start is called before the first frame update
    void Start()
    {
        if (cam == null)
        {
            cam = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Player.player.transform.position + new Vector3(0, 24, -25.5f);
    }
}
