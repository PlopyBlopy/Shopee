import {
  Button,
  FormControl,
  FormErrorMessage,
  Heading,
  Input,
  useToast,
} from "@chakra-ui/react";
import { useState } from "react";

export default function CreateProductForm({ onCreate }) {
  const [product, setProduct] = useState({
    title: "",
    description: "",
    price: "",
    rating: "",
    sellerId: "",
    categoryId: "",
  });

  const [errors, setErrors] = useState({
    itle: false,
    description: false,
    price: false,
    rating: false,
    sellerId: false,
    categoryId: false,
  });

  const productInputs = {
    title: "productTitle",
    description: "productDescription",
    price: "productPrice",
    rating: "productRating",
  };

  const map = new Map([
    [productInputs.title, "Название должно содержать не менее 5 символов."],
    [productInputs.description, "Описание должно быть не более 700 символов."],
    [productInputs.price, "Цена должна быть больше 50."],
    [productInputs.rating, "Пожалуйста, введите корректный рейтинг."],
  ]);

  const errorMessages = [
    "Название должно содержать не менее 5 символов.",
    "",
    "",
    "",
    "",
  ];

  //всплывающнн уведомление
  const toast = useToast(); // Инициализация Toast

  //Обработка ошибок
  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setProduct({ ...product, [name]: value });
    setErrors({ ...errors, [name]: false });
  };

  //обработкак события отправки при нажатии на кнопку
  const onSubmit = (e) => {
    //вернуть в default значение
    e.preventDefault();
    //локальная переменная ошибки
    let hasError = false;

    //условия для ошибки
    const newErrors = {
      title: !product.title || product.title.length < 5, // Проверка длины title
      description: product.description.length > 700, // Проверка длины description
      price:
        !product.price ||
        isNaN(product.price) ||
        Number(product.price) < 50 ||
        Number(product.price) > 1000000, // Проверка цены
      rating:
        !product.rating ||
        isNaN(product.rating) ||
        Number(product.rating) < 0 ||
        Number(product.rating) > 5, // Проверка рейтинга
    };

    //проверка всех свойств на ошибки
    for (const key in newErrors) {
      if (newErrors[key]) {
        hasError = true;
      }
    }
    //установка значения ошибки
    setErrors(newErrors);

    //ошибка
    if (hasError) {
      // Вызов Toast при наличии ошибок
      toast({
        title: "Ошибка при добавлении товара",
        description: "Пожалуйста, исправьте ошибки в форме.",
        status: "error",
        duration: 3000, // Время отображения в миллисекундах
        isClosable: true,
        position: "top", // Позиция Toast
      });
      setProduct((prevProduct) => ({
        title: prevProduct.title,
        description: prevProduct.description,
        price: newErrors.price ? "" : prevProduct.price,
        rating: newErrors.rating ? "" : prevProduct.rating,
      }));
      return;
    }
    //вызов функции в App
    onCreate(product);
    //установка значения по умолчанию
    setProduct({ title: "", description: "", price: "", rating: "" });

    // Вызов Toast при успешном добавлении товара
    toast({
      title: "Товар успешно добавлен!",
      description:
        "Спасибо за добавление товара. Наша команда свяжется с вами в ближайшее время.",
      status: "success",
      duration: 3000, // Время отображения в миллисекундах
      isClosable: true,
      position: "top", // Позиция Toast
    });
  };

  return (
    <>
      <form className="w-full flex flex-col gap-3" onSubmit={onSubmit}>
        <Heading as="h3" size="lg">
          Создать новый товар
        </Heading>

        <FormControl isInvalid={errors.title}>
          <Input
            name="title"
            placeholder="название (меньше 5 символов)"
            value={product.title}
            onChange={handleInputChange}
            autoComplete="off"
          />
          <FormErrorMessage>{map.get(productInputs.title)}</FormErrorMessage>
        </FormControl>

        <FormControl isInvalid={errors.description}>
          <Input
            name="description"
            placeholder="описание (необязательно)"
            value={product.description}
            onChange={handleInputChange}
            autoComplete="off"
          />
          <FormErrorMessage>
            {map.get(productInputs.description)}
          </FormErrorMessage>
        </FormControl>

        <FormControl isInvalid={errors.price}>
          <Input
            name="price"
            placeholder="цена (больше 50)"
            value={product.price}
            onChange={handleInputChange}
            autoComplete="off"
          />
          <FormErrorMessage>{map.get(productInputs.price)}</FormErrorMessage>
        </FormControl>

        <FormControl isInvalid={errors.rating}>
          <Input
            name="rating"
            placeholder="рейтинг"
            value={product.rating}
            onChange={handleInputChange}
            autoComplete="off"
          />
          <FormErrorMessage>{map.get(productInputs.rating)}</FormErrorMessage>
        </FormControl>

        <Input
          name="sellerId"
          placeholder="sellerId"
          value={product.sellerId}
          onChange={handleInputChange}
          autoComplete="off"
        />

        <Input
          name="categoryId"
          placeholder="categoryId"
          value={product.categoryId}
          onChange={handleInputChange}
          autoComplete="off"
        />

        <Button type="submit" colorScheme="blue">
          Создать
        </Button>
      </form>
    </>
  );
}
