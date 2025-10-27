using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace InGame
{
    public class DeckManager : MonoBehaviour
    {
        public static DeckManager instance;

        [Header("Manager Object")]
        public HandManager _handManager;
        public PrefabManager _prefabManager;
        public StepAndDayManager _stepAndDayManager;

        public List<string> _deckStartName;
        public List<string> _deckName;
        public List<string> _deckMagnusName;

        public Button _deck;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }

            //GameManagerからデッキのデータを受け取る
            for (int i = 0; i < GameManager.instance._deckStartName.Count; i++)
            {
                for (int j = 0; j < GameManager.instance._deckStartAmount[i]; j++)
                {
                    _deckStartName.Add(GameManager.instance._deckStartName[i]);
                }
            }
            for (int i = 0; i < GameManager.instance._deckEEName.Count; i++)
            {
                for (int j = 0; j < GameManager.instance._deckEEAmount[i]; j++)
                {
                    _deckName.Add(GameManager.instance._deckEEName[i]);
                }
            }
            for (int i = 0; i < GameManager.instance._deckItemName.Count; i++)
            {
                for (int j = 0; j < GameManager.instance._deckItemAmount[i]; j++)
                {
                    _deckName.Add(GameManager.instance._deckItemName[i]);
                }
            }
            for (int i = 0; i < GameManager.instance._deckMagnusName.Count; i++)
            {
                for (int j = 0; j < GameManager.instance._deckMagnusAmount[i]; j++)
                {
                    _deckMagnusName.Add(GameManager.instance._deckMagnusName[i]);
                }
            }
        }
        void Start()
        {

        }

        public void DrawButtonOn()
        {
            _deck.interactable = true;
        }

        public void DrawButtonOff()
        {
            _deck.interactable = false;
        }

        public void OnClickDraw()
        {
            if (_stepAndDayManager._step == Step.Draw && !ActiveEffectManager.instance._cardActiving)
            {
                _stepAndDayManager._drawEnd = true;
                Draw(1);
                DrawButtonOff();
            }
        }
        public void Draw(int numberCard)
        {
            for (int i = 0; i < numberCard; i++)
            {
                _handManager.AddCardToHandP1(_deckName[_deckName.Count - 1]);
                _deckName.RemoveAt(_deckName.Count - 1);
            }

            if (_deckName.Count <= 0)
            {
                _stepAndDayManager._isWinGame = false;
                _stepAndDayManager._isGameEnd = true;
            }
        }

        public List<string> ShuffDeck(List<string> deck)
        {
            List<int> deckIDRand = new List<int>();

            for (int i = 0; i < deck.Count; i++)
            {
                bool check = false;
                while (!check)
                {
                    int rand = Random.Range(0, deck.Count);
                    bool getRand = true;
                    for (int j = 0; j < deckIDRand.Count; j++)
                    {
                        if (deckIDRand[j] == rand)
                        {
                            getRand = false;
                        }
                    }
                    if (getRand)
                    {
                        deckIDRand.Add(rand);
                        check = true;
                    }
                }
            }

            List<string> shuffedDeck = new List<string>();
            for (int i = 0; i < deck.Count; i++)
            {
                shuffedDeck.Add(deck[deckIDRand[i]]);
            }

            return shuffedDeck;
        }
    }

}