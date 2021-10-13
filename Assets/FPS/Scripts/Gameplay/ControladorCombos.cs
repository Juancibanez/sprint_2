using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControladorCombos : MonoBehaviour
{
    [Tooltip("UI Combos")]
    public GameObject combosUI;

    public float tiempo = -1.0f;

    public int kills = 0;

    [Tooltip("Texto Combos")]

    public Text textoCombo;

    //Función que se llama cada vez que hay un kill
    public void kill () {
        tiempo = 5.0f;
        kills += 1;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       //Revisar si el tiempo ya terminó

        if (tiempo >= 0)
        {
            combosUI.SetActive(true);
        }
        else
        {
            combosUI.SetActive(false);
            kills = 0;
        }

        tiempo -= 0.025f;

        // Opciones para el texto

        if (kills == 0)
        {
            textoCombo.text = "";
        }

        else if (kills == 1)
        {
            textoCombo.text = "x1";
        }

        else if (kills == 2)
        {
            textoCombo.text = "x2";
        }


        else if (kills == 3)
        {
            textoCombo.text = "x3";
        }


        else if (kills == 4)
        {
            textoCombo.text = "x4";
        }


        else if (kills >= 5)
        {
            textoCombo.text = "BONUS";
        }


    }
}
