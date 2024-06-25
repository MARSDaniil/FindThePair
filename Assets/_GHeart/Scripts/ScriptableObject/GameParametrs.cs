using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameParametrs", menuName = "GHeart/CreateGameParametrs", order = 1)]
public class GameParametrs : ScriptableObject {

    public int minPairCount = 2;
    public int maxPairCount = 20;
    public int minPlayCount = 1;
    public int maxPlayCount = 5;
    public List<LevelDiffucult> levelDiffucults;


    public float GetLevelDiffucultsTime(Difficult a_levelDiffucult) {
        foreach(LevelDiffucult levelDiffucult in levelDiffucults) {
            if(levelDiffucult.levelType == a_levelDiffucult) {
                return levelDiffucult.difficultyTime;
            }
        }
        Debug.LogError($"Cant find this diffucult {a_levelDiffucult}");
        return 5;
    }

    [Serializable]
    public struct LevelDiffucult {
        public Difficult levelType;
        public float difficultyTime;
    }

    public enum Difficult {
        Easy,
        Normal,
        Hard,
        DeathMarch
    }

    public enum FrontImagePack {
        KidsPack,
        FlagsPack,
        VanGhog
    }

    public enum BackImagePack {
        Pack1,
        Pack2,
        Pack3, 
        Pack4
    }
}
