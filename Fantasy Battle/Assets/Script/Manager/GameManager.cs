using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    //User Information
    public static string _userName;
    public List<string> _deckStartName;
    public List<int> _deckStartAmount;
    public List<string> _deckEEName;
    public List<int> _deckEEAmount;
    public List<string> _deckItemName;
    public List<int> _deckItemAmount;
    public List<string> _deckMagnusName;
    public List<int> _deckMagnusAmount;

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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DestroyChild(Transform parent)
    {
        foreach (Transform chil in parent)
        {
            Destroy(chil.gameObject);
        }
    }
}
