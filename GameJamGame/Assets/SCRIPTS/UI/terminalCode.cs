using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class terminalCode : MonoBehaviour
{
    public GameObject textOpen;
    public GameObject terminal;
    public AudioSource computerSound;
    bool textShown = false;

    // Start is called before the first frame update
    void Update()
    {
        if(textShown && Input.GetKey(KeyCode.T))
        {
            computerSound.Play();
            terminal.SetActive(true);
            textOpen.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "GoodPlayer" || collision.gameObject.name == "BadPlayer")
        {
            textOpen.SetActive(true);
            textShown = true;
        }
        Debug.Log(collision.gameObject.name);
    }
}
