import 'bootstrap/dist/css/bootstrap.css';
import logo from './logo.jfif'

function ProductForm(product)
{
    return(
        <>
            <div className="col-sm-2">
              <div className="card mb-4 box-shadow">
                <img className="card-img-top" alt={"1"} style={{height: "175px", width: "100%", display: "block"}} src={logo} data-holder-rendered="true"></img>
                <div className="card-body">
                  <div className="d-flex justify-content-between align-items-center">
                    <div className="btn-group">
                      
                    </div>
                    <small className="text-muted">{product.productPrice} грн</small>
                  </div>
                  <p className="card-text">{product.productName}</p>                 
                </div>
              </div>
            </div>
        </>
    )
}
export default ProductForm;