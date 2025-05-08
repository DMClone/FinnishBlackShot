using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Shotgun : MonoBehaviour
{
    public int totalRounds = 5;
    public int loadedRounds = 1;
    public List<bool> shots = new List<bool>();
    public Animator shotgunAnimator;

    private void Start()
    {
        FillAndShuffleRounds();
    }

    public void FillAndShuffleRounds()
    {
        for (int i = 0; i < loadedRounds; i++)
        {
            shots.Add(true);
        }
        for (int i = loadedRounds; i < totalRounds; i++)
        {
            shots.Add(false);
        }

        shots = shots.OrderBy(x => Random.value).ToList();
    }
}
