import ProductCartComponent from "./ProductCartComp";

import cardStyle from "../styles/stylesheets/product-card.module.css";

function ProductsComp({ products }) {
  return (
    <>
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
    </>
  );
}
export default ProductsComp;
