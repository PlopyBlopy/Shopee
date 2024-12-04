import { useContext } from "react";
import styles from "../styles/other/category.module.css";
import Category from "./CategoryComp";
import { FilterContext } from "./FilterContext.js";

const CategoryList = ({ categories, onClose, depth = 0 }) => {
  const { setCategory } = useContext(FilterContext);

  if (!categories || categories.length === 0) {
    return null;
  }

  const HandleChange = (e) => {
    // onClose();
    setCategory(e.target.value);
  };
  return (
    <ul className={styles.container}>
      {categories.map((category) => (
        <li className={styles[`depth-${depth}`]} key={category.id}>
          <Category
            id={category.id}
            title={category.title}
            depth={depth}
            HandleChange={HandleChange}
          />
          {category.subcategories && category.subcategories.length > 0 && (
            <CategoryList
              categories={category.subcategories}
              depth={depth + 1}
            />
          )}
        </li>
      ))}
    </ul>
  );
};

export default CategoryList;
