using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ControladorCombos : MonoBehaviour
{
    [Tooltip("UI Combos")]
    public GameObject combosUI;

    public int kills = 0;

    [Tooltip("Texto Combos")]

    public Text textoCombo;

    public Text cuentaRegr;
    float currentTime = 0f;
    float startingTime = 0f;


    //Función que se llama cada vez que hay un kill
    public void kill ()
    {
        currentTime = 5.0f;
        kills += 1;
    }


    //Getter del número de Kills (Se llama en cada Update)

    public int getKills() 
    {

        return this.kills;
    }

    void Start()
    {
       currentTime = startingTime;
    }

    // Update is called once per frame
    void Update()
    {

       currentTime -= 1 * Time.deltaTime;
       cuentaRegr.text = currentTime.ToString("0.0");
        if(currentTime <= 0)
        {
            currentTime = 0;
        }
       
       
       //Revisar si el tiempo ya terminó

        if (currentTime <= 0)
        {
            kills = 0;
        }
        
    
        // Opciones para el texto

        if (true)
        {
            textoCombo.text = kills.ToString();
        }


    }
}
