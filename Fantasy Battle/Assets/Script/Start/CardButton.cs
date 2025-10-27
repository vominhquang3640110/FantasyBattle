using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardButton : MonoBehaviour, IPointerEnterHandler
{
    PreparationManager preparationManager;
    public string _cardName;
    public string _cardType;
    public GameObject _myPrefab;

    // Start is called before the first frame update
    void Start()
    {
        preparationManager = PreparationManager.instance;
    }
    
    public void OnClickCard()
    {
        //Start Card
        List<string> deckStartName = preparationManager._deckStartName;
        List<int> deckStartAmount = preparationManager._deckStartAmount;
        if (_cardType == "Start" && (deckStartName.Count <= 0 || deckStartAmount[0] < preparationManager._cardStartLimit))
        {
            if (deckStartName.Count <= 0)
            {
                preparationManager._choicedStartText.text = "Start {1}";
                preparationManager.OnClickChoicedStartButton();
                deckStartName.Add(_cardName);
                deckStartAmount.Add(1);
                GameObject card = Instantiate(_myPrefab, Vector3.zero, Quaternion.identity);
                card.transform.SetParent(preparationManager._choicedStartContent.transform);
                card.transform.localScale = Vector3.one;
            }
            else
            {
                if (deckStartName[0] == _cardName && deckStartAmount[0] < preparationManager._cardStartLimit)
                {
                    preparationManager.OnClickChoicedStartButton();
                    deckStartAmount[0]++;
                    preparationManager._choicedStartText.text = "Start {" + deckStartAmount[0].ToString() + "}";
                }
            }
        }

        //EE Card
        List<string> deckEEName = preparationManager._deckEEName;
        List<int> deckEEAmount = preparationManager._deckEEAmount;
        if (_cardType == "EE")
        {
            int nowCardAmountOfDeck = preparationManager.CardOfDeckCount(deckEEAmount);
            int thisCardIndexOfDeck = preparationManager.FindCardToIndex(deckEEName, _cardName);
            if (nowCardAmountOfDeck < preparationManager._deckEELimit)
            {
                if (thisCardIndexOfDeck < 0)
                {
                    preparationManager.OnClickChoicedEEButton();
                    deckEEName.Add(_cardName);
                    deckEEAmount.Add(1);
                    GameObject card = Instantiate(_myPrefab, Vector3.zero, Quaternion.identity);
                    card.transform.SetParent(preparationManager._choicedEEContent.transform);
                    card.transform.localScale = Vector3.one;
                }
                else if (thisCardIndexOfDeck >= 0 && deckEEAmount[thisCardIndexOfDeck] < preparationManager._cardEELimit)
                {
                    preparationManager.OnClickChoicedEEButton();
                    deckEEAmount[thisCardIndexOfDeck]++;
                }
                preparationManager._choicedEEText.text = "EE {" + preparationManager.CardOfDeckCount(deckEEAmount).ToString() + "}";
            }
        }

        //Item Card
        List<string> deckItemName = preparationManager._deckItemName;
        List<int> deckItemAmount = preparationManager._deckItemAmount;
        if (_cardType == "Item")
        {
            int nowCardAmountOfDeck = preparationManager.CardOfDeckCount(deckItemAmount);
            int thisCardIndexOfDeck = preparationManager.FindCardToIndex(deckItemName, _cardName);
            if (nowCardAmountOfDeck < preparationManager._deckItemLimit)
            {
                if (thisCardIndexOfDeck < 0)
                {
                    preparationManager.OnClickChoicedItemButton();
                    deckItemName.Add(_cardName);
                    deckItemAmount.Add(1);
                    GameObject card = Instantiate(_myPrefab, Vector3.zero, Quaternion.identity);
                    card.transform.SetParent(preparationManager._choicedItemContent.transform);
                    card.transform.localScale = Vector3.one;
                }
                else if (thisCardIndexOfDeck >= 0 && deckItemAmount[thisCardIndexOfDeck] < preparationManager._cardItemLimt)
                {
                    preparationManager.OnClickChoicedItemButton();
                    deckItemAmount[thisCardIndexOfDeck]++;
                }
                preparationManager._choicedItemText.text = "Item {" + preparationManager.CardOfDeckCount(deckItemAmount).ToString() + "}";
            }
        }

        //Magnus Card
        List<string> deckMagnusName = preparationManager._deckMagnusName;
        List<int> deckMagnusAmount = preparationManager._deckMagnusAmount;
        if (_cardType == "Magnus")
        {
            int nowCardAmountOfDeck = preparationManager.CardOfDeckCount(deckMagnusAmount);
            int thisCardIndexOfDeck = preparationManager.FindCardToIndex(deckMagnusName, _cardName);
            if (nowCardAmountOfDeck < preparationManager._deckMagnusLimit)
            {
                if (thisCardIndexOfDeck < 0)
                {
                    preparationManager.OnClickChoicedMagnusButton();
                    deckMagnusName.Add(_cardName);
                    deckMagnusAmount.Add(1);
                    GameObject card = Instantiate(_myPrefab, Vector3.zero, Quaternion.identity);
                    card.transform.SetParent(preparationManager._choicedMagnusContent.transform);
                    card.transform.localScale = Vector3.one;
                }
                else if (thisCardIndexOfDeck >= 0 && deckMagnusAmount[thisCardIndexOfDeck] < preparationManager._cardMagnusLimt)
                {
                    preparationManager.OnClickChoicedMagnusButton();
                    deckMagnusAmount[thisCardIndexOfDeck]++;
                }
                preparationManager._choicedMagnusText.text = "Magnus {" + preparationManager.CardOfDeckCount(deckMagnusAmount).ToString() + "}";
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
