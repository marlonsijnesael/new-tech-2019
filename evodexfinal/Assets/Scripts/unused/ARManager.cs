using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ARManager : MonoBehaviour
{

    public Dictionary<string, PokemonBase> pokeDict = new Dictionary<string, PokemonBase>();

    public PokemonBase[] Pokemons;
    public Text UI;
    public GameObject currentPokemon;
    private MeshRenderer meshRenderer;
    private MeshFilter meshFilter;

    public static ARManager _Instance;

    

    private void Awake()
    {
        _Instance = this;
    }

    private void Start()
    {
        TrackingHandler.OnTrackedCard += UpdateAugmentedObjectTrackingFound;

        meshRenderer = currentPokemon.GetComponent<MeshRenderer>();
        meshFilter = currentPokemon.GetComponent<MeshFilter>();
        currentPokemon.SetActive(false);

        for (int i = 0; i < Pokemons.Length; i++)
        {
            pokeDict.Add(Pokemons[i].Name.ToLower(), Pokemons[i]);
        }
    }

    private void Update()
    {
    }

    private  void UpdateAugmentedObjectTrackingFound(string _pokemonName, Transform _transform)
    {
        UI.text = "uh";

        if (pokeDict.ContainsKey(_pokemonName.ToLower()))
        {
            ChangePokemon(pokeDict[_pokemonName]);
            UpdatePokemonLocation(_transform);
            UI.text = "in dict wtf";
        }

        else
        {
            UI.text = "not in dict";
        }
    }

    private void UpdateAugmentedObjectTrackingLost()
    {
        currentPokemon.transform.SetParent(null);
        UI.text = "not tracking";
    }

    private void UpdatePokemonLocation(Transform _transform)
    {
        currentPokemon.SetActive(true);
        currentPokemon.transform.position = _transform.position;
        currentPokemon.transform.SetParent(_transform);
    }

    private void ChangePokemon(PokemonBase _pokemonBase)
    {
        meshFilter.mesh = _pokemonBase.pokemonModel;
        meshRenderer.materials = _pokemonBase.textures;
    }

}
