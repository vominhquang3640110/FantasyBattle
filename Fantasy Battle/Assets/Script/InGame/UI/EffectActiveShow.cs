using InGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectActiveShow : MonoBehaviour
{
    public static EffectActiveShow instance;

    public Image _activeCardImage;
    private Animator _animator;

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
    void Start()
    {
        _animator = GetComponent<Animator>(); 
    }

    public void OpenWindown(CardController cardImage)
    {
        if (cardImage._Type == "Magnus")
        {
            _activeCardImage.GetComponent<RectTransform>().sizeDelta = new Vector2(202.5f, 135);
        }
        else
        {
            _activeCardImage.GetComponent<RectTransform>().sizeDelta = new Vector2(135, 202.5f);
        }
        if (cardImage.frontCardImage != null)
        {
            _activeCardImage.sprite = cardImage.frontCardImage;
        }
        else
        {
            _activeCardImage.sprite = cardImage.GetComponent<Image>().sprite;
        }
        _animator.SetBool("isOpen", true);
    }
    public void CloseWindown(CardController cardImage)
    {
        if (cardImage.frontCardImage != null)
        {
            _activeCardImage.sprite = cardImage.frontCardImage;
        }
        else
        {
            _activeCardImage.sprite = cardImage.GetComponent<Image>().sprite;
        }
        _animator.SetBool("isOpen", false);
    }
    public IEnumerator OpenAndCloseWindown(Sprite cardImage)
    {
        _activeCardImage.sprite = cardImage;
        _animator.SetBool("isOpen", true);
        yield return new WaitForSeconds(2);
        _animator.SetBool("isOpen", false);
    }
}
