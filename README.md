# Index

# 1 Objective
Evolutive simulator  
RO_1 - The entities are defined by a genetic code           
RO_2 - The genetic code define the attributes at the born of the entity and its not modifiable a posteriori      
RO_3 - Each entity has its own AI ( pending to define)  
RO_4 - Obtain statistical information about the lifetime of the entities  
RO_5 - The modifications on the genetic code happens between generations base on a pseudo genetic algorithm

# 2 Development Phases
# 2.1 Into V 1.x
* [x] Simulated environment : One smooth sphere
* [x] The entities change the color with selection
* [x] The entities can move across the surface of the sphere
* [x] Terminal for control command input
* [x] Only Photosynthesis(*REF_NK_1*) allowed for entities
    * [x] Only entities in the collider of the light source will get energy
* [x] Only Mitosis / Bipartition (*REF_RK_4*) allowed for entities
* [x] Entities Maximum Bulk = 1 unit
* [x] The energy expenditure in a lifecycle of the entities is defined by:
    * [x] Energy cost by Bulk
* [x] The light origin rotates slowly around the map
    * [x] The rotation exceeds the maximum time that the entity can survive without gaining energy
* [ ] AI neural network based for each entity
    * [ ] AI is part of the genome
    * [ ] The data created by the network would be storage for future use
* [x] All the entities have the same species
* [x] The initial population of entities would be generated by a random distribution upon the surface of the sphere

# 3 Development Environment
RE_1 - Using unity 3d as graphical platform  
RE_1.1 - Using Unity 2021.1.22f1  
RE_2 - Using C#

# 4 Simulated Environment
RSE_1 - Spherical map (SM) where the entities will develop their vital process  
RSE_2 - The SM is orbited by a Sun

# 5 Genetics
All the information related with the genetics and the connected processes

## 5.1 Genetic code
RGC_1 - The genome of each entity is individual  
RGC_2 - The genome defines the specie of the entity   
RGC_3 - The genome is represented by a RGB color in the map

## 5.2 Genetic Characteristics

* -General-(*REF_GC_1*)
    * Aging : Probability of death by natural causes in each cycle
    * Maximum Bulk : Defines the maximum bulk that a entity can reach
    * Energy cost by Bulk : Defines the energy cost each life cycle for unity of bulk
    * Physical Hardness : Defines the hardness of the Bulk of the entities

* -Nutrition Related-(*REF_GC_2*)
    * Ability to Eat Hardness : Defines which is the maximum level of hardness that the entity can try to eat
    * Efficiency to Digest Hardness : Defines the efficiency obtaining energy from the hardness nutrition activity
    * Photosynthesis Efficiency : Defines the efficiency obtaining energy from the nutrition photosynthetic activity

* -Reproduction Related-(*REF_GC_3*)
    * Birth Cycles : Defines the amount of cycles between the Reproduction Activity and the birth of a new entity
    * Reproduction Efficiency : Defines the energy needed for making the reproductive activity
    * Genetic Sex : Genetic sex of the entity
    * Sexual Maturity : Cycles before been able to reproduce
    * Genetic Reproduction : Defines all the information related to the reproduction characteristics of the entity

## 5.3 Genetic algorithm
RGA_1 - Population filtering by acceptance function is not performed  
RGA_2 - Population generation is not forced  
RGA_3 - Population filtering will be done by the interaction between the entity and the environment or other entities  
RGA_4 - Population generation is a decision of the individual AI restricted by rules

# 6 Entities
All the information related with the characteristics and activities of the entities

## 6.1 Characteristics
REF_BC_1
* Age : Amount of lived vital cycles
* Energy : Represents the energy of the entity
    * Obtained from Nutrition
    * Needed for activities


* Bulk : Represents the mass of the entity
    * Grows proportionally each time it obtains extra energy in a life cycle
    * Decreases if the entity dont get the minimum energy to cover the energy cost
    * A larger Bulk allows to storage more energy


* Movement Cost : Defines the energy cost for each movement unity
    * Proportional to the difference between the Bulk and the Maximum Bulk
    * Equation : 1 / (genome.Maximum_Bulk - bulk)


