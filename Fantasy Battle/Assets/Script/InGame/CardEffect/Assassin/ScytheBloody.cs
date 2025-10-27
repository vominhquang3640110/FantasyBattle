using InGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame
{
    public class ScytheBloody : MonoBehaviour
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
        public bool active = false;
        public bool getCost = false;

        int _giveDame = 0;
        // Start is called before the first frame update
        void Start()
        {
            _cardEffectText = "ÅsPassiveÅtWhen Attacked, Reduce HP By Half, To Counter Dame With The Amount Of HP Lost.";

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
            if (!getEffect && _cardController.MZ != null)
            {
                getEffect = true;
                BattleStepCardController card = _cardController.MZ.GetComponentInChildren<BattleStepCardController>();
                card._counterDameFromEquip = true;
            }
            if (((gameObject.tag == "P2" && _inGameManager._P2._playerStep == "Draw") ||(gameObject.tag == "P1" && _stepAndDayManager._step == Step.Draw)) && _giveDame != 0)
            {
                _giveDame = 0;
                active = false;
            }
        }

        public bool CheckConditions()
        {
            if (_cardController.MZ != null && _cardController.MZ.GetComponentInChildren<CardController>()._HP > 1)
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
            if (!active)
            {
                active = true;
                _giveDame = Mathf.CeilToInt(_cardController.MZ.GetComponentInChildren<CardController>()._HP/2);
                Debug.LogWarning(_giveDame);
                _cardController.MZ.GetComponentInChildren<CardController>()._HP -= _giveDame;
                _battleStepCardController.GetDame(_giveDame, cardController);
                if (_inGameManager._P2._isTurn)
                {
                    _inGameManager._P1._message = "Åy" + _stepAndDayManager._turn + "Åz" + " Åu" + cardController._name + "Åv" + " Took " + _giveDame + " Damage by " + _cardController._name;
                }
            }
        }
    }
}

