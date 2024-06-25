using HandyTools;
using UnityEngine;
using static GameParametrs;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
public class GameInstance : AManager<GameInstance> {

    override protected bool DontDOnLoad { get { return true; } }
    public UnityAction eventOnSceneLoadRequest;

    public static Difficult levelDiffucul = Difficult.Easy;
    public static FrontImagePack frontImagePack = FrontImagePack.KidsPack;
    public static BackImagePack backImagePack = BackImagePack.Pack1;
    public static int countOfPair = 2;
    public static int countOfPlayer = 1;

    private void Start() {
        eventOnSceneLoadRequest += LoadLobby;

        if (LoadingGameMode.Exist) {
            LoadingGameMode.I.WaitTime();
        }
    }

    private void LoadLobby() {
        eventOnSceneLoadRequest -= LoadLobby;
        LoadingScene(_GHeart.Constants.Scenes.LOBBY);
    }

    public string GetPackPath() {

        switch (frontImagePack) {
            case FrontImagePack.KidsPack:
                return _GHeart.Constants.AdressPath.KidsPathPack;
            case FrontImagePack.FlagsPack:
                return _GHeart.Constants.AdressPath.FlagsPackPath;
            case FrontImagePack.VanGhog:
                return _GHeart.Constants.AdressPath.VanGhogPath;
        }

        Debug.Log("Cant find pack path");

        return null;
    }

    public int GetPackCapacity() {
        switch (frontImagePack) {
            case FrontImagePack.KidsPack:
                return _GHeart.Constants.AdressPath.KidPackCapacity;
            case FrontImagePack.FlagsPack:
                return _GHeart.Constants.AdressPath.FlagsPackCapacity;
            case FrontImagePack.VanGhog:
                return _GHeart.Constants.AdressPath.VanGhogPackCapacity;
        }

        Debug.Log("Cant find pack capacity");
        return 0;
    }
    public int GetBackCapacity() {
        switch (backImagePack) {
            case BackImagePack.Pack1:
                return 1;
            case BackImagePack.Pack2:
                return 2;
            case BackImagePack.Pack3:
                return 3;
            case BackImagePack.Pack4:
                return 4;
        }

        Debug.Log("Cant find pack capacity");
        return 0;
    }

    public static void LoadingScene(string a_sceneName) {
        SceneManager.LoadScene(a_sceneName);
    }

}
