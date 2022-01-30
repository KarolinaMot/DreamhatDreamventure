using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalToHub : MonoBehaviour
{
    public GameObject textOpen;
    public Animator badAnimator;
    public Animator badHandAnimator;
    public Sprite good;
    public Sprite goodHands;
    public SpriteRenderer render;
    public SpriteRenderer handRender;
    bool textShown = false;
    public int hub;

    // Start is called before the first frame update
    void Update()
    {
        if (textShown && Input.GetKey(KeyCode.T))
        {
            textOpen.SetActive(false);
            badAnimator.SetTrigger("isTransforming");
            badHandAnimator.SetTrigger("isTransforming");
        }

        if (badAnimator.GetCurrentAnimatorStateInfo(0).IsName("Empty3") && badAnimator.GetCurrentAnimatorStateInfo(0).IsName("Empty3"))
        {
            render.sprite = good;
            handRender.sprite = goodHands;

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
