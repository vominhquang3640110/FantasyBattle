using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace InGame
{
    public enum Player
    {
        P1,
        P2,
    }
    public class UserIFShow : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
    {
        public GameObject TextBox;
        public float showSpeed;
        public bool isShow = false;

        public Player _player;
        

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            TextBoxShow();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            isShow = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            isShow = false;
        }

        void TextBoxShow()
        {
            float textBoxX = TextBox.GetComponent<RectTransform>().localPosition.x;
            if (_player == Player.P1)
            {
                if (isShow && textBoxX < 175)
                {
                    TextBox.GetComponent<RectTransform>().localPosition += new Vector3(Time.deltaTime * showSpeed, 0, 0);
                }
                else if (!isShow && textBoxX > 25)
                {
                    TextBox.GetComponent<RectTransform>().localPosition -= new Vector3(Time.deltaTime * showSpeed, 0, 0);
                }
                if (isShow && textBoxX > 175)
                {
                    TextBox.GetComponent<RectTransform>().localPosition = new Vector3(175, 0, 0);
                }
                if (!isShow && textBoxX < 25)
                {
                    TextBox.GetComponent<RectTransform>().localPosition = new Vector3(25, 0, 0);
                }
            }
            else
            {
                if (isShow && textBoxX > -175)
                {
                    TextBox.GetComponent<RectTransform>().localPosition -= new Vector3(Time.deltaTime * showSpeed, 0, 0);
                }
                else if (!isShow && textBoxX < -25)
                {
                    TextBox.GetComponent<RectTransform>().localPosition += new Vector3(Time.deltaTime * showSpeed, 0, 0);
                }
                if (isShow && textBoxX < -175)
                {
                    TextBox.GetComponent<RectTransform>().localPosition = new Vector3(-175, 0, 0);
                }
                if (!isShow && textBoxX > -25)
                {
                    TextBox.GetComponent<RectTransform>().localPosition = new Vector3(-25, 0, 0);
                }
            }
        }
    }
}
