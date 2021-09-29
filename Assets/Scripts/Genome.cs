using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Genome
{
    #region General-REF_GC_1

    public float Aging = 0.05f;
    public float Maximum_Bulk = 1f;
    public float Energy_cost_by_Bulk = 0.01f;
    public float Physical_Hardness = 1f;
    
    #endregion
    
    #region Nutrition_Related-REF_GC_2
    
    public float Ability_to_Eat_Hardness = 0;
    public float Efficiency_to_Digest_Hardness = 0;
    public float Photosynthesis_Efficiency = 1;
    
    #endregion
    
    #region Reproduction_Related-REF_GC_3

    public int Birth_Cycles = 1;
    public float Reproduction_Efficiency = 1;
    public int Genetic_Sex = 1;
    public int Sexual_Maturity = 4;
        
    #endregion
    
}
