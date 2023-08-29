using System.Collections;
using UnityEngine;

public class FoodItem : MonoBehaviour
{
    public string foodType;
    public float respawnTime = 30f;
    private bool isConsumed = false;
    public GameObject player;

    private void Start()
    {
        // Find the player object in the scene
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void Consume()
    {
        Debug.Log("Consuming food: " + gameObject.name);

        if (!isConsumed)
        {
            isConsumed = true;
            gameObject.SetActive(false);

            // Inform the player's attributes to apply the effects of the consumed food
            if (player)
            {
                PlayerAttributes playerAttributes = player.GetComponent<PlayerAttributes>();
                if (playerAttributes)
                {
                    playerAttributes.ConsumeFood(foodType);
                }
            }

            // Start the coroutine on the main camera instead of the inactive food item
            Camera.main.gameObject.AddComponent<FoodItemCoroutineStarter>().StartCoroutine(Respawn());
        }
    }



    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(respawnTime);
        isConsumed = false;
        gameObject.SetActive(true);
    }
}
