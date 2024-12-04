import {
  CreateProduct,
  FetchProductsCategory,
  FetchProductsFiltered,
} from "./services/product";
import { FetchCategoryTree } from "./services/category.js";

// import CreateProductForm from "./components/CreateProductForm";
import ProductCartComponent from "./components/ProductCartComponent";
import FilterComponent from "./components/FilterComponent";
import SearchComponent from "./components/SearchComponent.jsx";
import Cataloge from "./components/CatalogeComp.jsx";

import mainStyle from "./styles/mainContainer.module.css";
import headerStyle from "./styles/header/header.module.css";
import middleStyle from "./styles/middle/middle.module.css";
import cardStyle from "./styles/other/product-card.module.css";

import { useEffect, useState } from "react";
import { FilterContext } from "./components/FilterContext.js";

function App() {
  const [products, setProducts] = useState([]);
  const [search, setSearch] = useState({
    search: "",
  });
  const [filter, setFilter] = useState({
    sortItem: "rating",
    sortOrder: "desc",
  });
  const [category, setCategory] = useState(
    "ffa731f7-28d7-45c4-9039-029c441c54ee"
  );

  const [categories, setCategories] = useState([]);

  useEffect(() => {
    const fetchData = async () => {
      let productsData = await FetchProductsFiltered(search, filter);
      setProducts(productsData);
    };

    fetchData();
  }, [search, filter]);

  useEffect(() => {
    const fetchData = async () => {
      let categoryData = await FetchCategoryTree();
      setCategories(categoryData);
    };

    fetchData();
  }, []);

  useEffect(() => {
    console.log(category);
    const fetchData = async () => {
      let productsData = await FetchProductsCategory(category);
      setProducts(productsData);
    };
    fetchData();
  }, [category]);

  // const onCreate = async (product) => {
  //   await CreateProduct(product);
  //   let products = await FetchProductsFiltered(search);
  //   setProducts(products);
  // };

  return (
    <div className={mainStyle.container}>
      <section className={mainStyle.content}>
        {/* <div className="grid-overlay">
          {Array.from({ length: 12 }).map((_, index) => (
            <div key={index}></div>
          ))}
        </div> */}

        <div className={headerStyle.header}>
          <div className={headerStyle.content}>
            <FilterContext.Provider value={{ category, setCategory }}>
              <Cataloge categories={categories} />
            </FilterContext.Provider>
            <SearchComponent search={search} setSearch={setSearch} />

            {/* <Header search={search} setSearch={setSearch} /> */}
          </div>
        </div>

        <div className={middleStyle.middle}>
          <div className={middleStyle.column}>
            <div className={middleStyle.column1}>
              {/* <CreateProductForm onCreate={onCreate} /> */}
              <FilterComponent filter={filter} setFilter={setFilter} />
            </div>
            <div className={middleStyle.column2}>
              <ul className={cardStyle.container}>
                {products.map((p) => {
                  return (
                    <li className={cardStyle.card} key={p.id}>
                      <ProductCartComponent
                        title={p.title}
                        price={p.price}
                        rating={p.rating}
                      />
                    </li>
                  );
                })}
              </ul>
            </div>
          </div>
        </div>
      </section>
    </div>
  );
}

export default App;
