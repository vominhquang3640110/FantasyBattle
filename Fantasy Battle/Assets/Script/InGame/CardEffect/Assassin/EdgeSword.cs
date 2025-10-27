using InGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame
{
    public class EdgeSword : MonoBehaviour
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

        private string _message = "Give \'can 2 times attck\' to";

        public bool getEffect = false;
        public bool getCost = false;
        // Start is called before the first frame update
        void Start()
        {
            _cardEffectText = "《Eternal》Reduces ATK By Half.\r\n《Act》Reduced 2 MP By 2 To Attack 2 Times.";

            _inGameManager = InGameManager.instance;
            _playerFiledManager = PlayerFiledManager.instance;
            _stepAndDayManager = StepAndDayManager.instance;
            _battleManager = BattleManager.instance;

            _cardEffect.Eternal = EternalPross;
            _cardEffect.CheckConditionspross = CheckConditions;
            _cardEffect.ActiveEffectPross = ActiveEff;
            _cardEffect.ActivedEffectPross = ActivedEff;
            _cardController._cardEffectText = _cardEffectText;
        }

        // Update is called once per frame
        void Update()
        {
            //if (!getCost && _cardController.MZ != null && _cardController.MZ.GetComponent<CardController>()._Type != "Start")
            //{
            //    getCost = true;
            //    CardController card = _cardController.MZ.GetComponentInChildren<CardController>();
            //    int value = Mathf.CeilToInt(card._ATK / 2);
            //    card._ATK -= value;
            //}
        }

        void EternalPross()
        {
            CardController card = _cardController.MZ.GetComponentInChildren<CardController>();
            int value = Mathf.CeilToInt(card._ATK / 2);
            card._ATK -= value;
        }

        public bool CheckConditions()
        {
            if (_cardController.MZ != null && gameObject.tag == "P1" && _cardController.MZ.GetComponentInChildren<CardController>()._MP >= 2)
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
            _cardController.MZ.GetComponentInChildren<CardController>()._MP -= 2;
        }

        public void ActivedEff(CardController cardController)
        {
            cardController.GetComponent<BattleStepCardController>()._canAttackTimesMax = 2;
            if (gameObject.tag == "P1")
            {
                _inGameManager._P1._message = "【" + _stepAndDayManager._turn + "】" + _cardController._name + ": " + _message + " 「" + cardController._name + "」";
            }
        }
    }
}

