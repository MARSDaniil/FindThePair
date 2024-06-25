using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HandyTools;
using Cysharp.Threading.Tasks;
using System;
public class LoadingGameMode : AManager<LoadingGameMode>
{
    

    public async UniTask WaitTime() {

        await UniTask.Delay(TimeSpan.FromSeconds(4f), false);

        if (GameInstance.Exist) {
            GameInstance.I.eventOnSceneLoadRequest?.Invoke();
        }
    }
}
