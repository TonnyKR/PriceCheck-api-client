import 'bootstrap/dist/css/bootstrap.css';
import logo from './logo.jfif'

function Logo()
{
    
    return(
        <>
        <div className="container-fluid" style={{backgroundColor: "#FFA235"}}>
            <div className="row text-center">
                <img className="mx-auto d-block" src={logo} style={{maxWidth: "20%", maxHeight: "20%"}} alt="PriceCheck"></img> 
            </div>
        </div>  
        </>
    )
}
export default Logo