using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LeaderboardItem : MonoBehaviour {


    [SerializeField] private TMP_Text m_countPair;
    [SerializeField] private TMP_Text m_name;
    [SerializeField] private TMP_Text m_place;


    public void Init(int a_place, int a_nameIndex, int a_countPair) {
        m_name.text = $"Player {a_nameIndex}";
        m_countPair.text = a_countPair.ToString();
        m_place.text = a_place.ToString();
    }
}
