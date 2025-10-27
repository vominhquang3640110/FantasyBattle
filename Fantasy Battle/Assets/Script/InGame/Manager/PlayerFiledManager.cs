using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.XR;

namespace InGame
{
    public class PlayerFiledManager : MonoBehaviour
    {
        public static PlayerFiledManager instance;

        [Header("Manager")]
        public InGameManager _inGameManager;
        public StepAndDayManager _stepAndDayManager;
        public BattleManager _battleManager;
        public CreateAndDestroyCardManager _createAndDestroyCardManager;

        PlayerIFManager _P1;
        PlayerIFManager _P2;

        public List<GameObject> _MZP1 = new List<GameObject>();
        public List<GameObject> _EZP1 = new List<GameObject>();
        public List<GameObject> _MZP2 = new List<GameObject>();
        public List<GameObject> _EZP2 = new List<GameObject>();

        public GameObject GYView;
        public GameObject GYP1;
        //public GameObject CloseGYP1Button;
        public GameObject GYP2;
        //public GameObject CloseGYP2Button;

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
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (_inGameManager._isGetPlayerIF)
            {
                _P1 = _inGameManager._P1;
                _P2 = _inGameManager._P2;
            }
        }

        public int CheckCharOfField(string who)
        {
            int noCharCountP1 = 0;
            int noCharCountP2 = 0;
            for (int i = 0; i < _P1._characterZone.Length; i++)
            {
                if (_P1._characterZone[i] == _P1._startCard || _P1._characterZone[i] == "")
                {
                    noCharCountP1++;
                }
            }
            for (int i = 0; i < _P2._characterZone.Length; i++)
            {
                if (_P2._characterZone[i] == _P2._startCard || _P2._characterZone[i] == "")
                {
                    noCharCountP2++;
                }
            }

            if (who == "P1")
            {
                return (4 - noCharCountP1);
            }
            else
            {
                return (4 - noCharCountP2);
            }
        }
    

        public int PIFIndexFromZone(string zone)
        {
            int index = int.Parse(zone.Substring(2,1)) - 1;
            return index;
        }

        public int NowIndexGYList(string[] GYList)
        {
            int index = -1;
            for (int i = 0; i < GYList.Length; i++)
            {
                if (GYList[i] == null || GYList[i] == "")
                {
                    index = i;
                    break;
                }
            }
            return index;
        }

        public void OnCLickGYP1Button()
        {
            GYView.SetActive(true);
            GYP1.SetActive(true);
            _createAndDestroyCardManager.CreateAndDestroyCardOfGY(true);
        }
        public void OnClickCloseGYP1Button()
        {
            GYP1.SetActive(false);
            GYView.SetActive(false);
        }
        public void OnCLickGYP2Button()
        {
            GYView.SetActive(true);
            GYP2.SetActive(true);
            _createAndDestroyCardManager.CreateAndDestroyCardOfGY(false);
        }
        public void OnClickCloseGYP2Button()
        {
            GYP2.SetActive(false);
            GYView.SetActive(false);
        }


        public List<GameObject> GetP1SoulCardList()
        {
            List<GameObject> soulCard = new List<GameObject>();
            for (int i = 0; i < _MZP1.Count; i++)
            {
                if (_MZP1[i].GetComponentInChildren<CardController>()._Type != "Start")
                {
                    soulCard.Add(_MZP1[i].GetComponentInChildren<CardController>().gameObject);
                }
            }
            return soulCard;
        }
        public List<GameObject> GetP2SoulCardList()
        {
            List<GameObject> soulCard = new List<GameObject>();
            for (int i = 0; i < _MZP2.Count; i++)
            {
                if (_MZP2[i].GetComponentInChildren<CardController>()._Type != "Start")
                {
                    soulCard.Add(_MZP2[i].GetComponentInChildren<CardController>().gameObject);
                }
            }
            return soulCard;
        }
        public void AddCardToGY(CardController card)
        {
            int index = NowIndexGYList(_P1._gy);
            _P1._gy[index] = card._name;
        }
    }
}