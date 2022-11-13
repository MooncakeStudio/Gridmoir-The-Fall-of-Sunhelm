using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DataToBattle : MonoBehaviour
{
    private ListaPlayerSerializable lsp = new ListaPlayerSerializable();
    private Celda[] listaCeldas = new Celda[3];
    [SerializeField] private List<GameObject> enemigosNivel;
    private List<CeldaManager> listCeldas = new List<CeldaManager>();

    public void putGrid()
    {
        var i = 0;

        foreach (var celda in listCeldas)
        {
            if(celda.getCelda().GetPersonaje() != null)
            {
                addSP(celda.getCelda().GetPersonaje().GetComponent<RectTransform>().Find("Character").gameObject);
                listaCeldas[i] = celda.getCelda();
                i++;
            }
            
        }

        foreach(var celda in listaCeldas)
        {
            //Debug.Log("Celda [" + celda.GetX() + "," + celda.GetY() + "]: " + celda.IsOccupied());
        }

        DontDestroyOnLoad(gameObject);
        SceneManager.LoadScene("Batalla");
    }

    public ListaPlayerSerializable getLSP() { return lsp; }
    public List<CeldaManager> getCeldas() { return listCeldas; }

    public List<GameObject> getEnemigos() { return enemigosNivel; }

    public void addCelda(CeldaManager celda) 
    { 
        listCeldas.Add(celda); 
    }

    public void removeCelda(CeldaManager celda) { listCeldas.Remove(celda); }
    private void addSP(GameObject p)
    {
        //Debug.Log("A�adir personaje: " + p);
        var newFlequillo = p.transform.Find("Flequillo").GetComponent<Image>();
        var fn = newFlequillo.sprite.name;

        var newPelo = p.transform.Find("Pelo").GetComponent<Image>().sprite.name;

        var newPesta�as = p.transform.Find("Pesta�as").GetComponent<Image>().sprite.name;

        var newOrejas = p.transform.Find("Orejas").GetComponent<Image>().sprite.name;

        var newNarices = p.transform.Find("Nariz").GetComponent<Image>().sprite.name;

        var newBoca = p.transform.Find("Boca").GetComponent<Image>().sprite.name;

        var newExtra = p.transform.Find("Extra").GetComponent<Image>().sprite.name;

        var newCejas = p.transform.Find("Cejas").GetComponent<Image>().sprite.name;

        var ropa = p.transform.Find("Ropa").GetComponent<Image>().sprite.name;

        var newIris = p.transform.Find("Ojos").transform.Find("Iris").GetComponent<Image>();

        var rp = newFlequillo.color.r;
        var gp = newFlequillo.color.g;
        var bp = newFlequillo.color.b;

        var ri = newIris.color.r;
        var gi = newIris.color.g;
        var bi = newIris.color.b;

        
        var person = p.GetComponent<PlayerController>().getPersonaje();
        SerializablePlayer sp = new SerializablePlayer(int.Parse(fn),int.Parse(newPelo),int.Parse(newPesta�as),int.Parse(newOrejas)
            ,int.Parse(newNarices),int.Parse(newBoca)
            ,int.Parse(newExtra),int.Parse(newCejas),int.Parse(ropa),rp,gp,bp,ri,gi,bi,person.GetAtaque(),
            person.GetDefensa(),person.GetVida(),person.getVidaMax(),(int)person.GetTipoAtaque(),person.GetNombre(),(int)person.GetRareza());

        lsp.list.Add(sp);
    }
}
