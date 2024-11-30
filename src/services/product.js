import axios from "axios"


export const FetchProductsFiltered = async (search, filter) => {
    try{
        var response = await axios.get("http://localhost:5050/products/ReadFiltered", {
            params: {
                search: search?.search,
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

// export const FetchProducts = async () => {
//     try{
//         var response = await axios.get("http://localhost:5050/products/readAll");
//         console.log(response.data);
//         return response.data;
//     }
//     catch(ex){
//         console.error(ex);
//         return [];
//     }
// };

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