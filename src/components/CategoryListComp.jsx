import { useContext } from "react";

import { FilterContext } from "./Contexts/FilterContext";
import Category from "./CategoryItemComp";

import styles from "../styles/stylesheets/category.module.css";

const CategoryList = ({ categories, depth = 0 }) => {
  const { setFilter } = useContext(FilterContext);

  if (!categories || categories.length === 0) {
    return null;
  }

  const handleCategoryChange = (e) => {
    setFilter({ categoryId: e.target.value });
  };

  return (
    <ul className={styles.container}>
      {categories.map((category) => (
        <li className={styles[`depth-${depth}`]} key={category.id}>
          <Category
            id={category.id}
            title={category.title}
            depth={depth}
            onCategoryChange={handleCategoryChange}
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
