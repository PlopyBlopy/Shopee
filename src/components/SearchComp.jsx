import { Input } from "@chakra-ui/react";
import { useContext } from "react";

import { FilterContext } from "./Contexts/FilterContext.js";

import styles from "../styles/stylesheets/search.module.css";

export default function SearchProduct() {
  const { setFilter } = useContext(FilterContext);

  const handleInputChange = (e) =>
    setFilter((prev) => ({ ...prev, search: e.target.value }));

  return (
    <div className={styles.container}>
      <Input
        className={styles.input}
        placeholder="Поиск"
        onChange={handleInputChange}
      />
    </div>
  );
}
