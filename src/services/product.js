import axios from "axios"


export const FetchProductsFiltered = async (filter) => {
    try{
        var response = await axios.get("http://localhost:5050/Products/GetFiltered", {
            params: {
                search: filter?.search,
                categoryId: filter?.categoryId,
                sortItem: filter?.sortItem,
                sortOrder: filter?.sortOrder
            }
        });
        return response.data;
    }
    catch(ex){
        console.error(ex);
        return [];
    }
}



export const CreateProduct = async (product) => {
    try{
        var response = await axios.post("http://localhost:5050/products/add", product);
        return response.status;
    }
    catch(ex){
        console.error(ex);
        return [];
    }
};

// export const FetchProductsCategory = async (categoryId) => {
//     console.log("response ID: "+ "http://localhost:5050/Products/GetCategoryAll", categoryId);
//     var response = await axios.get("http://localhost:5050/Products/GetCategoryAll" + categoryId);
//     console.log(response.data);
//     return response.data;
// }