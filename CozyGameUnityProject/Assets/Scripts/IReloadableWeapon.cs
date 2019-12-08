using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IReloadableWeapon
{
    bool IsReloading {
        get;
    }

    float ReloadingProgress {
        get;
    }
}
