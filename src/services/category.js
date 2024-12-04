import axios from "axios"

export const FetchCategoryTree = async () => {
    try{
        var response = await axios.get("http://localhost:5050/Category/GetCategoryTree", {
        });
        console.log(response.data.subcategories);
        return response.data.subcategories;
    }
    catch(ex){
        console.error(ex);
        return [];
    }
}















