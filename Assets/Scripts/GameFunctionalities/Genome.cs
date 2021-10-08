using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class GenomeManager
{
    //https://github.com/shack3/EvoSimulator/tree/Documentation#52-genetic-characteristics

    #region General-REF_GC_1

    public float Aging ;
    public float MaximumBulk;
    public float EnergyCostByBulk;
    public float PhysicalHardness;

    #endregion

    #region Nutrition_Related-REF_GC_2


    public float AbilityToEatHardness;
    public float EfficiencyToDigestHardness;
    public float PhotosynthesisEfficiency;

    #endregion

    #region Reproduction_Related-REF_GC_3

    public float BirthCycles;
    public float ReproductionEfficiency;
    public float GeneticSex;
    public float SexualMaturity;

    #endregion

    public IDictionary<string, float> Genome = new Dictionary<string, float>();

    void awake()
    {
        Genome.Add("Aging", 5f);
        Genome.Add("MaximumBulk", 1f);
        Genome.Add("EnergyCostByBulk", 0.01f);
        Genome.Add("PhysicalHardness", 1f);
        Genome.Add("AbilityToEatHardness", 0f);
        Genome.Add("EfficiencyToDigestHardness", 0f);
        Genome.Add("PhotosynthesisEfficiency", 1f);
        Genome.Add("BirthCycles", 1);
        Genome.Add("ReproductionEfficiency", 1);
        Genome.Add("Aging", 5);
        Genome.Add("GeneticSex",1);
        Genome.Add("SexualMaturity",4);

        Aging = Genome["Aging"];
        MaximumBulk = Genome["MaximumBulk"];
        EnergyCostByBulk = Genome["EnergyCostByBulk"];
        PhysicalHardness = Genome["PhysicalHardness"];
        AbilityToEatHardness = Genome["AbilityToEatHardness"];
        EfficiencyToDigestHardness = Genome["EfficiencyToDigestHardness"];
        PhotosynthesisEfficiency = Genome["PhotosynthesisEfficiency"];
        BirthCycles = Genome["BirthCycles"];
        ReproductionEfficiency = Genome["ReproductionEfficiency"];
        GeneticSex = Genome["GeneticSex"];
        SexualMaturity = Genome["SexualMaturity"];
    }
    
    public IDictionary<string, float> GenerateNewGenome(IDictionary<string, float> Progenitor1,
        IDictionary<string, float> Progenitor2)
    {
        IDictionary<string, float> NewGenome = new Dictionary<string, float>();
        int Operation;
        float Value;
        foreach (var Chr in Progenitor1.Keys)
        {   
            Operation = UnityEngine.Random.Range(0,5);
            Value = GenerateNewChromosome(Progenitor1[Chr], Progenitor2[Chr], Operation);
            NewGenome.Add(Chr,Value);
        }


        return NewGenome;
    }

    private float GenerateNewChromosome(float Chr1,float Chr2,int Operation)
    {
        float ToReturn,ChangeIndex=0.5f;
        switch (Operation)
        {
            case 0://new Chr is Chr1 without modification
                ToReturn = Chr1;
                break;
            case 1://new Chr is Chr2 without modification
                ToReturn = Chr2;
                break;
            case 2://new Chr is Chr1 with modification
                ToReturn = Chr1 + UnityEngine.Random.Range(-ChangeIndex,ChangeIndex);
                break;
            case 3://new Chr is Chr2 with modification
                ToReturn = Chr2 + UnityEngine.Random.Range(-ChangeIndex,ChangeIndex);
                break;
            case 4://new Chr is (Chr1 + Chr2)/2 without modification
                ToReturn = (Chr1 + Chr2)/2;
                break;
            case 5://new Chr is (Chr1 + Chr2)/2 with modification
                ToReturn = (Chr1 + Chr2)/2 + UnityEngine.Random.Range(-ChangeIndex,ChangeIndex);
                break;
            default:
                ToReturn = 0f;
                break;
        }

        if (ToReturn < 0f)
            ToReturn = 0f; 
        
        return ToReturn;
    }

}
