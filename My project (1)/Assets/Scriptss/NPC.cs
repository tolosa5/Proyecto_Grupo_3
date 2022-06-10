using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPC : MonoBehaviour
{
    [TextArea]
    [SerializeField] string[] phrases;
    
    [SerializeField] GameObject box;
    [SerializeField] TextMeshProUGUI chat;

    int contPhrases = -1;
    [HideInInspector] public bool talking;

    public void Talk()
    {
        Player.player.GoBusy();

        box.SetActive(true);
        NextPhrase();
    }

    IEnumerator WritePhrase()
    {
        talking = true;
        chat.text = "";
        char[] chars = phrases[contPhrases].ToCharArray();

        for (int i = 0; i < chars.Length; i++)
        {
            chat.text += chars[i];
            yield return new WaitForSeconds(0.05f);
        }
        talking = false;
    }

    public void AutoComplete()
    {
        StopAllCoroutines();
        chat.text = "";
        chat.text = phrases[contPhrases];
        talking = false;
    }
    
    void NextPhrase()
    {
        contPhrases++;
        if (contPhrases == phrases.Length)
        {
            box.SetActive(false);
            contPhrases = -1;
            Player.player.GoFree();
            Player.player.key++;
        }
        else
        {
            StartCoroutine(WritePhrase());
        }
    }
}
