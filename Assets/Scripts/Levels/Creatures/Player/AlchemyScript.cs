using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlchemyScript : MonoBehaviour
{
    //Polish
    [SerializeField] GameObject ingredientParticlesParent;
    ParticleSystem[] ingredientParticles;
    SFXManager soundManager;
    [SerializeField] AudioClip[] ingredientSFX;
    [SerializeField] float volume;

    //The input to clear all ingredients
    [SerializeField] string clearInput;
    //The input to create a potion
    [SerializeField] string enterInput;
    //The place where thrown potions spawn
    [SerializeField] Transform throwSpawn;
    //Repository objects
    PotionRepository potionRepository;
    IngredientRepository ingredientRepository;

    //misc

    int buffDamage;
    int[] activeIngredients = new int[6];
    int intVersionOfIngredients = 0;
    int currentSlot = 0;
    ActiveIngredientUI activeIngredientUI;
    BuffDisplay buffDisplay;
    void Start()
    {
        activeIngredientUI = FindObjectOfType<ActiveIngredientUI>();
        buffDisplay = FindObjectOfType<BuffDisplay>();
        potionRepository = FindObjectOfType<PotionRepository>();
        ingredientRepository = FindObjectOfType<IngredientRepository>();
        ingredientParticles = ingredientParticlesParent.GetComponentsInChildren<ParticleSystem>();
        soundManager = FindObjectOfType<SFXManager>();
    }
    void Update()
    {
        if(Time.deltaTime > 0)
        {
            CreateOrCancel();
            DetermineIngredient();
        }
    }

    private void DetermineIngredient()
    {
        if (ingredientRepository.ReturnIngredient(1).unlocked && Input.GetKeyDown(ingredientRepository.ReturnIngredient(0).input))
        {
            AddIngredient(0);
            ingredientParticles[0].Play();
        }
        if (ingredientRepository.ReturnIngredient(1).unlocked && Input.GetKeyDown(ingredientRepository.ReturnIngredient(1).input))
        {
            AddIngredient(1);
            ingredientParticles[1].Play();
        }
        if (ingredientRepository.ReturnIngredient(2).unlocked && Input.GetKeyDown(ingredientRepository.ReturnIngredient(2).input))
        {
            AddIngredient(2);
            ingredientParticles[2].Play();
        }
        if (ingredientRepository.ReturnIngredient(3).unlocked && Input.GetKeyDown(ingredientRepository.ReturnIngredient(3).input))
        {
            AddIngredient(3);
            ingredientParticles[3].Play();
        }
        if (ingredientRepository.ReturnIngredient(4).unlocked && Input.GetKeyDown(ingredientRepository.ReturnIngredient(4).input))
        {
            AddIngredient(4);
            ingredientParticles[4].Play();
        }
        if (ingredientRepository.ReturnIngredient(5).unlocked && Input.GetKeyDown(ingredientRepository.ReturnIngredient(5).input))
        {
            AddIngredient(5);
            ingredientParticles[5].Play();
        }
    }

    private void CreateOrCancel()
    {
        if (Input.GetKeyDown(enterInput))
        {
            MakePotion();
        }
        if (Input.GetKeyDown(clearInput))
        {
            ClearIngredients();
        }
    }

    private void AddIngredient(int index)
    {
        if (currentSlot != activeIngredients.Length)
        {
            intVersionOfIngredients = intVersionOfIngredients * 10 + index + 1;
        }
        if (currentSlot == activeIngredients.Length)
        {
            MakePotion();
            return;
        }
        activeIngredients[currentSlot] = ingredientRepository.ReturnIngredient(index).ID;
        currentSlot++;
        soundManager.PlayAudioClip(ingredientSFX[index], transform, volume);
        activeIngredientUI.UpdateIngredientUI();
    }
    private void MakePotion()
    {
        int potionIndex = 0;
        foreach (Potion potion in potionRepository.ReturnPotions())
        {
            if(potion.ingredientCombo == intVersionOfIngredients)
            {
                if (potion.discoverOnCreate)
                {
                    potion.discovered = true;
                }
                else if(!potion.discovered)
                {
                    break;
                }
                PotionTypeHandler(potionIndex);
                break;
            }
            potionIndex++;
        }
        ClearIngredients();
    }
    private void PotionTypeHandler(int potionIndex)
    {
        if(potionRepository.ReturnPotion(potionIndex).spawnType == Potion.potionTypeEnum.spawn_on_player)
        {
            Instantiate(potionRepository.ReturnPotion(potionIndex).prefab, gameObject.transform.position - new Vector3(0, 0, transform.position.z), gameObject.transform.rotation);
        }
        if(potionRepository.ReturnPotion(potionIndex).spawnType == Potion.potionTypeEnum.spawn_on_aim)
        {
            if(FindObjectOfType<PlayerAutoAim>().ReturnCanTarget() == true || !FindObjectOfType<PlayerAutoAim>().enabled)
            {
                SpawnAttackPotion(potionIndex);
            }
            else
            {
                Debug.Log("No Target");
            }
        }
        if(potionRepository.ReturnPotion(potionIndex).spawnType == Potion.potionTypeEnum.spawn_as_child)
        {
            Instantiate(potionRepository.ReturnPotion(potionIndex).prefab, transform);
        }
    }

    private void SpawnAttackPotion(int potionIndex)
    {
        var thrownPotion = Instantiate(potionRepository.ReturnPotion(potionIndex).prefab, throwSpawn.position, throwSpawn.rotation * Quaternion.Euler(0, 0, -90));
        thrownPotion.GetComponent<ProjectileBehavior>().AddExtraDamage(buffDamage);
    }

    private void ClearIngredients()
    {
        for (int i = 0; i < activeIngredients.Length; i++)
        {
            activeIngredients[i] = 0;
        }
        intVersionOfIngredients = 0;
        currentSlot = 0;
        activeIngredientUI.UpdateIngredientUI();
    }
    public int ReturnActiveIngredients(int ingredientIndex)
    {
        return activeIngredients[ingredientIndex];
    }
    public void AddBuffDamage(int additionalDamage)
    {
        buffDamage += additionalDamage;
        buffDisplay.UpdateDamageUI(buffDamage);
    }
}