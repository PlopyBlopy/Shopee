import { useState } from "react";

import { FilterContext } from "./components/Contexts/FilterContext.js";
import HeaderComp from "./components/header/HeaderComp.jsx";
import MiddleComp from "./components/middle/MiddleComp.jsx";
import BottomComp from "./components/bottom/BottomComp.jsx";

import appStyles from "./styles/app.module.css";

function App() {
  const [filter, setFilter] = useState({
    search: "",
    categoryId: "ffa731f7-28d7-45c4-9039-029c441c54ee",
    sortItem: "rating",
    sortOrder: "desc",
  });

  return (
    <div className={appStyles.container}>
      <section className={appStyles.content}>
        <FilterContext.Provider value={{ filter, setFilter }}>
          <HeaderComp />
          <MiddleComp />
        </FilterContext.Provider>
        <BottomComp />
      </section>
    </div>
  );
}

export default App;

{
  /* <div>
<h2>Список продуктов:</h2>
{products.map((product) => (
  <div key={product.id}>
    <h3>{product.name}</h3>
    <p>{product.description}</p>{" "}
    <p>Цена: {product.price}</p>{" "}
  </div>
))}
</div> */
}
