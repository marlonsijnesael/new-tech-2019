using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "pokemon", menuName = "pokemon/DexEntry", order = 0)]
public class PokemonBase : ScriptableObject
{
    public string Name;
    public enum Typing { fire = 0, water = 1, electric = 2, ground = 4, grass = 5, rock = 6, dragon = 7, fairy = 8, normal = 9 }
    public Typing Type;
    public string Description;
    public Mesh pokemonModel;
    public Material[] textures;


}

