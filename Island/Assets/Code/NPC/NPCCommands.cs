using UnityEngine;

public class NPCCommands : MonoBehaviour
{
    [SerializeField] private GameObject _currentNPC;
    [SerializeField] private GameObject _player; // Assign in inspector or tag your player as "Player"
    [SerializeField] private NPCFollow _npcFollowing;
    [SerializeField] private NPCStay _npcStay;

    void Start()
    {
        if (_currentNPC != null)
        {
            _npcFollowing = _currentNPC.GetComponent<NPCFollow>();
            _npcStay = _currentNPC.GetComponent<NPCStay>();
        }
        else
        {
            Debug.LogError("NPCCommands: No NPC assigned. Assign an NPC GameObject in the inspector.");
        }

        if (_player == null)
        {
            GameObject found = GameObject.FindGameObjectWithTag("Player");
            if (found != null) _player = found;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            HandleFollow();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            HandleStay();
        }
    }

    private void HandleFollow()
    {
        if (_npcFollowing == null)
        {
            Debug.LogError("NPCCommands: NPCFollow component not found on the assigned NPC!");
            return;
        }

        if (_player == null)
        {
            Debug.LogError("NPCCommands: Player reference is null. Assign _player in the inspector or tag the player as 'Player'.");
            return;
        }

        if (_npcStay != null)
        {
            _npcStay.ResumeMovement();
        }

        _npcFollowing.StartFollowing(_player);
        Debug.Log("NPCCommands: Follow command executed (1)");
    }

    private void HandleStay()
    {
        if (_npcStay == null)
        {
            Debug.LogError("NPCCommands: NPCStay component not found on the assigned NPC!");
            return;
        }

        if (_npcFollowing != null)
        {
            _npcFollowing.StopFollowing();
        }

        _npcStay.Stay();
        Debug.Log("NPCCommands: Stay command executed (2)");
    }
}
