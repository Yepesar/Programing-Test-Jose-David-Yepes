using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class FireButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    private PlayerController playerController;

    public void OnPointerDown(PointerEventData eventData)
    {
        playerController.Shooting = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        playerController.Shooting = false;
    }
}
