using InGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame
{
    public class IceSlime : MonoBehaviour
    {
        [Header("Manager And Controller")]
        private string _message = "Op cannot active item to end step of self";
        public CardController _cardController;
        public BattleStepCardController _battleStepCardController;
        public CardEffect _cardEffect;
        private InGameManager _inGameManager;
        private PlayerFiledManager _playerFiledManager;
        private StepAndDayManager _stepAndDayManager;
        private BattleManager _battleManager;
        private DeckManager _deckManager;
        private string _cardEffectText;

        private bool _getEffect = false;
        private int _activeTurn = 0;

        // Start is called before the first frame update
        void Start()
        {
            _cardEffectText = "ÅsAct/1TurnÅtÅy3MPÅzThe Opponent Cannot Use The \"Item\" Cards Until The End. Of The Turn.";

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
            if (_getEffect && _activeTurn != _stepAndDayManager._turn)
            {
                _getEffect = false;
            }
        }

        public bool CheckConditions()
        {
            if (_cardController._MP >= 3)
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
            _cardController._MP -= 3;
        }

        public void ActivedEff(CardController cardController)
        {
            if (!_getEffect)
            {
                _getEffect = true;
                _activeTurn = _stepAndDayManager._turn;
                if (gameObject.tag == "P1")
                {
                    _inGameManager._P1._opCannotActiveItem = true;
                    _inGameManager._P1._message = "Åy" + _stepAndDayManager._turn + "Åz" + _cardController._name + ": " + _message;
                }
            }
        }
    }

}