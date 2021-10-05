using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Genome
{
    //https://github.com/shack3/EvoSimulator/tree/Documentation#52-genetic-characteristics
    
    #region General-REF_GC_1

    public bool photosynthetic;
    
    public int Aging;
    public float Maximum_Bulk;
    public float Energy_cost_by_Bulk;
    public float Physical_Hardness;
    
    #endregion
    
    #region Nutrition_Related-REF_GC_2

    public bool hardEater;
    
    public float Ability_to_Eat_Hardness;
    public float Efficiency_to_Digest_Hardness;
    public float Photosynthesis_Efficiency;
    
    #endregion
    
    #region Reproduction_Related-REF_GC_3

    public int Birth_Cycles;
    public float Reproduction_Efficiency;
    public int Genetic_Sex;
    public int Sexual_Maturity;
        
    #endregion

    void Awake()
    {
        Aging = 5;
        Maximum_Bulk = 1f;
        Energy_cost_by_Bulk = 0.01f;
        Physical_Hardness = 1f;
        
        Ability_to_Eat_Hardness = 0;
        Efficiency_to_Digest_Hardness = 0;
        Photosynthesis_Efficiency = 1;

        Birth_Cycles = 1;
        Reproduction_Efficiency = 1;
        Genetic_Sex = 1;
        Sexual_Maturity = 4;
    }
    
}
