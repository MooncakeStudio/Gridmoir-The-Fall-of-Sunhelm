using UnityEngine;

public class Enemigo
{
    /*------ Estadisticas ------*/
    [SerializeField]
    private float vida;

    [SerializeField]
    private float vidaMax;

    [SerializeField]
    private float ataque;

    [SerializeField]
    private float defensa;

    /*------ Modificadores ------*/
    [SerializeField]
    private TipoAtaque tipoAtaque;

    public Enemigo(TipoEnemigo tipoEnemigo) 
    {
        switch (tipoEnemigo)
        {
            case TipoEnemigo.PERRO_INFERNAL:
                ataque = Random.Range(2, 7);
                defensa = Random.Range(1, 6);
                vida = Random.Range(5, 15);
                vidaMax = vida;
                tipoAtaque = (TipoAtaque)Random.Range(0, 2);
                break;

            case TipoEnemigo.MURICELAGO_VAMPIRO:
                ataque = Random.Range(3, 9);
                defensa = Random.Range(2, 6);
                vida = Random.Range(5, 20);
                vidaMax = vida;
                tipoAtaque = (TipoAtaque)Random.Range(1, 3);
                break;

            case TipoEnemigo.GOBLIN:
                ataque = Random.Range(5, 10);
                defensa = Random.Range(4, 8);
                vida = Random.Range(10, 25);
                vidaMax = vida;
                tipoAtaque = (TipoAtaque)Random.Range(0, 3);
                break;

            case TipoEnemigo.DEMONIO_HEMBRA:
                ataque = Random.Range(8, 20);
                defensa = Random.Range(8, 15);
                vida = Random.Range(15, 30);
                vidaMax = vida;
                tipoAtaque = (TipoAtaque)Random.Range(0, 5);
                break;

            case TipoEnemigo.DEMONIO_MACHO:
                ataque = Random.Range(8, 20);
                defensa = Random.Range(8, 15);
                vida = Random.Range(15, 30);
                vidaMax = vida;
                tipoAtaque = (TipoAtaque)Random.Range(0, 5);
                break;
        }
    }

    public float GetVida() { return this.vida; }
    public float GetVidaMax() { return this.vidaMax; }
    public float GetAtaque() { return this.ataque; }
    public float GetDefensa() { return this.defensa; }
    public TipoAtaque GetTipoAtaque() { return this.tipoAtaque; }
    public void SetVida(float vida) { this.vida = vida; }
    public void SetVidaMax(float vidaMax) { this.vidaMax = vidaMax; }
    public void SetAtaque(float ataque) { this.ataque = ataque; }
    public void SetDefensa(float defensa) { this.defensa = defensa; }
    public void SetTipoAtaque(TipoAtaque tipoAtaque) { this.tipoAtaque = tipoAtaque; }

    public void takeDamage(float damage) { this.vida -= damage; }
    public void curar(float cura)
    {
        if(vida+cura > vidaMax)
        {
            vida = vidaMax;
        }
        else
        {
            vida += cura;
        }
    }
}

public enum TipoEnemigo
{
    PERRO_INFERNAL,
    MURICELAGO_VAMPIRO,
    GOBLIN,
    DEMONIO_HEMBRA,
    DEMONIO_MACHO
}