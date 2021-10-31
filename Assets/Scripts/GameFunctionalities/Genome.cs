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
    public float[][][] NeuralNetwork = new float[100][][];
    
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
    
    public void InitNeuralNetwork()
    {
        for (int pos = 0; pos < NeuralNetwork.Length; pos++)
        {
            NeuralNetwork[pos] = new float[100][];
            for (int pos2 = 0; pos2 < NeuralNetwork[pos].Length; pos2++)
            {
                NeuralNetwork[pos][pos2] = new float[100];
                for (int pos3 = 0; pos3 < NeuralNetwork[pos].Length; pos3++)
                {
                        NeuralNetwork[pos][pos2][pos3] = (float) UnityEngine.Random.Range(0, 1);
                }
            }
        }
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

/*All the information related with the neural network for decision making of the entities
RNN_1 - We have a neural network for each entity
RNN_2 - The genetic part of the neural network
RNN_2.1 - Defines the connections between the neurons
RNN_2.2 - Defines the number of layers and the connection between layers
RNN_3 - The knowledge part of the neural network
RNN_3.1 - Is part of the entity
RNN_3.2 - Defines the bias of the neurons
RNN_4 - Calculation of results
RNN_4.1 - For each cycle, the neural network generates a set of results
RNN_4.2 - Each set of result is calculated based on the defined connections between the neurons
RNN_4.3 - In each set of results, the values are calculated first in base of the saved bias
RNN_4.3.1 - The rest of the results of the set is based in randomization in range of the bias of the neurons
RNN_5 - Learning process
RNN_5.1 - Generate a new results set based on the saved bias
RNN_5.2 - Generate a number of result sets based in the saved bias modified by a random in a genetically determined range
RNN_5.2 - Evaluates by the quality function which is the best expected result and save the bias that generates that result
*/
