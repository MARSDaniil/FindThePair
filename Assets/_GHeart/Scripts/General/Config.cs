using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HandyTools;
public class Config : AManager<Config> {
    override protected bool DontDOnLoad { get { return true; } }

    public GameParametrs gameParametrs;
}
