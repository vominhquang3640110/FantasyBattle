using InGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame
{
    public class TwinIceSwords_E : MonoBehaviour
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

        private bool getEffect = false; 

        // Start is called before the first frame update
        void Start()
        {
            _cardEffectText = "ÅsActÅtÅy3MPÅzWhen The Opponent Has 2 Or More ÅgSoulsÅh Equipped With This Card Attacks 2 Times.";

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
            if (getEffect && ((gameObject.tag == "P1" && _inGameManager._P1._playerStep == "Draw") || (gameObject.tag == "P2" && _inGameManager._P2._playerStep == "Draw")))
            {
                getEffect = false;
                BattleStepCardController cardBattleSTCtrll = _cardController.MZ.GetComponentInChildren<BattleStepCardController>();
                cardBattleSTCtrll._canAttackTimesMax = 1;
            }
        }

        public bool CheckConditions()
        {
            int souCardP2 = PlayerFiledManager.instance.CheckCharOfField("P2");
            if (souCardP2 >= 2 && _cardController.MZ != null && gameObject.tag == "P1" && _cardController.MZ.GetComponentInChildren<CardController>()._MP >= 3 && !getEffect)
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
                BattleStepCardController cardBattleSTCtrll = _cardController.MZ.GetComponentInChildren<BattleStepCardController>();
                cardBattleSTCtrll._canAttackTimesMax = 2;
                if (gameObject.tag == "P1")
                {
                    string _message = "Active effect!!";
                    _inGameManager._P1._message = "Åy" + _stepAndDayManager._turn + "Åz" + _cardController._name + ": " + _message;
                }
            }
        }
    }

}