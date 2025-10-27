using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChoicedCard : MonoBehaviour, IPointerEnterHandler
{
    PreparationManager preparationManager;
    GameManager gameManager;
    public string _cardName;
    public string _cardType;
    public Text _cardAmountText;

    List<string> deckStartName;
    List<int> deckStartAmount;
    List<string> deckEEName;
    List<int> deckEEAmount;
    List<string> deckItemName;
    List<int> deckItemAmount;
    List<string> deckMagnusName;
    List<int> deckMagnusAmount;

    // Start is called before the first frame update
    void Start()
    {
        preparationManager = PreparationManager.instance;
        gameManager = GameManager.instance;
        deckStartName = preparationManager._deckStartName;
        deckStartAmount = preparationManager._deckStartAmount;
        deckEEName = preparationManager._deckEEName;
        deckEEAmount = preparationManager._deckEEAmount;
        deckItemName = preparationManager._deckItemName;
        deckItemAmount = preparationManager._deckItemAmount;
        deckMagnusName = preparationManager._deckMagnusName;
        deckMagnusAmount = preparationManager._deckMagnusAmount;
    }

    // Update is called once per frame
    void Update()
    {
        if (_cardType == "Start")
        {
            int thisCardIndexOfDeck = preparationManager.FindCardToIndex(deckStartName, _cardName);
            if (thisCardIndexOfDeck >= 0)
            {
                _cardAmountText.text = deckStartAmount[thisCardIndexOfDeck].ToString();
            }
        }
        else if (_cardType == "Item")
        {
            int thisCardIndexOfDeck = preparationManager.FindCardToIndex(deckItemName, _cardName);
            if (thisCardIndexOfDeck >= 0)
            {
                _cardAmountText.text = deckItemAmount[thisCardIndexOfDeck].ToString();
            }
        }
        else if (_cardType == "Magnus")
        {
            int thisCardIndexOfDeck = preparationManager.FindCardToIndex(deckMagnusName, _cardName);
            if (thisCardIndexOfDeck >= 0)
            {
                _cardAmountText.text = deckMagnusAmount[thisCardIndexOfDeck].ToString();
            }
        }
        else if (_cardType == "EE")
        {
            int thisCardIndexOfDeck = preparationManager.FindCardToIndex(deckEEName, _cardName);
            if (thisCardIndexOfDeck >= 0)
            {
                _cardAmountText.text = deckEEAmount[thisCardIndexOfDeck].ToString();
            }
        }
    }

    public void OnCLickCard()
    {
        //Start Card
        if (_cardType == "Start")
        {
            int thisCardIndexOfDeck = preparationManager.FindCardToIndex(deckStartName, _cardName);
            if (thisCardIndexOfDeck >= 0)
            {
                deckStartAmount[thisCardIndexOfDeck]--;
                preparationManager._choicedStartText.text = "Start {"+ deckStartAmount[thisCardIndexOfDeck].ToString() +"}";
                if (deckStartAmount[thisCardIndexOfDeck] < preparationManager._cardStartLimit)
                {
                    if (deckStartAmount[thisCardIndexOfDeck] <= 0)
                    {
                        deckStartName.RemoveAt(thisCardIndexOfDeck);
                        deckStartAmount.RemoveAt(thisCardIndexOfDeck);
                        gameManager.DestroyChild(preparationManager._choicedStartContent.transform);
                    }
                    deckMagnusName.Clear();
                    deckMagnusAmount.Clear();
                    deckItemName.Clear();
                    deckItemAmount.Clear();
                    deckEEName.Clear();
                    deckEEAmount.Clear();
                    gameManager.DestroyChild(preparationManager._choicedMagnusContent.transform);
                    gameManager.DestroyChild(preparationManager._choicedItemContent.transform);
                    gameManager.DestroyChild(preparationManager._choicedEEContent.transform);
                    preparationManager._choicedMagnusText.text = "Magnus {0}";
                    preparationManager._choicedItemText.text = "Item {0}";
                    preparationManager._choicedEEText.text = "EE {0}";
                }
            }
        }

        //Item Card
        if (_cardType == "Item")
        {
            int thisCardIndexOfDeck = preparationManager.FindCardToIndex(deckItemName, _cardName);
            if (thisCardIndexOfDeck >= 0)
            {
                deckItemAmount[thisCardIndexOfDeck]--;
                preparationManager._choicedItemText.text = "Item {" + preparationManager.CardOfDeckCount(deckItemAmount).ToString() + "}";
                if (deckItemAmount[thisCardIndexOfDeck] <= 0)
                {
                    deckItemName.RemoveAt(thisCardIndexOfDeck);
                    deckItemAmount.RemoveAt(thisCardIndexOfDeck);
                    Destroy(gameObject);
                }
            }
        }


        //Magnus Card
        if (_cardType == "Magnus")
        {
            int thisCardIndexOfDeck = preparationManager.FindCardToIndex(deckMagnusName, _cardName);
            if (thisCardIndexOfDeck >= 0)
            {
                deckMagnusAmount[thisCardIndexOfDeck]--;
                preparationManager._choicedMagnusText.text = "Magnus {" + preparationManager.CardOfDeckCount(deckMagnusAmount).ToString() + "}";
                if (deckMagnusAmount[thisCardIndexOfDeck] <= 0)
                {
                    deckMagnusName.RemoveAt(thisCardIndexOfDeck);
                    deckMagnusAmount.RemoveAt(thisCardIndexOfDeck);
                    Destroy(gameObject);
                }
            }
        }

        //EE Card
        if (_cardType == "EE")
        {
            int thisCardIndexOfDeck = preparationManager.FindCardToIndex(deckEEName, _cardName);
            if (thisCardIndexOfDeck >= 0)
            {
                deckEEAmount[thisCardIndexOfDeck]--;
                preparationManager._choicedEEText.text = "EE {" + preparationManager.CardOfDeckCount(deckEEAmount).ToString() + "}";
                if (deckEEAmount[thisCardIndexOfDeck] <= 0)
                {
                    deckEEName.RemoveAt(thisCardIndexOfDeck);
                    deckEEAmount.RemoveAt(thisCardIndexOfDeck);
                    Destroy(gameObject);
                }
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        RectTransform showRectTransform = ShowCard.showRectTransform;
        if (_cardType == "Magnus")
        {
            showRectTransform.sizeDelta = new Vector2(600, 400);
        }
        else
        {
            showRectTransform.sizeDelta = new Vector2(400, 600);
        }
        ShowCard.showSprite = GetComponent<Image>().sprite;
    }
}
