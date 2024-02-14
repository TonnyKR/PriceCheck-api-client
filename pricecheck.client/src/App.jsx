import { useEffect, useState } from 'react';

function App() {

    useEffect(() => {
        populateWeatherData();
        getATBproducts();
    }, []);

    
    return (
        <>
        <div className="container-fluid" style={{backgroundColor: "#FFA235"}}>
            <div className="row text-center">
                <img className="mx-auto d-block" src="logo.jfif" style={{maxWidth: "20%", maxHeight: "20%"}} alt="PriceCheck"></img> 
            </div>
        </div>

            <nav className="navbar navbar-expand-sm bg-light navbar-light">
                <div className="container-fluid">
                    <ul className="navbar-nav">
                        <li className="nav-item">
                            <a className="nav-link active" href="#">Active</a>
                        </li>
                        <li className="nav-item">
                            <a className="nav-link" href="#">Link</a>
                        </li>
                        <li className="nav-item">
                            <a className="nav-link" href="#">Link</a>
                        </li>
                        <li className="nav-item">
                            <a className="nav-link disabled" href="#">Disabled</a>
                        </li>
                    </ul>
                </div>
            </nav>           


        <div>
            <h1 id="tabelLabel">Weather forecast</h1>
            <p>This component demonstrates fetching data from the server.</p>
            {contents}
            {prod}
        </div>
        </>
    );
    
    async function populateWeatherData() {
        const response = await fetch('api/weatherforecast');
        const data = await response.json();
        setForecasts(data);
    }
    async function getATBproducts(){
        const response = await fetch('api/atb');
        const data = await response.json();
        setProducts(data);
    }

}

export default App;