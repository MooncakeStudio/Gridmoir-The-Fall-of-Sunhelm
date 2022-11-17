using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoController : MonoBehaviour
{
    [SerializeField] private TipoEnemigo tipoEnemigo;
    private Enemigo enemigo;


    public Enemigo getEnemigo() { return enemigo; }
    public void setEnemigo(Enemigo enemigo) { this.enemigo = enemigo; }
    // Start is called before the first frame update
    public void crearEnemigo()
    {
        enemigo = new Enemigo(this.tipoEnemigo);
    }

}
