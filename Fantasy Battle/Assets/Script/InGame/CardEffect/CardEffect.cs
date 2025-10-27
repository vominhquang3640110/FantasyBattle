using InGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace InGame
{
    public class CardEffect : MonoBehaviour
    {
        [Header("Manager And Controller")]
        public CardController _cardController;
        public BattleStepCardController _battleStepCardController;
        private PlayerFiledManager _playerFiledManager;
        private InGameManager _inGameManager;
        private StepAndDayManager _stepAndDayManager;
        private BattleManager _battleManager;
        private ActiveEffectManager _activeEffectManager;
        private PrefabManager _prefabManager;

        public bool _isBurn = false;
        public int _BurnDamage = 2;
        public bool _isPoison = false;
        public int _PoisonDamage = 1;

        bool _getBurn = false;
        bool _getPoison = false;


        [Header("My Parament")]
        public bool thisCardActiveEffect = false;
        public bool thisCardActiveTargetEffect = false;
        public Image ActiveEffectLogo_1;
        public Image ActiveEffectLogo_2;
        public bool isActiveAnimationEffectLogo = false;
        public float time = 0;

        public bool haveActiveEff = false;
        private bool oldHaveActiveEff;
        public GameObject activeButton;
        public bool isMagnusActive = false;
        public bool isActive = false;
        public bool isActiveEnd = false;
        public bool isEffectToSelf = false;
        public bool isEffectToP1 = false;
        public bool isEffectToP2 = false;
        public bool isEffectToEqP1 = false;
        public bool isEffectToEqP2 = false;
        public delegate void EternalPross();
        public delegate bool CheckConditions();
        public delegate void ActiveEffect();
        public delegate void ActivedEffect(CardController cardController);
        [HideInInspector] public EternalPross Eternal = null;
        [HideInInspector] public CheckConditions CheckConditionspross = null;
        [HideInInspector] public ActiveEffect ActiveEffectPross = null;
        [HideInInspector] public ActivedEffect ActivedEffectPross = null;

        private bool oldColorChange = false;

        // Start is called before the first frame update
        void Start()
        {
            _playerFiledManager = PlayerFiledManager.instance;
            _inGameManager = InGameManager.instance;
            _stepAndDayManager = StepAndDayManager.instance;
            _battleManager = BattleManager.instance;
            _activeEffectManager = ActiveEffectManager.instance;
            _prefabManager = PrefabManager.instance;

            oldHaveActiveEff = haveActiveEff;
        }

        // Update is called once per frame
        void Update()
        {
            CardEffectActiveAnimation();

            BurnDamageProcess();
            PoisonDamageProcess();

            if (isActive && _stepAndDayManager._step == Step.End)
            {
                isActive = false;
                isActiveEnd = false;
            }

            if (_stepAndDayManager._choiceCard != gameObject)
            {
                activeButton.SetActive(false);
            }

            GameObject choiceCard = StepAndDayManager.instance._choiceCard;
            if (choiceCard != null && choiceCard.GetComponent<CardEffect>().isActive && !choiceCard.GetComponent<CardEffect>().isActiveEnd && _cardController._Type == "EE")
            {
                oldColorChange = true;
                if (choiceCard.GetComponent<CardEffect>().isEffectToP1 && gameObject.tag == "P1" && _cardController._zone != "" && !_cardController._isEuqip)
                {
                    CardColorChange(1.3f);
                    thisCardActiveEffect = true;
                }
                if (choiceCard.GetComponent<CardEffect>().isEffectToP2 && gameObject.tag == "P2" && _cardController._zone != "" && !_cardController._isEuqip)
                {
                    CardColorChange(1.3f);
                    thisCardActiveEffect = true;
                }
                if (choiceCard.GetComponent<CardEffect>().isEffectToEqP1 && gameObject.tag == "P1" && _cardController._zone != "" && _cardController._isEuqip)
                {
                    CardColorChange(1.3f);
                    thisCardActiveEffect = true;
                }
                if (choiceCard.GetComponent<CardEffect>().isEffectToEqP2 && gameObject.tag == "P2" && _cardController._zone != "" && _cardController._isEuqip)
                {
                    CardColorChange(1.3f);
                    thisCardActiveEffect = true;
                }
            }
            if (oldColorChange && _activeEffectManager._choiceCardActived != null)
            {
                oldColorChange = false;
                GetComponent<Image>().color = Color.white;
                CardColorChange(1);
            }

            OPofCardActiveEffect();

            if (oldHaveActiveEff && _stepAndDayManager._step == Step.Draw && !haveActiveEff)
            {
                haveActiveEff = true;
            }
        }

        void OPofCardActiveEffect()
        {
            if (_inGameManager._P2._activeAndActived[0] != "" && !_activeEffectManager.isActiving)
            {
                if (_inGameManager._P2._isActiveItem && !_activeEffectManager._isCreatedPrefab)
                {
                    _activeEffectManager._isCreatedPrefab = true;
                    GameObject itemCard = Instantiate(_prefabManager.FindCardPrefab(_inGameManager._P2._activeAndActived[0]), Vector3.zero, Quaternion.identity);
                    itemCard.tag = "P2";
                    //CreateAndDestroyCardManager.instance.DestroyChil(_inGameManager.gameObject);
                    itemCard.transform.SetParent(_inGameManager.transform);
                    _stepAndDayManager._choiceCard = itemCard;
                }
                else
                {
                    string zone1 = _inGameManager._P2._activeAndActived[0].Substring(0, 3) + "P2";
                    if (zone1 == _cardController._zone && gameObject.tag == "P2" && _cardController._zone != "")
                    {
                        _stepAndDayManager._choiceCard = gameObject;
                        thisCardActiveEffect = true;
                        CardColorChange(1.3f);
                    }
                }
               
            }
            if (_inGameManager._P2._activeAndActived[1] != "" && !_activeEffectManager.isActiving)
            {
                if (_inGameManager._P2._activeAndActived[1].Substring(3, 2) == "P1")
                {
                    string zone1 = _inGameManager._P2._activeAndActived[1].Substring(0, 3) + "P2";
                    if (zone1 == _cardController._zone && gameObject.tag == "P2" && _cardController._zone != "")
                    {
                        _activeEffectManager._choiceCardActived = gameObject;
                        thisCardActiveTargetEffect = true;
                        CardColorChange(1.3f);
                    }
                }
                else if (_inGameManager._P2._activeAndActived[1].Substring(3, 2) == "P2")
                {
                    string zone2 = _inGameManager._P2._activeAndActived[1].Substring(0, 3) + "P1";
                    if (zone2 == _cardController._zone && gameObject.tag == "P1" && _cardController._zone != "")
                    {
                        _activeEffectManager._choiceCardActived = gameObject;
                        thisCardActiveTargetEffect = true;
                        CardColorChange(1.3f);
                    }
                }
                else if (_inGameManager._P2._activeAndActived[1] == "ItemP1" && _stepAndDayManager._choiceCard != null)
                {
                    _activeEffectManager._choiceCardActived = _stepAndDayManager._choiceCard;
                    CardColorChange(1.3f);
                }
                else if (_inGameManager._P2._activeAndActived[1] == "Magnus1P1")
                {
                    if (GetComponent<CardMagnusController>() != null && GetComponent<CardMagnusController>()._magnusZone == MagnusZone.Magnus1P2)
                    {
                        _activeEffectManager._choiceCardActived = gameObject;
                        CardColorChange(1.3f);
                    }
                }
                else if (_inGameManager._P2._activeAndActived[1] == "Magnus2P1")
                {
                    if (GetComponent<CardMagnusController>() != null && GetComponent<CardMagnusController>()._magnusZone == MagnusZone.Magnus2P2)
                    {
                        _activeEffectManager._choiceCardActived = gameObject;
                        CardColorChange(1.3f);
                    }
                }
            }
        }

        public void OnClick()
        {
            if (_stepAndDayManager._step == Step.Preparation && gameObject.tag == "P1" && !isActive && !_cardController._isGY &&
                ((_cardController._zone != "" && haveActiveEff) || (_cardController._Type == "Item" && !_inGameManager._P2._opCannotActiveItem) || _cardController._Type == "Magnus" && !isMagnusActive) && !_activeEffectManager._isClickeedActiveButton)
            {
                if (_stepAndDayManager._choiceCard != null)
                {
                    _stepAndDayManager._choiceCard.GetComponent<Image>().color = Color.white;
                    _stepAndDayManager._choiceCard.GetComponent<CardEffect>().CardColorChange(1f);
                }

                _activeEffectManager._isClickeedActiveButton = true;
                _stepAndDayManager._choiceCard = gameObject;
                GetComponent<Image>().color = Color.yellow;
                activeButton.GetComponent<Image>().color = Color.white;
                activeButton.SetActive(true);
                activeButton.transform.position = _inGameManager.activeButtonEffPosition.position;
                activeButton.transform.SetParent(_inGameManager.activeButtonEffPosition);
            }

            //Œø‰Ê”­“®’†
            GameObject choiceCard = StepAndDayManager.instance._choiceCard;
            if (choiceCard != null && choiceCard.GetComponent<CardEffect>().isActive && !choiceCard.GetComponent<CardEffect>().isActiveEnd)
            {
                if (choiceCard.GetComponent<CardController>()._zone == _cardController.EZ.GetComponentInChildren<CardController>()._zone)
                {
                    thisCardActiveTargetEffect = true;
                    StartCoroutine(EffectActivePross1());
                }
                if (choiceCard.GetComponent<CardEffect>().isEffectToP1 && !_cardController._isEuqip && gameObject.tag == "P1" && choiceCard.GetComponent<CardEffect>().ActivedEffectPross != null)
                {
                    thisCardActiveTargetEffect = true;
                    StartCoroutine(EffectActivePross1());
                }
                if (choiceCard.GetComponent<CardEffect>().isEffectToP2 && !_cardController._isEuqip && gameObject.tag == "P2" && choiceCard.GetComponent<CardEffect>().ActivedEffectPross != null)
                {
                    thisCardActiveTargetEffect = true;
                    StartCoroutine(EffectActivePross1());
                }
                if (choiceCard.GetComponent<CardEffect>().isEffectToEqP1 && _cardController._isEuqip && gameObject.tag == "P1" && choiceCard.GetComponent<CardEffect>().ActivedEffectPross != null)
                {
                    thisCardActiveTargetEffect = true;
                    StartCoroutine(EffectActivePross1());
                }
                if (choiceCard.GetComponent<CardEffect>().isEffectToEqP2 && _cardController._isEuqip && gameObject.tag == "P2" && choiceCard.GetComponent<CardEffect>().ActivedEffectPross != null)
                {
                    thisCardActiveTargetEffect = true;
                    StartCoroutine(EffectActivePross1());
                }
            }
        }
        IEnumerator EffectActivePross1()
        {
            GameObject choiceCard = StepAndDayManager.instance._choiceCard;
            if (_cardController._Type == "Magnus" && isEffectToSelf)
            {
                if (GetComponent<CardMagnusController>()._magnusZone == MagnusZone.Magnus1P1)
                {
                    _inGameManager._P1.AddActiveAndActived("Magnus1P1", false);
                }
                else
                {
                    _inGameManager._P1.AddActiveAndActived("Magnus2P1", false);
                }
            }
            else if(_cardController._Type == "Item" && isEffectToSelf)
            {
                _inGameManager._P1.AddActiveAndActived("ItemP1", false);
            }
            else
            {
                _inGameManager._P1.AddActiveAndActived(_cardController._zone, false);
            }
            yield return StartCoroutine(_activeEffectManager.EffectActivePross2(choiceCard, gameObject));
            choiceCard.GetComponent<CardEffect>().CardColorChange(1f);
            StepAndDayManager.instance._choiceCard = null;
            _inGameManager._P1.RemoveActiveAndActived();
            GetComponent<Image>().color = Color.white;
            CardColorChange(1f);
            _activeEffectManager._isClickeedActiveButton = false;
            _inGameManager._P1.isRandNumber = false;
            _inGameManager._P1._randNumber = 0;
        }
   

        public void CardColorChange(float value)
        {
            Color color = GetComponent<Image>().color;
            color.a = value;
            GetComponent<Image>().color = color;
        }

        public void OnCLickActiveButton()
        {
            if (ActiveEffectPross != null && ((_cardController._Type == "EE" && haveActiveEff) || (_cardController._Type == "Item") || (_cardController._Type == "Magnus")))
            {
                if (CheckConditionspross() && ((isEffectToP1 || isEffectToP2 || isEffectToEqP1 || isEffectToEqP2 || isEffectToSelf) || _cardController._isEuqip)) 
                {
                    _activeEffectManager._activeCard.Add(GetComponent<CardEffect>());
                }
                else
                {
                    Debug.LogWarning(CheckConditionspross() + "  " + isEffectToP1 + "  " + isEffectToP2 + "  " + isEffectToEqP1 + "  " + isEffectToEqP2);
                    activeButton.GetComponent<CardEffect>().thisCardActiveTargetEffect = true;
                }
            }
        }
        public void EffectActiveProccess()
        {
            ActiveEffectPross();
            activeButton.SetActive(false);
            isActive = true;
            if (_cardController._Type == "Item" || _cardController._Type == "Magnus")
            {
                _inGameManager._P1._isActiveItem = true;
                _inGameManager._P1.AddActiveAndActived(_cardController._name, true);
                if (_cardController._Type == "Magnus")
                {
                    if (GetComponent<CardMagnusController>()._magnusZone == MagnusZone.Magnus1P1)
                    {
                        _inGameManager._P1._MagnusActive[0] = true;
                    }
                    if (GetComponent<CardMagnusController>()._magnusZone == MagnusZone.Magnus2P1)
                    {
                        _inGameManager._P1._MagnusActive[1] = true;
                    }
                }
            }
            else
            {
                _inGameManager._P1._isActiveItem = false;
                _inGameManager._P1.AddActiveAndActived(_cardController._zone, true);
            }
            if (_cardController._isEuqip)
            {
                GameObject cardMZ = _cardController.MZ.GetComponentInChildren<CardController>().gameObject;
                cardMZ.GetComponent<CardEffect>().thisCardActiveTargetEffect = true;
                StartCoroutine(cardMZ.GetComponent<CardEffect>().EffectActivePross1());
            }
            if (isEffectToSelf)
            {
                StartCoroutine(EffectActivePross1());
            }
            thisCardActiveEffect = true;
        }

        void BurnDamageProcess()
        {
            if (((gameObject.tag == "P1" && _stepAndDayManager._step == Step.Preparation) || (gameObject.tag == "P2" && _inGameManager._P2._playerStep == "Preparation")) && _isBurn && !_getBurn)
            {
                _getBurn = true;
                _battleStepCardController.GetDame(_BurnDamage, _cardController);
            }
            if (((gameObject.tag == "P1" && _stepAndDayManager._step == Step.End) || (gameObject.tag == "P2" && _inGameManager._P2._playerStep == "End")) && _stepAndDayManager._step == Step.End && _getBurn)
            {
                _getBurn = false;
            }
        }
        void PoisonDamageProcess()
        {
            if (((gameObject.tag == "P1" && _stepAndDayManager._step == Step.Preparation) || (gameObject.tag == "P2" && _inGameManager._P2._playerStep == "Preparation")) && _stepAndDayManager._step == Step.Preparation && _isPoison && !_getPoison)
            {
                _getPoison = true;
                _battleStepCardController.GetDameToHP(_PoisonDamage, _cardController);
            }
            if (((gameObject.tag == "P1" && _stepAndDayManager._step == Step.End) || (gameObject.tag == "P2" && _inGameManager._P2._playerStep == "End")) && _stepAndDayManager._step == Step.End && _getPoison)
            {
                _getPoison = false;
            }
        }

        void CardEffectActiveAnimation()
        {
            if (isActiveAnimationEffectLogo && time < 3)
            {
                time += Time.deltaTime;

                float currentAngle_1 = ActiveEffectLogo_1.GetComponent<RectTransform>().localRotation.eulerAngles.z;
                float currentAngle_2 = ActiveEffectLogo_2.GetComponent<RectTransform>().localRotation.eulerAngles.z;

                currentAngle_1 += Time.deltaTime * 20;
                ActiveEffectLogo_1.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, currentAngle_1);
                currentAngle_2 -= Time.deltaTime * 20;
                ActiveEffectLogo_2.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, currentAngle_2);
            }

            if (isActiveAnimationEffectLogo && time >= 3)
            {
                thisCardActiveEffect = false;
                thisCardActiveTargetEffect = false;
                isActiveAnimationEffectLogo = false;
                time = 0;
            }

            if (thisCardActiveEffect && !isActiveAnimationEffectLogo)
            {
                ActiveEffectLogo_1.color = Color.yellow;
                ActiveEffectLogo_2.color = Color.yellow;
                ActiveEffectLogo_1.gameObject.SetActive(true);
                ActiveEffectLogo_2.gameObject.SetActive(true);
                isActiveAnimationEffectLogo = true;
                time = 0;
            }
            else if (thisCardActiveTargetEffect && !isActiveAnimationEffectLogo) 
            {
                ActiveEffectLogo_1.color = Color.white;
                ActiveEffectLogo_2.color = Color.white;
                ActiveEffectLogo_1.gameObject.SetActive(true);
                ActiveEffectLogo_2.gameObject.SetActive(true);
                isActiveAnimationEffectLogo = true;
                time = 0;
            }


            if (!thisCardActiveEffect && !thisCardActiveTargetEffect)
            {
                ActiveEffectLogo_1.gameObject.SetActive(false);
                ActiveEffectLogo_2.gameObject.SetActive(false);
                ActiveEffectLogo_1.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, 0);
                ActiveEffectLogo_2.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, 0);
                ActiveEffectLogo_1.color = Color.white;
                ActiveEffectLogo_2.color = Color.white;
            }
        }
    }

}