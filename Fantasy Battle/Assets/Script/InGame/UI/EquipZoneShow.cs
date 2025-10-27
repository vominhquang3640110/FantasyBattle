using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace InGame
{
    public class EquipZoneShow : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public GameObject EquipZone;
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
                    if (EquipZone.GetComponent<RectTransform>().localPosition.y > -152)
                    {
                        EquipZone.GetComponent<RectTransform>().localPosition -= new Vector3(0, Time.deltaTime * speedShow, 0);
                    }
                    if (EquipZone.GetComponent<RectTransform>().localPosition.y < -152)
                    {
                        EquipZone.GetComponent<RectTransform>().localPosition = new Vector3(EquipZone.GetComponent<RectTransform>().localPosition.x, -152, EquipZone.GetComponent<RectTransform>().localPosition.z);
                    }
                }
                else
                {
                    if (EquipZone.GetComponent<RectTransform>().localPosition.y < 10)
                    {
                        EquipZone.GetComponent<RectTransform>().localPosition += new Vector3(0, Time.deltaTime * speedShow, 0);
                    }
                    if (EquipZone.GetComponent<RectTransform>().localPosition.y > 10)
                    {
                        EquipZone.GetComponent<RectTransform>().localPosition = new Vector3(EquipZone.GetComponent<RectTransform>().localPosition.x, 10, EquipZone.GetComponent<RectTransform>().localPosition.z);
                    }
                }
            }
            else
            {
                if (isShow)
                {
                    if (EquipZone.GetComponent<RectTransform>().localPosition.y < 152)
                    {
                        EquipZone.GetComponent<RectTransform>().localPosition += new Vector3(0, Time.deltaTime * speedShow, 0);
                    }
                    if (EquipZone.GetComponent<RectTransform>().localPosition.y > 152)
                    {
                        EquipZone.GetComponent<RectTransform>().localPosition = new Vector3(EquipZone.GetComponent<RectTransform>().localPosition.x, 152, EquipZone.GetComponent<RectTransform>().localPosition.z);
                    }
                }
                else
                {
                    if (EquipZone.GetComponent<RectTransform>().localPosition.y > -10)
                    {
                        EquipZone.GetComponent<RectTransform>().localPosition -= new Vector3(0, Time.deltaTime * speedShow, 0);
                    }
                    if (EquipZone.GetComponent<RectTransform>().localPosition.y < -10)
                    {
                        EquipZone.GetComponent<RectTransform>().localPosition = new Vector3(EquipZone.GetComponent<RectTransform>().localPosition.x, -10, EquipZone.GetComponent<RectTransform>().localPosition.z);
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