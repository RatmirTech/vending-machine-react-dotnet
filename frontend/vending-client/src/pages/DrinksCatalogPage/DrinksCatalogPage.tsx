import { Container } from '@mui/material';
import { Filters } from '../../widgets/Filters/Filters';
import { ProductList } from '../../widgets/ProductList/ProductList';

export const DrinksCatalogPage = () => {
    return (
        <Container maxWidth="lg" sx={{ padding: 2 }}>
            <h1>Газированные напитки</h1>
            <Filters />
            <ProductList />
        </Container>
    );
};
