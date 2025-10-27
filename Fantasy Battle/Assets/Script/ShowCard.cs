using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShowCard : MonoBehaviour
{
    [HideInInspector] public static Sprite showSprite;
    [HideInInspector] public static RectTransform showRectTransform;

    void Start()
    {
        showSprite = GetComponent<Image>().sprite;
        showRectTransform = GetComponent<RectTransform>();
    }
    void Update()
    {
        GetComponent<Image>().sprite = showSprite;
    }
}
