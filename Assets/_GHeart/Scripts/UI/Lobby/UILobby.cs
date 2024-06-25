using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILobby : MonoBehaviour {


    [SerializeField] Button m_combatButton;


    private void Start () {
        m_combatButton.onClick.AddListener(StartCombat);
    }

    private void StartCombat() {
        if (GameInstance.Exist) {
            GameInstance.LoadingScene(_GHeart.Constants.Scenes.COMBAT);
        } 
    }
}
