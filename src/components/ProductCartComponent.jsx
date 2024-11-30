import {
  Button,
  Card,
  CardBody,
  CardFooter,
  CardHeader,
  Heading,
  Image,
  Stack,
  Text,
} from "@chakra-ui/react";
// import moment from "moment";

export default function ProductCartComponent({
  title,
  // description,
  price,
  rating,
  // createAt,
}) {
  return (
    <Card variant="elevated" borderRadius="3xl" maxW="300">
      <CardHeader
        padding={0}
        minW={300}
        minH={300}
        background="gray.50"
        borderRadius="3xl"
        className="flex items-center">
        <Image
          src="https://images.unsplash.com/photo-1555041469-a586c61ea9bc?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1770&q=80"
          alt={title}
        />
      </CardHeader>
      <CardBody>
        <Stack>
          <Heading size="md">{title}</Heading>
          {/* <Heading size="sm">{description}</Heading> */}
          <Text color="blue.600" fontSize="2xl">
            {price}
          </Text>
          <Text color="orange.300" fontWeight="medium" fontSize="2xl">
            {rating}
          </Text>
          <Text color="orange.300" fontWeight="medium" fontSize="2xl">
            {rating}
          </Text>
          {/* <Text color="orange.300" fontWeight="medium" fontSize="2xl">
            {moment(createAt).format("DD.MM.YYYY HH:MM")}
          </Text> */}
        </Stack>
      </CardBody>
      <CardFooter className="flex justify-center">
        <Button colorScheme="blue">Купить</Button>
      </CardFooter>
    </Card>
  );
}
