using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Fuel : MonoBehaviour
{
    [SerializeField] float fuel = 100;
    [SerializeField] float fuelDecrease = 1.5f;
    [SerializeField] TextMeshProUGUI fuelText;

    // Update is called once per frame
    void Update()
    {
        DecreaseFuel(0);
        if(fuel < 0)
        {
            Invoke("EndOfTheGame", 0.5f);
        }
        
    }
    public void DecreaseFuel(int factor)
    {
        var biggerWeight = Chicken.isExisted ? 4 : 1;
        
        fuel -= Time.deltaTime * fuelDecrease * biggerWeight - factor ;
        fuelText.text = "Fuel: " + (int)fuel;
    }

    public void IncreaseFuel()
    {
        fuel = 100;
        fuelText.text = "Fuel: " + (int)fuel;
    }

    private void EndOfTheGame()
    {
        SceneManager.LoadScene(0);
    }
}
