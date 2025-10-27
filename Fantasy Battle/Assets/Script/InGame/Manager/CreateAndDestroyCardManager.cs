using InGame;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace InGame
{
    public class CreateAndDestroyCardManager : MonoBehaviour
    {
        public static CreateAndDestroyCardManager instance;

        [Header("Manager")]
        public InGameManager _inGameManager;
        public PrefabManager _prefabManager;

        [Header("Character Zone")]
        //P1
        public GameObject MZ1P1;
        public GameObject MZ2P1;
        public GameObject MZ3P1;
        public GameObject MZ4P1;
        //P2
        public GameObject MZ1P2;
        public GameObject MZ2P2;
        public GameObject MZ3P2;
        public GameObject MZ4P2;

        [Header("Equipment Zone")]
        //P1
        public GameObject EZ1P1;
        public GameObject EZ2P1;
        public GameObject EZ3P1;
        public GameObject EZ4P1;
        //P2
        public GameObject EZ1P2;
        public GameObject EZ2P2;
        public GameObject EZ3P2;
        public GameObject EZ4P2;

        [Header("Magnus Zone")]
        //P1
        public GameObject MagnusZone1P1;
        public GameObject MagnusZone2P1;
        //P2
        public GameObject MagnusZone1P2;
        public GameObject MagnusZone2P2;

        [Header("GY")]
        public GameObject GYListP1;
        public GameObject GYListP2;

        [Header("P1 P2 Field Infor")]
        PlayerIFManager _P1;
        PlayerIFManager _P2;
        //P1
        public string[] _oldcharacterZoneP1;
        public string[] _oldequipmentZoneP1;
        public string[] _nowcharacterZoneP1;
        public string[] _nowequipmentZoneP1;
        //P2
        public string[] _oldcharacterZoneP2;
        public string[] _oldequipmentZoneP2;
        public string[] _nowcharacterZoneP2;
        public string[] _nowequipmentZoneP2;

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
        private void Start()
        {
            _oldcharacterZoneP1 = new string[4];
            _oldequipmentZoneP1 = new string[4];
            _nowcharacterZoneP1 = new string[4];
            _nowequipmentZoneP1 = new string[4];
            _oldcharacterZoneP2 = new string[4];
            _oldequipmentZoneP2 = new string[4];
            _nowcharacterZoneP2 = new string[4];
            _nowequipmentZoneP2 = new string[4];
        }

        private void Update()
        {

        }

        public void UpdateField()
        {
            _P1 = _inGameManager._P1;
            _P2 = _inGameManager._P2;
            
            ZoneCardUpdate();
        }

        void ZoneCardUpdate()
        {
            for (int x = 0; x < 4; x++)
            {
                _nowcharacterZoneP1[x] = _P1._characterZone[x];
                _nowequipmentZoneP1[x] = _P1._equipmentZone[x];
                _nowcharacterZoneP2[x] = _P2._characterZone[x];
                _nowequipmentZoneP2[x] = _P2._equipmentZone[x];
 
                //P1
                if (_oldcharacterZoneP1[x] != _nowcharacterZoneP1[x])
                {
                    _oldcharacterZoneP1[x] = _nowcharacterZoneP1[x];
                    int zoneIndex = x + 1;
                    CreateAndDestroyCardToZone(_nowcharacterZoneP1[x], "MZ" + zoneIndex + "P1");
                }
                if (_oldequipmentZoneP1[x] != _nowequipmentZoneP1[x])
                {
                    _oldequipmentZoneP1[x] = _nowequipmentZoneP1[x];
                    int zoneIndex = x + 1;
                    CreateAndDestroyCardToZone(_nowequipmentZoneP1[x], "EZ" + zoneIndex + "P1");
                }
                //P2
                if (_oldcharacterZoneP2[x] != _nowcharacterZoneP2[x])
                {
                    _oldcharacterZoneP2[x] = _nowcharacterZoneP2[x];
                    int zoneIndex = x + 1;
                    CreateAndDestroyCardToZone(_nowcharacterZoneP2[x], "MZ" + zoneIndex + "P2");
                }
                if (_oldequipmentZoneP2[x] != _nowequipmentZoneP2[x])
                {
                    _oldequipmentZoneP2[x] = _nowequipmentZoneP2[x];
                    int zoneIndex = x + 1;
                    CreateAndDestroyCardToZone(_nowequipmentZoneP2[x], "EZ" + zoneIndex + "P2");
                }
            }
        }

        void CreateAndDestroyCardToZone(string cardName, string zoneName)
        {
            GameObject zone = null;
            string tag = "P1";
            switch (zoneName)
            {
                //P1
                case "MZ1P1":
                    zone = MZ1P1; break;
                case "MZ2P1":
                    zone = MZ2P1; break;
                case "MZ3P1":
                    zone = MZ3P1; break;
                case "MZ4P1":
                    zone = MZ4P1; break;
                case "EZ1P1":
                    zone = EZ1P1; break;
                case "EZ2P1":
                    zone = EZ2P1; break;
                case "EZ3P1":
                    zone = EZ3P1; break;
                case "EZ4P1":
                    zone = EZ4P1; break;
                //P2
                case "MZ1P2":
                    zone = MZ1P2; tag = "P2"; break;
                case "MZ2P2":
                    zone = MZ2P2; tag = "P2"; break;
                case "MZ3P2":
                    zone = MZ3P2; tag = "P2"; break;
                case "MZ4P2":
                    zone = MZ4P2; tag = "P2"; break;
                case "EZ1P2":
                    zone = EZ1P2; tag = "P2"; break;
                case "EZ2P2":
                    zone = EZ2P2; tag = "P2"; break;
                case "EZ3P2":
                    zone = EZ3P2; tag = "P2"; break;
                case "EZ4P2":
                    zone = EZ4P2; tag = "P2"; break;
                default:
                    break;
            }
            if (zone != null)
            {
                DestroyChil(zone);
                if (cardName != "" && cardName != null)
                {
                    GameObject card = Instantiate(_prefabManager.FindCardPrefab(cardName), zone.transform.position, Quaternion.identity);
                    card.transform.SetParent(zone.transform);
                    card.transform.localScale = Vector3.one;
                    card.gameObject.tag = tag;
                    card.GetComponent<CardController>()._zone = zoneName;
                    bool isEquip = card.GetComponent<CardController>()._isEuqip;
                    if (isEquip)
                    {
                        int index = int.Parse(zoneName.Substring(2, 1));
                        card.GetComponent<CardController>().MZ = FindCharCardZone("MZ" + index + tag);
                        if (StepAndDayManager.instance._round > 0)
                        {
                            card.GetComponent<CardController>().MZ.GetComponentInChildren<CardController>().FromEquipAbilityPlus();
                        }
                    }
                    else if (!isEquip)
                    {
                        int index = int.Parse(zoneName.Substring(2, 1));
                        card.GetComponent<CardController>().EZ = FindEquipCardZone("EZ" + index + tag);
                        if (StepAndDayManager.instance._round > 0)
                        {
                            card.GetComponent<CardController>().FromEquipAbilityPlus();
                        }
                    }
                }
            }
            else
            {
                Debug.LogError("Create Error");
            }
        }

        public void CreateAndDestroyCardOfGY(bool _isGYP1)
        {
            if (_isGYP1)
            {
                DestroyChil(GYListP1);
                int numberCardOfGY = PlayerFiledManager.instance.NowIndexGYList(InGameManager.instance._P1._gy);
                for (int i = 0; i < numberCardOfGY; i++)
                {
                    GameObject card = Instantiate(_prefabManager.FindCardPrefab(InGameManager.instance._P1._gy[i]), Vector3.zero, Quaternion.identity);
                    card.gameObject.tag = "P1";
                    card.GetComponent<CardController>()._isGY = true;
                    card.transform.SetParent(GYListP1.transform);
                    card.transform.localScale = Vector2.one;
                    card.GetComponent<RectTransform>().sizeDelta = new Vector2(card.GetComponent<RectTransform>().sizeDelta.x, card.GetComponent<RectTransform>().sizeDelta.y);
                }
            }
            else
            {
                DestroyChil(GYListP2);
                int numberCardOfGY = PlayerFiledManager.instance.NowIndexGYList(InGameManager.instance._P2._gy);
                for (int i = 0; i < numberCardOfGY; i++)
                {
                    GameObject card = Instantiate(_prefabManager.FindCardPrefab(InGameManager.instance._P2._gy[i]), Vector3.zero, Quaternion.identity);
                    card.gameObject.tag = "P2";
                    card.GetComponent<CardController>()._isGY = true;
                    card.transform.SetParent(GYListP2.transform);
                    card.transform.localScale = Vector2.one;
                    card.GetComponent<RectTransform>().sizeDelta = new Vector2(card.GetComponent<RectTransform>().sizeDelta.x, card.GetComponent<RectTransform>().sizeDelta.y);
                }
            }
        }

        public void UpdateMagnusZone()
        {
            CardMagnusController cardMagnus1P1 = MagnusZone1P1.GetComponentInChildren<CardMagnusController>();
            CardMagnusController cardMagnus2P1 = MagnusZone2P1.GetComponentInChildren<CardMagnusController>();
            CardMagnusController cardMagnus1P2 = MagnusZone1P2.GetComponentInChildren<CardMagnusController>();
            CardMagnusController cardMagnus2P2 = MagnusZone2P2.GetComponentInChildren<CardMagnusController>();
            
            //P1
            if (cardMagnus1P1 == null)
            {
                GameObject card = Instantiate(_prefabManager.FindCardPrefab(_P1._deckMagnusName[0]), MagnusZone1P1.transform.position, Quaternion.identity, MagnusZone1P1.transform);
                card.GetComponent<CardController>().frontCardImage = card.GetComponent<Image>().sprite;
                card.transform.localScale = Vector3.one;
                card.gameObject.tag = "P1";
                card.GetComponent<CardMagnusController>().BackCardShow();
                card.GetComponent<CardMagnusController>()._magnusZone = MagnusZone.Magnus1P1;
            }
            else if (_P1._MagnusActive[0])
            {
                cardMagnus1P1.FrontCardShow();
                cardMagnus1P1.GetComponent<CardEffect>().isMagnusActive = true;
            }
            if (cardMagnus2P1 == null)
            {
                GameObject card = Instantiate(_prefabManager.FindCardPrefab(_P1._deckMagnusName[1]), MagnusZone2P1.transform.position, Quaternion.identity, MagnusZone2P1.transform);
                card.GetComponent<CardController>().frontCardImage = card.GetComponent<Image>().sprite;
                card.transform.localScale = Vector3.one;
                card.gameObject.tag = "P1";
                card.GetComponent<CardMagnusController>().BackCardShow();
                card.GetComponent<CardMagnusController>()._magnusZone = MagnusZone.Magnus2P1;
            }
            else if (_P1._MagnusActive[1])
            {
                cardMagnus2P1.FrontCardShow();
                cardMagnus2P1.GetComponent<CardEffect>().isMagnusActive = true;
            }
            //P2
            if (cardMagnus1P2 == null)
            {
                GameObject card = Instantiate(_prefabManager.FindCardPrefab(_P2._deckMagnusName[0]), MagnusZone1P2.transform.position, Quaternion.identity, MagnusZone1P2.transform);
                card.GetComponent<CardController>().frontCardImage = card.GetComponent<Image>().sprite;
                card.transform.localScale = Vector3.one;
                card.gameObject.tag = "P2";
                card.GetComponent<CardMagnusController>().BackCardShow();
                card.GetComponent<CardMagnusController>()._magnusZone = MagnusZone.Magnus1P2;
            }
            else if (_P2._MagnusActive[0])
            {
                cardMagnus1P2.FrontCardShow();
                cardMagnus1P2.GetComponent<CardEffect>().isMagnusActive = true;
            }
            if (cardMagnus2P2 == null)
            {
                GameObject card = Instantiate(_prefabManager.FindCardPrefab(_P2._deckMagnusName[1]), MagnusZone2P2.transform.position, Quaternion.identity, MagnusZone2P2.transform);
                card.GetComponent<CardController>().frontCardImage = card.GetComponent<Image>().sprite;
                card.transform.localScale = Vector3.one;
                card.gameObject.tag = "P2";
                card.GetComponent<CardMagnusController>().BackCardShow();
                card.GetComponent<CardMagnusController>()._magnusZone = MagnusZone.Magnus2P2;
            }
            else if (_P2._MagnusActive[1])
            {
                cardMagnus2P2.FrontCardShow();
                cardMagnus2P2.GetComponent<CardEffect>().isMagnusActive = true;
            }
        }

        public void DestroyChil(GameObject parent)
        {
            foreach (Transform chil in parent.transform)
            {
                Destroy(chil.gameObject);
            }
        }


        public GameObject FindEquipCardZone(string zoneName)
        {
            if (zoneName == "EZ1P1")
            {
                return EZ1P1;
            }
            else if (zoneName == "EZ2P1")
            {
                return EZ2P1;
            }
            else if (zoneName == "EZ3P1")
            {
                return EZ3P1;
            }
            else if (zoneName == "EZ4P1")
            {
                return EZ4P1;
            }
            else if (zoneName == "EZ1P2")
            {
                return EZ1P2;
            }
            else if (zoneName == "EZ2P2")
            {
                return EZ2P2;
            }
            else if (zoneName == "EZ3P2")
            {
                return EZ3P2;
            }
            else if (zoneName == "EZ4P2")
            {
                return EZ4P2;
            }
            else
            {
                return null;
            }
        }
        public GameObject FindCharCardZone(string zoneName)
        {
            if (zoneName == "MZ1P1")
            {
                return MZ1P1;
            }
            else if (zoneName == "MZ2P1")
            {
                return MZ2P1;
            }
            else if (zoneName == "MZ3P1")
            {
                return MZ3P1;
            }
            else if (zoneName == "MZ4P1")
            {
                return MZ4P1;
            }
            else if (zoneName == "MZ1P2")
            {
                return MZ1P2;
            }
            else if (zoneName == "MZ2P2")
            {
                return MZ2P2;
            }
            else if (zoneName == "MZ3P2")
            {
                return MZ3P2;
            }
            else if (zoneName == "MZ4P2")
            {
                return MZ4P2;
            }
            else
            {
                return null;
            }
        }
    }

}