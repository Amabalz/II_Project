import React from "react";
import { Link, Routes, Route } from "react-router-dom"; // Import necessary components
import logo from "../assets/logoFull.svg"; // Make sure to import your logo

function Navbar() {
  return (
    <nav className="navbar navbar-expand-lg bg-light" data-bs-theme="light">
      <div className="container-fluid">
        <Link to="/" className="navbar-brand">
          <img src={logo} width="100" alt="Logo" />{" "}
          {/* Use alt attribute for accessibility */}
        </Link>
        <button
          className="navbar-toggler"
          type="button"
          data-bs-toggle="collapse"
          data-bs-target="#navbarColor03"
          aria-controls="navbarColor03"
          aria-expanded="false"
          aria-label="Toggle navigation"
        >
          <span className="navbar-toggler-icon"></span>
        </button>
        <div className="collapse navbar-collapse" id="navbarColor03">
          <ul className="navbar-nav me-auto">
            <li className="nav-item">
              <Link to="/" className="nav-link active">
                Home
              </Link>
            </li>
            <li className="nav-item">
              <Link to="/register" className="nav-link">
                Register
              </Link>{" "}
              {/* Add Register link */}
            </li>
            <li className="nav-item">
              <Link to="/login" className="nav-link">
                Login
              </Link>
            </li>
            <li className="nav-item">
              <Link to="/about" className="nav-link">
                About
              </Link>
            </li>
            {/* Other menu items */}
          </ul>
        </div>
      </div>
    </nav>
  );
}

export default Navbar;
