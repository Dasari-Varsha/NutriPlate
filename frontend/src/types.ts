export type Nutrition = { calories: number; protein: number; carbohydrates: number; fat: number };
export type Ingredient = { name: string; quantity: number; unit: string };
export type Recipe = {
  id: string; name: string; description: string; ingredients: Ingredient[];
  instructions: string[]; prepMinutes: number; servings: number; difficulty: string;
  cuisine: string; dietaryTags: string[]; nutrition: Nutrition;
};
export type MealType = 'Breakfast' | 'Lunch' | 'Dinner' | 'Snack';
export type PlannedMeal = { id: string; recipeId: string; recipeName: string; date: string; mealType: MealType; servings: number; nutrition: Nutrition };
export type MealPlan = { id: string; name: string; startDate: string; endDate: string; meals: PlannedMeal[] };
export type GroceryItem = { id: string; name: string; quantity: number; unit: string; purchased: boolean };
export type GroceryList = { id: string; name: string; items: GroceryItem[] };
export type UserProfile = { id: string; email: string; displayName: string; dietaryPreferences: string[]; foodRestrictions: string[] };
