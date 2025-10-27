using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace InGame
{
    public class ActiveEffectManager : MonoBehaviour
    {
        static public ActiveEffectManager instance;
        public StepAndDayManager _stepAndDayManager;
        public InGameManager _inGameManager;
        public PlayerFiledManager _playerFiledManager;

        [SerializeField] public GameObject _choiceCardActived = null;

        public bool _isClickeedActiveButton = false;
        public bool isActiving = false;
        public bool _isCreatedPrefab = false;
        public List<CardEffect> _activeCard;
        public bool _cardActiving = false;


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
        }
        // Start is called before the first frame update
        void Start()
        {
            _activeCard = new List<CardEffect>();
        }

        // Update is called once per frame
        void Update()
        {
            if (_inGameManager._isGetPlayerIF && _inGameManager._P1._opCannotActiveItem && _inGameManager._P2._playerStep == "End")
            {
                _inGameManager._P1._opCannotActiveItem = false;
            }

            if (_choiceCardActived != null)
            {
                _choiceCardActived.GetComponent<CardEffect>().thisCardActiveTargetEffect = true;
                Color color = _choiceCardActived.GetComponent<Image>().color;
                color.a = 1.2f;
                _choiceCardActived.GetComponent<Image>().color = color;
            }

            if (!isActiving && _stepAndDayManager._choiceCard != null && _choiceCardActived != null)
            {
                Debug.LogWarning(_stepAndDayManager._choiceCard.GetComponent<CardController>()._name + " " + _choiceCardActived.GetComponent<CardController>()._name);
                CardEffect cardEff = _stepAndDayManager._choiceCard.GetComponent<CardEffect>();
                CardController cardCtrll = _stepAndDayManager._choiceCard.GetComponent<CardController>();
                if (cardEff.haveActiveEff && cardCtrll._Type == "EE" && !cardCtrll._isEuqip)
                {
                    cardEff.CheckConditionspross();
                }
                isActiving = true;
                _stepAndDayManager._choiceCard.GetComponent<CardEffect>().ActiveEffectPross();
                StartCoroutine(EffectActivePross2(_stepAndDayManager._choiceCard, _choiceCardActived));
            }

            if (isActiving && _inGameManager._P2._activeAndActived[0] == "" && _inGameManager._P2._activeAndActived[1] == "")
            {
                isActiving = false;
                _isCreatedPrefab = false;
                if (_stepAndDayManager._choiceCard != null && _choiceCardActived != null)
                {
                    RestColorAndNull(_stepAndDayManager._choiceCard, _choiceCardActived);
                }
            }

            if (_stepAndDayManager._step == Step.None)
            {
                if (_stepAndDayManager._choiceCard != null && _inGameManager._P2._activeAndActived[0] == "")
                {
                    _stepAndDayManager._choiceCard.GetComponent<CardEffect>().CardColorChange(1f);
                    _stepAndDayManager._choiceCard.GetComponent<Image>().color = Color.white;
                    _stepAndDayManager._choiceCard = null;
                }
                if (_choiceCardActived != null && _inGameManager._P2._activeAndActived[1] == "")
                {
                    _choiceCardActived.GetComponent<CardEffect>().CardColorChange(1f);
                    _choiceCardActived.GetComponent<Image>().color = Color.white;
                    _choiceCardActived = null;
                }
            }

            //“¯Žž”­“®‚Ìê‡
            if (!_cardActiving && _activeCard.Count > 0)
            {
                _cardActiving = true;
                StartCoroutine(ActiveCard());
            }
        }

        IEnumerator ActiveCard()
        {
            for (int i = 0; i < _activeCard.Count; i++)
            {
                _activeCard[i].haveActiveEff = true;
                StepAndDayManager.instance._choiceCard = _activeCard[i].gameObject;
                _activeCard[i].EffectActiveProccess();
                _activeCard[i].haveActiveEff = false;
                yield return new WaitForSeconds(4);
            }
            _activeCard.Clear();
            _cardActiving = false;
        }

        public IEnumerator EffectActivePross2(GameObject cardActive, GameObject cardActived)
        {
            cardActive.GetComponent<CardEffect>().ActivedEffectPross(cardActived.GetComponent<CardController>());
            cardActive.GetComponent<CardEffect>().isActiveEnd = true;

            EffectActiveShow.instance.OpenWindown(cardActive.GetComponent<CardController>());
            CreateAndDestroyCardManager.instance.UpdateMagnusZone();

            yield return new WaitForSeconds(3);
            EffectActiveShow.instance.CloseWindown(cardActive.GetComponent<CardController>());
            RestColorAndNull(cardActive, cardActived);
        }

        void RestColorAndNull(GameObject cardActive, GameObject cardActived)
        {
            cardActive.GetComponent<Image>().color = Color.white;
            cardActived.GetComponent<Image>().color = Color.white;
            cardActive.GetComponent<CardEffect>().CardColorChange(1);
            cardActived.GetComponent<CardEffect>().CardColorChange(1);
            if (cardActive.GetComponent<CardController>()._Type == "Item" && cardActive.tag == "P1")
            {
                _playerFiledManager.AddCardToGY(cardActive.GetComponent<CardController>());
                Destroy(cardActive);
            }
            _stepAndDayManager._choiceCard = null;
            _choiceCardActived = null;
        }
    }
}

