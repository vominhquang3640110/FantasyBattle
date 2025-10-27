using InGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame
{
    public class AssassinMain : MonoBehaviour
    {
        [Header("Manager And Controller")]
        private string _message = "Both Player draw from EE Deck 1 card";
        public CardController _cardController;
        public BattleStepCardController _battleStepCardController;
        public CardEffect _cardEffect;
        private InGameManager _inGameManager;
        private PlayerFiledManager _playerFiledManager;
        private StepAndDayManager _stepAndDayManager;
        private BattleManager _battleManager;
        private DeckManager _deckManager;
        private string _cardEffectText;

        private bool _isActived = false;

        // Start is called before the first frame update
        void Start()
        {
            _cardEffectText = "ÅsPassiveÅtÅy1MP ÅzEvery Draw Phase, Each Player Draw 1 Card.";

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
            //é©ï™ÇÃÉJÅ[ÉhÇÃèÍçá
            if (_inGameManager._P1._isTurn && _inGameManager._P1._playerStep == "Draw" && !_isActived && CheckConditions() && gameObject.tag == "P1" && _cardController._zone != "")
            {
                _isActived = true;
                ActiveEffectManager.instance._activeCard.Add(_cardEffect);
            }
            //ëäéËÇÃÉJÅ[ÉhÇÃèÍçá
            if (_inGameManager._P2._isTurn && _inGameManager._P2._playerStep == "Draw" && !_isActived && CheckConditions() && gameObject.tag == "P2" && _cardController._zone != "")
            {
                _isActived = true;
            }

            if (_stepAndDayManager._step == Step.End && _isActived)
            {
                _isActived = false;
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
            _cardController._MP -= 1;
        }

        public void ActivedEff(CardController cardController)
        {
            
            _deckManager.Draw(1);
            _inGameManager._P1._message = "Åy" + _stepAndDayManager._turn + "Åz" + _cardController._name + ": " + _message;
        }
    }

}