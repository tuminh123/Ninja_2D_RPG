using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeCard : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private Image recipeIcon;

    private Recipe recipeLoaded;
    public Recipe RecipeLoaded => recipeLoaded;

    public void InitRecipeCard(Recipe recipe)
    {
        recipeLoaded = recipe;
        recipeIcon.sprite = recipe.finalItem.icon;
    }
    public void SetRecipeLoaded(Recipe recipeLoad)
    {
        this.recipeLoaded = recipeLoad;
    }

    public void ClickRecipe()
    {
        CraftManager.Instance.ShowRecipe(recipeLoaded);
    }
}
