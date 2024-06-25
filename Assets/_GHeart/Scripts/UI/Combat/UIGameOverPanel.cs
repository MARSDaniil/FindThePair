using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HandyTools;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using UnityEngine.SceneManagement;
public class UIGameOverPanel : PopupPanel {

    [SerializeField] private Button m_goToLobby;
    [SerializeField] private Button m_restartGame;
    [SerializeField] private TMP_Text m_loopText;

    [SerializeField] private Transform LeaderBoard;


    [SerializeField] private LeaderboardItem m_itemObject;
    private List<LeaderboardItem> m_leaderboards = new List<LeaderboardItem>();
    private void Start() {
        m_goToLobby.onClick.AddListener(GoToLobby);
        m_restartGame.onClick.AddListener(RestartGame);
    }

    public void Init() {
        if (CombatGameMode.Exist) {


            m_loopText.text = $"Loops count: {CombatGameMode.I.loop}";

            var list = CombatGameMode.I.players.OrderByDescending(u => u.collectedCount).ToList();

            int place = 1;

            for (int i = 0; i < list.Count(); i++) {
                if (i > 0) {
                    if (list[i].collectedCount < list[i - 1].collectedCount) {
                        place++;
                    }
                }
                LeaderboardItem leaderboardItem = Instantiate(m_itemObject, LeaderBoard);
                leaderboardItem.Init(place, list[i].index, list[i].collectedCount);

                m_leaderboards.Add(leaderboardItem);

            }
        }
    }


    private void RestartGame() {
        if (GameInstance.Exist) {
            GameInstance.LoadingScene(_GHeart.Constants.Scenes.COMBAT);
        } else {
            SceneManager.LoadScene(_GHeart.Constants.Scenes.COMBAT);
        }
    }
    private void GoToLobby() {
        if (GameInstance.Exist) {
            GameInstance.LoadingScene(_GHeart.Constants.Scenes.LOBBY);
        }
    }
}
