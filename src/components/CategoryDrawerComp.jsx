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
import { useRef } from "react";

import CategoryList from "./CategoryListComp.jsx";

export default function CategoryDrawer({ categories }) {
  const { isOpen, onOpen, onClose } = useDisclosure();
  const btnRef = useRef();

  const handlerCloseDrawer = () => {
    console.log("handlerCloseDrawer:");
  };

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
            <CategoryList
              categories={categories}
              onCloseDrawer={handlerCloseDrawer}
            />
          </DrawerBody>

          <DrawerFooter></DrawerFooter>
        </DrawerContent>
      </Drawer>
    </>
  );
}
