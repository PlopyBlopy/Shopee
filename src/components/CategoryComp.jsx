import { Button } from "@chakra-ui/react";
import style from "../styles/other/category.module.css";

export default function Category({ id, title, depth, HandleChange }) {
  return (
    <>
      {depth < 1 ? (
        <Button size="lg" variant="ghost" onClick={HandleChange} value={id}>
          {title}
        </Button>
      ) : (
        <Button size="md" variant="ghost" onClick={HandleChange} value={id}>
          {title}
        </Button>
      )}
    </>
  );
}
