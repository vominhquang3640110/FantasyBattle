using InGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame
{
    public class SonicBoom_E : MonoBehaviour
    {
        [Header("Manager And Controller")]
        public CardController _cardController;
        private PlayerFiledManager _playerFiledManager;
        private StepAndDayManager _stepAndDayManager;
        private BattleManager _battleManager;
        private string _cardEffectText;

        public int _prizeDamage = 2;
        public bool _isActive = false;

        // Start is called before the first frame update
        void Start()
        {
            _cardEffectText = "ÅsPassiveÅtÅy2MPÅzWhen Attacking, Deals 2 Damage To All Enemy ÅgSoulsÅh .";

            _playerFiledManager = PlayerFiledManager.instance;
            _stepAndDayManager = StepAndDayManager.instance;
            _battleManager = BattleManager.instance;

            _cardController._cardEffectText = _cardEffectText;
        }

        // Update is called once per frame
        void Update()
        {
            if (_battleManager._attackCard != null && _battleManager._attackedCard != null && _cardController.MZ != null
                && _battleManager._attackCard.GetComponent<CardController>()._zone == _cardController.MZ.GetComponentInChildren<CardController>()._zone
                && !_isActive && _cardController.MZ != null && _cardController.MZ.GetComponentInChildren<CardController>()._MP >= 2)
            {
                _cardController.MZ.GetComponentInChildren<CardController>()._MP -= 2;
                if (_battleManager._attackCard.tag == "P1")
                {
                    List<GameObject> cardP2List = _playerFiledManager.GetP2SoulCardList();
                    for (int i = 0; i < cardP2List.Count; i++)
                    {
                        cardP2List[i].GetComponent<BattleStepCardController>().GetDame(_prizeDamage, cardP2List[i].GetComponent<CardController>());
                    }
                    if (gameObject.tag == "P1")
                    {
                        string _message = "Active effect!!";
                        InGameManager.instance._P1._message = "Åy" + _stepAndDayManager._turn + "Åz" + _cardController._name + ": " + _message;
                    }
                }
                else if (_battleManager._attackCard.tag == "P2")
                {
                    List<GameObject> cardP1List = _playerFiledManager.GetP1SoulCardList();
                    for (int i = 0; i < cardP1List.Count; i++)
                    {
                        cardP1List[i].GetComponent<BattleStepCardController>().GetDame(_prizeDamage, cardP1List[i].GetComponent<CardController>());
                    }
                }
                _isActive = true;
            }

            if (_battleManager._attackCard == null && _battleManager._attackedCard == null && _cardController.MZ != null
                && _cardController.MZ.GetComponentInChildren<BattleStepCardController>()._canAttackTimes > 0 && _isActive)
            {
                _isActive = false;
            }
        }
    }

}