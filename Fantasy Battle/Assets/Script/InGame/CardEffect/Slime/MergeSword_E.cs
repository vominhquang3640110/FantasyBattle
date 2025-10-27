using InGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame
{
    public class MergeSword_E : MonoBehaviour
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

        // Start is called before the first frame update
        void Start()
        {
            _cardEffectText = "ÅsActÅtÅy3MPÅzChange DEF To ATK, And Vice Versa.";

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
            CardController card = _cardController.MZ.GetComponentInChildren<CardController>();
            int ATK = card._ATK;
            int DEF = card._DEF;
            card._DEF = ATK;
            card._ATK = DEF;
            if (gameObject.tag == "P1")
            {
                string _message = "Active effect!!";
                _inGameManager._P1._message = "Åy" + _stepAndDayManager._turn + "Åz" + _cardController._name + ": " + _message;
            }
        }
    }
}

