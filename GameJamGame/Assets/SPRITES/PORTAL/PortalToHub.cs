using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalToHub : MonoBehaviour
{
    public GameObject textOpen;
    public SceneChange sceneChange;
    bool textShown = false;
    public int hub;

    // Start is called before the first frame update
    void Update()
    {
        if (textShown && Input.GetKey(KeyCode.T))
        {
            textOpen.SetActive(false);
            sceneChange.OnSceneChange();
            SceneManager.LoadScene(hub);
        }      
        

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "GoodPlayer" || collision.gameObject.name == "BadPlayer")
        {
            textOpen.SetActive(true);
            textShown = true;
        }
        Debug.Log(collision.gameObject.name);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "GoodPlayer" || collision.gameObject.name == "BadPlayer")
        {
            textOpen.SetActive(false);
            textShown = false;
        }
    }
}
