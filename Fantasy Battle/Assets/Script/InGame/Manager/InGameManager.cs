using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace InGame
{
    public class InGameManager : MonoBehaviour
    {
        public static InGameManager instance;

        [Header("Manager Object")]
        public GameManager _gameManager;
        public RPSStepManager _RPSStepManager;
        public StepAndDayManager _stepAndDayManager;
        public DeckManager _deckManager;

        [Header("Player Information")]
        [HideInInspector] public PlayerIFManager _P1 = null;
        [HideInInspector] public PlayerIFManager _P2 = null;
        [HideInInspector] public bool _isGetPlayerIF = false;

        [Header("Card Information")]
        public bool _isShowCardInfor = false;
        public GameObject _cardInforWindown;

        [Header("Surrender")]
        public GameObject _surrenderBox;

        [Header("Position")]
        public Transform activeButtonEffPosition;

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
            GameObject playerIF = PhotonNetwork.Instantiate("PlayerIF", Vector3.zero, Quaternion.identity, 0);
        }

        // Start is called before the first frame update
        void Start()
        {
            _gameManager = GameManager.instance;
        }

        // Update is called once per frame
        void Update()
        {
            SetPlayerIF();

            if (_isGetPlayerIF)
            {
                //相手が勝手にゲームを終了するか降参するか
                GameObject[] players = GameObject.FindGameObjectsWithTag("PlayerIF");
                if (players.Length < 2)
                {
                    if (!_stepAndDayManager._isSurrender)
                    {
                        _stepAndDayManager._isWinGame = true;
                    }
                    else
                    {
                        _stepAndDayManager._isWinGame = false;
                    }
                    _stepAndDayManager._isGameEnd = true;
                }
                //自分が降参する
                if (Input.GetKeyDown(KeyCode.Escape) && !_stepAndDayManager._isGameEnd)
                {
                    _surrenderBox.SetActive(true);
                }
                //カードの情報を表示するウィンドウ
                if (Input.GetKeyDown(KeyCode.Tab) && !_isShowCardInfor)
                {
                    _isShowCardInfor = true;
                    _cardInforWindown.SetActive(true);
                }
                else if (Input.GetKeyDown(KeyCode.Tab) && _isShowCardInfor)
                {
                    _isShowCardInfor = false;
                    _cardInforWindown.SetActive(false);
                }
            }
        }

        void SetPlayerIF()
        {
            if (!_isGetPlayerIF)
            {
                GameObject[] players = GameObject.FindGameObjectsWithTag("PlayerIF");
                if (players.Length >= 2)
                {
                    for (int i = 0; i < players.Length; i++)
                    {
                        if (players[i].GetComponent<PlayerIFManager>()._photonView.isMine)
                        {
                            _P1 = players[i].GetComponent<PlayerIFManager>();
                        }
                        else
                        {
                            _P2 = players[i].GetComponent<PlayerIFManager>();
                        }
                    }
                    _isGetPlayerIF = true;
                }
            }
        }

        public void OnClickBackHomeButton()
        {
            Destroy(gameObject);
            SceneManager.LoadScene("Preparation");
        }
        public void OnClickSurrenderYes()
        {
            _surrenderBox.SetActive(false);
            _stepAndDayManager._isSurrender = true;
            _stepAndDayManager._isGameEnd = true;
        }
        public void OnClickSurrenderNo()
        {
            _surrenderBox.SetActive(false);
        }
    }

}
