using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace InGame
{
    public enum FrontBack
    {
        Front,
        Back
    }
    public class CardController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [Header("Card Information")]
        public Sprite backCardImage;
        public Sprite backCardImageM;
        public Sprite frontCardImage = default;
        public bool noShow = false;
        public FrontBack frontBack;
        public string _name;
        public int _cost;
        public string _Type;
        public bool _isEuqip;

        public int _HPOG;
        public int _ATKOG;
        public int _DEFOG;
        public int _MPOG;
        public int _HP;
        public int _ATK;
        public int _DEF;
        public int _MP;

        public string _cardEffectText;

        public int _destroySoulDamage = 2;

        [Header("Field")]
        public string _zone = "";

        public GameObject EZ = null;
        public GameObject MZ = null;

        public bool _isGY = false;
        bool _isChoice = false;

        public bool _isTouch = false;

        void Start()
        {
            Color color = GetComponent<Image>().color;
            color.a = 1f;
            GetComponent<Image>().color = color;
        }

        void Update()
        {
            if (_HP < 0)
            {
                _HP = 0;
            }
            if (_ATK < 0)
            {
                _ATK = 0;
            }
            if (_DEF < 0)
            {
                _DEF = 0;
            }
            if (_MP < 0)
            {
                _MP = 0;
            }

            //カードタッチアニメーション処理
            if (_isTouch)
            {
                AnimationManager.instance.TouchAnimation(GetComponent<RectTransform>());
            }
            else
            {
                AnimationManager.instance.UnTouchAnimation(GetComponent<RectTransform>());
            }

            //ドローステップにアンタップ
            if ((InGameManager.instance._P1._playerStep == "Draw" && gameObject.tag == "P1") || (InGameManager.instance._P2._playerStep == "Draw" && gameObject.tag == "P2"))
            {
                if (_isTouch)
                {
                    _isTouch = false;
                }
            }

            if (StepAndDayManager.instance._choiceCard == gameObject)
            {
                Color color = GetComponent<Image>().color;
                color.a = 1.3f;
                GetComponent<Image>().color = color;
            }

            if (!_isChoice && (StepAndDayManager.instance._step == Step.Preparation || StepAndDayManager.instance._step == Step.Battle))
            {
                NoChoice();
            }

            //カードが破壊される処理
            CardDestroyByHP0();
        }

        public void CardDestroyByHP0()
        {
            if (gameObject.tag == "P1" && _zone != "" && !_isGY && !_isEuqip && _HP <= 0 && _name != InGameManager.instance._P1._startCard && StepAndDayManager.instance._step != Step.Battle && InGameManager.instance._P2._playerStep != "Battle")
            {
                int index = 0;
                if (EZ != null && EZ.transform.childCount >= 1 && EZ.GetComponentInChildren<CardController>()._Type != "Start")
                {
                    //GYに装備カードを追加
                    index = PlayerFiledManager.instance.NowIndexGYList(InGameManager.instance._P1._gy);
                    InGameManager.instance._P1._gy[index] = EZ.GetComponentInChildren<CardController>()._name;
                    index = PlayerFiledManager.instance.PIFIndexFromZone(EZ.GetComponentInChildren<CardController>()._zone);
                    EZ.GetComponentInChildren<CardController>()._isGY = true;
                    EZ.GetComponentInChildren<CardController>()._zone = "";
                    EZ.GetComponentInChildren<CardController>().EZ = null;
                    EZ.GetComponentInChildren<CardController>().MZ = null;
                    EZ.GetComponentInChildren<CardController>().AbilityReset();
                    InGameManager.instance._P1._equipmentZone[index] = "EquipBasic";
                }
                AbilityReset();
                InGameManager.instance._P1.GetDame(_destroySoulDamage);
                //GYにこのカードを追加
                index = PlayerFiledManager.instance.NowIndexGYList(InGameManager.instance._P1._gy);
                InGameManager.instance._P1._gy[index] = _name;
                index = PlayerFiledManager.instance.PIFIndexFromZone(_zone);
                InGameManager.instance._P1._characterZone[index] = InGameManager.instance._P1._startCard;
                _isGY = true;
                _zone = "";
                EZ = null;
                MZ = null;
            }
        }

        void NoChoice()
        {
            //進化と装備ステップ
            if ((StepAndDayManager.instance._step == Step.Preparation || 
                (StepAndDayManager.instance._step == Step.Battle && InGameManager.instance._P1._attackAndAttacked[1] == "")) && (_Type == "EE" || _Type == "Item" || _Type == "Magnus"))
            {
                if (Input.GetMouseButtonDown(1) && StepAndDayManager.instance._choiceCard == gameObject)
                {
                    if (StepAndDayManager.instance._choiceCard == gameObject && GetComponent<CardEffect>().isActive && !GetComponent<CardEffect>().isActiveEnd)
                    {
                        return;
                    }
                    if (GetComponent<CardEffect>().activeButton.gameObject.activeSelf)
                    {
                        GetComponent<CardEffect>().activeButton.gameObject.SetActive(false);
                    }
                    StepAndDayManager.instance._choiceCard.GetComponent<Image>().color = Color.white;
                    Color color = StepAndDayManager.instance._choiceCard.GetComponent<Image>().color;
                    color.a = 1f;
                    StepAndDayManager.instance._choiceCard.GetComponent<Image>().color = color;
                    StepAndDayManager.instance._choiceCard = null;
                    BattleManager.instance._attackCard = null;
                    ActiveEffectManager.instance._isClickeedActiveButton = false;
                    if (StepAndDayManager.instance._step == Step.Battle)
                    {
                        InGameManager.instance._P1._attackAndAttacked[0] = "";
                    }
                }
            }
        }

        public void OnClick()
        {
            //進化と装備
            if (StepAndDayManager.instance._step == Step.Preparation && frontBack == FrontBack.Front && gameObject.tag == "P1" && InGameManager.instance._P1._nowCost >= _cost
                && !ActiveEffectManager.instance._isClickeedActiveButton)
            {
                GameObject choicedCard = StepAndDayManager.instance._choiceCard;
                if (choicedCard != null && choicedCard.GetComponent<CardEffect>().isActive && !choicedCard.GetComponent<CardEffect>().isActiveEnd)
                {
                    return;
                }
                if (_Type == "EE" || _Type == "Start")
                {
                    //Start Card 処理
                    if (_Type == "Start" && !_isEuqip && choicedCard != null && !choicedCard.GetComponent<CardController>()._isEuqip && choicedCard.GetComponent<CardController>()._zone == "")
                    {
                        InGameManager.instance._P1._nowCost -= choicedCard.GetComponent<CardController>()._cost;
                        int zoneIndex = int.Parse(_zone.Substring(2, 1));
                        InGameManager.instance._P1._characterZone[zoneIndex - 1] = choicedCard.GetComponent<CardController>()._name;
                        Destroy(choicedCard);
                    }
                    if (_Type == "Start" && _isEuqip && choicedCard != null && choicedCard.GetComponent<CardController>()._isEuqip && choicedCard.GetComponent<CardController>()._zone == "")
                    {
                        InGameManager.instance._P1._nowCost -= choicedCard.GetComponent<CardController>()._cost;
                        int zoneIndex = int.Parse(_zone.Substring(2, 1));
                        InGameManager.instance._P1._equipmentZone[zoneIndex - 1] = choicedCard.GetComponent<CardController>()._name;
                        Destroy(choicedCard);
                    }
                    //EE Card 処理
                    if (_Type == "EE")
                    {
                        StepAndDayManager.instance._choiceCard = gameObject;
                        GetComponent<Image>().color = Color.yellow;
                    }
                    //選択したカードのお色を戻す
                    if (choicedCard != null)
                    {
                        choicedCard.GetComponent<Image>().color = Color.white;
                        Color choicedCardColor = choicedCard.GetComponent<Image>().color;
                        choicedCardColor.a = 1f;
                        choicedCard.GetComponent<Image>().color = choicedCardColor;
                    }
                    
                }
                
            }
        }

        public void FromEquipAbilityPlus()
        {
            CardController equipCard;
            if (EZ != null)
            {
                equipCard = EZ.GetComponentsInChildren<CardController>()[EZ.transform.childCount - 1];
                _HP += equipCard._HP;
                _ATK += equipCard._ATK;
                _DEF += equipCard._DEF;
                _MP += equipCard._MP;
                CardEffect equipCardEffect = equipCard.GetComponent<CardEffect>();
                StartCoroutine(EternalIE(equipCardEffect));
                Debug.LogWarning("AAAAAAAA");
            }
        }

        IEnumerator EternalIE(CardEffect cardEffect)
        {
            yield return new WaitForSeconds(1);
            if (cardEffect.Eternal != null)
            {
                cardEffect.Eternal();
            }
        }

        public void AbilityReset()
        {
            _HP = _HPOG;
            _ATK = _ATKOG;
            _DEF = _DEFOG;
            _MP = _MPOG;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _isChoice = true;

            if (StepAndDayManager.instance._choiceCard != gameObject)
            {
                Color color = GetComponent<Image>().color;
                color.a = 1.3f;
                GetComponent<Image>().color = color;
            }
            if (!noShow)
            {
                ShowCardController.showSprite = GetComponent<Image>().sprite;
                ShowCardController._HP = _HP;
                ShowCardController._ATK = _ATK;
                ShowCardController._DEF = _DEF;
                ShowCardController._MP = _MP;
                ShowCardController._cardEffect = _cardEffectText;
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _isChoice = false;

            if (StepAndDayManager.instance._choiceCard != gameObject)
            {
                Color color = GetComponent<Image>().color;
                color.a = 1f;
                GetComponent<Image>().color = color;
            }
        
        }
    }
}