using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class terminalCode : MonoBehaviour
{
    public GameObject textOpen;
    public GameObject terminal;
    bool terminalOpen = false;
    bool textShown = false;

    // Start is called before the first frame update
    void Update()
    {
        if(textShown && Input.GetKey(KeyCode.T))
        {
            terminal.SetActive(true);
            textOpen.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            textOpen.SetActive(true);
            textShown = true;
        }
        Debug.Log(collision.gameObject.name);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            textOpen.SetActive(false);
            textShown = false;
        }
    }
}