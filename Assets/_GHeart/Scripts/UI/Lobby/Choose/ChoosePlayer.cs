using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoosePlayer : Choose {

    protected override void BeginGame() {

        if (GameInstance.Exist) {

            m_currentIndex = GameInstance.countOfPlayer - 1;
        }
        m_title.text = "Choose count of player";
        base.BeginGame();
    }

    protected override void Next() {
        base.Next();
        if (GameInstance.Exist) {
            GameInstance.countOfPlayer = m_currentIndex + 1;
        }
    }

    protected override void Previous() {
        base.Previous();
        if (GameInstance.Exist) {
            GameInstance.countOfPlayer = m_currentIndex + 1;
        }
    }

}
