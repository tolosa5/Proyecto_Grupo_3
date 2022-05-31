using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] Vector3 newScenePosDoor;
    [SerializeField] int newSceneIndex;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            anim.SetTrigger("FadeOut");
            GameManager.gM.newScenePositionGm = newScenePosDoor;
            StartCoroutine(WaitChanges());
        }
    }

    IEnumerator WaitChanges()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(newSceneIndex);
    }
}