* Movement speed : Defines the movement speed of the entity,
    * Inversely proportional to the Age
    * Proportional to the energy
    * Equation : energy / age


## 6.2 Activities
All the information related with the activities of the entities

### 6.2.1 Nutrition
entity activity that looks for gaining energy in order to survive


* Photosynthesis(*REF_NK_1*)
    * No need to interact between entities
    * Energy comes from the Sun
    * The amount of energy obtained from each life cycle is defined by:
        * entity in the trigger area of the sun receives energy.
        * The genetic Photosynthesis Efficiency
        * Proportionally to the entity Bulk

* Hardness digestion(*REF_NK_2*)
    * entity should have the Ability to Eat Hardness
    * The energy obtained is the one of the objective
        * Applies the Digestion Efficiency


### 6.2.2 Reproduction
To simplify the problem, each entity should have only one reproduction type.
It is a genetic characteristic that distinguishes species.
In each reproductive activity that implies the born of a new entity, part of the energy used in the reproduction is added to the new born entity

* Spores (*REF_RK_1*)
    * Dont need another entity
    * The progeny appears in a range from the entity
    * The genome of the progeny is based in the entity genome
    * Is triggered by the vital cycle of the entity


* Pollination (*REF_RK_2*)
    * Needs another entity of of the same kind as the entity
    * Every genetic sex is valid
    * The other entity has to be in a range
    * The progeny appears in a range after the Birth Cycles
    * The genome of the progeny is based in the two entities genome that participate in the activity
    * Is triggered by the vital cycle of the entity


* Hermaphrodite (*REF_RK_3*)
    * Needs another entity of of the same kind as the entity
    * Every genetic sex is valid
    * The other entity has to be adjacent
    * The progeny appears from an egg after the Birth Cycles
    * The egg can be eaten
    * The genome of the progeny is based in the two entities genome that participate in the activity
    * Is triggered by the vital cycle of the entity


* Mitosis / Bipartition (*REF_RK_4*)
    * Dont need another entity
    * The progeny appears immediately adjacent to the entity
    * The genome of the progeny is based in the entity genome
    * Is triggered by the vital cycle of the entity
    * The original entity dies


* Oviparous (*REF_RK_5*)
    * Needs another entity of of the same kind as the entity
    * The opposite genetic sex is needed
    * The progeny appears from an egg after the Birth Cycles
    * The egg can be eaten
    * The genome of the progeny is based in the two entities genome that participate in the activity
    * Is triggered by the vital cycle of the entity


* Mammal (*REF_RK_6*)
    * Needs another entity of of the same kind as the entity
    * The opposite genetic sex is needed
    * The progeny appears from the female after the Birth Cycles
    * The genome of the progeny is based in the two entities genome that participate in the activity


# 7 Neural Network
All the information related with the neural network for decision making of the entities
* RNN_1 - We have a neural network for each entity
* RNN_2 - The genetic part of the neural network
    * RNN_2.1 - Defines the connections between the neurons
    * RNN_2.2 - Defines the number of layers and the connection between layers
* RNN_3 - The knowledge part of the neural network
    * RNN_3.1 - Is part of the entity
    * RNN_3.2 - Defines the bias of the neurons
* RNN_4 - Calculation of results
    * RNN_4.1 - For each cycle, the neural network generates a set of results
    * RNN_4.2 - Each set of result is calculated based on the defined connections between the neurons
    * RNN_4.3 - In each set of results, the values are calculated first in base of the saved bias
* RNN_5 - Learning process
    * RNN_5.1 - Generate a new results set based on the saved bias
    * RNN_5.2 - Generate a number of result sets based in the saved bias modified by a random in a genetically determined range
    * RNN_5.2 - Evaluates by the quality function which is the best expected result and save the bias that generates that result


## 7.1 Inputs
* Energy of the entity
* Solar influence
* Bulk
* Movement specifications

## 7.2 Outputs
* Outputs of the network comes from the last layer of neurons
* We have an output neuron for each activity
* Is selected the activity whose representative neuron has the highest score
* Each neuron of the penultimate layer is connected with every neuron of the last layer

