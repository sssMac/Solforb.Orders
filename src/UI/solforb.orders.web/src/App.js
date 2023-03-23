import {Routes, Route, Link, useLocation} from "react-router-dom";
import './App.css';
import Main from "./pages/main/main";
import CreateOrder from "./pages/order/createOrder";
import ViewOrder from "./pages/order/viewOrder";
import EditOrder from "./pages/order/editOrder";

function App() {
  return (
    <div className="App">
      <Routes>
          <Route path="/" element={<Main/>} />
          <Route path="/orders" element={<Main/>} />
          <Route path="/order/:id" element={<ViewOrder/>} />
          <Route path="/order/create" element={<CreateOrder/>} />
          <Route path="/order/edit/:id" element={<EditOrder/>} />
      </Routes>
    </div>
  );
}

export default App;
