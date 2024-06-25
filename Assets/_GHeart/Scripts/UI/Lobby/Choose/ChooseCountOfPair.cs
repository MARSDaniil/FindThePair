using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameParametrs;

public class ChooseCountOfPair : Choose {
    protected override void BeginGame() {

        if (GameInstance.Exist && Config.Exist) {

            m_currentIndex = GameInstance.countOfPair - Config.I.gameParametrs.minPairCount;
        }
        m_title.text = "Choose count of pair";
        base.BeginGame();
    }

    protected override void Next() {
        base.Next();
        if (GameInstance.Exist && Config.Exist) {
            GameInstance.countOfPair = m_currentIndex + Config.I.gameParametrs.minPairCount;
        }
    }

    protected override void Previous() {
        base.Previous();
        if (GameInstance.Exist && Config.Exist) {
            GameInstance.countOfPair = m_currentIndex + Config.I.gameParametrs.minPairCount;
        }
    }
}
