using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GameJamTeam3.Scripts.Enums;
using Godot;

namespace GameJamTeam3.Scripts;

public partial class EnchantmentManager : Node
{
    [Signal] public delegate void EnchantmentAquiredEventHandler(Enchantment enchant);
    [Export] public string EnchantmentsPath { get; set; } = "res://Ressources/Enchantments/";
    [Export] public string BenedictionCardScenePath { get; set; } = "res://Prefabs/BenedictionCard.tscn";
    [Export] public string MaledictionCardScenePath { get; set; } = "res://Prefabs/MaledictionCard.tscn";

    public static EnchantmentManager Instance { get; private set; }
    public List<Enchantment> Benedictions { get; set; }
    public List<Enchantment> Maledictions { get; set; }
    public List<Enchantment> Unique { get; set; }

    public override void _Ready()
    {
        Instance = this;
        Benedictions = new List<Enchantment>();
        Maledictions = new List<Enchantment>();
        Unique = new List<Enchantment>();
        
        LoadEnchantments();
    }

    public void ShowNewDilemma()
    {
        var bscene = GD.Load<PackedScene>(BenedictionCardScenePath);
        var mscene = GD.Load<PackedScene>(MaledictionCardScenePath);

        Card c1 = bscene.Instantiate<Card>();
        c1.Enchantment = GetRandomBenediction();
        Card c2 = mscene.Instantiate<Card>();
        c2.Enchantment = GetRandomMalediction();
    }

    public Enchantment GetRandomMalediction()
    {
        return Maledictions[new Random().Next(Maledictions.Count)];
    }
    
    public Enchantment GetRandomBenediction()
    {
        return Benedictions[new Random().Next(Benedictions.Count)];
    }
    
    public Enchantment GetRandomUnique()
    {
        return Unique[new Random().Next(Unique.Count)];
    }
    
    private void LoadEnchantments()
    {
        DirAccess dirAccess = DirAccess.Open(EnchantmentsPath);
        if (dirAccess == null) return;

        string[] files = dirAccess.GetFiles();
        if (files == null) return;

        foreach(string fileName in files)
        {
            Enchantment loadedResource = GD.Load<Enchantment>(EnchantmentsPath + fileName);
            if (loadedResource == null) continue;

            if (loadedResource.Categories == EnchantmentCategories.BENEDICTION)
            {
                Benedictions.Add(loadedResource);
                return;
            }

            if (loadedResource.Categories == EnchantmentCategories.MALEDICTION)
            {
                Maledictions.Add(loadedResource);
                return;
            }
            
            Unique.Add(loadedResource);
        }
    }
}