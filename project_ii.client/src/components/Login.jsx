import React, { useState } from 'react';

function Login() {
  const [formData, setFormData] = useState({
    email: '',
    password: '',
  });

  const [error, setError] = useState('');

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData({
      ...formData,
      [name]: value,
    });
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    // Retrieve user data from local storage
    const storedUser = JSON.parse(localStorage.getItem('user'));

    if (storedUser) {
      const { email, password } = storedUser;
      // Check if credentials match
      if (email === formData.email && password === formData.password) {
        console.log('Login successful');
        setError('');
        // Redirect to another page or perform other actions on successful login
      } else {
        setError('Invalid email or password');
      }
    } else {
      setError('No user found. Please register first.');
    }
  };

  return (
    <div
      style={{
        display: "flex",
        justifyContent: "center",
        alignItems: "center",
        height: "50vh",
      }}
    >
      <form onSubmit={handleSubmit}>
        <h5>Login</h5>
        {error && <div className="alert alert-danger">{error}</div>}
        <div className="mb-3">
          <input
            type="email"
            className="form-control"
            id="inputEmailLogin"
            name="email"
            placeholder="Enter email"
            value={formData.email}
            onChange={handleChange}
            required
          />
        </div>
        <div className="mb-3">
          <input
            type="password"
            className="form-control"
            id="inputPasswordLogin"
            name="password"
            placeholder="Enter password"
            value={formData.password}
            onChange={handleChange}
            required
          />
        </div>
        <button
          type="submit"
          className="btn btn-dark"
          style={{ position: "relative" }}
        >
          Login
        </button>
      </form>
    </div>
  );
}

export default Login;
