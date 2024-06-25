using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameParametrs;

public class ChooseDiffuculty : Choose
{
    protected override void BeginGame() {

        if (GameInstance.Exist) {

            Difficult difficult = GameInstance.levelDiffucul;


            m_currentIndex = (int)difficult;
        }
        m_title.text = "Diffuclty";
        base.BeginGame();
    }

    protected override void Next() {
        base.Next();
        if(GameInstance.Exist) {
            GameInstance.levelDiffucul = (Difficult)(m_currentIndex);
        }
    }

    protected override void Previous() {
        base.Previous();
        if (GameInstance.Exist) {
            GameInstance.levelDiffucul = (Difficult)(m_currentIndex);
        }
    }
}
