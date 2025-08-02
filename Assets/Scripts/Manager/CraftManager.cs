using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CraftManager : Singleton<CraftManager>
{
    [Header("Config")]
    [SerializeField] private RecipeCard recipeCardPrefab;
    [SerializeField] private Transform recipeContainer;
    [SerializeField] private GameObject craftMaterialsPanel;

    [Header("Recipe Info")]
    [SerializeField] private TextMeshProUGUI recipeName;
    [SerializeField] private Image iconItem1;
    [SerializeField] private TextMeshProUGUI nameItem1;
    [SerializeField] private TextMeshProUGUI amountItem1;
    [SerializeField] private Image iconItem2;
    [SerializeField] private TextMeshProUGUI nameItem2;
    [SerializeField] private TextMeshProUGUI amountItem2;
    [SerializeField] private Button craftButton;

    [Header("Final Item")]
    [SerializeField] private Image iconFinalItem;
    [SerializeField] private TextMeshProUGUI nameFinalItem;
    [SerializeField] private TextMeshProUGUI descriptionFinalItem;

    [Header("Recipes")]
    [SerializeField] private RecipeList recipes;
    private Recipe recipeSelected;
    public Recipe RecipeSelected => recipeSelected;
    public void SetRecipeSelected(Recipe recipe)
    {
        this.recipeSelected = recipe;
    }

    private void Start()
    {
        LoadRecipes();
        craftMaterialsPanel.gameObject.SetActive(false);
    }

    public void LoadRecipes()
    {
        for(int i = 0; i < recipes.recipes.Length; i++)
        {
            RecipeCard recipeCard = Instantiate(recipeCardPrefab, recipeContainer);
            recipeCard.InitRecipeCard(recipes.recipes[i]);
        }
    }
    public void ShowRecipe(Recipe recipe)
    {
        if(craftMaterialsPanel.activeSelf == false)
        {
            craftMaterialsPanel.SetActive(true);
        }
        this.recipeSelected = recipe;

        recipeName.text = recipe.recipeName;
        //item 1
        iconItem1.sprite = recipe.item1.icon;
        nameItem1.text = recipe.item1.nameItem;
        amountItem1.text = $"{recipe.item1Amount}/{Inventory.Instance.GetItemCurrentStock(recipe.item1.id)}";
        //item 2
        iconItem2.sprite = recipe.item2.icon;
        nameItem2.text = recipe.item2.nameItem;
        amountItem2.text = $"{recipe.item2Amount}/{Inventory.Instance.GetItemCurrentStock(recipe.item2.id)}";
        //Final Item
        iconFinalItem.sprite = recipe.finalItem.icon;
        nameFinalItem.text = recipe.finalItem.nameItem;
        descriptionFinalItem.text = recipe.finalItem.description;

        craftButton.interactable = CanCraftItem(recipe);
    }
    private bool CanCraftItem(Recipe recipe)
    {
        int item1Stock = Inventory.Instance.GetItemCurrentStock(recipe.item1.id);
        int item2Stock = Inventory.Instance.GetItemCurrentStock(recipe.item2.id);
        if(item1Stock >= recipe.item1Amount && item2Stock >= recipe.item2Amount)
        {
            return true;
        }
        return false;
    }

    //Craft item
    public void CraftItem()
    {
        for (int i = 0; i < recipeSelected.item1Amount; i++)
        {
            Inventory.Instance.ConsumeItem(recipeSelected.item1.id);
        }
        for (int i = 0; i < recipeSelected.item2Amount; i++)
        {
            Inventory.Instance.ConsumeItem(recipeSelected.item2.id);
        }
        Inventory.Instance.AddItem(recipeSelected.finalItem, recipeSelected.finalItemAmount);
        ShowRecipe(recipeSelected);
    }

}
