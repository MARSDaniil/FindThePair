using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UICombat : MonoBehaviour {

    public Transform cardsParent;

    [SerializeField] private TMP_Text m_playerIndex;
    [SerializeField] private TMP_Text m_collectedByPlayer;
    [SerializeField] private TMP_Text m_loopOfGame;

    [SerializeField] private UIGameOverPanel m_gameOverPanel;

    [SerializeField] private Button m_restartButton;
    [SerializeField] private Button m_lobbyButton;

    private void Start () {
        m_restartButton.onClick.AddListener(Restart);
        m_lobbyButton.onClick.AddListener(Lobby);
    }
    public void SetPlayerState(Player a_player) {
        m_playerIndex.text = $"Current Player: {(a_player.index + 1).ToString()}";
        m_collectedByPlayer.text =$"Collected pairs: {(a_player.collectedCount).ToString()}" ;
    }

    public void SetLoop(int a_loop) {
        m_loopOfGame.text = $"Current loop {(a_loop + 1).ToString()}";
    }
    

    public void GameOver() {
        m_gameOverPanel.Show();
        m_gameOverPanel.Init();
    }

    private void Restart() {
        if (GameInstance.Exist) {
            GameInstance.LoadingScene(_GHeart.Constants.Scenes.COMBAT);
        } else {
            SceneManager.LoadScene(_GHeart.Constants.Scenes.COMBAT);
        }
    }

    private void Lobby() {
        if (GameInstance.Exist) {
            GameInstance.LoadingScene(_GHeart.Constants.Scenes.LOBBY);
        } 
    }
}
