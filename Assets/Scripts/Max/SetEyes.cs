using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
public class SetEyes : MonoBehaviour
{
    public List<EyeFollow> eyes;

    public void LookAtOtherPlayer()
    {
        foreach (EyeFollow eye in eyes)
        {
            eye.LookAtOtherPlayer();
        }
    }
    public void LookDown()
    {
        foreach (EyeFollow eye in eyes)
        {
            eye.LookDown();
        }
    }
}
