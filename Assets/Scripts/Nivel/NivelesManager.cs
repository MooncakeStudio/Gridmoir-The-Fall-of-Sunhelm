using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using VNCreator;

public class NivelesManager : MonoBehaviour
{
    // Atributos

    [SerializeField] private NivelesDataStream nivelesDS;
    private List<SerializableLevel> niveles;

    private int seleccion = 0;

    [Header("Informaci�n nivel")]
    [SerializeField] private GameObject infoNivel;

    [Header("Manejo UI")]
    [SerializeField] private List<Button> botonesUI;
    [SerializeField] private List<Button> botonesSeleccion;

    [Header("Im�genes")]
    [SerializeField] private Sprite imgBloqueado;
    [SerializeField] private Sprite imgDesbloqueado;

    [Header("Mundos")]
    [SerializeField] private GameObject fondo;
    [SerializeField] private List<Sprite> fondosMundo;
    [SerializeField] private List<GameObject> iconosNiveles;
    [SerializeField] private List<GameObject> flechasNiveles;
    [SerializeField] private TextMeshProUGUI textoMundo;
    private int nMundos;

    [Header("Historia")]
    [SerializeField] private GameObject historiaManager;
    [SerializeField] private List<String> historias;

    [Header("Enemigos")]
    [SerializeField] private List<Image> enemigos;
    [SerializeField] private List<Image> tipoAtaque;

    [SerializeField] private List<Sprite> spriteEnemigos;
    [SerializeField] private List<Sprite> spriteAtaques;

    [Header("Flechas cambio mundo")]
    [SerializeField] private GameObject prev;
    [SerializeField] private GameObject next;

    [Header("Audio men�")]
    [SerializeField] private AudioClip menuClip;

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
        GameManager.instance.setClip(menuClip);
        nMundos = fondosMundo.Count;
        Debug.Log(nMundos);

