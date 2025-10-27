using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace InGame
{
    public class BattleStepCardController : MonoBehaviour
    {
        [Header("Manager And Controller")]
        public CardEffect _cardEffect;
        public CardController _cardController;
        private InGameManager _inGameManager;
        private StepAndDayManager _stepAndDayManager;
        private BattleManager _battleManager;

        [Header("Card Effect")]
        public bool _giveBurnByAttack = false;
        public bool _givePoisonByAttack = false;
        public bool _counterDame = false;
        public bool _counterDameFromEquip = false;
        public bool _penetration = false;

        [Header("P1 And P2 Information")]
        PlayerIFManager _P1;
        PlayerIFManager _P2;
        bool _GotIF = false;

        [Header("Int")]
        public int _downDamage = 0;

        [Header("Bool")]
        public bool _perfectionDeff = false;
        public bool _cannotAttack = false;
        public int _canAttackTimesMax = 1;
        public int _canAttackTimes = 1;
        bool _oldColorChangeP1 = true;
        bool _oldColorChangeP2 = true;
        bool _canAttakGet = false;
        bool _getDamageProcessOn = false;

        // Start is called before the first frame update
        void Start()
        {
            _inGameManager = InGameManager.instance;
            _stepAndDayManager = StepAndDayManager.instance;
            _battleManager = BattleManager.instance;
        }

        // Update is called once per frame
        void Update()
        {
            if (_perfectionDeff && ((gameObject.tag == "P1" && _stepAndDayManager._step == Step.Draw) || (gameObject.tag == "P2" && _P2._playerStep == "Draw")))
            {
                _perfectionDeff = false;
            }
            if (_cannotAttack && ((gameObject.tag == "P1" && _P2._playerStep == "Draw") || (gameObject.tag == "P2" && _stepAndDayManager._step == Step.Draw)))
            {
                _cannotAttack = false;
            }


            if (_inGameManager._isGetPlayerIF && !_GotIF)
            {
                _GotIF = true;
                _P1 = _inGameManager._P1;
                _P2 = _inGameManager._P2;
            }

            if (_P1 != null && _P2 != null)
            {
                //相手のターンのバトルステップの処理「相手が選択したカードの色を変更」
                if (_P2._playerStep == "Battle")
                {
                    _oldColorChangeP2 = false;
                    if (_cardController._zone != "")
                    {
                        string attackZone = "";
                        string attackedZone = "";
                        if (_P2._attackAndAttacked[0] != "")
                        {
                            attackZone = _P2._attackAndAttacked[0].Substring(0, 3) + "P2";
                        }
                        if (_P2._attackAndAttacked[1] != "")
                        {
                            attackedZone = _P2._attackAndAttacked[1].Substring(0, 3) + "P1";
                        }
                        if (attackZone == _cardController._zone || attackedZone == _cardController._zone)
                        {
                            GetComponent<Image>().color = Color.yellow;
                        }
                        else
                        {
                            GetComponent<Image>().color = Color.white;
                            Color color = GetComponent<Image>().color;
                            color.a = 1.3f;
                            GetComponent<Image>().color = color;
                        }
                    }
                }
                if (_stepAndDayManager._step == Step.Battle)
                {
                    _oldColorChangeP1 = false;
                }
                //相手のターンのバトルステップ終了時、カードの色をもとに戻す
                //if ((_P2._playerStep != "Battle" && !_oldColorChangeP2) || (_stepAndDayManager._step != Step.Battle && !_oldColorChangeP1))
                if ((_P2._attackAndAttacked[0] == "" && _P2._attackAndAttacked[1] == "" && !_oldColorChangeP2) || (_P1._attackAndAttacked[0] == "" && _P1._attackAndAttacked[1] == "" && !_oldColorChangeP1))
                {
                    _oldColorChangeP1 = true;
                    _oldColorChangeP2 = true;
                    GetComponent<Image>().color = Color.white;
                    Color color = GetComponent<Image>().color;
                    color.a = 1f;
                    GetComponent<Image>().color = color;
                    if (_stepAndDayManager._choiceCard != null)
                    {
                        _stepAndDayManager._choiceCard = null;
                    }
                }

                if (_stepAndDayManager._step == Step.Battle && !_canAttakGet)
                {
                    _canAttakGet = true;
                    if (_cannotAttack)
                    {
                        _canAttackTimes = 0;
                    }
                    else
                    {
                        _canAttackTimes = _canAttackTimesMax;
                    }
                }
                else if (_stepAndDayManager._step == Step.Draw && _canAttakGet)
                {
                    _canAttakGet = false;
                }


                //相手のターンのバトルステップの処理
                OPBattleStep();
                //自分のターンのバトルステップ処理
                MYBattleStep();
                if (!_getDamageProcessOn && _battleManager._attackCard != null && _battleManager._attackedCard != null
                    && (_battleManager._attackCard.GetComponent<CardController>()._zone == _cardController._zone || _battleManager._attackCard.GetComponent<CardController>()._zone == _cardController._zone))
                {
                    _getDamageProcessOn = true;
                    StartCoroutine(GetDameProcess());
                }
                if (_getDamageProcessOn && _battleManager._attackCard == null && _battleManager._attackedCard == null)
                {
                    _getDamageProcessOn = false;
                }
            }
            
        }

        void MYBattleStep()
        {
            if (_battleManager._isAttacked && _battleManager._isAttackedZoneName != _cardController._zone && _cardController._zone != "")
            {
                _canAttackTimes = 0;
            }

            //相手がダメージを受けた
            if (_P2._GetDame && _P1._attackAndAttacked[0] != "" && _P1._attackAndAttacked[1] != "")
            {
                _battleManager._isAttacked = true;
                _battleManager._isAttackedZoneName = _P1._attackAndAttacked[0];
                _P1._attackAndAttacked[0] = "";
                _P1._attackAndAttacked[1] = "";
            }
        }
        void OPBattleStep()
        {
            //相手のターンの攻撃するカードと攻撃されるカードを自分のマネージャーに設定
            //攻撃するカード
            if (_battleManager._attackCardZone == _cardController._zone && _cardController._zone != "" && _cardController._Type != "Start")
            {
                _battleManager._attackCard = gameObject;
                _cardController._isTouch = true;
            }
            //攻撃されるカード
            if (_battleManager._attackedCardZone == _cardController._zone && _cardController._zone != "" && _cardController._Type != "Start")
            {
                _battleManager._attackedCard = gameObject;
            }
        }
        IEnumerator GetDameProcess()
        {
            //攻撃するカードと攻撃されるカードが決まった時、ダメージを受ける処理
            if (_battleManager._attackCard != null && _battleManager._attackedCard != null && !_P1._GetDame)
            {
                CardController attackCardControll = _battleManager._attackCard.GetComponent<CardController>();
                CardController attackedCardControll = _battleManager._attackedCard.GetComponent<CardController>();
                
                attackCardControll._isTouch = true;

                _battleManager._attackCard.GetComponent<BattleStepCardController>()._canAttackTimes--;
                _P1._canAttackTimes--;

                yield return new WaitForSeconds(0.5f);

                if (!attackedCardControll.GetComponent<BattleStepCardController>()._perfectionDeff && !(attackedCardControll.gameObject.tag == "P1" && _P1._isPerfectDefent) && !(attackedCardControll.gameObject.tag == "P2" && _P2._isPerfectDefent))
                {
                    int giveDamage = attackCardControll._ATK - attackedCardControll.GetComponent<BattleStepCardController>()._downDamage;
                    //ダメージを受ける処理
                    if (attackCardControll.GetComponent<BattleStepCardController>()._penetration)
                    {
                        //貫通
                        GetDameToHP(giveDamage, attackedCardControll);
                    }
                    else
                    {
                        GetDame(giveDamage, attackedCardControll);
                    }
                    // やけどを与える処理
                    if (_battleManager._attackCard.GetComponent<BattleStepCardController>()._giveBurnByAttack
                        && !attackedCardControll.GetComponent<CardEffect>()._isBurn && attackCardControll.GetComponent<CardEffect>().CheckConditionspross())
                    {
                        attackCardControll.GetComponent<CardEffect>().thisCardActiveEffect = true;
                        attackedCardControll.GetComponent<CardEffect>().thisCardActiveTargetEffect = true;
                        attackedCardControll.GetComponent<CardEffect>()._isBurn = true;
                        yield return new WaitForSeconds(3);
                    }
                    // 毒を与える処理
                    if (_battleManager._attackCard.GetComponent<BattleStepCardController>()._givePoisonByAttack
                        && !attackedCardControll.GetComponent<CardEffect>()._isPoison && attackCardControll.GetComponent<CardEffect>().CheckConditionspross())
                    {
                        attackCardControll.GetComponent<CardEffect>().thisCardActiveEffect = true;
                        attackedCardControll.GetComponent<CardEffect>().thisCardActiveTargetEffect = true;
                        attackedCardControll.GetComponent<CardEffect>()._isPoison = true;
                        yield return new WaitForSeconds(3);
                    }
                    // Counter Dame
                    if (_battleManager._attackedCard.GetComponent<BattleStepCardController>()._counterDame
                        && attackedCardControll.GetComponent<CardEffect>().CheckConditionspross())
                    {
                        attackCardControll.GetComponent<CardEffect>().thisCardActiveTargetEffect = true;
                        attackedCardControll.GetComponent<CardEffect>().thisCardActiveEffect = true;
                        attackedCardControll.GetComponent<CardEffect>().ActiveEffectPross();
                        attackedCardControll.GetComponent<CardEffect>().ActivedEffectPross(attackCardControll);
                    }
                    //
                    if (_battleManager._attackedCard.GetComponent<BattleStepCardController>()._counterDameFromEquip
                       && attackedCardControll.EZ.GetComponentInChildren<CardEffect>().CheckConditionspross())
                    {
                        attackCardControll.GetComponent<CardEffect>().thisCardActiveTargetEffect = true;
                        attackedCardControll.EZ.GetComponentInChildren<CardEffect>().thisCardActiveEffect = true;
                        attackedCardControll.EZ.GetComponentInChildren<CardEffect>().ActiveEffectPross();
                        attackedCardControll.EZ.GetComponentInChildren<CardEffect>().ActivedEffectPross(attackCardControll);
                    }
                }

                if (_P1._canAttackTimes > 0)
                {
                    attackCardControll._isTouch = false;
                }
                
                yield return new WaitForSeconds(0.5f);

                _battleManager._attackCard = null;
                _battleManager._attackedCard = null;
                _battleManager._attackCardZone = "";
                _battleManager._attackedCardZone = "";
                

                //バトルステップを終了に
                if (_P1._canAttackTimes <= 0)
                {
                    _stepAndDayManager._battleEnd = true;
                    _stepAndDayManager._battleButton.SetActive(false);
                }
                _P1._GetDame = true;
            }
        }
        public void GetDame(int damage, CardController cardController)
        {
            int attackATK = damage;
            int attackedDEF = cardController._DEF;
            if (attackedDEF >= attackATK)
            {
                cardController._DEF -= attackATK;
            }
            else if (attackedDEF < attackATK)
            {
                attackATK -= attackedDEF;
                cardController._DEF = 0;
                cardController._HP -= attackATK;
            }

            if (cardController._HP <= 0)
            {
                cardController._HP = 0;
            }
        }
        public void GetDameToHP(int damage, CardController cardController)
        {
            cardController._HP -= damage;
            if (cardController._HP <= 0)
            {
                cardController._HP = 0;
            }
        }
        public void OnClick()
        {
            //攻撃するカードを選択
            if (_canAttackTimes > 0 && _stepAndDayManager._step == Step.Battle && _P1._attackAndAttacked[0] == "" && _P1._attackAndAttacked[1] == ""
                && _cardController._zone != "" && gameObject.tag == "P1" && _cardController._Type == "EE" && !_cardController._isEuqip)
            {
                //攻撃するカードをマネージャーに格納
                _battleManager._attackCard = gameObject;
                //攻撃するカードの色を変更
                StepAndDayManager.instance._choiceCard = gameObject;
                GetComponent<Image>().color = Color.yellow;
                //攻撃するカードの場所をP1IFに送信
                _P1._attackAndAttacked[0] = _cardController._zone;
                _P1._canAttackTimes = _canAttackTimes;


            }

            //攻撃されるカードを選択
            if (_stepAndDayManager._step == Step.Battle && _P1._attackAndAttacked[0] != "" && _P1._attackAndAttacked[1] == ""
                && _cardController._zone != "" && gameObject.tag == "P2" && _cardController._Type == "EE" && !_cardController._isEuqip)
            {
                //攻撃されるカードをマネージャーに格納
                _battleManager._attackedCard = gameObject;
                //攻撃されるカードの色を変更
                GetComponent<Image>().color = Color.red;
                //攻撃されるカードの場所をP1IFに送信
                _P1._attackAndAttacked[1] = _cardController._zone;
            }
        }
    }

}