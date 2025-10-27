using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace InGame
{
    public class ShowCardController : MonoBehaviour
    {
        [HideInInspector] public static Sprite showSprite;
        [SerializeField] Text _cardInforText;
        [SerializeField] Text _cardEffectText;
        public static int _HP;
        public static int _ATK;
        public static int _DEF;
        public static int _MP;
        public static string _cardEffect;
        void Start()
        {
            showSprite = GetComponent<Image>().sprite;
            _HP = 0;
            _ATK = 0;
            _DEF = 0;
            _MP = 0;
        }

        void Update()
        {
            GetComponent<Image>().sprite = showSprite;
            _cardInforText.text = GetText(_HP) + "-" + GetText(_ATK) + "-" + GetText(_DEF) + "-" + GetText(_MP);
            _cardEffectText.text = _cardEffect;
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
