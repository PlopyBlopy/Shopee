import { useContext, useEffect, useState } from "react";

import { FilterContext } from "../Contexts/FilterContext.js";
import { FetchProductsFiltered } from "../../services/product.js";

import FilterComponent from "../FilterComp.jsx";
import ProductsComp from "../ProductsComp.jsx";

import middleStyles from "../../styles/middle/middle.module.css";

function MiddleComp() {
  const [products, setProducts] = useState([]);
  const { filter, setFilter } = useContext(FilterContext);
  const [oldCategoryId, setOldCategoryId] = useState("");

  useEffect(() => {
    const fetchData = async () => {
      let productsData = await FetchProductsFiltered(filter);
      setProducts(productsData);
    };
    fetchData();
    setOldCategoryId(filter.categoryId);
    if (filter.categoryId != oldCategoryId) {
      filter.sortItem = "rating";
      filter.sortOrder = "desc";
      fetchData();
    }
  }, [filter]);

  return (
    <>
      <div className={middleStyles.middle}>
        <div className={middleStyles.column}>
          <div className={middleStyles.column1}>
            <FilterComponent filter={filter} setFilter={setFilter} />
          </div>
          <div className={middleStyles.column2}>
            <ProductsComp products={products} />
          </div>
        </div>
      </div>
    </>
  );
}

export default MiddleComp;
