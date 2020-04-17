using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End : MonoBehaviour
{
    public GameObject shipPart1;
    public GameObject shipPart2;
    public GameObject shipPart3;
    public GameObject shipPart4;
    public GameObject canvaseind;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D trigger)
    {
        StartCoroutine(ItemPop());
    }
    private IEnumerator ItemPop()
    { 
        shipPart1.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        shipPart2.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        shipPart3.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        shipPart4.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        canvaseind.SetActive(true);
    }

}
