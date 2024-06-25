using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameParametrs;

public class ChooseFront : Choose
{
    protected override void BeginGame() {

        if (GameInstance.Exist) {

            m_currentIndex = (int)GameInstance.frontImagePack;
        }
        m_title.text = "Choose pack of card";
        base.BeginGame();
    }

    protected override void Next() {
        base.Next();
        if (GameInstance.Exist) {
            GameInstance.frontImagePack = (FrontImagePack)(m_currentIndex);
        }
    }

    protected override void Previous() {
        base.Previous();
        if (GameInstance.Exist) {
            GameInstance.frontImagePack = (FrontImagePack)(m_currentIndex);
        }
    }
}
