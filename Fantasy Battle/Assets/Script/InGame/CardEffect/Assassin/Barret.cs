using InGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static InGame.CardEffect;

namespace InGame
{
    public class Barret : MonoBehaviour
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

        //public bool getEffect = false;

        // Start is called before the first frame update
        void Start()
        {
            _cardEffectText = "《Eternal》When Equipped To \"Sniper\" +2MP.\r\n《Act》When Equipped To \"Sniper\" Effect x2.";

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
            //if (!getEffect && _cardController.MZ != null && _cardController.MZ.GetComponentInChildren<CardController>()._name == "Sniper")
            //{
            //    getEffect = true;
            //    _cardController.MZ.GetComponentInChildren<CardController>()._MP += 2;
            //    _cardController.MZ.GetComponentInChildren<Sniper>()._priveDamage *= 2;
            //}
        }

        void EternalPross()
        {
            if (_cardController.MZ.GetComponentInChildren<CardController>()._name == "Sniper")
            {
                _cardController.MZ.GetComponentInChildren<CardController>()._MP += 2;
                _cardController.MZ.GetComponentInChildren<Sniper>()._priveDamage *= 2;
            }
        }

        public bool CheckConditions()
        {
            return true;
        }

        public void ActiveEff()
        {
            
        }

        public void ActivedEff(CardController cardController)
        {
           
        }
    }
}

