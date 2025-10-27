using InGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame
{
    public class BladeDance : MonoBehaviour
    {
        [Header("Manager And Controller")]
        public CardController _cardController;
        public BattleStepCardController _battleStepCardController;
        public CardEffect _cardEffect;
        private InGameManager _inGameManager;
        private PlayerFiledManager _playerFiledManager;
        private StepAndDayManager _stepAndDayManager;
        private BattleManager _battleManager;
        private string _cardEffectText;

        private string _message = "Get Armor Piercing Effect To";

        public bool getEffect = false;
        public bool getCost = false;
        // Start is called before the first frame update
        void Start()
        {
            _cardEffectText = "ÅsActÅtGet Armor Piercing Effect. When End Battle Phase HP Down To 0.";

            _inGameManager = InGameManager.instance;
            _playerFiledManager = PlayerFiledManager.instance;
            _stepAndDayManager = StepAndDayManager.instance;
            _battleManager = BattleManager.instance;

            _cardEffect.CheckConditionspross = CheckConditions;
            _cardEffect.ActiveEffectPross = ActiveEff;
            _cardEffect.ActivedEffectPross = ActivedEff;
            _cardController._cardEffectText = _cardEffectText;
        }

        // Update is called once per frame
        void Update()
        {
            if (!getCost && getEffect && _stepAndDayManager._preparationEnd && _stepAndDayManager._battleEnd && _stepAndDayManager._endEnd)
            {
                getCost = true;
                CardController card = _cardController.MZ.GetComponentInChildren<CardController>();
                card._HP = 0;
            }
        }

        public bool CheckConditions()
        {
            if (_cardController.MZ != null && gameObject.tag == "P1" && _cardController.MZ.GetComponentInChildren<CardController>()._MP >= 3)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void ActiveEff()
        {
            _cardController.MZ.GetComponentInChildren<CardController>()._MP -= 3;
        }

        public void ActivedEff(CardController cardController)
        {
            if (!getEffect)
            {
                getEffect = true;
                cardController.GetComponent<BattleStepCardController>()._penetration = true;
                if (gameObject.tag == "P1")
                {
                    _inGameManager._P1._message = "Åy" + _stepAndDayManager._turn + "Åz" + _cardController._name + ": " + _message + " Åu" + cardController._name + "Åv";
                }
            }
        }
    }
}

