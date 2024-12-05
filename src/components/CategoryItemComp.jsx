import { Button } from "@chakra-ui/react";

export default function Category({ id, title, depth, onCategoryChange }) {
  return (
    <>
      {depth < 1 ? (
        <Button size="lg" variant="ghost" onClick={onCategoryChange} value={id}>
          {title}
        </Button>
      ) : (
        <Button size="md" variant="ghost" onClick={onCategoryChange} value={id}>
          {title}
        </Button>
      )}
    </>
  );
}
