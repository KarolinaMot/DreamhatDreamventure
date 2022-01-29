using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pillowCode : MonoBehaviour
{
    public GameObject textOpen;
    public Animator goodAnimator;
    public Animator goodhandAnimator;
    bool textShown = false;
    int random;

    // Start is called before the first frame update
    void Update()
    {
        if(textShown && Input.GetKey(KeyCode.T))
        {
            textOpen.SetActive(false);
            goodAnimator.SetTrigger("isTransforming");
            goodhandAnimator.SetTrigger("isTransforming");


            
                
        }

        if (goodAnimator.GetCurrentAnimatorStateInfo(0).IsName("Empty3") && goodAnimator.GetCurrentAnimatorStateInfo(0).IsName("Empty3"))
        {
            random = Random.Range(1, 4);

            SceneManager.LoadScene(random);
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

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "GoodPlayer" || collision.gameObject.name == "BadPlayer")
        {
            textOpen.SetActive(false);
            textShown = false;
        }
    }
}
