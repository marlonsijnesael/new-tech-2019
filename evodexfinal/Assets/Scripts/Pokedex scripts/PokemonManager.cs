using System.IO;
using UnityEngine;
using System.Collections.Generic;


/// <summary>
/// This class is a bit of a mess...
/// The process of loading pokemon changed drastically over the course of only a few hours
/// The rquest pokemon function is not used at all right now, because it is handled by the trackinghandler
/// this shoudl be fixed in later iterations 
/// </summary>
public class PokemonManager : MonoBehaviour
{
    private string jsonPath;
    [SerializeField] private string activePokemon = "none";

    private Dictionary<string, Pokemon> database = new Dictionary<string, Pokemon>();
    private Dictionary<string, Pokemon> databaseLoaded = new Dictionary<string, Pokemon>();

    [SerializeField] private string jsonFileName;

    public static PokemonManager _Instance;

    private void Awake()
    {
        if (_Instance == null)
        {
            _Instance = this;
        }
        else
        {
            Destroy(this);
        }
        LoadFromJson();
        Debug.Log(database.Count);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.touchCount > 0)
        {
            //RequestPokemon("eevee", transform);
        }
    }

    ///load the pokemon jsonfile from the resources path. 
    private void LoadFromJson()
    {
        //jsonPath = Path.Combine(Application.streamingAssetsPath, jsonFileName);
        TextAsset jsonData = Resources.Load<TextAsset>("pokemon");
        //.ReadAllText(jsonPath);

        var data = JsonHelper.FromJson<Pokemon>(jsonData.text);
        Pokemon[] pokemonDatabase = new Pokemon[data.Length];
        pokemonDatabase = data;

        ToDict(pokemonDatabase, database);
    }

    ///small helper function to convert the pokemon array to a dictionary 
    ///which we can access using the pokemon's name as a key
    private void ToDict(Pokemon[] pokemonArray, Dictionary<string, Pokemon> database)
    {
        for (int i = 0; i < pokemonArray.Length; i++)
        {
            database.Add(pokemonArray[i].pokemonName, pokemonArray[i]);
            print(database[pokemonArray[i].pokemonName].pokemonName);
        }
    }

    /// <summary>
    /// this function will check if the pokemon is already in the loaded pokemon database
    /// when this is not true, it will load the pokemon and add it to the loaden-database
    /// </summary>
    /// <param name="pokemonName"> the name of the requested pokemon</param>
    /// <param name="targetTransform">the </param>
    public void RequestPokemon(Pokemon pokemon)
    {
        if (databaseLoaded.ContainsKey(pokemon.pokemonName))//pokemonName
        {
            databaseLoaded[pokemon.pokemonName].EnableInstance();
            activePokemon = pokemon.pokemonName;
        }
    }

    public void DisablePokemon(string pokemonName)
    {
        if (activePokemon != "none")
        {
            databaseLoaded[pokemonName].DisableInstace();
            activePokemon = "none";
        }
    }

    /// <summary>
    /// if the requested pokemon is not present in the loaded-database
    /// this function will load the specific prefab and assign/instantiate it to the pokemon
    /// </summary>
    /// <param name="pokemonName"> the name of the requested pokemon</param>
    /// <param name="targetTransform">the </param>
    public Pokemon SpawnObject(string pokemonName, Transform targetTransform)
    {
        Pokemon newPokemon = database[pokemonName];
        string path = database[pokemonName].prefabName;
        GameObject prefab = Instantiate( Resources.Load<Object>(path) as GameObject, targetTransform.position, targetTransform.rotation, targetTransform);

        if (!Resources.Load<GameObject>(path))
        {
            prefab = Instantiate(GameObject.CreatePrimitive(PrimitiveType.Cube), targetTransform.position, targetTransform.rotation, targetTransform);
        }
        newPokemon.Init(prefab);

        //add pokemon to loaded-pokemon databse
        if (!database.ContainsKey(pokemonName))
        {
            databaseLoaded.Add(pokemonName, newPokemon);
        }
       return newPokemon;
    }
}
