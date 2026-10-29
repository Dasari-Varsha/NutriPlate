import type { Recipe, MealPlan, GroceryList, UserProfile } from '../types';
const base = import.meta.env.VITE_API_BASE_URL || 'http://localhost:5080/api';
let token = localStorage.getItem('nutriplate_token');
export const setToken = (value: string | null) => { token = value; value ? localStorage.setItem('nutriplate_token', value) : localStorage.removeItem('nutriplate_token'); };
async function request<T>(path: string, init: RequestInit = {}): Promise<T> {
  const response = await fetch(`${base}${path}`, { ...init, headers: { 'Content-Type':'application/json', ...(token ? {Authorization:`Bearer ${token}`} : {}), ...init.headers } });
  if (!response.ok) { const message = await response.text(); throw new Error(message || `Request failed (${response.status})`); }
  if (response.status === 204) return undefined as T;
  return response.json() as Promise<T>;
}
export const api = {
  recipes: (query = '') => request<Recipe[]>(`/recipes${query ? `?search=${encodeURIComponent(query)}` : ''}`),
  recipe: (id: string) => request<Recipe>(`/recipes/${id}`),
  register: (email: string, password: string, displayName: string) => request<{token:string}>('/auth/register',{method:'POST',body:JSON.stringify({email,password,displayName})}),
  login: (email: string, password: string) => request<{token:string}>('/auth/login',{method:'POST',body:JSON.stringify({email,password})}),
  profile: () => request<UserProfile>('/users/me'),
  updateProfile: (data: Partial<UserProfile>) => request<UserProfile>('/users/me',{method:'PUT',body:JSON.stringify(data)}),
  favorites: () => request<Recipe[]>('/favorites'),
  favorite: (id: string) => request<void>(`/favorites/${id}`,{method:'POST'}),
  unfavorite: (id: string) => request<void>(`/favorites/${id}`,{method:'DELETE'}),
  plans: () => request<MealPlan[]>('/mealplans'),
  createPlan: (name: string, startDate: string, endDate: string) => request<MealPlan>('/mealplans',{method:'POST',body:JSON.stringify({name,startDate,endDate})}),
  addMeal: (planId: string, recipeId: string, date: string, mealType: string, servings: number) => request<MealPlan>(`/mealplans/${planId}/meals`,{method:'POST',body:JSON.stringify({recipeId,date,mealType,servings})}),
  removeMeal: (planId: string, mealId: string) => request<void>(`/mealplans/${planId}/meals/${mealId}`,{method:'DELETE'}),
  groceryLists: () => request<GroceryList[]>('/grocerylists'),
  generateGrocery: (planId: string) => request<GroceryList>('/grocerylists/generate',{method:'POST',body:JSON.stringify({mealPlanId:planId})}),
  toggleGrocery: (listId: string, itemId: string, purchased: boolean) => request<GroceryList>(`/grocerylists/${listId}/items/${itemId}`,{method:'PATCH',body:JSON.stringify({purchased})}),
  addGrocery: (listId: string, name: string, quantity: number, unit: string) => request<GroceryList>(`/grocerylists/${listId}/items`,{method:'POST',body:JSON.stringify({name,quantity,unit})}),
  removeGrocery: (listId: string, itemId: string) => request<void>(`/grocerylists/${listId}/items/${itemId}`,{method:'DELETE'})
};
