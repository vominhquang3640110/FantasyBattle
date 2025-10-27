using InGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame
{
    public class NightBlade : MonoBehaviour
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

        public bool getEffect = false;
        public bool getCost = false;
        // Start is called before the first frame update
        void Start()
        {
            _cardEffectText = "ÅsActÅtReduces DEF To 0, Increases ATK By The Amount Of Reduced DEF.";

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

        }

        public bool CheckConditions()
        {
            if (_cardController.MZ != null && gameObject.tag == "P1" && _cardController.MZ.GetComponentInChildren<CardController>()._DEF > 0)
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
            
        }

        public void ActivedEff(CardController cardController)
        {
            int buffATK = cardController._DEF;
            cardController._ATK += cardController._DEF;
            cardController._DEF = 0;
            if (gameObject.tag == "P1")
            {
                _inGameManager._P1._message = "Åy" + _stepAndDayManager._turn + "Åz" + _cardController._name + ": Buff " + buffATK + "ATK to Åu" + cardController._name + "Åv";
            }
        }
    }
}

