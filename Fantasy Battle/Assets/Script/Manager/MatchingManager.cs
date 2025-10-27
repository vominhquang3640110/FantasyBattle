using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MatchingManager : MonoBehaviour
{
    [SerializeField] Button _matchingButton;
    [SerializeField] Text _matchingText;
    private bool _isMatching;

    void Start()
    {
        _isMatching = false;
        _matchingText.gameObject.SetActive(false);
        _matchingButton.gameObject.SetActive(true);
    }

    void Update()
    {
        if (_isMatching)
        {
            _isMatching = false;
            _matchingText.gameObject.SetActive(true);
            StartCoroutine(MatchingTextAnimation());
        }

        //Game Start
        if (PhotonNetwork.playerList.Length == 2)
        {
            PhotonNetwork.LoadLevel("InGame");
        }
    }

    public void OnCLickMachingButton()
    {
        _isMatching = true;
        _matchingButton.gameObject.SetActive(false);
        StartCoroutine(WaitMatching());

    }
    IEnumerator WaitMatching()
    {
        yield return new WaitForSeconds(3);
        PhotonNetwork.JoinRandomRoom();
    }
    IEnumerator MatchingTextAnimation()
    {
        _matchingText.text = "Matching";
        yield return new WaitForSeconds(0.5f);
        _matchingText.text = "Matching.";
        yield return new WaitForSeconds(0.5f);
        _matchingText.text = "Matching..";
        yield return new WaitForSeconds(0.5f);
        _matchingText.text = "Matching...";
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(MatchingTextAnimation());
    }

    public void OnClickBackHomeButton()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene("Preparation");
    }
}
