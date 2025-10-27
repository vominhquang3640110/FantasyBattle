using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace InGame
{
    public class HandManager : MonoBehaviour
    {
        public static HandManager instance;
        [Header("Manager")]
        public InGameManager _inGameManager;
        public PrefabManager _prefabManager;
        public PlayerFiledManager _playerFiledManager;

        [Header("Hand Content")]
        [SerializeField] GameObject _P1HandContent;
        [SerializeField] GameObject _P2HandContent;

        [Header("P2 Hand Number Card")]
        public int _nowHandNumberCardP2;
        public int _oldHandNumberCardP2;

        PlayerIFManager _P1;
        PlayerIFManager _P2;

        public Text handNumberP1;
        public Text handNumberP2;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        void Start()
        {

        }
        private void Update()
        {
            if (_inGameManager._isGetPlayerIF)
            {
                _P1 = _inGameManager._P1;
                _P2 = _inGameManager._P2;
                _P1._handCardNumber = _P1HandContent.transform.childCount;
                handNumberP1.text = "HAND {" + _inGameManager._P1._handCardNumber + "}";
                handNumberP2.text = "HAND {" + _inGameManager._P2._handCardNumber + "}";
            }
            //HandP2 Update
            HandP2Update();
        }

        public void AddCardToHandP1(string cardName)
        {
            GameObject cardPrefab = _prefabManager.FindCardPrefab(cardName);
            GameObject card = Instantiate(cardPrefab, Vector3.zero, Quaternion.identity);
            card.transform.SetParent(_P1HandContent.transform);
            card.transform.localScale = Vector3.one;
        }
        public void DisAllCardHandP1()
        {
            foreach (var card in _P1HandContent.transform.GetComponentsInChildren<CardController>())
            {
                int index = _playerFiledManager.NowIndexGYList(_P1._gy);
                _P1._gy[index] = card._name;
                Destroy(card.gameObject);
            }
        }
        public void HandP2Update()
        {
            if (_inGameManager._isGetPlayerIF)
            {
                _nowHandNumberCardP2 = _inGameManager._P2._handCardNumber;
            }
            if (_nowHandNumberCardP2 != _oldHandNumberCardP2)
            {
                _oldHandNumberCardP2 = _nowHandNumberCardP2;
                //HandP2‚Ìq—v‘f‚ğ‘S•”íœ
                foreach (Transform chil in _P2HandContent.transform)
                {
                    Destroy(chil.gameObject);
                }
                //HandP2‚ÉCardBack‚ğÄ¶¬
                for (int i = 0; i < _oldHandNumberCardP2; i++)
                {
                    GameObject card = Instantiate(_prefabManager.FindCardPrefab("Card Back"), Vector3.zero, Quaternion.identity);
                    card.transform.SetParent(_P2HandContent.transform);
                    card.transform.localScale = Vector3.one;
                }
            }
        }
    }

}