## 7.3 Evaluation function
Function that defines the operation in each neuron
With inputs as X0 to XN
With Bias as Y
With result as Z  
Z = SIN(SUM(Xn*Y)) FOR n in range 0,N


## 7.4 Quality function
The bigger the best
* Quality = Bulk + Energy * 3 + NumProle * 2

## 7.5 learning process
With Q as the result of quality function  
With A as start state  
With Bi as representation of the bias  

* The learning process is based on try and error  
* The base of the learning process is the matrix of Bias
* Before an activity the entity generates a new matrix of Bias mutating from the previous one
* After the activity the entity evaluates the quality result
  * If the quality result is equal or better than the previous the entity maintains the mutation of the Bias
  * If the quality result is worse than the previous, the entity discard the mutation of the Bias

Example explication :   
Starting from state A with a quality evaluation with value 4.  
The entity generate a Bias with Id equal to 0.  
The entity goes to state B with a quality evaluation with value 5 and an Id of Bias equal to 1 so it maintains the Bias that orders the activity.  
The entity generate a Bias with Id equal to 1.  
The entity goes to state C with a quality evaluation with value 4 and an Id of Bias equal to 2 so it maintains the Bias that orders the activity.  
The entity discard the Bias 1 because it generates a less efficient result, so the entity returns to the Bias 0.  
The entity generates a new Bias based en Bias 0.  
In case of not discarding the bias, the entity generates the Bias based on Bias 1.  

A -> Q = 4 Bi = 0  
|  
B -> Q = 5 Bi = 1   
|  
C -> Q = 4 Bi = (regression to Bi = 0)  

# Methods
* E01 - eat (#REF_GC_2):
    * Eating action, receives the objective to be eaten
    * Receives :  object.transform
    * Returns: the vital_energy obtained from the objective
* T-E01 test_eat:
    * Overwrite : E01_01 - photosynthetic
    * Overwrite : E01_02 - hardness_eater
    * Check : the method returns the sum of the two values


* E01_01 - photosynthetic :
    * Returns the vital_energy obtained based on the distance to the sun and the entity photo efficiency
    * Receives : object.transform
    * Returns : vital_energy obtained from the sun
* T-E01_01 test_photosynthetic:
    * Overwrite : E01_01_01 - distance_to_sun
    * Check : the method returns the vital_energy value based on the distance between the sun and the objective, the photosynthetic efficiency and the size of the objective


* E01_01_01 - distance_to_sun :
    * Receives : object.transform
    * Returns : distance to sun
* T-E01_01_01 - test_distance_to_sun :
    * Overwrite : object.transform
    * Check : the method returns the distance between the sun and the objective


* E01_02 - hardness_eater :
    * Returns the vital_energy obtained from eating an entity, the result is based in the hardness of the objective, the efficiency of the digestion and the energy of the objective
    * Energy_obtained = hard_obj x hard_digest x energy_obj
    * Receives : object.transform
    * Returns : the vital_energy obtained from eating other entities
* T-E01_02 - test_hardness_eater :
    * Overwrite : E01_02_01 - hardness_objective
    * Overwrite : E01_02_02 - hardness_digestion
    * Overwrite : E01_02_03 - objective_energy
    * Check : That the returning value of the vital_energy match the one obtained from the predefined equation


* E01_02_01 - hardness_objective
    * Uses the perception of the entity to calculate the objective entity hardness
    * Receives : object.transform
    * Returns : objective_hardness_value
* T-E01_02_01 - test_hardness_objective
    * Check : Returns the expected value of the object.transform hardness


* E01_02_02 - hardness_digestion
    * Returns the vital_energy modifier based in the objective hardness and the entity digestion efficiency
    * Receives : objective_hardness_value, hardness_digestion_efficiency
    * Returns : multiplier_for_the_energy_obtained
* T-E01_02_02 - test_hardness_digestion
    * Check : Returns the expected value of the object.transform hardness_digestion modifier


* E01_02_03 - objective_energy
    * Receives : object.transform
    * Returns : objective_energy
* T-E01_02_03 - test_objective_energy
    * Check : Returns the expected value o the object.transform vital_energy

