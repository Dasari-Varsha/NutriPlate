import { render,screen } from '@testing-library/react';
import { MemoryRouter } from 'react-router-dom';
import { describe,it,expect } from 'vitest';
import RecipeCard from '../src/components/RecipeCard';
import type { Recipe } from '../src/types';
const recipe:Recipe={id:'r1',name:'Test bowl',description:'A tasty demo bowl',ingredients:[],instructions:[],prepMinutes:15,servings:2,difficulty:'Easy',cuisine:'Mediterranean',dietaryTags:['Vegetarian'],nutrition:{calories:350,protein:12,carbohydrates:40,fat:10}};
describe('RecipeCard',()=>{it('renders recipe title and estimated calories',()=>{render(<MemoryRouter><RecipeCard recipe={recipe}/></MemoryRouter>);expect(screen.getByText('Test bowl')).toBeInTheDocument();expect(screen.getByText(/350/)).toBeInTheDocument();});});
