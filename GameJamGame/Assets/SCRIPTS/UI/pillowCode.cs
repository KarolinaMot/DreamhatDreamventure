using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pillowCode : MonoBehaviour
{
    public GameObject textOpen;
    public Animator goodAnimator;
    public Animator goodhandAnimator;
    public Sprite bad;
    public Sprite badHands;
    public SpriteRenderer render;
    public SpriteRenderer handRender;
    public SceneChange sceneChange;
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
            render.sprite = bad;
            handRender.sprite = badHands;
            random = Random.Range(2, 4);
            sceneChange.OnSceneChange();
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
