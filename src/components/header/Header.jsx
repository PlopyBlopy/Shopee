import { useEffect } from "react";
import SearchComponent from "./SearchComponent.jsx";

export default function Header(search, setSearch) {
  return (
    <>
      <SearchComponent search={search} setSearch={setSearch} />
    </>
  );
}
