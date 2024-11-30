import { CreateProduct, FetchProductsFiltered } from "./services/product";
import CreateProductForm from "./components/CreateProductForm";
import ProductCartComponent from "./components/ProductCartComponent";
import SearchComponent from "./components/SearchComponent";
import FilterComponent from "./components/FilterComponent";

import { useEffect, useState } from "react";

function App() {
  const [products, setProducts] = useState([]);
  const [search, setSearch] = useState({
    search: "",
  });
  const [filter, setFilter] = useState({
    sortItem: "rating",
    sortOrder: "desc",
  });

  //происходит сам 1 раз при загрузке страницы
  useEffect(() => {
    const fetchData = async () => {
      let productsData = await FetchProductsFiltered(search, filter); //получаю новые продукты

      setProducts(productsData); //изменяю products на новые продукты
    };

    fetchData(); //вызываю получение и установку новых продуктов
  }, [search, filter]);

  const onCreate = async (product) => {
    await CreateProduct(product);
    let products = await FetchProductsFiltered(search);
    setProducts(products);
  };

  return (
    <section className="pt-5 pb-5 pl-20 pr-20 flex justify-start items-start gap-12">
      <div className="flex flex-col gap-10">
        <CreateProductNewForm onCreate={onCreate} />

        <div className="flex flex-col gap-3">
          <SearchComponent search={search} setSearch={setSearch} />
          <FilterComponent filter={filter} setFilter={setFilter} />
        </div>
      </div>
      <div>
        <ul className="grid grid-cols-4 gap-6">
          {products.map((p) => {
            return (
              <li key={p.id}>
                <ProductCartComponent
                  title={p.title}
                  // description={p.description}
                  price={p.price}
                  rating={p.rating}
                  // createAt={p.createAt}
                  // sellerId={p.sellerId}
                  // categoryId={p.categoryId}
                />
              </li>
            );
          })}
        </ul>
      </div>
    </section>
  );
}

export default App;
