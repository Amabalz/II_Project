import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import LoginPage from "./components/LoginPage";
import RegisterPage from "./components/RegisterPage";
import Home from "./components/Home";
import Navbar from "./components/Navbar"; // Make sure to import Navbar if needed

function App() {
  return (
    <Router>
      <Navbar /> {/* Include your Navbar component */}
      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/register" element={<RegisterPage />} />{" "}
        {/* Add Register route */}
        <Route path="/login" element={<LoginPage />} /> {/* Add Login route */}
        {/* Add other routes here */}
      </Routes>
    </Router>
  );
}

export default App;
