using System;
using System.Collections.Generic;
using GameJamTeam3.Scripts.Enums;
using Godot;

namespace GameJamTeam3.Scripts;

public partial class EnchantmentManager : Node
{
    [Export] public string EnchantmentsPath { get; set; } = "res://Ressources/Enchantments/";

    public List<Enchantment> Benedictions { get; set; }
    public List<Enchantment> Maledictions { get; set; }
    public List<Enchantment> Unique { get; set; }

    public override void _Ready()
    {
        Benedictions = new List<Enchantment>();
        Maledictions = new List<Enchantment>();
        Unique = new List<Enchantment>();
        
        LoadEnchantments();
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