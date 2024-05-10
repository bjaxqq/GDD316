using UnityEngine;
using UnityEngine.AI;

public class AIMovement : MonoBehaviour
{
    private GameObject player; // Reference to the player object
    private NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player"); // Find the player object by tag
        if (player == null)
        {
            Debug.LogError("No object with tag 'Player' found.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            agent.destination = player.transform.position; // Move towards the player
        }
    }
}