        //PlayerPrefs.DeleteKey("Estados Niveles");
        // Si no existe la tabla de estados en el PlayerPrefs
        if (!PlayerPrefs.HasKey("Estados Niveles"))
        {
            // Creamos el array de estados
            SerializableEstadoList estados = new SerializableEstadoList();
            int contador = 0;

            //Rellenamos el array(primera posici�n no jugado el resto bloqueados)
            estados.list.Add(Estado.NO_JUGADO);
            contador++;

            while (contador < botonesSeleccion.Count)
            {
                estados.list.Add(Estado.BLOQUEADO);
                contador++;
            }

            // Parseamos el array a un formato string
            string estadosString = JsonUtility.ToJson(estados);
            Debug.Log(estadosString);

            // Guardamos la informaci�n en los PlayerPrefs
            PlayerPrefs.SetString("Estados Niveles", estadosString);
        }
        niveles = nivelesDS.ObtenerLista();
    }

    // Start is called before the first frame update
    void Start()
    {

        CambiaMundo(GameManager.instance.GetMundoSeleccionado());

        if(GameManager.instance.GetMundoSeleccionado() > 1 && GameManager.instance.GetMundoSeleccionado() < 3)
        {
            if (!prev.activeSelf) prev.SetActive(true);
        }else if(GameManager.instance.GetMundoSeleccionado() >= 3)
        {
            if(next.activeSelf) next.SetActive(false);
            if(!prev.activeSelf) prev.SetActive(true);
        }
        else if(GameManager.instance.GetMundoSeleccionado() <= 1)
        {
            if (!next.activeSelf) next.SetActive(true);
            if (prev.activeSelf) prev.SetActive(false);
        }
        string estadosString = PlayerPrefs.GetString("Estados Niveles");

        SerializableEstadoList estados = JsonUtility.FromJson<SerializableEstadoList>(estadosString);


        for (int i = 0; i < botonesSeleccion.Count; i++)
        {
            var imagen = botonesSeleccion[i].GetComponent<Image>();

            if (estados.list[i] == Estado.BLOQUEADO)
            {
                imagen.sprite = imgBloqueado;

                botonesSeleccion[i].enabled = false;
                
            } else
            {
                imagen.sprite = imgDesbloqueado;
                botonesSeleccion[i].enabled = true;
            }
        }

        switch (GameManager.instance.GetMundoSeleccionado())
        {
            case 1:
                textoMundo.SetText("Cantermahl");
                break;
            case 2:
                textoMundo.SetText("Thelia");
                break;
            case 3:
                textoMundo.SetText("Muddybog");
                break;
        }
    }


    // METODOS

    public void ActivaInfo(int idx)
    {
        SetSeleccion(idx);

        var manager = historiaManager.GetComponent<HistoriaManager>() as HistoriaManager;
        var idxHistoria = niveles[this.seleccion].historia;
        if (niveles[this.seleccion].historia != 0)
        {
            manager.SetTieneHistoria(true);
            manager.SetHistoria(historias[idxHistoria - 1]);
        }

        RellenaInfo();

        foreach (var boton in botonesSeleccion)
        {
            boton.enabled = false;
        }
        foreach (var boton in botonesUI)
        {
            boton.enabled=false;
        }

        infoNivel.SetActive(true);
    }

    public void DesactivaInfo()
    {
        var manager = historiaManager.GetComponent<HistoriaManager>() as HistoriaManager;
        manager.SetTieneHistoria(false);

        foreach (var boton in botonesSeleccion)
        {
            boton.enabled = true;
        }
        foreach (var boton in botonesUI)
        {
            boton.enabled = true;
        }

        infoNivel.SetActive(false);
    }

    private void RellenaInfo()
    {
        SerializableLevel sl = niveles[this.seleccion];

        var id = infoNivel.transform.Find("ID");
        var nombre = infoNivel.transform.Find("Nombre");

        id.GetComponent<TextMeshProUGUI>().SetText(sl.mundo + "-" + sl.id);
        nombre.GetComponent<TextMeshProUGUI>().SetText(sl.nombre);
        
        

        for (var i=0; i < 3; i++)
        {
            enemigos[i].sprite = spriteEnemigos[sl.enemigos[i]];
            tipoAtaque[i].sprite = spriteAtaques[sl.tipoAtaque[i]];
        }
    }

    public void CambiaMundo(int mundo)
    {
        GameManager.instance.cambiarMundo(mundo);
        fondo.GetComponent<SpriteRenderer>().sprite = fondosMundo[mundo - 1];

        for (int i = 0; i < nMundos; i++)
        {
            if (i == (mundo - 1))
            {
                fondo.GetComponent<SpriteRenderer>().sprite = fondosMundo[i];
                iconosNiveles[i].SetActive(true);
                flechasNiveles[i].SetActive(true);
            }
            else
            {
                iconosNiveles[i].SetActive(false);
                flechasNiveles[i].SetActive(false);
            }
        }
    }

    public void nextMundo()
    {
        var mundo = GameManager.instance.GetMundoSeleccionado();
        if ( mundo < 3)
        {
            mundo++;
            GameManager.instance.cambiarMundo(mundo);
            fondo.GetComponent<SpriteRenderer>().sprite = fondosMundo[mundo - 1];

            for (int i = 0; i < nMundos; i++)
            {
                if (i == (mundo - 1))
                {
                    fondo.GetComponent<SpriteRenderer>().sprite = fondosMundo[i];
                    iconosNiveles[i].SetActive(true);
                    flechasNiveles[i].SetActive(true);
                }
                else
                {
                    iconosNiveles[i].SetActive(false);
                    flechasNiveles[i].SetActive(false);
                }
            }

            if (mundo == 3) { next.SetActive(false); }
            if (!prev.activeSelf) prev.SetActive(true);

        }

        switch (mundo)
        {
            case 1:
                textoMundo.SetText("Cantermahl");
                break;
            case 2:
                textoMundo.SetText("Thelia");
                break;
            case 3:
                textoMundo.SetText("Muddybog");
                break;
        }

    }

    public void prevMundo()
    {
        var mundo = GameManager.instance.GetMundoSeleccionado();
        if(mundo > 1)
        {
            mundo--;
            GameManager.instance.cambiarMundo(mundo);
            fondo.GetComponent<SpriteRenderer>().sprite = fondosMundo[mundo - 1];

            for (int i = 0; i < nMundos; i++)
            {
                if (i == (mundo - 1))
                {
                    fondo.GetComponent<SpriteRenderer>().sprite = fondosMundo[i];
                    iconosNiveles[i].SetActive(true);
                    flechasNiveles[i].SetActive(true);
                }
                else
                {
                    iconosNiveles[i].SetActive(false);
                    flechasNiveles[i].SetActive(false);
                }
            }

            if (mundo == 1) { prev.SetActive(false); }
            if (!next.activeSelf) next.SetActive(true);
        }

        switch (mundo)
        {
            case 1:
                textoMundo.SetText("Cantermahl");
                break;
            case 2:
                textoMundo.SetText("Thelia");
                break;
            case 3:
                textoMundo.SetText("Muddybog");
                break;
        }
    }
}

[Serializable]
public class SerializableEstadoList
{
    public List<Estado> list = new List<Estado>();
}