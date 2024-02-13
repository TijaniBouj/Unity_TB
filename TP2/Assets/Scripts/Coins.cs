using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour
{


    private Vector3 initialPlayerPosition;

    public int coins;
    // Start is called before the first frame update
    void Start()
    {
        
        initialPlayerPosition = transform.position;
    }
    public void OnTriggerEnter(Collider Col)
    {
        if (Col.gameObject.tag == "coin")
        {
            Debug.Log("Coin collected");
            coins = coins + 1;

            // Deactivate the collected coin
            Col.gameObject.SetActive(false);

            
            StartCoroutine(ReactivateCoinAfterDelay(Col.gameObject, 2f));
        }
    }

    private IEnumerator ReactivateCoinAfterDelay(GameObject coinToReactivate, float delay)
    {
        
        yield return new WaitForSeconds(delay);

       
        if (coinToReactivate != null)
        {
            coinToReactivate.SetActive(true);
        }
    }

    void update()
    {
        if (coins == 6)
        {
            transform.position = initialPlayerPosition;
            coins = 0;
        }
    }
}


