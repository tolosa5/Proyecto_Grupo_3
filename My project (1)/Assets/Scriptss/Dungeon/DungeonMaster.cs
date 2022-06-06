using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonMaster : MonoBehaviour
{
    public static DungeonMaster sharedDM;

    [Range(0, 3)]
    public int timesPressed;

    // Start is called before the first frame update

    private void Awake()
    {
        if (sharedDM == null)
        {
            sharedDM = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timesPressed == 2)
        {
            ActivatedPreassure();
        }
        else if (timesPressed == 3)
        {
            ActivatedPreassure2();
        }
    }

    void ActivatedPreassure()
    {

        timesPressed = 0;
    }

    void ActivatedPreassure2()
    {

        timesPressed = 0;
    }
}
