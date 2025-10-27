using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace InGame
{
    public class PlayerInforManager : MonoBehaviour
    {
        [Header("Manager Object")]
        public InGameManager _inGameManager;

        [Header("Player InFor GameObject")]
        public Text _nameP1;
        public Text _nameP2;
        public Text _counterDownP1;
        public Text _counterDownP2;
        public Text _HPP1;
        public Text _HPP2;
        public Text _CostP1;
        public Text _CostP2;
        public Text _deckP1;
        public Text _deckP2;
        public Text _GYP1;
        public Text _GYP2;

        [Header("Change color Box")]
        public Image _TurnBoxIMG;
        //P1
        public Image _deckIMGP1;
        public Image _GYIMGP1;
        //P2
        public Image _deckIMGP2;
        public Image _GYIMGP2;

        [Header("============")]
        public Text _turn;
        public Text _step;
        public GameObject _ActivedEffTextPrefab;
        public Transform _ActivedEffTextContent;
        

        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (!StepAndDayManager.instance._isGameEnd)
            {
                if (_inGameManager._isGetPlayerIF)
                {
                    _nameP1.text = _inGameManager._P1._name;
                    _nameP2.text = _inGameManager._P2._name;
                    _counterDownP1.text = _inGameManager._P1._counterDown.ToString();
                    _counterDownP2.text = _inGameManager._P2._counterDown.ToString();
                    _HPP1.text = _inGameManager._P1._HP.ToString();
                    _HPP2.text = _inGameManager._P2._HP.ToString();
                    _CostP1.text = _inGameManager._P1._nowCost + "\nÅ\\n" + _inGameManager._P1._maxCost;
                    _CostP2.text = _inGameManager._P2._nowCost + "\nÅ\\n" + _inGameManager._P2._maxCost;
                    _deckP1.text = _inGameManager._P1._deckNumber.ToString();
                    _deckP2.text = _inGameManager._P2._deckNumber.ToString();
                    _GYP1.text = PlayerFiledManager.instance.NowIndexGYList(_inGameManager._P1._gy).ToString();
                    _GYP2.text = PlayerFiledManager.instance.NowIndexGYList(_inGameManager._P2._gy).ToString();
                }

                if ((_inGameManager._P1._playerStep == "Draw" && _inGameManager._P2._playerStep == "") ||
                    (_inGameManager._P2._playerStep == "Draw" && _inGameManager._P1._playerStep == ""))
                {
                    _step.text = "Draw Step";
                }
                else if ((_inGameManager._P1._playerStep == "Preparation" && _inGameManager._P2._playerStep == "") ||
                    (_inGameManager._P2._playerStep == "Preparation" && _inGameManager._P1._playerStep == ""))
                {
                    _step.text = "Preparation Step";
                }
                else if ((_inGameManager._P1._playerStep == "Battle" && _inGameManager._P2._playerStep == "") ||
                    (_inGameManager._P2._playerStep == "Battle" && _inGameManager._P1._playerStep == ""))
                {
                    _step.text = "Battle Step";
                }
                else if ((_inGameManager._P1._playerStep == "End" && _inGameManager._P2._playerStep == "") ||
                    (_inGameManager._P2._playerStep == "End" && _inGameManager._P1._playerStep == ""))
                {
                    _step.text = "End Step";
                }

                _turn.text = "Turn: " + StepAndDayManager.instance._turn.ToString();

                GetMessActiveEffText();
                ChangeColorTurn();
            }
        }

        void GetMessActiveEffText()
        {
            if (_inGameManager._P1._isTurn && !_inGameManager._P1._gotMessage && _inGameManager._P1._message != "")
            {
                _inGameManager._P1._gotMessage = true;
                GameObject text = Instantiate(_ActivedEffTextPrefab, Vector2.one, Quaternion.identity, _ActivedEffTextContent);
                GameObject _text = Instantiate(_ActivedEffTextPrefab, Vector2.one, Quaternion.identity, _ActivedEffTextContent);
                Destroy(text);
                _text.GetComponent<Text>().color = Color.green;
                _text.GetComponent<Text>().text = _inGameManager._P1._message + " (YOU)";
            }
            if (_inGameManager._P2._isTurn && !_inGameManager._P1._gotMessage && _inGameManager._P2._message != "")
            {
                _inGameManager._P1._gotMessage = true;
                GameObject text = Instantiate(_ActivedEffTextPrefab, Vector2.one, Quaternion.identity, _ActivedEffTextContent);
                GameObject _text = Instantiate(_ActivedEffTextPrefab, Vector2.one, Quaternion.identity, _ActivedEffTextContent);
                Destroy(text);
                _text.GetComponent<Text>().color = Color.red;
                _text.GetComponent<Text>().text = _inGameManager._P2._message + " (OP)";
            }

            if (_inGameManager._P1._isTurn && _inGameManager._P1._gotMessage && _inGameManager._P2._gotMessage)
            {
                _inGameManager._P1._message = "";
                _inGameManager._P1._gotMessage = false;
            }
            if (_inGameManager._P2._isTurn && _inGameManager._P2._message == "")
            {
                _inGameManager._P1._gotMessage = false;
            }
        }

        void ChangeColorTurn()
        {
            if (_inGameManager._P1._isTurn)
            {
                _TurnBoxIMG.color = Color.blue;
                _deckIMGP1.color = Color.blue;
                _GYIMGP1.color = Color.blue;
                ChangeColor(1f, _TurnBoxIMG);
                ChangeColor(1f, _deckIMGP1);
                ChangeColor(1f, _GYIMGP1);
            }
            else
            {
                _deckIMGP1.color = Color.white;
                _GYIMGP1 .color = Color.white;
                ChangeColor(1f, _deckIMGP1);
                ChangeColor(1f, _GYIMGP1);
            }

            if (_inGameManager._P2._isTurn)
            {
                _TurnBoxIMG.color = Color.red;
                _deckIMGP2.color = Color.red;
                _GYIMGP2.color = Color.red;
                ChangeColor(1f, _TurnBoxIMG);
                ChangeColor(1f, _deckIMGP2);
                ChangeColor(1f, _GYIMGP2);
            }
            else
            {
                _deckIMGP2.color = Color.white;
                _GYIMGP2.color = Color.white;
                ChangeColor(1f, _deckIMGP2);
                ChangeColor(1f, _GYIMGP2);
            }
        }
        void ChangeColor(float value, Image text)
        {
            Color color = text.GetComponent<Image>().color;
            color.a = value;
            text.GetComponent<Image>().color = color;
        }
    }
}
