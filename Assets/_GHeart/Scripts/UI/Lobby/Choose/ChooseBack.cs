using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameParametrs;

public class ChooseBack : Choose
{
    protected override void BeginGame() {

        if (GameInstance.Exist) {

            m_currentIndex = (int)GameInstance.backImagePack;
        }
        m_title.text = "Choose background of card";
        base.BeginGame();
    }

    protected override void Next() {
        base.Next();
        if (GameInstance.Exist) {
            GameInstance.backImagePack = (BackImagePack)(m_currentIndex);
        }
    }

    protected override void Previous() {
        base.Previous();
        if (GameInstance.Exist) {
            GameInstance.backImagePack = (BackImagePack)(m_currentIndex);
        }
    }
}
