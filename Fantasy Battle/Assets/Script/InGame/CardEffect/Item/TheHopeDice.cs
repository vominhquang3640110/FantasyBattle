using InGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame
{
    public class TheHopeDice : MonoBehaviour
    {
        [Header("Manager And Controller")]
        private string _message = "";
        public CardController _cardController;
        public BattleStepCardController _battleStepCardController;
        public CardEffect _cardEffect;
        private InGameManager _inGameManager;
        private PlayerFiledManager _playerFiledManager;
        private StepAndDayManager _stepAndDayManager;
        private BattleManager _battleManager;
        private ActiveEffectManager _activeEffectManager;
        private DeckManager _deckManager;
        private string _cardEffectText;

        private int _diceNumber = 0;
        public bool _isGotRandNumber = false;

        // Start is called before the first frame update
        void Start()
        {
            _cardEffectText = "When Rolling A Dice, Odd Numbers Reduce Your Opponent's \"Soul\" ATK, Even Numbers Increase Your \"Soul\" ATK (Increase Or Decrease ATK Based On The Number Of On The Dice)";

            _inGameManager = InGameManager.instance;
            _playerFiledManager = PlayerFiledManager.instance;
            _stepAndDayManager = StepAndDayManager.instance;
            _battleManager = BattleManager.instance;
            _activeEffectManager = ActiveEffectManager.instance;
            _deckManager = DeckManager.instance;

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
           
            if (gameObject.tag == "P1" && _inGameManager._P1._nowCost >= _cardController._cost)
            {
                _diceNumber = Random.Range(1, 7);
                _message += _diceNumber;
                _inGameManager._P1._message = "Åy" + _stepAndDayManager._turn + "Åz" + _cardController._name + ": " + _message;

                int souCardP1 = PlayerFiledManager.instance.CheckCharOfField("P1");
                int souCardP2 = PlayerFiledManager.instance.CheckCharOfField("P2");
                //ãÙêîÇÃèÍçá
                if (_diceNumber % 2 == 0 && souCardP1 > 0)
                {
                    _cardEffect.isEffectToP1 = true;
                    _inGameManager._P1.isRandNumber = true;
                    _inGameManager._P1._randNumber = _diceNumber;
                    return true;
                }
                //äÔêîÇÃèÍçá
                else if (_diceNumber % 2 == 1 && souCardP2 > 0)
                {
                    _cardEffect.isEffectToP2 = true;
                    _inGameManager._P1.isRandNumber = true;
                    _inGameManager._P1._randNumber = _diceNumber;
                    return true;
                }
                else
                {
                    _inGameManager._P1._nowCost -= _cardController._cost;

                    _message = "Activation failure";
                    _inGameManager._P1._message = "Åy" + _stepAndDayManager._turn + "Åz" + _cardController._name + ": " + _message;

                    _playerFiledManager.AddCardToGY(_cardController);
                    _stepAndDayManager._choiceCard = null;
                    _inGameManager._P1.RemoveActiveAndActived();
                    _activeEffectManager._isClickeedActiveButton = false;
                    _inGameManager._P1.isRandNumber = false;
                    _inGameManager._P1._randNumber = 0;

                    Destroy(_cardEffect.activeButton.gameObject);
                    Destroy(gameObject);
                    return false;
                }

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

            if (_inGameManager._P2._isTurn && !_isGotRandNumber && _inGameManager._P2.isRandNumber)
            {
                _isGotRandNumber = true;
                _diceNumber = _inGameManager._P2._randNumber;
            }
        }

        public void ActivedEff(CardController cardController)
        {
            Debug.LogWarning(cardController._name + "   " + _diceNumber + "   " + (_diceNumber%2));
            //ãÙêîÇÃèÍçá
            if (_diceNumber % 2 == 0)
            {
                Debug.LogWarning("AAA");
                cardController._ATK += _diceNumber;
            }
            //äÔêîÇÃèÍçá
            else if (_diceNumber % 2 == 1)
            {
                Debug.LogWarning("BBBB");
                cardController._ATK -= _diceNumber;
            }
        }
    }

}