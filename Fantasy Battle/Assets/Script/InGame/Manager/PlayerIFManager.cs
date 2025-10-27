using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame
{
    public class PlayerIFManager : MonoBehaviour
    {
        [Header("Photon")]
        public PhotonView _photonView;

        [Header("User Information")]
        public int _turn = 0;
        public string _playerStep = "";
        public string _name = "";
        public int _counterDown;
        public int _HP = 0;
        public int _nowCost = 0;
        public int _maxCost = 0;
        public bool _isTurn = false;
        public string _startCard;
        public List<string> _deckStartName;
        public List<string> _deckName;
        public string[] _deckMagnusName;
        public int _deckStartNumber;
        public int _deckNumber;
        public int _deckMagnusNumber;
        public int _handCardNumber;

        [Header("Filed Information")]
        public bool _isPerfectDefent = false;
        public bool _gotMessage = false;
        public string _message = "";
        public bool _opCannotActiveItem = false;
        public string[] _attackAndAttacked;
        public string[] _activeAndActived;
        public bool _isActiveItem;
        public string[] _characterZone;
        public string[] _equipmentZone;
        public bool isRandNumber = false;
        public int _randNumber;
        public bool[] _MagnusActive;

        [Header("GY Information")]
        public string[] _gy;

        [Header("R-P-S Step Information")]
        public string _RPSValue = "";

        [Header("Counter Down")]
        public bool _isCounterDown = false;

        [Header("Int")]
        public int _canAttackTimes;

        [Header("Bool")]
        public bool _isPreparationEnd = false;
        public bool _isNextP2Turn = false;
        public bool _GetDame = false;

        // Start is called before the first frame update
        void Start()
        {

            _deckMagnusName = new string[2];
            _HP = 20;
            _attackAndAttacked = new string[2];
            _attackAndAttacked[0] = "";
            _attackAndAttacked[1] = "";
            _activeAndActived = new string[50];
            for (int i = 0; i < _activeAndActived.Length; i++)
            {
                _activeAndActived[i] = "";
            }
            _characterZone = new string[4];
            _equipmentZone = new string[4];
            _MagnusActive = new bool[2];
            _MagnusActive[0] = false;
            _MagnusActive[1] = false;

            _gy = new string[100];
        }

        // Update is called once per frame
        void Update()
        {
            if (_photonView.isMine)
            {
                if (StepAndDayManager.instance._step == Step.Draw)
                {
                    _playerStep = "Draw";
                }
                else if (StepAndDayManager.instance._step == Step.Preparation)
                {
                    _playerStep = "Preparation";
                }
                else if (StepAndDayManager.instance._step == Step.Battle)
                {
                    _playerStep = "Battle";
                }
                else if (StepAndDayManager.instance._step == Step.End)
                {
                    _playerStep = "End";
                }
                else
                {
                    _playerStep = "";
                }
                _name = GameManager._userName;
                _counterDown = StepAndDayManager.instance._counterDown;
                _deckStartName = DeckManager.instance._deckStartName;
                _deckName = DeckManager.instance._deckName;
                _deckMagnusName[0] = DeckManager.instance._deckMagnusName[0];
                _deckMagnusName[1] = DeckManager.instance._deckMagnusName[1];
                _deckStartNumber = _deckStartName.Count;
                _deckNumber = _deckName.Count;
                _deckMagnusNumber = _deckMagnusName.Length;
                _startCard = _deckStartName[0];
                if (InGameManager.instance._isGetPlayerIF && InGameManager.instance._P2._isNextP2Turn && !_isTurn && !InGameManager.instance._P2._isTurn)
                {
                    _isTurn = true;
                }
                if (InGameManager.instance._isGetPlayerIF && InGameManager.instance._P2._isTurn && _isNextP2Turn)
                {
                    _isNextP2Turn = false;
                }
                if (InGameManager.instance._isGetPlayerIF && _GetDame && InGameManager.instance._P2._attackAndAttacked[0] == "" && InGameManager.instance._P2._attackAndAttacked[1] == "")
                {
                    _GetDame = false;
                }
            }
        }

        public void GetDame(int dameValue)
        {
            _HP -= dameValue;
            if (_HP <= 0)
            {
                StepAndDayManager.instance._isGameEnd = true;
            }
        }

        public void AddActiveAndActived(string text ,bool isActive)
        {
            int index = 0;
            for (int i = 0; i < _activeAndActived.Length; i++)
            {
                if (_activeAndActived[i] == "")
                {
                    index = i; break;
                }
            }
            if (index % 2 == 1)
            {
                index--;
            }
            if (isActive)
            {
                _activeAndActived[index] = text;
            }
            else
            {
                _activeAndActived[index + 1] = text;
            }
        }

        public void RemoveActiveAndActived()
        {
            Debug.LogWarning("sdfasdads");
            string[] save = new string[50];
            for (int i = 0; i < _activeAndActived.Length - 2; i++)
            {
                save[i] = _activeAndActived[i + 2];
            }
            for (int i = 0; i < _activeAndActived.Length; i++)
            {
                _activeAndActived[i] = save[i];
            }
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.isWriting)
            {
                //RPS Infor
                stream.SendNext(_RPSValue);
                //Counter Down
                stream.SendNext(_isCounterDown);
                //User Infor
                stream.SendNext(_turn);
                stream.SendNext(_playerStep);
                stream.SendNext(_name);
                stream.SendNext(_counterDown);
                stream.SendNext(_HP);
                stream.SendNext(_nowCost);
                stream.SendNext(_maxCost);
                stream.SendNext(_isTurn);
                stream.SendNext(_startCard);
                stream.SendNext(_deckStartNumber);
                stream.SendNext(_deckNumber);
                stream.SendNext(_deckMagnusNumber);
                stream.SendNext(_handCardNumber);
                //Field Infor
                stream.SendNext(_isPerfectDefent);
                stream.SendNext(_gotMessage);
                stream.SendNext(_message);
                stream.SendNext(_opCannotActiveItem);
                stream.SendNext(_attackAndAttacked[0]);
                stream.SendNext(_attackAndAttacked[1]);
                for (int i = 0; i < _activeAndActived.Length; i++)
                {
                    stream.SendNext(_activeAndActived[i]);
                }
                stream.SendNext(_isActiveItem);
                for (int i = 0; i < _characterZone.Length; i++)
                {
                    stream.SendNext(_characterZone[i]);
                }
                for (int i = 0; i < _equipmentZone.Length; i++)
                {
                    stream.SendNext(_equipmentZone[i]);
                }
                stream.SendNext(isRandNumber);
                stream.SendNext(_randNumber);
                for (int i = 0; i < _deckMagnusName.Length; i++)
                {
                    stream.SendNext(_deckMagnusName[i]);
                }
                for (int i = 0; i < _MagnusActive.Length; i++)
                {
                    stream.SendNext(_MagnusActive[i]);
                }

                for (int i = 0; i < _gy.Length; i++)
                {
                    stream.SendNext(_gy[i]);
                }
                //Int 
                stream.SendNext(_canAttackTimes);
                //Bool
                stream.SendNext(_isPreparationEnd);
                stream.SendNext(_isNextP2Turn);
                stream.SendNext(_GetDame);
            }
            else
            {
                //RPS Infor
                _RPSValue = (string)stream.ReceiveNext();
                //Counter Down
                _isCounterDown = (bool)stream.ReceiveNext();
                //User Infor
                _turn = (int)stream.ReceiveNext();
                _playerStep = (string)stream.ReceiveNext();
                _name = (string)stream.ReceiveNext();
                _counterDown = (int)stream.ReceiveNext();
                _HP = (int)stream.ReceiveNext();
                _nowCost = (int)stream.ReceiveNext();
                _maxCost = (int)stream.ReceiveNext();
                _isTurn = (bool)stream.ReceiveNext();
                _startCard = (string)stream.ReceiveNext();
                _deckStartNumber = (int)stream.ReceiveNext();
                _deckNumber = (int)stream.ReceiveNext();
                _deckMagnusNumber = (int)stream.ReceiveNext();
                _handCardNumber = (int)stream.ReceiveNext();
                //Field Infor
                _isPerfectDefent = (bool)stream.ReceiveNext();
                _gotMessage = (bool)stream.ReceiveNext();
                _message = (string)stream.ReceiveNext();
                _opCannotActiveItem = (bool)stream.ReceiveNext();
                _attackAndAttacked[0] = (string)stream.ReceiveNext();
                _attackAndAttacked[1] = (string)stream.ReceiveNext();
                for (int i = 0; i < _activeAndActived.Length; i++)
                {
                    _activeAndActived[i] = (string)stream.ReceiveNext();
                }
                _isActiveItem = (bool)stream.ReceiveNext();
                for (int i = 0; i < _characterZone.Length; i++)
                {
                    _characterZone[i] = (string)stream.ReceiveNext();
                }
                for (int i = 0; i < _equipmentZone.Length; i++)
                {
                    _equipmentZone[i] = (string)stream.ReceiveNext();
                }
                isRandNumber = (bool)stream.ReceiveNext();
                _randNumber = (int)stream.ReceiveNext();
                for (int i = 0; i < _deckMagnusName.Length; i++)
                {
                    _deckMagnusName[i] = (string)stream.ReceiveNext();
                }
                for (int i = 0; i < _MagnusActive.Length; i++)
                {
                    _MagnusActive[i] = (bool)stream.ReceiveNext();
                }

                for (int i = 0; i < _gy.Length; i++)
                {
                    _gy[i] = (string)stream.ReceiveNext();
                }
                //Int
                _canAttackTimes = (int)stream.ReceiveNext();
                //Bool
                _isPreparationEnd = (bool)stream.ReceiveNext();
                _isNextP2Turn = (bool)stream.ReceiveNext();
                _GetDame = (bool)stream.ReceiveNext();
            }
        }
    }

}
