using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour {

    #region Fields

    [SerializeField] private Button m_cardButton;
    [SerializeField] private SpriteRenderer m_backCardSpriteRender;
    [SerializeField] private SpriteRenderer m_frontCardSpriteRender;
    [SerializeField] private CardState m_cardState;
    public CardState cardState {
        get {
            return m_cardState;
        }
    }

    private bool m_canRotate = true;

    #endregion

    #region Unity Methods
    
    private void Start () {
        m_cardButton.onClick.AddListener(OpenCard);
    }

    #endregion


    #region Helpers


    private void OpenCard () {
        if(m_canRotate && CombatGameMode.Exist && CombatGameMode.I.canRotateCard){

            CombatGameMode.I.SetCard(this);

            gameObject.transform.DORotate(new Vector3(0, 180, 0), 1f);
            m_canRotate = false;
        }
    }

    #endregion

    #region Public
    public void Init(CardState a_state) {
        m_cardState = a_state;

        if(m_cardState.sprite != null) {
            m_frontCardSpriteRender.sprite = cardState.sprite;
        }

        gameObject.name = $"Card {m_cardState.id}";
    }

    public void CloseCard() {

        gameObject.transform.DORotate(new Vector3(0, 0, 0), 1f);
        m_canRotate = true;
    }
    
    public void DELETE() {
        gameObject.transform.DORotate(new Vector3(0,180, 0), 1f);
    }

    public void SetBack(Sprite a_sprite) {
        m_backCardSpriteRender.sprite = a_sprite;
    }
    public void FindedCard() {
        Color emptyColor = Color.white;
        emptyColor.a = 0;
        Color halfEmptyColor = Color.white;
        halfEmptyColor.a = 0.5f;

        m_backCardSpriteRender.DOColor(emptyColor, 0);
        m_frontCardSpriteRender.DOColor(halfEmptyColor, 1f);
    }

    public void DeleteTHisMethdod() {
        gameObject.transform.DORotate(new Vector3(0, 180, 0), 0f);
    }

    #endregion
    [Serializable]
    public struct CardState {
        public int id;
        public Sprite sprite;
    }
}
