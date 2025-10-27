using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PreparationManager : MonoBehaviour
{
    public static PreparationManager instance;

    public Text _userName;
    string _evo = "";

    //Choice Deck Button
    public Button _StartButton;
    public Button _ItemButton;
    public Button _MagnusButton;
    public Button _AssassinButton;
    public Button _SlimeButton;
    //Choice List 
    public GameObject _choiceStart;
    public GameObject _choiceItem;
    public GameObject _choiceMagnus;
    public GameObject _choiceAssassin;
    public GameObject _choiceSlime;

    //Choiced Deck Button
    public Button _ChoicedStartButton;
    public Button _ChoicedItemButton;
    public Button _ChoicedMagnusButton;
    public Button _ChoicedEEButton;
    //Choice List 
    public GameObject _choicedStart;
    public GameObject _choicedItem;
    public GameObject _choicedMagnus;
    public GameObject _choicedEE;
    //Choiced List Content
    public GameObject _choicedStartContent;
    public GameObject _choicedItemContent;
    public GameObject _choicedMagnusContent;
    public GameObject _choicedEEContent;
    //Choiced List Text
    public Text _choicedStartText;
    public Text _choicedItemText;
    public Text _choicedMagnusText;
    public Text _choicedEEText;

    public List<string> _deckStartName = new List<string>();
    public List<int> _deckStartAmount = new List<int>();
    public List<string> _deckEEName = new List<string>();
    public List<int> _deckEEAmount = new List<int>();
    public List<string> _deckItemName = new List<string>();
    public List<int> _deckItemAmount = new List<int>();
    public List<string> _deckMagnusName = new List<string>();
    public List<int> _deckMagnusAmount = new List<int>();

    //Limit Card
    public int _cardStartLimit = 4;
    public int _cardEELimit;
    public int _cardItemLimt;
    public int _cardMagnusLimt;
    public int _deckEELimit;
    public int _deckItemLimit;
    public int _deckMagnusLimit;

    //Choiced Card Prefab
    //Start
    public GameObject _Mukurin;
    public GameObject _Slime;
    //Item
    public GameObject _BloodCuffs;
    public GameObject _Bread;
    public GameObject _CursedBook;
    public GameObject _ItemBackpack;
    public GameObject _MysteriousBackpack;
    public GameObject _MagicRing;
    public GameObject _ManaBottle;
    public GameObject _MysteriousPendulum;
    public GameObject _RoyalShield;
    public GameObject _TheHopeDice;
    //Magnus
    public GameObject _THFH;
    public GameObject _GOA;
    //Assassin
    public GameObject _AssassinMain;
    public GameObject _BloodMask;
    public GameObject _HiredAssassin;
    public GameObject _LonelyWolf;
    public GameObject _Sniper;
    public GameObject _Barret;
    public GameObject _BladeDance;
    public GameObject _EdgeSword;
    public GameObject _NightBlade;
    public GameObject _ScytheBloody; 
    public GameObject _Stiletto;
    //Slime
    public GameObject _AquaSlime;
    public GameObject _AxitSlime;
    public GameObject _IceSlime;
    public GameObject _ShiledSlime;
    public GameObject _SoulSlime;
    public GameObject _MergeSword;
    public GameObject _PowerGloves;
    public GameObject _SlimeOfEyes;
    public GameObject _SonicBoom;
    public GameObject _TwinIceSwords;

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
        _userName.text = GameManager._userName;
        OnClickChoiceStartButton();
        OnClickChoicedStartButton();
        ChoiceCardTypeButtonOff();
        GetSavedData();
    }

    // Update is called once per frame
    void Update()
    {
        if (_deckStartAmount.Count > 0 && _evo == "")
        {
            if (_deckStartAmount[0] >= 4 && _deckStartName[0] == "Mukurin")
            {
                _AssassinButton.enabled = true;
                _ItemButton.enabled = true;
                _MagnusButton.enabled = true;
                _AssassinButton.GetComponent<Image>().color = Color.white;
                _ItemButton.GetComponent<Image>().color = Color.white;
                _MagnusButton.GetComponent<Image>().color= Color.white;
                _evo = "Assassin";
            }
            else if (_deckStartAmount[0] >= 4 && _deckStartName[0] == "Slime")
            {
                _SlimeButton.enabled = true;
                _ItemButton.enabled = true;
                _MagnusButton.enabled = true;
                _SlimeButton.GetComponent<Image>().color = Color.white;
                _ItemButton.GetComponent<Image>().color = Color.white;
                _MagnusButton.GetComponent<Image>().color = Color.white;
                _evo = "Slime";
            }
        }
        if (_deckStartAmount.Count > 0 && _deckStartAmount[0] < 4)
        {
            OnClickChoiceStartButton();
            ChoiceCardTypeButtonOff();
            _evo = "";
        }
    }

    public void ChoiceCardTypeButtonOff()
    {
        _StartButton.enabled = true;
        _ItemButton.enabled = false;
        _MagnusButton.enabled = false;
        _AssassinButton.enabled = false;
        _SlimeButton.enabled = false;
    }
    public int CardOfDeckCount(List<int> deckAmount)
    {
        int _return = 0;
        foreach (int amount in deckAmount)
        {
            _return += amount;
        }
        return _return;
    }
    public int FindCardToIndex(List<string> deckName, string cardName)
    {
        int index = -1;
        for (int i = 0; i < deckName.Count; i++)
        {
            if (cardName == deckName[i])
            {
                index = i;
            }
        }
        return index;
    }

    //Choice Button Active
    public void OnClickChoiceStartButton()
    {
        _StartButton.GetComponent<Image>().color = Color.gray;
        if (_deckStartAmount.Count > 0 && _deckStartAmount[0] >= _cardStartLimit)
        {
            _ItemButton.GetComponent<Image>().color = Color.white;
            _MagnusButton.GetComponent<Image>().color = Color.white;
            if (_evo == "Assassin")
            {
                _AssassinButton.GetComponent<Image>().color = Color.white;
            }
            if (_evo == "Slime")
            {
                _SlimeButton.GetComponent<Image>().color = Color.white;
            }
        }
        else if (_deckStartAmount.Count <= 0 || (_deckStartAmount.Count > 0 && _deckStartAmount[0] < _cardStartLimit))
        {
            _ItemButton.GetComponent<Image>().color = Color.black;
            _MagnusButton.GetComponent<Image>().color = Color.black;
            _AssassinButton.GetComponent<Image>().color = Color.black;
            _SlimeButton.GetComponent<Image>().color = Color.black;
        }

        _choiceStart.SetActive(true);
        _choiceItem.SetActive(false);
        _choiceMagnus.SetActive(false);
        _choiceAssassin.SetActive(false);
        _choiceSlime.SetActive(false);
    }
    public void OnClickChoiceItemButton()
    {
        _StartButton.GetComponent<Image>().color = Color.white;
        _ItemButton.GetComponent<Image>().color = Color.gray;
        _MagnusButton.GetComponent <Image>().color = Color.white;
        if (_evo == "Assassin")
        {
            _AssassinButton.GetComponent<Image>().color = Color.white;
        }
        if (_evo == "Slime")
        {
            _SlimeButton.GetComponent<Image>().color = Color.white;
        }

        _choiceStart.SetActive(false);
        _choiceItem.SetActive(true);
        _choiceMagnus.SetActive(false);
        _choiceAssassin.SetActive(false);
        _choiceSlime.SetActive(false);
    }

    public void OnClickChoiceMagnusButton()
    {
        _StartButton.GetComponent<Image>().color = Color.white;
        _ItemButton.GetComponent<Image>().color = Color.white;
        _MagnusButton.GetComponent<Image>().color = Color.gray;
        if (_evo == "Assassin")
        {
            _AssassinButton.GetComponent<Image>().color = Color.white;
        }
        if (_evo == "Slime")
        {
            _SlimeButton.GetComponent<Image>().color = Color.white;
        }

        _choiceStart.SetActive(false);
        _choiceItem.SetActive(false);
        _choiceMagnus.SetActive(true);
        _choiceAssassin.SetActive(false);
        _choiceSlime.SetActive(false);
    }

    public void OnClickChoiceAssassinButton()
    {
        _StartButton.GetComponent<Image>().color = Color.white;
        _ItemButton.GetComponent<Image>().color = Color.white;
        _MagnusButton.GetComponent<Image>().color = Color.white;
        _AssassinButton.GetComponent<Image>().color = Color.gray;

        _choiceStart.SetActive(false);
        _choiceItem.SetActive(false);
        _choiceMagnus.SetActive(false);
        _choiceAssassin.SetActive(true);
        _choiceSlime.SetActive(false);
    }
    public void OnClickChoiceSlimeButton()
    {
        _StartButton.GetComponent<Image>().color = Color.white;
        _ItemButton.GetComponent<Image>().color = Color.white;
        _MagnusButton.GetComponent<Image>().color = Color.white;
        _SlimeButton.GetComponent<Image>().color = Color.gray;

        _choiceStart.SetActive(false);
        _choiceItem.SetActive(false);
        _choiceMagnus.SetActive(false);
        _choiceAssassin.SetActive(false);
        _choiceSlime.SetActive(true);
    }
    //Choiced Button Active
    public void OnClickChoicedStartButton()
    {
        _ChoicedStartButton.GetComponent<Image>().color = Color.gray;
        _ChoicedItemButton.GetComponent<Image>().color = Color.white;
        _ChoicedMagnusButton.GetComponent<Image>().color = Color.white;
        _ChoicedEEButton.GetComponent<Image>().color = Color.white;

        _choicedStart.SetActive(true);
        _choicedItem.SetActive(false);
        _choicedMagnus.SetActive(false);
        _choicedEE.SetActive(false);
    }
    public void OnClickChoicedItemButton()
    {
        _ChoicedStartButton.GetComponent<Image>().color = Color.white;
        _ChoicedItemButton.GetComponent<Image>().color = Color.gray;
        _ChoicedMagnusButton.GetComponent<Image>().color = Color.white;
        _ChoicedEEButton.GetComponent<Image>().color = Color.white;

        _choicedStart.SetActive(false);
        _choicedItem.SetActive(true);
        _choicedMagnus.SetActive(false);
        _choicedEE.SetActive(false);
    }
    public void OnClickChoicedMagnusButton()
    {
        _ChoicedStartButton.GetComponent<Image>().color = Color.white;
        _ChoicedItemButton.GetComponent<Image>().color = Color.white;
        _ChoicedMagnusButton.GetComponent <Image>().color = Color.gray;
        _ChoicedEEButton.GetComponent<Image>().color = Color.white;

        _choicedStart.SetActive(false);
        _choicedItem.SetActive(false);
        _choicedMagnus.SetActive(true);
        _choicedEE.SetActive(false);
    }
    public void OnClickChoicedEEButton()
    {
        _ChoicedStartButton.GetComponent<Image>().color = Color.white;
        _ChoicedItemButton.GetComponent<Image>().color = Color.white;
        _ChoicedMagnusButton.GetComponent<Image>().color = Color.white;
        _ChoicedEEButton.GetComponent<Image>().color = Color.gray;

        _choicedStart.SetActive(false);
        _choicedItem.SetActive(false);
        _choicedMagnus.SetActive(false);
        _choicedEE.SetActive(true);
    }

    public void OnClickSave()
    {
        int index;
        //Start
        if (_deckStartName.Count > 0)
        {
            PlayerPrefs.SetString("DeckStartName", _deckStartName[0]);
            PlayerPrefs.SetInt("DeckStartAmount", _deckStartAmount[0]);
            PlayerPrefs.SetInt("DeckStartNumber", 1);
        }

        //Item
        index = 0;
        foreach (string item in _deckItemName)
        {
            PlayerPrefs.SetString("DeckItemName" + index, item);
            index++;
        }
        index = 0;
        foreach (int item in _deckItemAmount)
        {
            PlayerPrefs.SetInt("DeckItemAmount" + index, item);
            index++;
        }
        PlayerPrefs.SetInt("DeckItemNumber", index);

        //Magnus
        index = 0;
        foreach (string item in _deckMagnusName)
        {
            PlayerPrefs.SetString("DeckMagnusName" + index, item);
            index++;
        }
        index = 0;
        foreach (int item in _deckMagnusAmount)
        {
            PlayerPrefs.SetInt("DeckMagnusAmount" + index, item);
            index++;
        }
        PlayerPrefs.SetInt("DeckMagnusNumber", index);

        //EE
        index = 0;
        foreach (string item in _deckEEName)
        {
            PlayerPrefs.SetString("DeckEEName" + index, item);
            index++;
        }
        index = 0;
        foreach (int item in _deckEEAmount)
        {
            PlayerPrefs.SetInt("DeckEEAmount" + index, item);
            index++;
        }
        PlayerPrefs.SetInt("DeckEENumber", index);
    }
    void GetSavedData()
    {
        int deckStartNumber = PlayerPrefs.GetInt("DeckStartNumber", 0);
        int deckItemNumber = PlayerPrefs.GetInt("DeckItemNumber", 0);
        int deckMagnusNumber = PlayerPrefs.GetInt("DeckMagnusNumber", 0);
        int deckEENumber = PlayerPrefs.GetInt("DeckEENumber", 0);

        if (deckStartNumber > 0)
        {
            _deckStartName.Add(PlayerPrefs.GetString("DeckStartName"));
            _deckStartAmount.Add(PlayerPrefs.GetInt("DeckStartAmount"));
        }
        if (deckItemNumber > 0)
        {
            for (int i = 0; i < deckItemNumber; i++)
            {
                _deckItemName.Add(PlayerPrefs.GetString("DeckItemName" + i));
                _deckItemAmount.Add(PlayerPrefs.GetInt("DeckItemAmount" + i));
            }
        }
        if (deckMagnusNumber > 0)
        {
            for (int i = 0; i < deckMagnusNumber; i++)
            {
                _deckMagnusName.Add(PlayerPrefs.GetString("DeckMagnusName" + i));
                _deckMagnusAmount.Add(PlayerPrefs.GetInt("DeckMagnusAmount" + i));
            }
        }
        if (deckEENumber > 0)
        {
            for (int i = 0; i < deckEENumber; i++)
            {
                _deckEEName.Add(PlayerPrefs.GetString("DeckEEName" + i));
                _deckEEAmount.Add(PlayerPrefs.GetInt("DeckEEAmount" + i));
            }
        }
        CreateCardFromSavedData(deckStartNumber, deckItemNumber, deckMagnusNumber, deckEENumber);
    }
    void CreateCardFromSavedData(int deckStartNumber, int deckItemNumber, int deckMagnusNumber, int deckEENumber)
    {
        if (deckStartNumber > 0)
        {
            GameObject card = Instantiate(FindPrefab(_deckStartName[0]), Vector3.zero, Quaternion.identity);
            card.transform.SetParent(_choicedStartContent.transform);
            card.transform.localScale = Vector3.one;
            _choicedStartText.text = "Start {" + CardOfDeckCount(_deckStartAmount).ToString() + "}";
        }
        if (deckItemNumber > 0)
        {
            for (int i = 0; i < deckItemNumber; i++)
            {
                GameObject card = Instantiate(FindPrefab(_deckItemName[i]), Vector3.zero, Quaternion.identity);
                card.transform.SetParent(_choicedItemContent.transform);
                card.transform.localScale = Vector3.one;
            }
            _choicedItemText.text = "Item {" + CardOfDeckCount(_deckItemAmount).ToString() + "}";
        }
        if (deckMagnusNumber > 0)
        {
            for (int i = 0; i < deckMagnusNumber; i++)
            {
                GameObject card = Instantiate(FindPrefab(_deckMagnusName[i]), Vector3.zero, Quaternion.identity);
                card.transform.SetParent(_choicedMagnusContent.transform);
                card.transform.localScale = Vector3.one;
            }
            _choicedMagnusText.text = "Magnus {" + CardOfDeckCount(_deckMagnusAmount).ToString() + "}";
        }
        if (deckEENumber > 0)
        {
            for (int i = 0; i < deckEENumber; i++)
            {
                GameObject card = Instantiate(FindPrefab(_deckEEName[i]), Vector3.zero, Quaternion.identity);
                card.transform.SetParent(_choicedEEContent.transform);
                card.transform.localScale = Vector3.one;
            }
            _choicedEEText.text = "EE {" + CardOfDeckCount(_deckEEAmount).ToString() + "}";
        }
    }
    GameObject FindPrefab(string cardName)
    {
        //Start
        if (cardName == "Mukurin")
        {
            return _Mukurin;
        }
        else if (cardName == "Slime")
        {
            return _Slime;
        }
        //Item
        else if (cardName == "Blood Cuffs (Item)")
        {
            return _BloodCuffs;
        }
        else if (cardName == "Bread (Item)")
        {
            return _Bread;
        }
        else if (cardName == "Cursed Book (Item)")
        {
            return _CursedBook;
        }
        else if (cardName == "Item Backpack (Item)")
        {
            return _ItemBackpack;
        }
        else if (cardName == "Mysterious Backpack (Item)")
        {
            return _MysteriousBackpack;
        }
        else if (cardName == "Magic Ring (Item)")
        {
            return _MagicRing;
        }
        else if (cardName == "Mana Bottle (Item)")
        {
            return _ManaBottle;
        }
        else if (cardName == "Mysterious Pendulum (Item)")
        {
            return _MysteriousPendulum;
        }
        else if (cardName == "Royal Shield (Item)")
        {
            return _RoyalShield;
        }
        else if (cardName == "The Hope Dice (Item)")
        {
            return _TheHopeDice;
        }
        //Magnus
        else if (cardName == "THFH (M)")
        {
            return _THFH;
        }
        else if (cardName == "GOA (M)")
        {
            return _GOA;
        }
        //Assassin
        else if (cardName == "Assassin Main")
        {
            return _AssassinMain;
        }
        else if (cardName == "Blood Mask")
        {
            return _BloodMask;
        }
        else if (cardName == "Hired Assassin")
        {
            return _HiredAssassin;
        }
        else if (cardName == "Lonely Wolf")
        {
            return _LonelyWolf;
        }
        else if (cardName == "Sniper")
        {
            return _Sniper;
        }
        else if (cardName == "Barret (E)")
        {
            return _Barret;
        }
        else if (cardName == "Blade Dance (E)")
        {
            return _BladeDance;   
        }
        else if (cardName == "Edge Sword (E)")
        {
            return _EdgeSword;
        }
        else if (cardName == "Night Blade (E)")
        {
            return _NightBlade;
        }
        else if (cardName == "Scythe Bloody (E)")
        {
            return _ScytheBloody;
        }
        else if (cardName == "Stiletto (E)")
        {
            return _Stiletto;
        }
        //Slime
        else if (cardName == "Aqua Slime")
        {
            return _AquaSlime;
        }
        else if (cardName == "Axit Slime")
        {
            return _AxitSlime;
        }
        else if (cardName == "Ice Slime")
        {
            return _IceSlime;
        }
        else if (cardName == "Shiled Slime")
        {
            return _ShiledSlime;
        }
        else if (cardName == "Soul Slime")
        {
            return _SoulSlime;
        }
        else if (cardName == "Merge Sword (E)")
        {
            return _MergeSword;
        }
        else if (cardName == "Power Gloves (E)")
        {
            return _PowerGloves;
        }
        else if (cardName == "Slime Of Eyes (E)")
        {
            return _SlimeOfEyes;
        }
        else if (cardName == "Sonic Boom (E)")
        {
            return _SonicBoom;
        }
        else if (cardName == "Twin Ice Swords (E)")
        {
            return _TwinIceSwords;
        }
        return null;
    }
    public void OnClickPlay()
    {
        if (_deckStartAmount.Count > 0 && _deckItemAmount.Count > 0 && _deckMagnusAmount.Count > 0 && _deckEEAmount.Count > 0)
        {
            int deckStartNumber = CardOfDeckCount(_deckStartAmount);
            int deckItemNumber = CardOfDeckCount(_deckItemAmount);
            int deckMagnusNumber = CardOfDeckCount(_deckMagnusAmount);
            int deckEENumber = CardOfDeckCount(_deckEEAmount);
            if (deckStartNumber >= _cardStartLimit && deckItemNumber >= _deckItemLimit && deckMagnusNumber >= _deckMagnusLimit && deckEENumber >= _deckEELimit)
            {
                OnClickSave();
                GameManager.instance._deckStartName = _deckStartName;
                GameManager.instance._deckStartAmount = _deckStartAmount;
                GameManager.instance._deckItemName = _deckItemName;
                GameManager.instance._deckItemAmount = _deckItemAmount;
                GameManager.instance._deckMagnusName = _deckMagnusName;
                GameManager.instance._deckMagnusAmount = _deckMagnusAmount;
                GameManager.instance._deckEEName = _deckEEName;
                GameManager.instance._deckEEAmount = _deckEEAmount;
                Destroy(gameObject);
                SceneManager.LoadScene("Matching");
            }
        }
    }
}
