import axios from "axios"

export const FetchCategoryTree = async () => {
    try{
        var response = await axios.get("http://localhost:5050/Category/GetCategoryTree", {
        });
        return response.data.subcategories;
    }
    catch(ex){
        console.error(ex);
        return [];
    }
}















