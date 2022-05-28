using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStatusEffect
{
    public abstract float GetDuration();

    public abstract IEnumerator Activate(Character character);
}
