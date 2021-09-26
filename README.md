# Index

# 1 Objective
Evolutive simulator  
RO_1 - The beings are defined by a genetic code           
RO_2 - The genetic code define the attributes at the born of the being and its not modifiable a posteriori      
RO_3 - Each being has its own AI ( pending to define)  
RO_4 - Obtain stadistical information about the lifetime of the beings  
RO_5 - The modifications on the genetic code happens between generations base on a pseudo genetic algorithm

# 2 Development Environment 
RE_1 - Using unity 3d as graphical platform  
RE_1.1 - Using Unity 2021.1.22f1  
RE_2 - Using C#

# 3 Simulated Environment 
RSE_1 - Spherical map (SM) where the beings will develop their vital process  
RSE_2 - The SM is orbited by a Sun

# 4 Genetics
All the information related with the genetics and the connected processes  

## 4.1 Genetic code
RGC_1 - The genome of each being is individual  
RGC_2 - The genome defines the specie of the being   
RGC_3 - The genome is represented by a RGB color in the map  

## 4.2 Genetic Characteristics

* -General-(*REF_GC_1*)
	* Aging : Probability of death by natural causes in each cycle
	* Maximum Bulk : Defines the maximum bulk that a being can reach
	* Energy cost by Bulk : Defines the energy cost each lice cycle for unity of bulk
	* Physical Hardness : Defines the hardness of the Bulk of the Beings
	
* -Nutrition Related-(*REF_GC_2*)
    * Ability to Eat Hardness : Defines which is the maximum level of hardnees that the being can try to eat
    * Efficiency to Digest Hardness : Defines the efficiency obtaining energy from the hardness nutrition activity
    * Photosynthesis Efficiency : Defines the efficiency obtaining energy from the nutrition photosynthetic activity
  
* -Reproduction Related-(*REF_GC_3*)
	* Birth Cycles : Defines the amount of cycles between the Reproduction Activity and the birth of a new Being
	* Reproduction Efficiency : Defines the energy needed for making the reproductive actitivy
	* Genetic Sex : Genetic sex of the being
	* Sexual Maturity : Cycles before been able to reproduce
	* Genetic Reproduction : Defines all the information related to the reproduction characteristics of the being
	
## 4.3 Genetic algorithm
RGA_1 - Population filtering by acceptance function is not performed  
RGA_2 - Population generation is not forced  
RGA_3 - Population filtering will be done by the interaction between the being and the environment or other beings  
RGA_4 - Population generation is a decision of the individual AI restricted by rules  

	
# 5 Beings 
All the information related with the characteristics and activities of the beings

## 5.1 Characteristics
REF_BC_1
* Age : Amount of lived vital cycles
* Energy : Represents the energy of the being
    * Obtained from Nutrition
    * Needed for activities
    

* Bulk : Represents the mass of the being
    * Grows proportionally each time it obtains extra energy in a life cycle
    * Decreases if the being dont get the minimum energy to cover the energy coste
    * A larger Bulk allows to storage more energy
  

* Movement Efficiency : Defines the energy cost for each movement unity
    * Inversely proportional to the Bulk
  

* Movement speed : Defines the movement speed of the being, 
	* Inversely proportional to the Age
	

## 5.2 Activities 
All the information related with the activities of the beings

### 5.2.1 Nutrition
Being activity that looks for gaining energy in order to survive


* Photosynthesis(*REF_NK_1*)
    * No need to interact between beings
    * Energy comes from the Sun
    * The amount of energy obtained from each life cycle is defined by:
        * Distance between the sun and the being
        * The genetic Photosynthesis Efficiency 
        * Proportionally to the being Bulk

* Hardness digestion(*REF_NK_2*)
    * Being should have the Ability to Eat Hardness
    * The energy obtained is the one of the objective
        * Applies the Digestion Efficiency
	

### 5.2.2 Reproduction
To simplify the problem, each Being should have only one reproduction type.
It is a genetic characteristic that distinguishes species.
In each reproductive activity that implies the born of a new being, part of the energy used in the reproduction is added to the new born being 

* Spores (*REF_RK_1*)
	* Dont need another being 
	* The progeny appears in a range from the being
	* The genome of the progeny is based in the being genome
	* Is trigered by the vital cicle of the being
	

