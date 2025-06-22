import { BrowserRouter, Routes, Route } from 'react-router-dom'
import { DrinksCatalogPage } from '../../pages/DrinksCatalogPage/DrinksCatalogPage'
import { CartPage } from '../../pages/CartPage/CartPage'
import { PaymentPage } from '../../pages/PaymentPage/PaymentPage'
import { SuccessPage } from '../../pages/SuccessPage/SuccessPage'

export const AppRouter = () => (
    <BrowserRouter>
        <Routes>
            <Route path="/" element={<DrinksCatalogPage />} />
            <Route path="/cart" element={<CartPage />} />
            <Route path="/payment" element={<PaymentPage />} />
            <Route path="/success" element={<SuccessPage />} />
        </Routes>
    </BrowserRouter>
)
