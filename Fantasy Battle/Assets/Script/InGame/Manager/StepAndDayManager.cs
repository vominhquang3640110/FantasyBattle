using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace InGame
{
    public enum Step
    {
        Draw,
        Preparation,
        Battle,
        End,
        None
    }

    public class StepAndDayManager : MonoBehaviour
    {
        static public StepAndDayManager instance;

        [Header("Manager")]
        public InGameManager _inGameManager;
        public RPSStepManager _RPSStepManager;
        public DeckManager _deckManager;
        public CreateAndDestroyCardManager _createCardManager;
        public BattleManager _battleManager;
        public PlayerFiledManager _playerFiledManager;
        public ActiveEffectManager _activeEffectManager;

        [Header("Component")]
        PlayerIFManager _P1;
        PlayerIFManager _P2;

        [Header("GameEnd")]
        public bool _isWinGame = default;
        public bool _isSurrender = false;
        public GameObject _resultGame;
        public Text _resultGameText;

        [Header("Step Infor")]
        public int _turn = 0;
        public Step _step;
        public int _round = 0;
        public int _counterDown = 0;
        private int _COUNTERDOWN = 2;
        public bool _getDameCD = false;

        [Header("Game Object")]
        public GameObject _preparationButton;
        public GameObject _battleButton;
        public GameObject _endButton;

        [Header("Bool")]
        public bool _isGameStart = false;
        public bool _isGameEnd = false;
        public bool _drawEnd = false;
        public bool _preparationEnd = false;
        public bool _battleEnd = false;
        public bool _endEnd = false;

        [Header("Choice")]
        [HideInInspector] public GameObject _choiceCard = null;

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
            _preparationButton.SetActive(false);
            _counterDown = _COUNTERDOWN;
        }

        // Update is called once per frame
        void Update()
        {
            if (_inGameManager._isGetPlayerIF && !_isGameStart)
            {
                _isGameStart = true;
                _P1 = _inGameManager._P1;
                _P2 = _inGameManager._P2;
                StartCoroutine(GameFlow()); 
            }
            //ボタンを無効
            if (_step == Step.Preparation && _choiceCard != null)
            {
                _preparationButton.GetComponent<Button>().interactable = false;
            }
            else if (_step == Step.Preparation && _choiceCard == null)
            {
                _preparationButton.GetComponent<Button>().interactable = true;
            }

            if (_round >= 1)
            {
                _createCardManager.UpdateField();
            }

            //カウンタダウン
            if (_P1 != null && _P2 != null)
            {
                _turn = _P1._turn + _P2._turn;
                CheckCounterDown();
            }

            //ゲームエンド
            if (_isGameEnd)
            {
                _resultGame.SetActive(true);

                if (_P1._HP > 0 && _P2._HP <= 0)
                {
                    _isWinGame = true;
                }
                else if (_P1._HP <= 0 && _P2._HP > 0)
                {
                    _isWinGame = false;
                }
                else if (_P1._HP <= 0 && _P2._HP <= 0)
                {
                    _isWinGame = false;
                }

                if (_isWinGame)
                {
                    _resultGameText.text = "You win";
                }
                else
                {
                    _resultGameText.text = "You lose";
                }
                PhotonNetwork.LeaveRoom();
                PhotonNetwork.Disconnect();
            }

        }

        void CheckCounterDown()
        {
            //相手のカウンタダウンが0になったらフィルドリセット
            if (_P2._isCounterDown && !_getDameCD)
            {
                _getDameCD = true;
                _counterDown = _COUNTERDOWN;
                for (int i = 0; i < _P1._characterZone.Length; i++)
                {
                    if (_P1._characterZone[i] != _P1._startCard)
                    {
                        int index = _playerFiledManager.NowIndexGYList(_P1._gy);
                        _P1._gy[index] = _P1._characterZone[i];
                        _P1._characterZone[i] = _P1._startCard;
                    }
                    if (_P1._equipmentZone[i] != "EquipBasic")
                    {
                        int index = _playerFiledManager.NowIndexGYList(_P1._gy);
                        _P1._gy[index] = _P1._equipmentZone[i];
                        _P1._equipmentZone[i] = "EquipBasic";
                    }
                }
                //能力をリセット
                for (int i = 0; i < _playerFiledManager._MZP2.Count; i++)
                {
                    _playerFiledManager._MZP1[i].GetComponentInChildren<CardController>().AbilityReset();
                    _playerFiledManager._MZP2[i].GetComponentInChildren<CardController>().AbilityReset();
                }
            }
        }

        void CounterDownProcess()
        {
            _getDameCD = false;
            _P1._isCounterDown = false;
            int charCountP1 = _playerFiledManager.CheckCharOfField("P1");
            int charCountP2 = _playerFiledManager.CheckCharOfField("P2");
            if (charCountP1 > 0)
            {
                _counterDown = _COUNTERDOWN;
            }
            if (_counterDown > 0 && charCountP1 <= 0)
            {
                _counterDown--;

                if (_counterDown <= 0 && charCountP1 <= 0 && !_getDameCD)
                {
                    _getDameCD = true;
                    _P1._isCounterDown = true;
                    _P1.GetDame(charCountP2 * 2);
                    _counterDown = _COUNTERDOWN;
                    for (int i = 0; i < _P1._equipmentZone.Length; i++)
                    {
                        if (_P1._equipmentZone[i] != "EquipBasic")
                        {
                            int index = _playerFiledManager.NowIndexGYList(_P1._gy);
                            _P1._gy[index] = _P1._equipmentZone[i];
                            _P1._equipmentZone[i] = "EquipBasic";
                        }
                    }
                    //能力をリセット
                    for (int i = 0; i < _playerFiledManager._MZP1.Count; i++)
                    {
                        _playerFiledManager._MZP1[i].GetComponentInChildren<CardController>().AbilityReset();
                        _playerFiledManager._MZP2[i].GetComponentInChildren<CardController>().AbilityReset();
                    }
                }
            }

        }

        IEnumerator GameFlow()
        {
            _step = Step.None;
            //デッキをシャフル
            _deckManager._deckName = _deckManager.ShuffDeck(_deckManager._deckName);
            //4枚ドロー（初期手札）
            _deckManager.Draw(4);
            for (int i = 0; i < _P1._characterZone.Length; i++)
            {
                _P1._characterZone[i] = _P1._deckStartName[i];
                _P1._equipmentZone[i] = "EquipBasic";
            }
            yield return new WaitForSeconds(1);
            //Start Cardを生成
            _createCardManager.UpdateField();
            _createCardManager.UpdateMagnusZone();
            //yield return new WaitForSeconds(3);
            //Round Plus
            _round++;
            //じゃんけんで先後が決まる
            _RPSStepManager.StartRPS();
            while (!_RPSStepManager._isRPSEnd)
            {
                yield return null;
            }
            if (_RPSStepManager._isRPSWin)
            {
                _P1._isTurn = true;
            }
            //yield return new WaitForSeconds(5);
            // 勝者が決まるまでループ
            while (!_isGameEnd)
            {
                if (_P1._isTurn)
                {
                    //Turnアップ
                    _P1._turn++;
                    //カウンタダウン処理
                    if (_P1._turn > 1)
                    {
                        CounterDownProcess();
                    }
                    //プレイポイント回復と増加
                    if (_P1._maxCost < 15)
                    {
                        if (_P1._turn == 1)
                        {
                            _P1._maxCost = 3;
                        }
                        else
                        {
                            _P1._maxCost++;
                        }
                    }
                    _P1._nowCost = _P1._maxCost;
                    //クリックをリセット
                    _activeEffectManager._isClickeedActiveButton = false;
                    //ドロー可能に
                    _deckManager.DrawButtonOn();
                    _step = Step.Draw;
                    _choiceCard = null;
                    _drawEnd = false;
                    BattleManager.instance._isAttacked = false;
                    BattleManager.instance._isAttackedZoneName = "";
                    _P1._isPerfectDefent = false;
                    while (!_drawEnd)
                    {
                        yield return null;
                    }
                    yield return new WaitForSeconds(1);
                    //クリックをリセット
                    _activeEffectManager._isClickeedActiveButton = false;
                    //準備ステップに
                    _step = Step.Preparation;
                    _preparationButton.SetActive(true);
                    _preparationEnd = false;
                    while (!_preparationEnd)
                    {
                        yield return null;
                    }
                    yield return new WaitForSeconds(1);
                    //クリックをリセット
                    _activeEffectManager._isClickeedActiveButton = false;
                    //バトルステップに
                    _step = Step.Battle;
                    _battleButton.SetActive(true);
                    _battleEnd = false;
                    while (!_battleEnd)
                    {
                        yield return null;
                    }
                    yield return new WaitForSeconds(1);
                    //クリックをリセット
                    _activeEffectManager._isClickeedActiveButton = false;
                    //エンドステップに
                    _step = Step.End;
                    _endButton.SetActive(true);
                    _endEnd = false;
                    while (!_endEnd)
                    {
                        yield return null;
                    }
                    _step = Step.None;
                    //クリックをリセット
                    _activeEffectManager._isClickeedActiveButton = false;
                }
                else
                {
                    yield return null;
                }
            }
        }

        public void OnPreparationEndButtonClick()
        {
            if (_step == Step.Preparation)
            {
                _preparationEnd = true;
                _preparationButton.SetActive(false);
                _choiceCard = null;
            }
        }
        public void OnBattleEndButtonClick()
        {
            if (_step == Step.Battle)
            {
                _battleEnd = true;
                _battleButton.SetActive(false);
                _choiceCard = null;
            }
        }
        public void OnEndEndButtonClick()
        {
            if (_step == Step.End)
            {
                _endEnd = true;
                _P1._isNextP2Turn = true;
                _P1._isTurn = false;
                _endButton.SetActive(false);
                _choiceCard = null;
            }
        }
    }

}
