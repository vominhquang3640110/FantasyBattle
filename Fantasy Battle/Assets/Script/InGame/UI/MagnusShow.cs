using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace InGame
{
    public class MagnusShow : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public Player _player;
        public bool isShow = false;
        public float speedShow = 500;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (_player == Player.P1)
            {
                if (isShow)
                {
                    if (GetComponent<RectTransform>().localPosition.y < -465f)
                    {
                        GetComponent<RectTransform>().localPosition += new Vector3(0, Time.deltaTime * speedShow, 0);
                    }
                    if (GetComponent<RectTransform>().localPosition.y > -465f)
                    {
                        GetComponent<RectTransform>().localPosition = new Vector3(GetComponent<RectTransform>().localPosition.x, -465f, GetComponent<RectTransform>().localPosition.z);
                    }
                }
                else
                {
                    if (GetComponent<RectTransform>().localPosition.y > -615f)
                    {
                        GetComponent<RectTransform>().localPosition -= new Vector3(0, Time.deltaTime * speedShow, 0);

                    }
                    if (GetComponent<RectTransform>().localPosition.y < -615f)
                    {
                        GetComponent<RectTransform>().localPosition = new Vector3(GetComponent<RectTransform>().localPosition.x, -615f, GetComponent<RectTransform>().localPosition.z);
                    }
                }
            }
            else
            {
                if (isShow)
                {
                    if (GetComponent<RectTransform>().localPosition.y > 470f)
                    {
                        GetComponent<RectTransform>().localPosition -= new Vector3(0, Time.deltaTime * speedShow, 0);
                    }
                    if (GetComponent<RectTransform>().localPosition.y < 470f)
                    {
                        GetComponent<RectTransform>().localPosition = new Vector3(GetComponent<RectTransform>().localPosition.x, 470f, GetComponent<RectTransform>().localPosition.z);
                    }
                }
                else
                {
                    if (GetComponent<RectTransform>().localPosition.y < 615f)
                    {
                        GetComponent<RectTransform>().localPosition += new Vector3(0, Time.deltaTime * speedShow, 0);

                    }
                    if (GetComponent<RectTransform>().localPosition.y > 615f)
                    {
                        GetComponent<RectTransform>().localPosition = new Vector3(GetComponent<RectTransform>().localPosition.x, 615f, GetComponent<RectTransform>().localPosition.z);
                    }
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
