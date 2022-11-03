using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterSwitcher : MonoBehaviour
{

    int index = 0;
    [SerializeField]
    List<GameObject> Spieler = new List<GameObject>();
    PlayerInputManager manager;
 
    void Start()
    {
        manager = GetComponent<PlayerInputManager>();
        index = Random.Range(0, Spieler.Count);
        manager.playerPrefab = Spieler[index];

    }

    // Update is called once per frame
    public void SwitchNextSpawnCharacter(PlayerInput input)

    {
        index = Random.Range(0, Spieler.Count);
        manager.playerPrefab = Spieler[index];
    }
}
