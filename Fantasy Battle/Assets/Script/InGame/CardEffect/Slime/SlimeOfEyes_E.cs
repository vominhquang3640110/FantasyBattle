using InGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame
{
    public class SlimeOfEyes_E : MonoBehaviour
    {
        [Header("Manager And Controller")]
        public CardController _cardController;
        private PlayerFiledManager _playerFiledManager;
        private StepAndDayManager _stepAndDayManager;
        private BattleManager _battleManager;
        private string _cardEffectText;

        [Header("Bool")]
        bool _getEffect = false;

        // Start is called before the first frame update
        void Start()
        {
            _cardEffectText = "ÅsPassiveÅtWhen Attacked, Damage Taken Reduces By 2.";

            _playerFiledManager = PlayerFiledManager.instance;
            _stepAndDayManager = StepAndDayManager.instance;
            _battleManager = BattleManager.instance;

            _cardController._cardEffectText = _cardEffectText;
        }

        // Update is called once per frame
        void Update()
        {
            if (_cardController._zone != "" && _cardController.MZ != null && !_getEffect)
            {
                _getEffect = true;
                _cardController.MZ.GetComponentInChildren<BattleStepCardController>()._downDamage += 2;
            }
        }
    }
}