using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace InGame
{
    public enum MagnusZone
    {
        Magnus1P1,
        Magnus2P1,
        Magnus1P2,
        Magnus2P2,
    }
    public class CardMagnusController : MonoBehaviour
    {
        [Header("Manager And Controller")]
        public CardController _cardController;
        public BattleStepCardController _battleStepCardController;
        public CardEffect _cardEffect;
        private PlayerFiledManager _playerFiledManager;
        private InGameManager _inGameManager;
        private StepAndDayManager _stepAndDayManager;
        private BattleManager _battleManager;
        private ActiveEffectManager _activeEffectManager;
        private PrefabManager _prefabManager;

        [Header("Infor")]
        public MagnusZone _magnusZone;


        // Start is called before the first frame update
        void Start()
        {
            _playerFiledManager = PlayerFiledManager.instance;
            _inGameManager = InGameManager.instance;
            _stepAndDayManager = StepAndDayManager.instance;
            _battleManager = BattleManager.instance;
            _activeEffectManager = ActiveEffectManager.instance;
            _prefabManager = PrefabManager.instance;
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void BackCardShow()
        {
            GetComponent<Image>().sprite = _cardController.backCardImageM;
        }
        public void FrontCardShow()
        {
            GetComponent<Image>().sprite = _cardController.frontCardImage;
        }
    }

}