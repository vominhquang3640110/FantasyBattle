using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartManager : MonoBehaviour
{
    [SerializeField] private Text _nameInputText;
    [SerializeField] private Text _errorEnterText;

    void Start()
    {
        //PlayerPrefs.DeleteAll();
        string name = PlayerPrefs.GetString("UserName", "");
        if (name != "")
        {
            GameManager._userName = name;
            SceneManager.LoadScene("Preparation");
        }
    }

    public void CLickOKButton()
    {
        if (_nameInputText.text.Length <= 0 || _nameInputText.text.Length > 8)
        {
            _nameInputText.text = "";
            _errorEnterText.text = "Name 1~8";
        }
        else
        {
            GameManager._userName = _nameInputText.text;
            PlayerPrefs.SetString("UserName", GameManager._userName);
            SceneManager.LoadScene("Preparation");
        }
    }
}
