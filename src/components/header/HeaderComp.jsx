import { useEffect, useState } from "react";

import { FetchCategoryTree } from "../../services/category.js";

import headerStyles from "../../styles/header/header.module.css";
import CategoryDrawer from "../CategoryDrawerComp.jsx";
import SearchComponent from "../SearchComp.jsx";

function HeaderComp() {
  const [categories, setCategories] = useState([]);

  useEffect(() => {
    const fetchData = async () => {
      let categoryData = await FetchCategoryTree();
      setCategories(categoryData);
    };

    fetchData();
  }, []);

  return (
    <>
      <div className={headerStyles.header}>
        <div className={headerStyles.content}>
          <CategoryDrawer categories={categories} />
          <SearchComponent />
        </div>
      </div>
    </>
  );
}
export default HeaderComp;
