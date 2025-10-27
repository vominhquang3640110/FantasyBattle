using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace InGame
{
    public class RPSStepManager : MonoBehaviour
    {
        static public RPSStepManager instance;

        [Header("Manager Object")]
        public InGameManager _ingameManager;
        public StepAndDayManager _stepAndDayManager;

        [Header("RPS Screen")]
        [SerializeField] GameObject _screen;

        [Header("My RPS")]
        [SerializeField] GameObject _myRPS;
        [SerializeField] GameObject R_myRPS;
        [SerializeField] GameObject P_myRPS;
        [SerializeField] GameObject S_myRPS;

        [Header("ResultRPS(1)")]
        [SerializeField] GameObject _resultRPS1;
        [SerializeField] GameObject R_resultRPS1;
        [SerializeField] GameObject P_resultRPS1;
        [SerializeField] GameObject S_resultRPS1;
        [SerializeField] GameObject R_resultRPS1_P2;
        [SerializeField] GameObject P_resultRPS1_P2;
        [SerializeField] GameObject S_resultRPS1_P2;

        [Header("ResultRPS(2)")]
        [SerializeField] GameObject _resultRPS2;
        [SerializeField] Text _resultText;

        [Header("Bool")]
        public bool _checkResult = false;
        public bool _isRPSWin = default;
        public bool _isRPSEnd = true;

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
            _screen.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            if (_ingameManager._isGetPlayerIF)
            {

            }

            if (_stepAndDayManager._isGameStart && !_checkResult && _ingameManager._P1._RPSValue != "" && _ingameManager._P2._RPSValue != "")
            {
                _checkResult = true;
                ResetColor(R_myRPS);ResetColor(P_myRPS);ResetColor(S_myRPS);
                ResetActiveRPS(_myRPS, R_myRPS, P_myRPS, S_myRPS);
                _resultRPS1.SetActive(true);
                //P1 Result
                if (_ingameManager._P1._RPSValue == "R")
                {
                    ResultR(_resultRPS1, R_resultRPS1, P_resultRPS1, S_resultRPS1);
                }
                else if (_ingameManager._P1._RPSValue == "P")
                {
                    ResultP(_resultRPS1, R_resultRPS1, P_resultRPS1, S_resultRPS1);
                }
                else if (_ingameManager._P1._RPSValue == "S")
                {
                    ResultS(_resultRPS1, R_resultRPS1, P_resultRPS1, S_resultRPS1);
                }
                //P2 Result
                if (_ingameManager._P2._RPSValue == "R")
                {
                    ResultR(_resultRPS1, R_resultRPS1_P2, P_resultRPS1_P2, S_resultRPS1_P2);
                }
                else if (_ingameManager._P2._RPSValue == "P")
                {
                    ResultP(_resultRPS1, R_resultRPS1_P2, P_resultRPS1_P2, S_resultRPS1_P2);
                }
                else if (_ingameManager._P2._RPSValue == "S")
                {
                    ResultS(_resultRPS1, R_resultRPS1_P2, P_resultRPS1_P2, S_resultRPS1_P2);
                }
                string _p1 = _ingameManager._P1._RPSValue;
                string _p2 = _ingameManager._P2._RPSValue;
                
                StartCoroutine(Wait2SShowResult(_p1, _p2));
            }
        }

        public void StartRPS()
        {
            _isRPSEnd = false;
            _screen.SetActive(true);
            _myRPS.SetActive(true);
            _resultRPS1.SetActive(false);
            _resultRPS2.SetActive(false);
            RPSButtonOn();
        }
        public void OnClickRock()
        {
            RPSButtonOff();
            ChoicedColor(R_myRPS);
            _ingameManager._P1._RPSValue = "R";
        }
        public void OnClickPaper()
        {
            RPSButtonOff();
            ChoicedColor(P_myRPS);
            _ingameManager._P1._RPSValue = "P";
        }
        public void OnClickScissors()
        {
            RPSButtonOff();
            ChoicedColor(S_myRPS);
            _ingameManager._P1._RPSValue = "S";
        }

        void RPSButtonOff()
        {
            R_myRPS.GetComponent<Button>().interactable = false;
            P_myRPS.GetComponent<Button>().interactable = false;
            S_myRPS.GetComponent<Button>().interactable = false;
        }
        void RPSButtonOn()
        {
            R_myRPS.GetComponent<Button>().interactable = true;
            P_myRPS.GetComponent<Button>().interactable = true;
            S_myRPS.GetComponent<Button>().interactable = true;
        }
        void ResultR(GameObject result, GameObject R_result, GameObject P_result, GameObject S_result)
        {
            R_result.SetActive(true);
            P_result.SetActive(false);
            S_result.SetActive(false);
        }
        void ResultP(GameObject result, GameObject R_result, GameObject P_result, GameObject S_result)
        {
            R_result.SetActive(false);
            P_result.SetActive(true);
            S_result.SetActive(false);
        }
        void ResultS(GameObject result, GameObject R_result, GameObject P_result, GameObject S_result)
        {
            R_result.SetActive(false);
            P_result.SetActive(false);
            S_result.SetActive(true);
        }

        void ChoicedColor(GameObject button)
        {
            button.GetComponent<Image>().color = Color.yellow;
        }
        void ResetColor(GameObject button)
        {
            button.GetComponent<Image>().color = Color.white;
        }
        void ResetActiveRPS(GameObject gameObject, GameObject R_gameObject, GameObject P_gameObject, GameObject S_gameObject)
        {
            gameObject.SetActive(false);
            R_gameObject.SetActive(true);
            P_gameObject.SetActive(true);
            S_gameObject.SetActive(true);
        }

        IEnumerator Wait2SShowResult(string _p1, string _p2)
        {
            yield return new WaitForSeconds(2);

            ResetActiveRPS(_resultRPS1, R_resultRPS1, P_resultRPS1, S_resultRPS1);
            ResetActiveRPS(_resultRPS1, R_resultRPS1_P2, P_resultRPS1_P2, S_resultRPS1_P2);
            _checkResult = false;
            _ingameManager._P1._RPSValue = "";
            bool _isWin = false;
            if (_p1 == _p2)
            {
                StartRPS();
            }
            else if (_p1 == "R" && _p2 == "P")
            {
                _isWin = false;
                ResultTextShow(_isWin);
            }
            else if (_p1 == "R" && _p2 == "S")
            {
                _isWin = true;
                ResultTextShow(_isWin);
            }
            else if (_p1 == "P" && _p2 == "R")
            {
                _isWin = true;
                ResultTextShow(_isWin);
            }
            else if (_p1 == "P" && _p2 == "S")
            {
                _isWin = false;
                ResultTextShow(_isWin);
            }
            else if (_p1 == "S" && _p2 == "R")
            {
                _isWin = false;
                ResultTextShow(_isWin);
            }
            else if (_p1 == "S" && _p2 == "P")
            {
                _isWin = true;
                ResultTextShow(_isWin);
            }
        }
        void ResultTextShow(bool isWin)
        {
            _resultRPS2.SetActive(true);
            if (isWin)
            {
                _resultText.text = "You Win";
                _isRPSWin = true;
            }
            else
            {
                _resultText.text = "You Lose";
                _isRPSWin = false;
            }
            StartCoroutine(Wait2SCloseResult());
        }
        IEnumerator Wait2SCloseResult()
        {
            yield return new WaitForSeconds(2);
            _resultRPS2.SetActive(false);
            _screen.SetActive(false);
            _isRPSEnd = true;
        }
    }
}
