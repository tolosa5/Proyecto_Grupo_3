using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class GameManager : MonoBehaviour
{
    public static GameManager gM;
    public Vector3 newScenePositionGm;
    [HideInInspector] public Animator animSlider;

    [HideInInspector] public GameObject marco;
    [HideInInspector] public TextMeshProUGUI text;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (gM == null)
        {
            gM = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        newScenePositionGm = Player.player.transform.position;
    }

    void OnSceneLoaded(Scene loadedScene, LoadSceneMode loadMode)
    {
        Player.player.transform.position = newScenePositionGm;

        marco = GameObject.FindGameObjectWithTag("Marco");
        if (loadedScene.buildIndex == 0)
        {
            animSlider = GameObject.FindGameObjectWithTag("Slider").GetComponent<Animator>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
