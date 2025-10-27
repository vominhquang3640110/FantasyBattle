using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace InGame
{
    public class AbilityTextShow : MonoBehaviour
    {
        public GameObject MZ;
        public Text text;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            int chillCount = MZ.transform.childCount;
            if (chillCount > 0)
            {
                text.text = GetText(MZ.GetComponentInChildren<CardController>()._HP) + "-" + GetText(MZ.GetComponentInChildren<CardController>()._ATK) + "-" +
                    GetText(MZ.GetComponentInChildren<CardController>()._DEF) + "-" + GetText(MZ.GetComponentInChildren<CardController>()._MP);
            }
        }

        string GetText(int _infor)
        {
            if (_infor < 10)
            {
                return "0" + _infor.ToString();
            }
            else
            {
                return _infor.ToString();
            }
        }
    }
}
