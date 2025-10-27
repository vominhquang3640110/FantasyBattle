using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace InGame
{
    public class TextViewBoxShow : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public bool isShow = false;
        public float speedShow = 500;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            
            if (isShow)
            {
                if (GetComponent<RectTransform>().localPosition.x < -790)
                {
                    GetComponent<RectTransform>().localPosition += new Vector3(Time.deltaTime * speedShow, 0, 0);
                }
                if (GetComponent<RectTransform>().localPosition.x > -790)
                {
                    GetComponent<RectTransform>().localPosition = new Vector3(-790, GetComponent<RectTransform>().localPosition.y, GetComponent<RectTransform>().localPosition.z);
                }
            }
            else
            {
                if (GetComponent<RectTransform>().localPosition.x > -1130)
                {
                    GetComponent<RectTransform>().localPosition -= new Vector3(Time.deltaTime * speedShow, 0, 0);

                }
                if (GetComponent<RectTransform>().localPosition.x < -1130)
                {
                    GetComponent<RectTransform>().localPosition = new Vector3(-1130, GetComponent<RectTransform>().localPosition.y, GetComponent<RectTransform>().localPosition.z);
                }
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            isShow = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            isShow = false;
        }
    }

}