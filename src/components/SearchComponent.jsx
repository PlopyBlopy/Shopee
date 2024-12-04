import { Input } from "@chakra-ui/react";
import style from "../styles/other/Search.module.css";

export default function SearchProduct({ search, setSearch }) {
  const handleInputChange = (e) =>
    setSearch({ ...search, search: e.target.value });

  return (
    <div className={style.container}>
      <Input
        className={style.input}
        placeholder="Поиск"
        onChange={handleInputChange}
      />
    </div>
  );
}
