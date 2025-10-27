using InGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame
{
    public class AxitSlime : MonoBehaviour
    {
        [Header("Manager And Controller")]
        public CardController _cardController;
        public BattleStepCardController _battleStepCardController;
        public CardEffect _cardEffect;
        private PlayerFiledManager _playerFiledManager;
        private StepAndDayManager _stepAndDayManager;
        private BattleManager _battleManager;
        private string _cardEffectText;

        [Header("Bool")]
        bool _getEffect = false;

        // Start is called before the first frame update
        void Start()
        {
            _cardEffectText = "ÅsPassiveÅtÅy2MPÅzWhen Attacking, The Attacked \"Soul\" Will Be Poisoned.";

            _playerFiledManager = PlayerFiledManager.instance;
            _stepAndDayManager = StepAndDayManager.instance;
            _battleManager = BattleManager.instance;

            _cardEffect.CheckConditionspross = CheckConditions;
            _cardController._cardEffectText = _cardEffectText;
        }

        // Update is called once per frame
        void Update()
        {
            if (_cardController._zone != "" && !_battleStepCardController._givePoisonByAttack && !_getEffect)
            {
                _getEffect = true;
                _battleStepCardController._givePoisonByAttack = true;
            }
        }

        public bool CheckConditions()
        {
            //List<GameObject> souCardP1 = PlayerFiledManager.instance.GetP1SoulCardList();
            //int souCardP2 = PlayerFiledManager.instance.CheckCharOfField("P2");
            if (_cardController._MP >= 2)
            {
                _cardController._MP -= 2;
                if (gameObject.tag == "P1")
                {
                    string _message = "Active effect!!";
                    InGameManager.instance._P1._message = "Åy" + _stepAndDayManager._turn + "Åz" + _cardController._name + ": " + _message;
                }
                return true;
            }
            else
            {
                return false;
            }
        }
    }

}