using InGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame
{
    public class GOA : MonoBehaviour
    {
        [Header("Manager And Controller")]
        //private string _message = "Op cannot active item to end step of self";
        public CardController _cardController;
        public BattleStepCardController _battleStepCardController;
        public CardEffect _cardEffect;
        private InGameManager _inGameManager;
        private PlayerFiledManager _playerFiledManager;
        private StepAndDayManager _stepAndDayManager;
        private BattleManager _battleManager;
        private DeckManager _deckManager;

        // Start is called before the first frame update
        void Start()
        {
            _inGameManager = InGameManager.instance;
            _playerFiledManager = PlayerFiledManager.instance;
            _stepAndDayManager = StepAndDayManager.instance;
            _battleManager = BattleManager.instance;
            _deckManager = DeckManager.instance;

            _cardEffect.CheckConditionspross = CheckConditions;
            _cardEffect.ActiveEffectPross = ActiveEff;
            _cardEffect.ActivedEffectPross = ActivedEff;
        }

        // Update is called once per frame
        void Update()
        {

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
            if (gameObject.tag == "P1")
            {
                _inGameManager._P1._isPerfectDefent = true;
                string _message = "Active effect!!";
                _inGameManager._P1._message = "Åy" + _stepAndDayManager._turn + "Åz" + _cardController._name + ": " + _message;
            }
            //CreateAndDestroyCardManager.instance.UpdateMagnusZone();
        }
    }

}