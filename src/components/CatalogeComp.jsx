import { Box, Button, useDisclosure } from "@chakra-ui/react";

import {
  Drawer,
  DrawerBody,
  DrawerCloseButton,
  DrawerContent,
  DrawerFooter,
  DrawerHeader,
  DrawerOverlay,
} from "@chakra-ui/react";
import React, { useEffect } from "react";
import CategoryList from "./CategoryList.jsx";

export default function Cataloge({ categories }) {
  const { isOpen, onOpen, onClose } = useDisclosure();
  const btnRef = React.useRef();

  useEffect(() => {
    console.log("ON CLOSE");
    onClose;
  }, [onClose]);

  return (
    <>
      <Button bg="blue.400" textColor="white" onClick={onOpen}>
        Каталог
      </Button>

      <Drawer
        isOpen={isOpen}
        placement="left"
        onClose={onClose}
        finalFocusRef={btnRef}
        size="md">
        <DrawerOverlay />
        <DrawerContent>
          <DrawerCloseButton color="white" size="lg" />
          <Box bgColor="blue.400">
            <DrawerHeader color="white">Каталог</DrawerHeader>
          </Box>

          <DrawerBody>
            <CategoryList categories={categories} onClose={onClose} />
          </DrawerBody>

          <DrawerFooter></DrawerFooter>
        </DrawerContent>
      </Drawer>
    </>
  );
}
