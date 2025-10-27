using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace InGame
{
    public class BattleManager : MonoBehaviour
    {
        public static BattleManager instance;

        [Header("Manager")]
        public InGameManager _inGameManager;
        public StepAndDayManager _stepAndDayManager;

        [SerializeField] public GameObject _attackCard = null;
        [SerializeField] public GameObject _attackedCard = null;
        public string _attackCardZone = "";
        public string _attackedCardZone = "";
        public bool _isAttacked = false;
        public string _isAttackedZoneName = "";

        PlayerIFManager _P1;
        PlayerIFManager _P2;

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
            
        }

        // Update is called once per frame
        void Update()
        {
            if (_inGameManager._isGetPlayerIF)
            {
                _P1 = _inGameManager._P1;
                _P2 = _inGameManager._P2;
                OPBattleStep();
            }

            if (_attackCard != null || _attackedCard != null)
            {
                _stepAndDayManager._battleButton.GetComponent<Button>().interactable = false;
            }
            else
            {
                _stepAndDayManager._battleButton.GetComponent<Button>().interactable = true;
            }

        }

        void OPBattleStep()
        {
            if (_P2._playerStep == "Battle" && _P2._attackAndAttacked[0] != "" && _P2._attackAndAttacked[1] != "")
            {
                _attackCardZone = _P2._attackAndAttacked[0].Substring(0, 3) + "P2"; 
                _attackedCardZone = _P2._attackAndAttacked[1].Substring(0, 3) + "P1";
            }
            if (_P2._playerStep == "Battle" && _P2._attackAndAttacked[0] == "" && _P2._attackAndAttacked[1] == "")
            {
                _attackCardZone = "";
                _attackedCardZone = "";
                _attackCard = null;
                _attackedCard = null;
            }
        }
    }
}
