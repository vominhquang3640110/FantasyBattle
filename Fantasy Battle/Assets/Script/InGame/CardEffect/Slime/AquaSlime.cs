using InGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame
{
    public class AquaSlime : MonoBehaviour
    {
        [Header("Manager And Controller")]
        public CardController _cardController;
        public BattleStepCardController _battleStepCardController;
        public CardEffect _cardEffect;
        private PlayerFiledManager _playerFiledManager;
        private StepAndDayManager _stepAndDayManager;
        private BattleManager _battleManager;
        private string _cardEffectText;

        // Start is called before the first frame update
        void Start()
        {
            _cardEffectText = "ÅsAct/1TurnÅtÅy3MPÅzReduce Your Own HP By Half, To Reduce The ATK Of The Enemy \"Soul\" By Half.";

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
            if (souCardP2 > 0 && _cardController._MP >= 3)
            {
                _cardController._MP -= 3;
                return true;
            }
            else
            {
                return false;
            }
        }

        public void ActiveEff()
        {
            _cardController._HP -= Mathf.CeilToInt(_cardController._HP / 2);
        }

        public void ActivedEff(CardController cardController)
        {
            cardController._ATK -= Mathf.CeilToInt(cardController._ATK / 2);
            if (gameObject.tag == "P1")
            {
                string _message = "Active effect!!";
                InGameManager.instance._P1._message = "Åy" + _stepAndDayManager._turn + "Åz" + _cardController._name + ": " + _message;
            }
        }
    }

}