using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public struct WinningTarget
{
    public string playerName;
    public WinStatus target;
    public Transform confettiSpawnPoint;
}

public class WinningTargetProcessor : MonoBehaviour
{
    [SerializeField] private EndScreen endScreen;

    [SerializeField] private ParticleSystem confetti;

    [SerializeField] private WinningTarget[] targets;

    private Dictionary<string, WinningTarget> winningTargetsMap;

    private void Start()
    {
        winningTargetsMap = new Dictionary<string, WinningTarget>();
        foreach (var target in targets)
        {
            winningTargetsMap.Add(target.playerName, target);
        }
    }

    public void ProcessWinningTarget(string playerName)
    {
        bool exists = winningTargetsMap.ContainsKey(playerName);
        if (!exists)
        {
            Debug.Log($"Player {playerName} does not have a winning target, consdering it a draw");
            endScreen.PlayEndSound(WinStatus.NO_ONE);
            return;
        }


        Debug.Log($"Player {playerName} has a winning target, {winningTargetsMap[playerName].target}");
        var target = winningTargetsMap[playerName];

        endScreen.WriteWinner($"{target.playerName} Wins!");
        endScreen.PlayEndSound(target.target);

        var confettiInstance = Instantiate(confetti, target.confettiSpawnPoint.position, Quaternion.identity);
        confettiInstance.Play();
        Destroy(confettiInstance.gameObject, confettiInstance.main.duration);
    }
}
