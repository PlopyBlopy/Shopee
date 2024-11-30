import { Input } from "@chakra-ui/react";

export default function SearchProduct({search, setSearch}) {
        const handleInputChange = (e) => setSearch({...search, search: e.target.value});

        return (
                <Input 
                placeholder="Поиск" 
                onChange={handleInputChange}
                />
        );
}

