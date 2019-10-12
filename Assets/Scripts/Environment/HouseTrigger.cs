using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseTrigger : MonoBehaviour
{
    [Tooltip("Objects must have an sprite renderer")]
    [SerializeField]
    private GameObject[] reactionableObjects;

    private Color32[] baseColors;
    private Color32 fadeColor;
    private SpriteRenderer[] sr;

    void Start()
    {
        CreateList();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" || other.gameObject.tag == "Enemy")
        {
            Fade(); 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Enemy")
        {
            Restore();
        }
    }

    private void CreateList()
    {
        sr = new SpriteRenderer[reactionableObjects.Length];
        baseColors = new Color32[reactionableObjects.Length];

        for (int i = 0; i < reactionableObjects.Length; i++)
        {
            if (reactionableObjects[i].GetComponent<SpriteRenderer>())
            {
                sr[i] = reactionableObjects[i].GetComponent<SpriteRenderer>();
                baseColors[i] = sr[i].color;
            }
        }

        fadeColor = new Color32(0, 0, 0, 0);
    }

    private void Fade()
    {
        for (int i = 0; i < reactionableObjects.Length; i++)
        {
            sr[i].color = fadeColor;
        }
    }

    private void Restore()
    {
        for (int i = 0; i < reactionableObjects.Length; i++)
        {
            sr[i].color = baseColors[i];
        }
    }
}
