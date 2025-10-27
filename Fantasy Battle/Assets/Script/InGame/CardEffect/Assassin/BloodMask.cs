using InGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame
{
    public class BloodMask : MonoBehaviour
    {
        [Header("Manager And Controller")]
        public CardController _cardController;
        public BattleStepCardController _battleStepCardController;
        public CardEffect _cardEffect;
        private InGameManager _inGameManager;
        private PlayerFiledManager _playerFiledManager;
        private StepAndDayManager _stepAndDayManager;
        private BattleManager _battleManager;
        private DeckManager _deckManager;
        private string _cardEffectText;

        [Header("Bool")]
        int _giveDame = 0;
        bool getEffect = false;

        // Start is called before the first frame update
        void Start()
        {
            _cardEffectText = "ÅsPassiveÅtWhen Attacked, Consumes All Available MP, Subtract The HP Of The ÅgSoulsÅh That Attacked This Card By The Amount Of MP Consumed.";

            _inGameManager = InGameManager.instance;
            _playerFiledManager = PlayerFiledManager.instance;
            _stepAndDayManager = StepAndDayManager.instance;
            _battleManager = BattleManager.instance;
            _deckManager = DeckManager.instance;

            _cardEffect.CheckConditionspross = CheckConditions;
            _cardEffect.ActiveEffectPross = ActiveEff;
            _cardEffect.ActivedEffectPross = ActivedEff;
            _cardController._cardEffectText = _cardEffectText;
        }

        // Update is called once per frame
        void Update()
        {
            if (((gameObject.tag == "P2" && _inGameManager._P2._playerStep == "Draw") || (gameObject.tag == "P1" && _stepAndDayManager._step == Step.Draw)) && _giveDame != 0)
            {
                _giveDame = 0;
                getEffect = false;
            }
        }

        public bool CheckConditions()
        {
            if (_cardController._MP >= 1)
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
            _giveDame = _cardController._MP;
            _cardController._MP = 0;
        }

        public void ActivedEff(CardController cardController)
        {
            if (!getEffect)
            {
                getEffect = true;
                _battleStepCardController.GetDame(_giveDame, cardController);
                if (_inGameManager._P2._isTurn)
                {
                    _inGameManager._P1._message = "Åy" + _stepAndDayManager._turn + "Åz" + " Åu" + cardController._name + "Åv" + " Took " + _giveDame + " Damage by " + _cardController._name;
                }
            }
        }
    }

}