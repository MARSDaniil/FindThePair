using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Choose : MonoBehaviour {

    [SerializeField] private Button m_next;
    [SerializeField] private Button m_prev;

    [SerializeField] List<GameObject> m_list;
    protected int m_currentIndex = 0;

    [SerializeField]protected TMP_Text m_title;
    private void Start() {
        m_next.onClick.AddListener(Next);
        m_prev.onClick.AddListener(Previous);

        BeginGame();
    }

    protected virtual void BeginGame() {
        foreach (GameObject go in m_list) {
            go.SetActive(false);
        }
        m_list[m_currentIndex].SetActive(true);

        CheckItem();
    }

    protected virtual void Next() {
        m_list[m_currentIndex].gameObject.SetActive(false);
        m_currentIndex++;
        m_list[m_currentIndex].gameObject.SetActive(true);

        CheckItem();
    }
    protected virtual void Previous() {
        m_list[m_currentIndex].gameObject.SetActive(false);
        m_currentIndex--;
        m_list[m_currentIndex].gameObject.SetActive(true);
        CheckItem();
    }

    private void CheckItem() {
        m_next.gameObject.SetActive(true);
        m_prev.gameObject.SetActive(true);

        if (m_currentIndex + 1 == m_list.Count) {
            m_next.gameObject.SetActive(false);
            m_prev.gameObject.SetActive(true);
        }else if (m_currentIndex -1 < 0) {
            m_next.gameObject.SetActive(true);
            m_prev.gameObject.SetActive(false);
        }
    }

}