* Pollination (*REF_RK_2*)
	* Needs another being of of the same kind as the being
	* Every genetic sex is valid
	* The other being has to be in a range
	* The progeny appears in a range after the Birth Cycles
	* The genome of the progeny is based in the two beings genome that participate in the activity
	* Is trigered by the vital cicle of the being


 * Hermaphrodite (*REF_RK_3*)  
	* Needs another being of of the same kind as the being  
	* Every genetic sex is valid  
	* The other being has to be adjacent  
	* The progeny appears from an egg after the Birth Cycles  
	* The egg can be eaten  
	* The genome of the progeny is based in the two beings genome that participate in the activity  
	* Is trigered by the vital cicle of the being  
	

* Mitosis / Bipartition (*REF_RK_4*)
	* Dont need another being 
	* The progeny appears immediately adjacent to the being
	* The genome of the progeny is based in the being genome
	* Is trigered by the vital cicle of the being or by the dead of a Being that is sexually mature
	* In case of part of the vital cicle, the original being dies


* Oviparous (*REF_RK_5*)
	* Needs another being of of the same kind as the being
	* The oposite genetic sex is needed
	* The progeny appears from an egg after the Birth Cycles
	* The egg can be eaten
	* The genome of the progeny is based in the two beings genome that participate in the activity
	* Is trigered by the vital cicle of the being


* Mamal (*REF_RK_6*)
	* Needs another being of of the same kind as the being
	* The oposite genetic sex is needed
	* The progeny appears from the female after the Birth Cycles
	* The genome of the progeny is based in the two beings genome that participate in the activity

#Methods



* E01 - eat (#REF_GC_2):  
  * Eating action, receives the ocjective to be eaten  
  * Receives :  object.transform  
  * Returns: the vital_energy obtained from the objective  
* T-E01 test_eat:  
  * Overwrite : E01_01 - photosintetic  
  * Overwrite : E01_02 - hardness_eater  
  * Check : the method returns the sum of the two values  


* E01_01 - photosintetic :  
  * Returns the vital_energy obtained based on the distance to the sun and the entity photo eficiency  
  * Receives : object.transform  
  * Returns : vital_energy obtained from the sun
* T-E01_01 test_photosintetic: 
  * Overwrite : E01_01_01 - distance_to_sun 
  * Check : the method returns the vital_energy value based on the distance between the sun and the objective, the photosintetic eficiency and the size of the objective


* E01_01_01 - distance_to_sun :
  * Receives : object.transform
  * Returns : distance to sun
* T-E01_01_01 - test_distance_to_sun : 
  * Overwrite : object.transform
  * Check : the method returns the distance between the sun and the objective


* E01_02 - hardness_eater : 
  * Returns the vital_energy obtained from eating an entity, the result is based in the hardness of the objective, the eficiency of the digestion and the energy of the objective
  * Energy_obtained = hard_obj x hard_digest x energy_obj 
  * Receives : object.transform
  * Returns : the vital_energy obtained from eating other entities
* T-E01_02 - test_hardness_eater :
    * Overwrite : E01_02_01 - hardness_objective 
    * Overwrite : E01_02_02 - hardness_digestion 
    * Overwrite : E01_02_03 - objective_energy
    * Check : That the returning value of the vital_energy match the one obtained from the predefined ecuation


* E01_02_01 - hardness_objective
  * Uses the perception of the entity to calculate the objective entity hardness
  * Receives : object.transform
  * Returns : objective_hardness_value
* T-E01_02_01 - test_hardness_objective
  * Check : Returns the expected value of the object.transform hardness


* E01_02_02 - hardness_digestion
  * Returns the vital_energy modifier based in the objective hardness and the entity digestion eficiency
  * Receives : objective_hardness_value, hardness_digestion_eficiency
  * Returns : multiplier_for_the_energy_obtained
* T-E01_02_02 - test_hardness_digestion
  * Check : Returns the expected value of the object.transform hardness_digestion modifier


* E01_02_03 - objective_energy
  * Receives : object.transform
  * Returns : objective_energy
* T-E01_02_03 - test_objective_energy
  * Check : Returns the expected value o the object.transform vital_energy
