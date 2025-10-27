using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame
{
    public class ShiledSlime : MonoBehaviour
    {
        [Header("Manager And Controller")]
        public CardController _cardController;
        public BattleStepCardController _battleStepCardController;
        public CardEffect _cardEffect;
        private PlayerFiledManager _playerFiledManager;
        private StepAndDayManager _stepAndDayManager;
        private BattleManager _battleManager;
        private string _cardEffectText;

        [Header("Int")]
        int _soulOfFieldNumber = 0;

        [Header("Bool")]
        bool _getEffect = false;

        // Start is called before the first frame update
        void Start()
        {
            _cardEffectText = "sPassivetWhen An Ally is Destroyed, Increases DEF And HP By 2.";

            _playerFiledManager = PlayerFiledManager.instance;
            _stepAndDayManager = StepAndDayManager.instance;
            _battleManager = BattleManager.instance;

            _cardController._cardEffectText = _cardEffectText;
        }

        // Update is called once per frame
        void Update()
        {
            //P1
            if (_cardController._zone != "" && _playerFiledManager.CheckCharOfField("P1") > _soulOfFieldNumber && gameObject.tag == "P1")
            {
                _soulOfFieldNumber = _playerFiledManager.CheckCharOfField("P1");
            }
            if (_cardController._zone != "" && _playerFiledManager.CheckCharOfField("P1") < _soulOfFieldNumber && gameObject.tag == "P1")
            {
                int value = _soulOfFieldNumber - _playerFiledManager.CheckCharOfField("P1");
                _soulOfFieldNumber = _playerFiledManager.CheckCharOfField("P1");
                _cardController._HP += (2 * value);
                _cardController._DEF += (2 * value);
            }

            //P2
            if (_cardController._zone != "" && _playerFiledManager.CheckCharOfField("P2") > _soulOfFieldNumber && gameObject.tag == "P2")
            {
                _soulOfFieldNumber = _playerFiledManager.CheckCharOfField("P2");
            }
            if (_cardController._zone != "" && _playerFiledManager.CheckCharOfField("P2") < _soulOfFieldNumber && gameObject.tag == "P2")
            {
                int value = _soulOfFieldNumber - _playerFiledManager.CheckCharOfField("P2");
                _soulOfFieldNumber = _playerFiledManager.CheckCharOfField("P2");
                _cardController._HP += (2 * value);
                _cardController._DEF += (2 * value);
            }
        }
    }

}