import { Container, Divider } from '@mui/material';
import { Filters } from '../../widgets/Filters/Filters';
import { ProductList } from '../../widgets/ProductList/ProductList';

export const DrinksCatalogPage = () => {
    return (
        <Container sx={{ padding: 2 }}>
            <Filters />
            <Divider sx={{ my: 3 }} />
            <ProductList />
        </Container>
    );
};
