import { Navigate,Route,Routes } from 'react-router-dom';
import Layout from './components/Layout';
import Auth from './pages/Auth';
import { Dashboard,Recipes,RecipeDetail,Favorites,Planner,Nutrition,Groceries,Profile } from './pages/Pages';
function Guard({children}:{children:React.ReactNode}){return localStorage.getItem('nutriplate_token')?children:<Navigate to="/login" replace/>}
export default function App(){return <Routes><Route path="/login" element={<Auth/>}/><Route path="/register" element={<Auth register/>}/><Route path="/" element={<Guard><Layout/></Guard>}><Route index element={<Dashboard/>}/><Route path="recipes" element={<Recipes/>}/><Route path="recipes/:id" element={<RecipeDetail/>}/><Route path="favorites" element={<Favorites/>}/><Route path="planner" element={<Planner/>}/><Route path="nutrition" element={<Nutrition/>}/><Route path="groceries" element={<Groceries/>}/><Route path="profile" element={<Profile/>}/></Route><Route path="*" element={<div className="empty-state"><h1>Page not found</h1><a href="/">Return to dashboard</a></div>}/></Routes>}
