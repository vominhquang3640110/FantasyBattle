using InGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame
{
    public class BloodCuffs : MonoBehaviour
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

        // Start is called before the first frame update
        void Start()
        {
            _cardEffectText = "Choose 1 Opponent's \"Soul\", It Cannot Attack In The Next Turn.";

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

        }

        public bool CheckConditions()
        {
            //List<GameObject> souCardP1 = PlayerFiledManager.instance.GetP1SoulCardList();
            int souCardP2 = PlayerFiledManager.instance.CheckCharOfField("P2");
            if (souCardP2 > 0 && gameObject.tag == "P1" && _inGameManager._P1._nowCost >= _cardController._cost)
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
            if (_inGameManager._P1._isTurn)
            {
                _inGameManager._P1._nowCost -= _cardController._cost;
            }
        }

        public void ActivedEff(CardController cardController)
        {
            cardController.GetComponent<BattleStepCardController>()._cannotAttack = true;
            if (gameObject.tag == "P1")
            {
                string _message = "Active effect!!";
                _inGameManager._P1._message = "Åy" + _stepAndDayManager._turn + "Åz" + _cardController._name + ": " + _message;
            }
        }
    }

}