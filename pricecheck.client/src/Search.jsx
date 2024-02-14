import 'bootstrap/dist/css/bootstrap.css';
import {useForm} from 'react-hook-form'
import { useState, useEffect } from 'react';
import ProductForm from './ProducForm';

function Search()
{   

    const { register, handleSubmit } = useForm();
    const [products, setProducts] = useState();


    const onSubmit = (data) => {
        getATBproducts(data.textInput);
      };

      const prod = products === undefined 
    ? <p>L</p>
    : <div class="album py-5 bg-dark">
        <div class="container">
          <div class="row">
            {products.map((p) => ProductForm(p))}
          </div>
        </div>
      </div>
    
    return(
        <>
        <nav className="navbar navbar-expand-sm pt-0 pb-0">
        <div className="container-fluid" style={{backgroundColor: "#061e2a"}}>
          <div className= "col-3"></div>
          <div className= "col-6">
            <form onSubmit={handleSubmit(onSubmit)}>
            <div className="input-group w-100" >
              <input type="text" name='searchString' {...register('textInput', { minLength: 3 })} className="form-control border border-dark" style={{textAlign: "center"}} placeholder="Пошук"></input>
              <button className="btn btn-success border border-dark" style={{backgroundColor: "#FFA235"}} type="submit">Знайти</button>
            </div>
            </form>            
          </div>
          <div className= "col-3"></div>
        </div>
      </nav>
      <div>
        {prod}
      </div>
        </>
    )
    async function getATBproducts(name){
        const response = await fetch(`api/atb/SearchByName/${name}`);
        const data = await response.json();
        setProducts(data);
    }
}

export default Search;