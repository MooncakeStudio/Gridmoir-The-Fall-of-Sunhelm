using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NivelesManager : MonoBehaviour
{
    // Atributos

    [SerializeField] private NivelesDataStream nivelesDS;
    private List<SerializableLevel> niveles;

    private int seleccion = 0;

    [SerializeField] private GameObject infoNivel;
    [SerializeField] private List<Button> botonesSeleccion;


    // GETTERS & SETTERS

    public NivelesDataStream GetNivelesDS() { return this.nivelesDS; }
    public List<SerializableLevel> GetNiveles() { return this.niveles; }
    public int GetSeleccion() { return this.seleccion; }

    public void SetNivelesDS(NivelesDataStream nivelesDS) { this.nivelesDS = nivelesDS; }
    public void SetNiveles(List<SerializableLevel> niveles) { this.niveles = niveles; }
    public void SetSeleccion(int seleccion) { this.seleccion = seleccion - 1; }


    // CONSTRUCTORES

    public NivelesManager() { }
    public NivelesManager(NivelesDataStream nivelesDS, List<SerializableLevel> niveles)
    {
        this.nivelesDS = nivelesDS;
        this.niveles = niveles;
    }



    // Awake se llama al cargar la instancia del script
    private void Awake()
    {
        //PlayerPrefs.DeleteKey("Estados Niveles");
        // Si no existe la tabla de estados en el PlayerPrefs
        if (!PlayerPrefs.HasKey("Estados Niveles"))
        {
            Debug.Log("He llegado");
            // Creamos el array de estados
            SerializableEstadoList estados = new SerializableEstadoList();
            int contador = 0;

            // Rellenamos el array (primera posición no jugado el resto bloqueados)
            estados.list.Add(Estado.NO_JUGADO);
            contador++;
            while (contador < 10)
            {
                estados.list.Add(Estado.BLOQUEADO);
                contador++;
            }

            // Parseamos el array a un formato string
            string estadosString = JsonUtility.ToJson(estados);
            Debug.Log(estadosString);

            // Guardamos la información en los PlayerPrefs
            PlayerPrefs.SetString("Estados Niveles", estadosString);
        }
        niveles = nivelesDS.ObtenerLista();
    }

    // Start is called before the first frame update
    void Start()
    {
        string estadosString = PlayerPrefs.GetString("Estados Niveles");

        SerializableEstadoList estados = JsonUtility.FromJson<SerializableEstadoList>(estadosString);

        Debug.Log(estados.list.Count);

        for (int i = 0; i < botonesSeleccion.Count; i++)
        {
            if (estados.list[i] == 0)
            {
                botonesSeleccion[i].enabled = false;
            }
        }
    }


    // METODOS

    public void ActivaInfo(int idx)
    {
        SetSeleccion(idx);
        RellenaInfo();
        infoNivel.SetActive(true);
    }

    public void DesactivaInfo()
    {
        infoNivel.SetActive(false);
    }

    private void RellenaInfo()
    {
        SerializableLevel sl = niveles[this.seleccion];

        var mundo = infoNivel.transform.Find("Mundo");
        var id = infoNivel.transform.Find("ID");
        var nombre = infoNivel.transform.Find("Nombre");

        mundo.GetComponent<TextMeshProUGUI>().SetText("Mundo: " + sl.mundo);
        id.GetComponent<TextMeshProUGUI>().SetText("ID: " + sl.id);
        nombre.GetComponent<TextMeshProUGUI>().SetText("Nombre: " + sl.nombre);
    }
}

[Serializable]
public class SerializableEstadoList
{
    public List<Estado> list = new List<Estado>();
}