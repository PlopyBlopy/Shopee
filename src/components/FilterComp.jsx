import { Heading, Select } from "@chakra-ui/react";

import style from "../styles/stylesheets/filter.module.css";

export default function ProductCartComponent({ filter, setFilter }) {
  const handleSortItemChange = (e) =>
    setFilter({ ...filter, sortItem: e.target.value });
  const handleSortOrderChange = (e) =>
    setFilter({ ...filter, sortOrder: e.target.value });

  return (
    <div className={style.container}>
      <Heading size="sm">Критерий сортировки</Heading>
      <Select
        borderColor="blue.400"
        value={filter.sortItem}
        onChange={handleSortItemChange}>
        <option value="rating">По рейтингу</option>
        <option value="price">По цене</option>
        <option value="title">По названию</option>
      </Select>

      <Heading size="sm">Порядок товаров</Heading>
      <Select
        borderColor="blue.400"
        value={filter.sortOrder}
        onChange={handleSortOrderChange}>
        <option value="desc">По убыванию</option>
        <option value="asc">По возрастанию</option>
      </Select>
    </div>
  );
}
