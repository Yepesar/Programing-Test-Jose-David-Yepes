using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private GameObject lockCollider;
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip openSound;
    [SerializeField]
    private AudioClip lockedSound;

    private BoxCollider boxCollider;
    private HomeController homeController;
    private PlayerController playerControllerTemp;
    private bool activeSound = true;

    // Start is called before the first frame update
    void Start()
    {
        homeController = GetComponentInParent<HomeController>();
        boxCollider = GetComponentInParent<BoxCollider>();
    }

    private void OnTriggerEnter (Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {         
            if (collision.gameObject.GetComponent<PlayerController>())
            {
                playerControllerTemp = collision.gameObject.GetComponent<PlayerController>();

                if (activeSound)
                {
                    if (playerControllerTemp.HasKey)
                    {
                        audioSource.PlayOneShot(openSound);
                        anim.SetInteger("State", 1);
                        boxCollider.enabled = false;
                        lockCollider.SetActive(false);
                        activeSound = false;
                    }
                    else
                    {
                        audioSource.PlayOneShot(lockedSound);
                    }
                }
            }          
        }
    }  
}
