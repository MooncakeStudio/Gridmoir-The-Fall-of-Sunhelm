using System.Collections.Generic;
using UnityEngine;

public class NivelesDataStream : MonoBehaviour
{

    private string nivelesJson = "{\"list\": [{\"mundo\": 1,\"id\": 1,\"nombre\": \"Patata\",\"enemigos\": [0, 1, 2],\"monedas\": 30,\"xp\": 30},{\"mundo\": 1,\"id\": 2,\"nombre\": \"Zanahoria\",\"enemigos\": [0, 1, 2],\"monedas\": 20,\"xp\": 20},{\"mundo\": 1,\"id\": 3,\"nombre\": \"Puerro\",\"estado\": 2,\"enemigos\": [0, 1, 2],\"monedas\": 10,\"xp\": 10}]}";

    private ListaLevelSerializable lls = new ListaLevelSerializable();

    // Start is called before the first frame update

    public List<SerializableLevel> ObtenerLista()
    {
        if (!string.IsNullOrEmpty(nivelesJson))
        {
            lls = JsonUtility.FromJson<ListaLevelSerializable>(nivelesJson);
        }

        return this.lls.list;
    }
}
